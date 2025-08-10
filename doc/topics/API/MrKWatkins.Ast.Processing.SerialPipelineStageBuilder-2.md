# SerialPipelineStageBuilder&lt;TContext, TBaseNode&gt; Class
## Definition

Fluent builder for a serial pipeline stage.

```c#
public sealed class SerialPipelineStageBuilder<TContext, TBaseNode> : PipelineStageBuilder<SerialPipelineStageBuilder<TContext, TBaseNode>, SerialPipelineStage<TContext, TBaseNode>, TBaseNode, Processor<TContext, TBaseNode>, Func<TContext, TBaseNode, Boolean>>
   where TBaseNode : Node<TBaseNode>
```

### Type Parameters

| Name | Description |
| ---- | ----------- |
| TContext | The type of the context for the processing. |
| TBaseNode | The base type of nodes in the tree. |

## Methods

| Name | Description |
| ---- | ----------- |
| [WithAlwaysContinue()](MrKWatkins.Ast.Processing.SerialPipelineStageBuilder-2.WithAlwaysContinue.md) | Always continue to the next stage after this one, irrespective of errors in the tree. |

