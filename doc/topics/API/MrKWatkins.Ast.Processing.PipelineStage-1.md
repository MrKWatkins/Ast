# PipelineStage&lt;TBaseNode&gt; Class
## Definition

A stage from a [Pipeline&lt;TBaseNode&gt;](MrKWatkins.Ast.Processing.Pipeline-1.md)

```c#
public abstract class PipelineStage<TBaseNode>
   where TBaseNode : Node<TBaseNode>
```

### Type Parameters

| Name | Description |
| ---- | ----------- |
| TBaseNode | The type of nodes in the tree. |

## Properties

| Name | Description |
| ---- | ----------- |
| [DefaultTraversal](MrKWatkins.Ast.Processing.PipelineStage-1.DefaultTraversal.md) | The default [ITraversal&lt;TNode&gt;](MrKWatkins.Ast.Traversal.ITraversal-1.md) to use when traversing the tree if not specified by an [OrderedProcessor&lt;TBaseNode&gt;](MrKWatkins.Ast.Processing.OrderedProcessor-1.md). |
| [Name](MrKWatkins.Ast.Processing.PipelineStage-1.Name.md) | The name of the stage. |
| [ShouldContinue](MrKWatkins.Ast.Processing.PipelineStage-1.ShouldContinue.md) | Function to run after the stage to determine if the pipeline should move on to the next stage or not. |

## Methods

| Name | Description |
| ---- | ----------- |
| [Run(TBaseNode)](MrKWatkins.Ast.Processing.PipelineStage-1.Run.md) | Runs the stage. |

