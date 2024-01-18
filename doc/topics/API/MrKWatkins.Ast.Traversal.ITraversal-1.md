# ITraversal&lt;TNode&gt; Interface
## Definition

Strategy for traversing nodes in a tree.

```c#
public abstract interface ITraversal<TNode>
   where TNode : Node<TNode>
```

### Type Parameters

| Name | Description |
| ---- | ----------- |
| TNode | The type of nodes in the tree. |

## Methods

| Name | Description |
| ---- | ----------- |
| [Enumerate(TNode, bool, Func&lt;TNode, Boolean&gt;)](MrKWatkins.Ast.Traversal.ITraversal-1.Enumerate.md) | Enumerates over a node and its descendents. |

