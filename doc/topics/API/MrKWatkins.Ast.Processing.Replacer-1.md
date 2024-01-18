# Replacer&lt;TNode&gt; Class
## Definition

An [OrderedProcessor&lt;TNode&gt;](MrKWatkins.Ast.Processing.OrderedProcessor-1.md) for optionally replacing nodes in a tree.

```c#
public abstract class Replacer<TNode> : OrderedProcessor<TNode>
   where TNode : Node<TNode>
```

### Type Parameters

| Name | Description |
| ---- | ----------- |
| TNode | The type of nodes in the tree. |

## Constructors

| Name | Description |
| ---- | ----------- |
| [Replacer()](MrKWatkins.Ast.Processing.Replacer-1.-ctor.md) |  |

## Properties

| Name | Description |
| ---- | ----------- |
| [ProcessReplacements](MrKWatkins.Ast.Processing.Replacer-1.ProcessReplacements.md) | If set to `true` then replacement nodes and their children will be processed too. Defaults to `false`. |

## Methods

| Name | Description |
| ---- | ----------- |
| [ProcessNode(TNode)](MrKWatkins.Ast.Processing.Replacer-1.ProcessNode.md) |  |
| [ReplaceNode(TNode)](MrKWatkins.Ast.Processing.Replacer-1.ReplaceNode.md) | Optionally replace the specified node. |

