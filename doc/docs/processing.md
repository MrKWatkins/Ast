# Processing

Processing runs a pipeline of stages over a tree. It is the counterpart to [listening](listeners.md): processing is best for mutating a tree — replacing nodes, validating them, annotating them — whereas listeners are better for building something new from one.

Each stage contains one or more processors and runs them either serially or in parallel. Stages decide whether the pipeline should carry on to the next one, which by default means stopping as soon as the tree has [errors](messages.md) in it.

## Processors

A [`Processor<TBaseNode>`](API/MrKWatkins.Ast.Processing/Processor-TBaseNode/index.md) has a single [`Process`](API/MrKWatkins.Ast.Processing/Processor-TBaseNode/Process.md) method that acts on **one** node. Walking the tree is the pipeline's job, not the processor's; [`Process`](API/MrKWatkins.Ast.Processing/Processor-TBaseNode/Process.md) is called once per node and should not touch descendents itself. Return the node you were given to say nothing changed; returning a different node tells the pipeline the root has been replaced, which is how a processor that swaps out the root reports the new one.

Most processors only care about one kind of node. [`NodeProcessor<TBaseNode, TNode>`](API/MrKWatkins.Ast.Processing/NodeProcessor-TBaseNode-TNode/index.md) does the type test for you and only calls your [`Process`](API/MrKWatkins.Ast.Processing/NodeProcessor-TBaseNode-TNode/Process.md) for matching nodes:

```c#
internal sealed class OperatorCounter : NodeProcessor<MathsNode, BinaryOperation>
{
    protected override MathsNode Process(BinaryOperation node)
    {
        Count++;
        return node;   // Return the node we were given; nothing was replaced.
    }

    public int Count { get; private set; }
}
```

Each family of processors also has a `TContext` variant — [`Processor<TContext, TBaseNode>`](API/MrKWatkins.Ast.Processing/Processor-TContext-TBaseNode/index.md), [`NodeProcessor<TContext, TBaseNode, TNode>`](API/MrKWatkins.Ast.Processing/NodeProcessor-TContext-TBaseNode-TNode/index.md) and so on — taking a context object supplied when the pipeline is run. Unlike [listeners](listeners.md), where the context is the point of the exercise, a processing context is usually for configuration or for caching data gathered as the tree is walked.

## Ordered Processors

A plain [`Processor<TBaseNode>`](API/MrKWatkins.Ast.Processing/Processor-TBaseNode/index.md) makes no promises about the order its nodes arrive in, which is what makes it safe to run in parallel. When order matters, inherit from [`OrderedProcessor<TBaseNode>`](API/MrKWatkins.Ast.Processing/OrderedProcessor-TBaseNode/index.md) instead. It adds two members:

- [`GetTraversal`](API/MrKWatkins.Ast.Processing/OrderedProcessor-TBaseNode/GetTraversal.md) returns the [`ITraversal<TNode>`](API/MrKWatkins.Ast.Traversal/ITraversal-TNode/index.md) the pipeline should walk the tree with for this processor, defaulting to [depth first pre-order](API/MrKWatkins.Ast.Traversal/DepthFirstPreOrderTraversal-TNode/index.md).
- [`ShouldProcessDescendents`](API/MrKWatkins.Ast.Processing/OrderedProcessor-TBaseNode/ShouldProcessDescendents.md) decides whether to walk into a node's descendents at all.

Ordered processors cannot be added to a parallel stage; the builder throws an `ArgumentException` if you try. In a parallel stage the tree is walked on one thread and nodes are handed to others for processing, so there is no order to guarantee.

[`OrderedNodeProcessor<TBaseNode, TNode>`](API/MrKWatkins.Ast.Processing/OrderedNodeProcessor-TBaseNode-TNode/index.md) combines the two, filtering by node type while still controlling the traversal.

## Replacers

Replacers are ordered processors that handle the mechanics of swapping nodes in a tree. Override [`Replace`](API/MrKWatkins.Ast.Processing/Replacer-TBaseNode/Replace.md) and return the new node; return the original node or `null` to leave it alone.

```c#
internal sealed class Reducer : NodeReplacer<MathsNode, BinaryOperation>
{
    public override ITraversal<MathsNode> GetTraversal(MathsNode root) => DepthFirstPostOrderTraversal<MathsNode>.Instance;

    protected override MathsNode? Replace(BinaryOperation node)
    {
        if (node is { Children: { First: Constant left, Last: Constant right }, Operator: '+' })
        {
            return new Constant(left.Value + right.Value);
        }

        return null;
    }
}
```

The post-order traversal above matters: reducing children before their parents means the parent sees the already reduced constants and can fold again in the same pass.

There are two base classes — [`Replacer<TBaseNode>`](API/MrKWatkins.Ast.Processing/Replacer-TBaseNode/index.md) for every node and [`NodeReplacer<TBaseNode, TNode>`](API/MrKWatkins.Ast.Processing/NodeReplacer-TBaseNode-TNode/index.md) for a specific type — plus context variants of each. Returning a node that already has a parent throws an `InvalidOperationException`.

