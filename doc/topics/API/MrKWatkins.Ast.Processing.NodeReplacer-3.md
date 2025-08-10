# NodeReplacer&lt;TContext, TBaseNode, TNode&gt; Class
## Definition

An [OrderedNodeProcessor&lt;TContext, TBaseNode, TNode&gt;](MrKWatkins.Ast.Processing.OrderedNodeProcessor-3.md) for optionally replacing nodes of a specific type in a tree.

```c#
public abstract class NodeReplacer<TContext, TBaseNode, TNode> : OrderedNodeProcessor<TContext, TBaseNode, TNode>
   where TBaseNode : Node<TBaseNode>
   where TNode : TBaseNode
```

### Type Parameters

| Name | Description |
| ---- | ----------- |
| TContext | The type of the processing context. |
| TBaseNode | The base type of nodes in the tree. |
| TNode | The type of nodes to replace. |

## Constructors

| Name | Description |
| ---- | ----------- |
| [NodeReplacer()](MrKWatkins.Ast.Processing.NodeReplacer-3.-ctor.md) |  |

## Methods

| Name | Description |
| ---- | ----------- |
| [Process(TContext, TNode)](MrKWatkins.Ast.Processing.NodeReplacer-3.Process.md) |  |
| [Replace(TContext, TNode)](MrKWatkins.Ast.Processing.NodeReplacer-3.Replace.md) | Optionally replace the specified node. |

