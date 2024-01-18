# DepthFirstPreOrderTraversal&lt;TNode&gt; Class
## Definition

Strategy for traversing nodes in a tree depth first, pre-order, i.e. depth first with parent nodes being enumerated before their children.

```c#
public sealed class DepthFirstPreOrderTraversal<TNode> : ITraversal<TNode>
   where TNode : Node<TNode>
```

### Type Parameters

| Name | Description |
| ---- | ----------- |
| TNode | The type of nodes in the tree. |

## Fields

| Name | Description |
| ---- | ----------- |
| [Instance](MrKWatkins.Ast.Traversal.DepthFirstPreOrderTraversal-1.Instance.md) | The singleton [DepthFirstPreOrderTraversal&lt;TNode&gt;](MrKWatkins.Ast.Traversal.DepthFirstPreOrderTraversal-1.md) instance. |

## Methods

| Name | Description |
| ---- | ----------- |
| [Enumerate(TNode, bool, Func&lt;TNode, Boolean&gt;)](MrKWatkins.Ast.Traversal.DepthFirstPreOrderTraversal-1.Enumerate.md) |  |

