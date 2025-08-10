# Validator&lt;TBaseNode&gt; Class
## Definition

An [Processor&lt;TBaseNode&gt;](MrKWatkins.Ast.Processing.Processor-1.md) for validating nodes in a tree.

```c#
public abstract class Validator<TBaseNode> : Processor<TBaseNode>
   where TBaseNode : Node<TBaseNode>
```

### Type Parameters

| Name | Description |
| ---- | ----------- |
| TBaseNode | The base type of nodes to validate. |

## Constructors

| Name | Description |
| ---- | ----------- |
| [Validator()](MrKWatkins.Ast.Processing.Validator-1.-ctor.md) |  |

## Methods

| Name | Description |
| ---- | ----------- |
| [Process(TBaseNode)](MrKWatkins.Ast.Processing.Validator-1.Process.md) |  |
| [Validate(TBaseNode)](MrKWatkins.Ast.Processing.Validator-1.Validate.md) | Validate the node and return any [Messages](MrKWatkins.Ast.Message.md) to attach to the node to describe any validation issues. |

