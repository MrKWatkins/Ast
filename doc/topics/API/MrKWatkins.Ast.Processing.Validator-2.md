# Validator&lt;TContext, TBaseNode&gt; Class
## Definition

An [Processor&lt;TBaseNode&gt;](MrKWatkins.Ast.Processing.Processor-1.md) for validating nodes in a tree.

```c#
public abstract class Validator<TContext, TBaseNode> : Processor<TContext, TBaseNode>
   where TBaseNode : Node<TBaseNode>
```

### Type Parameters

| Name | Description |
| ---- | ----------- |
| TContext | The type of the processing context. |
| TBaseNode | The base type of nodes to validate. |

## Constructors

| Name | Description |
| ---- | ----------- |
| [Validator()](MrKWatkins.Ast.Processing.Validator-2.-ctor.md) |  |

## Methods

| Name | Description |
| ---- | ----------- |
| [Process(TContext, TBaseNode)](MrKWatkins.Ast.Processing.Validator-2.Process.md) |  |
| [Validate(TContext, TBaseNode)](MrKWatkins.Ast.Processing.Validator-2.Validate.md) | Validate the node and return any [Messages](MrKWatkins.Ast.Message.md) to attach to the node to describe any validation issues. |

