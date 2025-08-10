# NodeReplacer&lt;TBaseNode, TNode&gt; Class
## Definition

An [OrderedNodeProcessor&lt;TBaseNode, TNode&gt;](MrKWatkins.Ast.Processing.OrderedNodeProcessor-2.md) for optionally replacing nodes of a specific type in a tree.

```c#
public abstract class NodeReplacer<TBaseNode, TNode> : OrderedNodeProcessor<TBaseNode, TNode>
   where TBaseNode : Node<TBaseNode>
   where TNode : TBaseNode
```

### Type Parameters

| Name | Description |
| ---- | ----------- |
| TBaseNode | The base type of nodes in the tree. |
| TNode | The type of nodes to replace. |

## Constructors

| Name | Description |
| ---- | ----------- |
| [NodeReplacer()](MrKWatkins.Ast.Processing.NodeReplacer-2.-ctor.md) |  |

## Methods

| Name | Description |
| ---- | ----------- |
| [Process(TNode)](MrKWatkins.Ast.Processing.NodeReplacer-2.Process.md) |  |
| [Replace(TNode)](MrKWatkins.Ast.Processing.NodeReplacer-2.Replace.md) | Optionally replace the specified node. |

