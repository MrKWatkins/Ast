# SerialPipelineStageBuilder&lt;TBaseNode&gt; Class
## Definition

Fluent builder for a serial pipeline stage.

```c#
public sealed class SerialPipelineStageBuilder<TBaseNode> : PipelineStageBuilder<SerialPipelineStageBuilder<TBaseNode>, SerialPipelineStage<TBaseNode>, TBaseNode, Processor<TBaseNode>, Func<TBaseNode, Boolean>>
   where TBaseNode : Node<TBaseNode>
```

### Type Parameters

| Name | Description |
| ---- | ----------- |
| TBaseNode | The base type of nodes in the tree. |

## Methods

| Name | Description |
| ---- | ----------- |
| [WithAlwaysContinue()](MrKWatkins.Ast.Processing.SerialPipelineStageBuilder-1.WithAlwaysContinue.md) | Always continue to the next stage after this one, irrespective of errors in the tree. |

