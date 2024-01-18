# Replacer&lt;TBaseNode, TNode, TReplacementNode&gt; Class
## Definition

An [OrderedProcessor&lt;TNode&gt;](MrKWatkins.Ast.Processing.OrderedProcessor-1.md) for optionally replacing nodes of a specific type in a tree with new nodes of a specific type.

```c#
public abstract class Replacer<TBaseNode, TNode, TReplacementNode> : OrderedProcessor<TBaseNode, TNode>
   where TBaseNode : Node<TBaseNode>
   where TNode : TBaseNode
   where TReplacementNode : TBaseNode
```

### Type Parameters

| Name | Description |
| ---- | ----------- |
| TBaseNode | The base type of nodes in the tree. |
| TNode | The type of nodes to process. |
| TReplacementNode | The type of the replacement nodes. |

## Constructors

| Name | Description |
| ---- | ----------- |
| [Replacer()](MrKWatkins.Ast.Processing.Replacer-3.-ctor.md) |  |

## Properties

| Name | Description |
| ---- | ----------- |
| [ProcessReplacements](MrKWatkins.Ast.Processing.Replacer-3.ProcessReplacements.md) | If set to `true` then replacement nodes and their children will be processed too. Defaults to `false`. |

## Methods

| Name | Description |
| ---- | ----------- |
| [ProcessNode](MrKWatkins.Ast.Processing.Replacer-3.ProcessNode.md) |  |
| [ReplaceNode](MrKWatkins.Ast.Processing.Replacer-3.ReplaceNode.md) | Optionally replace the specified node. |

