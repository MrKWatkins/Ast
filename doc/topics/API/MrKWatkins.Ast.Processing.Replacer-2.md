# Replacer&lt;TContext, TBaseNode&gt; Class
## Definition

An [OrderedProcessor&lt;TContext, TBaseNode&gt;](MrKWatkins.Ast.Processing.OrderedProcessor-2.md) for optionally replacing nodes in a tree.

```c#
public abstract class Replacer<TContext, TBaseNode> : OrderedProcessor<TContext, TBaseNode>
   where TBaseNode : Node<TBaseNode>
```

### Type Parameters

| Name | Description |
| ---- | ----------- |
| TContext | The type of the processing context. |
| TBaseNode | The base type of nodes in the tree. |

## Constructors

| Name | Description |
| ---- | ----------- |
| [Replacer()](MrKWatkins.Ast.Processing.Replacer-2.-ctor.md) |  |

## Methods

| Name | Description |
| ---- | ----------- |
| [Process(TContext, TBaseNode)](MrKWatkins.Ast.Processing.Replacer-2.Process.md) |  |
| [Replace(TContext, TBaseNode)](MrKWatkins.Ast.Processing.Replacer-2.Replace.md) | Optionally replace the specified node. |

