# Validator&lt;TBaseNode, TNode&gt; Class
## Definition

An [UnorderedProcessor&lt;TNode&gt;](MrKWatkins.Ast.Processing.UnorderedProcessor-1.md) for validating nodes of a specific type in a tree.

```c#
public abstract class Validator<TBaseNode, TNode> : UnorderedProcessor<TBaseNode, TNode>
   where TBaseNode : Node<TBaseNode>
   where TNode : TBaseNode
```

### Type Parameters

| Name | Description |
| ---- | ----------- |
| TBaseNode | The base type of nodes in the tree. |
| TNode | The type of nodes to process. |

## Constructors

| Name | Description |
| ---- | ----------- |
| [Validator()](MrKWatkins.Ast.Processing.Validator-2.-ctor.md) |  |

## Methods

| Name | Description |
| ---- | ----------- |
| [ProcessNode(TNode)](MrKWatkins.Ast.Processing.Validator-2.ProcessNode.md) |  |
| [ValidateNode(TNode)](MrKWatkins.Ast.Processing.Validator-2.ValidateNode.md) | Validate the node and return any [Messages](MrKWatkins.Ast.Message.md) to attach to the node to describe any validation issues. |

