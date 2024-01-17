# ProcessingException&lt;TNode&gt; Class
## Definition

Exception thrown by [Processors](MrKWatkins.Ast.Processing.Processor-1.md) when a problem occurs with details of the node that caused the problem.

```c#
public sealed class ProcessingException<TNode> : ProcessingException, ISerializable
   where TNode : Node<TNode>
```

### Type Parameters

| Name | Description |
| ---- | ----------- |
| TNode |  |

## Properties

| Name | Description |
| ---- | ----------- |
| [Message](MrKWatkins.Ast.Processing.ProcessingException-1.Message.md) |  |
| [Node](MrKWatkins.Ast.Processing.ProcessingException-1.Node.md) | The node that caused the problem. |

