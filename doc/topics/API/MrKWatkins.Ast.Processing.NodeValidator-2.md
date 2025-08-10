# NodeValidator&lt;TBaseNode, TNode&gt; Class
## Definition

An [NodeProcessor&lt;TBaseNode, TNode&gt;](MrKWatkins.Ast.Processing.NodeProcessor-2.md) for validating nodes of a specific type in a tree.

```c#
public abstract class NodeValidator<TBaseNode, TNode> : NodeProcessor<TBaseNode, TNode>
   where TBaseNode : Node<TBaseNode>
   where TNode : TBaseNode
```

### Type Parameters

| Name | Description |
| ---- | ----------- |
| TBaseNode | The base type of nodes in the tree. |
| TNode | The type of nodes to validate. |

## Constructors

| Name | Description |
| ---- | ----------- |
| [NodeValidator()](MrKWatkins.Ast.Processing.NodeValidator-2.-ctor.md) |  |

## Methods

| Name | Description |
| ---- | ----------- |
| [Process(TNode)](MrKWatkins.Ast.Processing.NodeValidator-2.Process.md) |  |
| [Validate(TNode)](MrKWatkins.Ast.Processing.NodeValidator-2.Validate.md) | Validate the node and return any [Messages](MrKWatkins.Ast.Message.md) to attach to the node to describe any validation issues. |

