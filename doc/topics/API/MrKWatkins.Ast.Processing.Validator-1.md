# Validator&lt;TNode&gt; Class
## Definition

An [UnorderedProcessor&lt;TNode&gt;](MrKWatkins.Ast.Processing.UnorderedProcessor-1.md) for validating nodes in a tree.

```c#
public abstract class Validator<TNode> : UnorderedProcessor<TNode>
   where TNode : Node<TNode>
```

### Type Parameters

| Name | Description |
| ---- | ----------- |
| TNode |  |

## Constructors

| Name | Description |
| ---- | ----------- |
| [Validator()](MrKWatkins.Ast.Processing.Validator-1.-ctor.md) |  |

## Methods

| Name | Description |
| ---- | ----------- |
| [ProcessNode(TNode)](MrKWatkins.Ast.Processing.Validator-1.ProcessNode.md) |  |
| [ValidateNode(TNode)](MrKWatkins.Ast.Processing.Validator-1.ValidateNode.md) | Validate the node and return any [Messages](MrKWatkins.Ast.Message.md) to attach to the node to describe any validation issues. |