Replacing the root node is allowed. As the root has no parent to swap it in, the new root comes back out of the pipeline; see [Pipelines](#pipelines) below.

A replacement is not itself visited by the walk that produced it, so a replacer cannot loop on its own output. The walk continues through the *replaced* node's children, which stay attached to it rather than moving to the replacement, so anything the new node should see needs a later stage.

## Validators

Validators check nodes and return [messages](messages.md) to attach to them. Override [`Validate`](API/MrKWatkins.Ast.Processing/Validator-TBaseNode/Validate.md) and yield any problems found; the base class adds them to the node for you.

```c#
internal sealed class DivideByZeroValidator : NodeValidator<MathsNode, BinaryOperation>
{
    protected override IEnumerable<Message> Validate(BinaryOperation node)
    {
        if (node is { Operator: '/', Right: Constant { Value: 0 } })
        {
            yield return Message.Error("Divide by zero.");
        }
    }
}
```

[`Validator<TBaseNode>`](API/MrKWatkins.Ast.Processing/Validator-TBaseNode/index.md) covers every node and [`NodeValidator<TBaseNode, TNode>`](API/MrKWatkins.Ast.Processing/NodeValidator-TBaseNode-TNode/index.md) a specific type, again with context variants. Validators are plain processors rather than ordered ones, so they can run in parallel stages.

## Pipelines

Build a pipeline with [`Pipeline<TBaseNode>.Build`](API/MrKWatkins.Ast.Processing/Pipeline-TBaseNode/Build.md), adding stages through the [`PipelineBuilder<TBaseNode>`](API/MrKWatkins.Ast.Processing/PipelineBuilder-TBaseNode/index.md) it hands you:

```c#
private static readonly Pipeline<MathsNode> Pipeline =
    Pipeline<MathsNode>
        .Build(
            builder =>
                builder
                    .AddStage<Reducer>("Reduction")
                    .AddStage<DivideByZeroValidator>("Validation"));
```

[`AddStage`](API/MrKWatkins.Ast.Processing/PipelineBuilder-TBaseNode/AddStage.md) creates a stage whose processors run one after the other, each getting its own walk of the tree. [`AddParallelStage`](API/MrKWatkins.Ast.Processing/PipelineBuilder-TBaseNode/AddParallelStage.md) creates one where the tree is walked once and node-and-processor pairs are dispatched across threads. Overloads of both take processor instances, a processor type with a parameterless constructor, an optional stage name, and an action on the stage builder for finer control. Unnamed stages are named after their position in the pipeline.

The stage builders offer:

| Method | Description |
| ------ | ----------- |
| [`Add`](API/MrKWatkins.Ast.Processing/PipelineStageBuilder-TSelf-TStage-TBaseNode-TProcessor-TShouldContinue/Add.md) | Adds processors, by instance or by type. |
| [`WithName`](API/MrKWatkins.Ast.Processing/PipelineStageBuilder-TSelf-TStage-TBaseNode-TProcessor-TShouldContinue/WithName.md) | Names the stage, for reporting which stage stopped the pipeline. |
| [`WithShouldContinue`](API/MrKWatkins.Ast.Processing/PipelineStageBuilder-TSelf-TStage-TBaseNode-TProcessor-TShouldContinue/WithShouldContinue.md) | Replaces the test for whether the pipeline continues after this stage. |
| [`WithAlwaysContinue`](API/MrKWatkins.Ast.Processing/PipelineStageBuilder-TSelf-TStage-TBaseNode-TProcessor-TShouldContinue/WithAlwaysContinue.md) | Continues regardless of errors in the tree. |
| [`WithDefaultTraversal`](API/MrKWatkins.Ast.Processing/PipelineStageBuilder-TSelf-TStage-TBaseNode-TProcessor-TShouldContinue/WithDefaultTraversal.md) | Sets the traversal used for processors that don't specify their own. |
| [`WithMaxDegreeOfParallelism`](API/MrKWatkins.Ast.Processing/ParallelPipelineStageBuilder-TBaseNode/WithMaxDegreeOfParallelism.md) | Parallel stages only; defaults to the machine's processor count. |

Run the pipeline on a root node:

```c#
var (success, newRoot, lastStageRun) = Pipeline.Run(function);
```

[`Run`](API/MrKWatkins.Ast.Processing/Pipeline-TBaseNode/Run.md) works through the stages in order and returns `true` if they all ran. If it returns `false`, `lastStageRun` names the stage that stopped the pipeline. Overloads return the same information through `out` parameters instead of a tuple. Always use the root that comes back rather than the one you passed in, as a [replacer](#replacers) may have swapped it.

By default a stage stops the pipeline if the tree has any errors once the stage completes — that is, if [`ThisAndDescendentsHaveErrors`](API/MrKWatkins.Ast/Node-TNode/ThisAndDescendentsHaveErrors.md) is `true` for the root. Use [`WithShouldContinue`](API/MrKWatkins.Ast.Processing/PipelineStageBuilder-TSelf-TStage-TBaseNode-TProcessor-TShouldContinue/WithShouldContinue.md) for a different rule, or [`WithAlwaysContinue`](API/MrKWatkins.Ast.Processing/PipelineStageBuilder-TSelf-TStage-TBaseNode-TProcessor-TShouldContinue/WithAlwaysContinue.md) to press on regardless — useful for a stage that only gathers extra diagnostics.

Exceptions from a processor, or from a should-continue function, are wrapped in a [`PipelineException`](API/MrKWatkins.Ast.Processing/PipelineException/index.md) naming the [`Stage`](API/MrKWatkins.Ast.Processing/PipelineException/Stage.md) they came from.

## Example

The [Maths example](https://github.com/MrKWatkins/Ast/tree/main/examples/Maths) uses a two stage pipeline to reduce constant expressions and then validate against divide by zero.
