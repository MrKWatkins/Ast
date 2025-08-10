# NodeValidator&lt;TContext, TBaseNode, TNode&gt; Class
## Definition

An [NodeProcessor&lt;TBaseNode, TNode&gt;](MrKWatkins.Ast.Processing.NodeProcessor-2.md) for validating nodes of a specific type in a tree.

```c#
public abstract class NodeValidator<TContext, TBaseNode, TNode> : NodeProcessor<TContext, TBaseNode, TNode>
   where TBaseNode : Node<TBaseNode>
   where TNode : TBaseNode
```

### Type Parameters

| Name | Description |
| ---- | ----------- |
| TContext | The type of the processing context. |
| TBaseNode | The base type of nodes in the tree. |
| TNode | The type of nodes to validate. |

## Constructors

| Name | Description |
| ---- | ----------- |
| [NodeValidator()](MrKWatkins.Ast.Processing.NodeValidator-3.-ctor.md) |  |

## Methods

| Name | Description |
| ---- | ----------- |
| [Process(TContext, TNode)](MrKWatkins.Ast.Processing.NodeValidator-3.Process.md) |  |
| [Validate(TContext, TNode)](MrKWatkins.Ast.Processing.NodeValidator-3.Validate.md) | Validate the node and return any [Messages](MrKWatkins.Ast.Message.md) to attach to the node to describe any validation issues. |

