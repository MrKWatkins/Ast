# PipelineStage&lt;TContext, TBaseNode&gt; Class
## Definition
A stage from a [Pipeline&lt;TContext, TBaseNode&gt;](MrKWatkins.Ast.Processing.Pipeline-2.md)
```c#
public abstract class PipelineStage<TContext, TBaseNode>
   where TBaseNode : Node<TBaseNode>
```
### Type Parameters
| Name | Description |
| ---- | ----------- |
| TContext | The type of the processing context. |
| TBaseNode | The type of nodes in the tree. |
## Properties
| Name | Description |
| ---- | ----------- |
| [DefaultTraversal](MrKWatkins.Ast.Processing.PipelineStage-2.DefaultTraversal.md) | The default [ITraversal&lt;TNode&gt;](MrKWatkins.Ast.Traversal.ITraversal-1.md) to use when traversing the tree if not specified by an [OrderedProcessor&lt;TBaseNode&gt;](MrKWatkins.Ast.Processing.OrderedProcessor-1.md). |
| [Name](MrKWatkins.Ast.Processing.PipelineStage-2.Name.md) | The name of the stage. |
| [ShouldContinue](MrKWatkins.Ast.Processing.PipelineStage-2.ShouldContinue.md) | Function to run after the stage to determine if the pipeline should move on to the next stage or not. |
## Methods
| Name | Description |
| ---- | ----------- |
| [Run(TContext, TBaseNode)](MrKWatkins.Ast.Processing.PipelineStage-2.Run.md#runtcontext-tbasenode) | Runs the stage, returning a tuple of whether the pipeline should continue and the potentially replaced root node. |
| [Run(TContext, TBaseNode, out TBaseNode)](MrKWatkins.Ast.Processing.PipelineStage-2.Run.md#runtcontext-tbasenode-out-tbasenode) | Runs the stage, returning the potentially new root via an out parameter. |
