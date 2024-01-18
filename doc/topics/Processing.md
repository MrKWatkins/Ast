# Processing

Processing is an alternative method to [listening](Listeners.md) for visiting nodes in a tree and performing some functionality. Processing is best for
mutating a tree, such as replacing nodes or validating errors, whereas listeners are more suitable for building something new from a tree. Processing
is performed via a pipeline of stages. Each stage consists of one or more processors which can optionally be run in parallel.

## Ordered vs. Unordered Processors

There are two main categories of processor, ordered and unordered. When an ordered processor is executed on a tree then a well defined order of processing
will be followed. Unordered processors do not guarantee the order in which nodes will be visited. Ordered processors cannot be run in parallel whereas
unordered processors can. The reason for this is the nature of the parallel processing. The tree is walked once and nodes are handed off to a work queue
for processing by other threads; this means race conditions stop us from guaranteeing the order of processing.

## Unordered Processors

There are four base unordered processors classes that can be inherited from to create a processor:

* [UnorderedProcessor\<TNode\>](MrKWatkins.Ast.Processing.UnorderedProcessor-1.md) - Processes all nodes in a tree.
* [UnorderedProcessor\<TBaseNode, TNode\>](MrKWatkins.Ast.Processing.UnorderedProcessor-2.md) - Processes all nodes of a specific type in a tree.
* [UnorderedProcessorWithContext\<TContext, TNode\>](MrKWatkins.Ast.Processing.UnorderedProcessorWithContext-2.md) - Processes all nodes in a tree with access to a context object.
* [UnorderedProcessorWithContext\<TContext, TBaseNode, TNode\>](MrKWatkins.Ast.Processing.UnorderedProcessorWithContext-3.md) - Processes all nodes of a specific type in a tree with access to a context object.

You must implement the [ProcessNode](MrKWatkins.Ast.Processing.UnorderedProcessor-1.ProcessNode.md) method to perform your processing. You can also
optionally override the [ShouldProcessNode](MrKWatkins.Ast.Processing.UnorderedProcessor-1.ShouldProcessNode.md) method to perform a check and decide whether [ProcessNode](MrKWatkins.Ast.Processing.UnorderedProcessor-1.ProcessNode.md) should be called for the node or not.

Processors with context must also override the [CreateContext](MrKWatkins.Ast.Processing.UnorderedProcessorWithContext-2.CreateContext.md) method to create
the context object from the root node. Each processor gets a single context object for the walk through the tree. This differs from [listening](Listeners.md)
where the context object is passed in to the listeners. This is more appropriate for listeners as they are designed to create something new, which can be
stored on the context object. For processors the context is designed to be used for caching data during the tree walk.

## Ordered Processors

There are four base ordered processors classes that can be inherited from to create a processor:

* [OrderedProcessor\<TNode\>](MrKWatkins.Ast.Processing.OrderedProcessor-1.md) - Processes all nodes in a tree.
* [OrderedProcessor\<TBaseNode, TNode\>](MrKWatkins.Ast.Processing.OrderedProcessor-2.md) - Processes all nodes of a specific type in a tree.
* [OrderedProcessorWithContext\<TContext, TNode\>](MrKWatkins.Ast.Processing.OrderedProcessorWithContext-2.md) - Processes all nodes in a tree with access to a context object.
* [OrderedProcessorWithContext\<TContext, TBaseNode, TNode\>](MrKWatkins.Ast.Processing.OrderedProcessorWithContext-3.md) - Processes all nodes of a specific type in a tree with access to a context object.

As with unordered listeners you must implement the [ProcessNode](MrKWatkins.Ast.Processing.OrderedProcessor-1.ProcessNode.md) method to perform your processing and can also optionally override the
[ShouldProcessNode](MrKWatkins.Ast.Processing.OrderedProcessor-1.ShouldProcessNode.md) method, and override the [CreateContext](MrKWatkins.Ast.Processing.OrderedProcessorWithContext-2.CreateContext.md) method if using context.

