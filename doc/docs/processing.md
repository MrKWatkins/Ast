# Processing

Processing runs a pipeline of stages over a tree. It is the counterpart to [listening](listeners.md): processing is best for mutating a tree — replacing nodes, validating them, annotating them — whereas listeners are better for building something new from one.

Each stage contains one or more processors and runs them either serially or in parallel. Stages decide whether the pipeline should carry on to the next one, which by default means stopping as soon as the tree has [errors](messages.md) in it.

## Processors

A [`Processor<TBaseNode>`](API/MrKWatkins.Ast.Processing/Processor-TBaseNode/index.md) has a single [`Process`](API/MrKWatkins.Ast.Processing/Processor-TBaseNode/Process.md) method that acts on **one** node. Walking the tree is the pipeline's job, not the processor's; [`Process`](API/MrKWatkins.Ast.Processing/Processor-TBaseNode/Process.md) is called once per node and should not touch descendants itself. Processors that need to swap nodes in or out of the tree, including the root, should be [replacers](#replacers), which handle the mechanics.

Most processors only care about one kind of node. [`NodeProcessor<TBaseNode, TNode>`](API/MrKWatkins.Ast.Processing/NodeProcessor-TBaseNode-TNode/index.md) does the type test for you and only calls your [`Process`](API/MrKWatkins.Ast.Processing/NodeProcessor-TBaseNode-TNode/Process.md) for matching nodes:

```c#
internal sealed class OperatorCounter : NodeProcessor<MathsNode, BinaryOperation>
{
    protected override void Process(BinaryOperation node) => Count++;

    public int Count { get; private set; }
}
```

Each family of processors also has a `TContext` variant — [`Processor<TContext, TBaseNode>`](API/MrKWatkins.Ast.Processing/Processor-TContext-TBaseNode/index.md), [`NodeProcessor<TContext, TBaseNode, TNode>`](API/MrKWatkins.Ast.Processing/NodeProcessor-TContext-TBaseNode-TNode/index.md) and so on — taking a context object supplied when the pipeline is run. Unlike [listeners](listeners.md), where the context is the point of the exercise, a processing context is usually for configuration or for caching data gathered as the tree is walked. The two forms cannot be mixed in one pipeline. Under the covers the context-free pipeline, stages and processors are thin wrappers over the context-taking ones using the empty [`NoContext`](API/MrKWatkins.Ast/NoContext/index.md) type, so both behave identically.

## Ordered Processors

A plain [`Processor<TBaseNode>`](API/MrKWatkins.Ast.Processing/Processor-TBaseNode/index.md) makes no promises about the order its nodes arrive in, which is what makes it safe to run in parallel. When order matters, inherit from [`OrderedProcessor<TBaseNode>`](API/MrKWatkins.Ast.Processing/OrderedProcessor-TBaseNode/index.md) instead. It adds two members:

- [`GetTraversal`](API/MrKWatkins.Ast.Processing/OrderedProcessor-TBaseNode/GetTraversal.md) returns the [`ITraversal<TNode>`](API/MrKWatkins.Ast.Traversal/ITraversal-TNode/index.md) the pipeline should walk the tree with for this processor, defaulting to [depth first pre-order](API/MrKWatkins.Ast.Traversal/DepthFirstPreOrderTraversal-TNode/index.md).
- [`ShouldProcessDescendants`](API/MrKWatkins.Ast.Processing/OrderedProcessor-TBaseNode/ShouldProcessDescendants.md) decides whether to walk into a node's descendants at all.

Ordered processors cannot be added to a parallel stage; the builder throws an `ArgumentException` if you try. In a parallel stage nodes are processed across threads, so there is no order to guarantee.

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

Replacing the root node is allowed. As the root has no parent to swap it in, the new root comes back out of the pipeline; see [Pipelines](#pipelines) below. That also means a replacer can only replace the root when run in a pipeline: calling `Process` on a replacer directly with a root node it would replace throws an `InvalidOperationException`.

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

[`AddStage`](API/MrKWatkins.Ast.Processing/PipelineBuilder-TBaseNode/AddStage.md) creates a stage whose processors run one after the other, each getting its own walk of the tree. [`AddParallelStage`](API/MrKWatkins.Ast.Processing/PipelineBuilder-TBaseNode/AddParallelStage.md) creates one where processing is spread across threads; see [Parallel Stages](#parallel-stages) below. Overloads of both take processor instances, a processor type with a parameterless constructor, an optional stage name, and an action on the stage builder for finer control. Unnamed stages are named after their position in the pipeline.

The stage builders offer:

| Method | Description |
| ------ | ----------- |
| [`Add`](API/MrKWatkins.Ast.Processing/PipelineStageBuilder-TSelf-TStage-TBaseNode-TProcessor-TShouldContinue/Add.md) | Adds processors, by instance or by type. |
| [`WithName`](API/MrKWatkins.Ast.Processing/PipelineStageBuilder-TSelf-TStage-TBaseNode-TProcessor-TShouldContinue/WithName.md) | Names the stage, for reporting which stage stopped the pipeline. |
| [`WithShouldContinue`](API/MrKWatkins.Ast.Processing/PipelineStageBuilder-TSelf-TStage-TBaseNode-TProcessor-TShouldContinue/WithShouldContinue.md) | Replaces the test for whether the pipeline continues after this stage. |
| [`WithAlwaysContinue`](API/MrKWatkins.Ast.Processing/PipelineStageBuilder-TSelf-TStage-TBaseNode-TProcessor-TShouldContinue/WithAlwaysContinue.md) | Continues regardless of errors in the tree. |
| [`WithDefaultTraversal`](API/MrKWatkins.Ast.Processing/PipelineStageBuilder-TSelf-TStage-TBaseNode-TProcessor-TShouldContinue/WithDefaultTraversal.md) | Sets the traversal used for processors that don't specify their own. Ordered processors always use their own [`GetTraversal`](API/MrKWatkins.Ast.Processing/OrderedProcessor-TBaseNode/GetTraversal.md), so this only applies to unordered ones. |
| [`WithMaxDegreeOfParallelism`](API/MrKWatkins.Ast.Processing/ParallelPipelineStageBuilder-TBaseNode/WithMaxDegreeOfParallelism.md) | Parallel stages only; defaults to the machine's processor count. |
| [`WithStrategy`](API/MrKWatkins.Ast.Processing/ParallelPipelineStageBuilder-TBaseNode/WithStrategy.md) | Parallel stages only; how work is divided between threads. See [Parallel Stages](#parallel-stages). |

Run the pipeline on a root node:

```c#
var result = Pipeline.Run(function);

if (!result.Success)
{
    Console.WriteLine($"Stopped at stage {result.LastStageRun}.");
}

return result.Root;
```

[`Run`](API/MrKWatkins.Ast.Processing/Pipeline-TBaseNode/Run.md) works through the stages in order and returns a [`PipelineResult<TBaseNode>`](API/MrKWatkins.Ast.Processing/PipelineResult-TBaseNode/index.md). [`Success`](API/MrKWatkins.Ast.Processing/PipelineResult-TBaseNode/Success.md) is `true` if all the stages ran; if it is `false`, [`LastStageRun`](API/MrKWatkins.Ast.Processing/PipelineResult-TBaseNode/LastStageRun.md) names the stage that stopped the pipeline. Always use the [`Root`](API/MrKWatkins.Ast.Processing/PipelineResult-TBaseNode/Root.md) that comes back rather than the one you passed in, as a [replacer](#replacers) may have swapped it. The result can be deconstructed if you prefer: `var (success, root, lastStageRun) = Pipeline.Run(function);`. Running a single stage returns a [`PipelineStageResult<TBaseNode>`](API/MrKWatkins.Ast.Processing/PipelineStageResult-TBaseNode/index.md) in the same way.

By default a stage stops the pipeline if the tree has any errors once the stage completes — that is, if [`ThisAndDescendantsHaveErrors`](API/MrKWatkins.Ast/Node-TNode/ThisAndDescendantsHaveErrors.md) is `true` for the root. Use [`WithShouldContinue`](API/MrKWatkins.Ast.Processing/PipelineStageBuilder-TSelf-TStage-TBaseNode-TProcessor-TShouldContinue/WithShouldContinue.md) for a different rule, or [`WithAlwaysContinue`](API/MrKWatkins.Ast.Processing/PipelineStageBuilder-TSelf-TStage-TBaseNode-TProcessor-TShouldContinue/WithAlwaysContinue.md) to press on regardless — useful for a stage that only gathers extra diagnostics.

Exceptions from a processor, or from a should-continue function, are wrapped in a [`PipelineException`](API/MrKWatkins.Ast.Processing/PipelineException/index.md) naming the [`Stage`](API/MrKWatkins.Ast.Processing/PipelineException/Stage.md) they came from and, for a processor, the node being processed. This holds for parallel stages too: if several processors throw, the first exception is the one you get.

## Parallel Stages

A parallel stage is for passes that leave the shape of the tree alone — validators, and processors that annotate the nodes they are given. The tree is walked lazily while the processors run, so it is never loaded into memory in its entirety, but it also means nodes are processed concurrently with their ancestors and descendants and a structural change would corrupt the walk. Replacers are ordered processors and so cannot be added.

How the work is divided between threads is set with [`WithStrategy`](API/MrKWatkins.Ast.Processing/ParallelPipelineStageBuilder-TBaseNode/WithStrategy.md) and a [`ParallelStrategy`](API/MrKWatkins.Ast.Processing/ParallelStrategy/index.md):

| Strategy | Description |
| -------- | ----------- |
| [`PerNode`](API/MrKWatkins.Ast.Processing/ParallelStrategy/index.md) | The default. The tree is walked once and each node is handed to a thread, which runs every processor on it in turn. A node is only ever being processed by one thread at a time, so processors can safely write to the node they are given. Suits many cheap processors. |
| [`PerProcessor`](API/MrKWatkins.Ast.Processing/ParallelStrategy/index.md) | Each processor runs on its own thread and walks the whole tree itself. Different processors can be processing the same node at once, so writes to a node must be thread safe; adding messages is. Suits a few expensive processors, or ones that read neighbouring nodes. |

## Example

The [Maths example](https://github.com/MrKWatkins/Ast/tree/main/examples/Maths) uses a two stage pipeline to reduce constant expressions and then validate against divide by zero.
