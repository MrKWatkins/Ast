# Replacer&lt;TBaseNode&gt; Class
## Definition

An [OrderedProcessor&lt;TBaseNode&gt;](MrKWatkins.Ast.Processing.OrderedProcessor-1.md) for optionally replacing nodes in a tree.

```c#
public abstract class Replacer<TBaseNode> : OrderedProcessor<TBaseNode>
   where TBaseNode : Node<TBaseNode>
```

### Type Parameters

| Name | Description |
| ---- | ----------- |
| TBaseNode | The base type of nodes in the tree. |

## Constructors

| Name | Description |
| ---- | ----------- |
| [Replacer()](MrKWatkins.Ast.Processing.Replacer-1.-ctor.md) |  |

## Methods

| Name | Description |
| ---- | ----------- |
| [Process(TBaseNode)](MrKWatkins.Ast.Processing.Replacer-1.Process.md) |  |
| [Replace(TBaseNode)](MrKWatkins.Ast.Processing.Replacer-1.Replace.md) | Optionally replace the specified node. |

