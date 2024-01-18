# Replacer&lt;TBaseNode, TNode&gt; Class
## Definition

An [OrderedProcessor&lt;TNode&gt;](MrKWatkins.Ast.Processing.OrderedProcessor-1.md) for optionally replacing nodes of a specific type in a tree.

```c#
public abstract class Replacer<TBaseNode, TNode> : OrderedProcessor<TBaseNode, TNode>
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
| [Replacer()](MrKWatkins.Ast.Processing.Replacer-2.-ctor.md) |  |

## Properties

| Name | Description |
| ---- | ----------- |
| [ProcessReplacements](MrKWatkins.Ast.Processing.Replacer-2.ProcessReplacements.md) | If set to `true` then replacement nodes and their children will be processed too. Defaults to `false`. |

## Methods

| Name | Description |
| ---- | ----------- |
| [ProcessNode](MrKWatkins.Ast.Processing.Replacer-2.ProcessNode.md) |  |
| [ReplaceNode](MrKWatkins.Ast.Processing.Replacer-2.ReplaceNode.md) | Optionally replace the specified node. |