Ordered processors also have ways to control the order of processing. The [Traversal](MrKWatkins.Ast.Processing.OrderedProcessor-1.Traversal.md) property can be overridden to define the order to
walk the tree; it defaults to [depth first pre-order](MrKWatkins.Ast.Traversal.DepthFirstPreOrderTraversal-1.md). The [ShouldProcessChildren](MrKWatkins.Ast.Processing.OrderedProcessor-1.ShouldProcessNode.md)
method can also be overridden to decide whether the children of a node should be processed or not.

## Replacers

Replacers are a type of ordered processor to simplify the process of replacing nodes in a tree. There are three base replacer classes:

* [Replacer\<TNode\>](MrKWatkins.Ast.Processing.Replacer-1.md) - Called for all nodes in a tree.
* [Replacer\<TBaseNode, TNode\>](MrKWatkins.Ast.Processing.Replacer-2.md) - Called for all nodes of a specific type in a tree.
* [Replacer\<TBaseNode, TNode, TReplacementNode\>](MrKWatkins.Ast.Processing.Replacer-3.md) - Called for all nodes of a specific type in a tree, with a specific replacement type.

Override the [ReplaceNode](MrKWatkins.Ast.Processing.Replacer-1.ReplaceNode.md) method to optionally replace a node. If a new node is returned then it will replace the old node; if the old node or `null`
is returned then the node will not be replaced.

By default, the replacer will not be called for a new node and it's children; override the [ReplaceNode](MrKWatkins.Ast.Processing.Replacer-1.ProcessReplacements.md) property to change this behaviour. This will
process the new node as well as its children so make sure not to get into an infinite loop!

## Validators

Validators are a type of unordered processor to simplify the process of validating nodes in a tree. There are two base validator classes:

* [Validator\<TNode\>](MrKWatkins.Ast.Processing.Validator-1.md) - Called for all nodes in a tree.
* [Validator\<TBaseNode, TNode\>](MrKWatkins.Ast.Processing.Validator-2.md) - Called for all nodes of a specific type in a tree.

Override the [ValidateNode](MrKWatkins.Ast.Processing.Validator-1.ValidateNode.md) method to return any errors, warnings or info [messages](MrKWatkins.Ast.Message.md) to attach to the node being validated.

## Pipelines

Processing pipelines consist of multiple stages, constructed using the [Build](MrKWatkins.Ast.Processing.Pipeline-1.Build.md) method. Use the [PipelineBuilder](MrKWatkins.Ast.Processing.PipelineBuilder-1.md) object to add stages to the pipeline
via the [AddStage](MrKWatkins.Ast.Processing.PipelineBuilder-1.AddStage.md#mrkwatkins-ast-processing-pipelinebuilder-1-addstage(system-action((mrkwatkins-ast-processing-serialpipelinestagebuilder((-0))))))
and [AddParallelStage](MrKWatkins.Ast.Processing.PipelineBuilder-1.AddParallelStage.md#mrkwatkins-ast-processing-pipelinebuilder-1-addparallelstage(system-action((mrkwatkins-ast-processing-parallelpipelinestagebuilder((-0))))))
methods. The overloads accept processor instances, processor types (which must have a parameterless constructor) or a [PipelineStageBuilder](MrKWatkins.Ast.Processing.PipelineStageBuilder-3.md) 
object for more fine grained control over the stage.

Once you have built a pipeline then it can be run on a tree using the [Run](MrKWatkins.Ast.Processing.Pipeline-1.Run.md#mrkwatkins-ast-processing-pipeline-1-run(-0)) 
method. This will proceed through all the stages in order, returning `true` if all stages completed successfully, or `false` otherwise. By default, the pipeline 
will stop after a stage if there are any errors in the tree once that stage is complete. This behaviour can be changed using the [WithShouldContinue](MrKWatkins.Ast.Processing.PipelineStageBuilder-3.WithShouldContinue.md) or
[WithAlwaysContinue](MrKWatkins.Ast.Processing.PipelineStageBuilder-3.WithAlwaysContinue.md) when building your pipeline stage.

## Example

The maths example at <https://github.com/MrKWatkins/Ast/tree/main/examples/Maths> uses processing to reduce expressions in the tree and validate against divide by zero.