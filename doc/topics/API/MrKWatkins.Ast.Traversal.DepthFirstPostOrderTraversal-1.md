# DepthFirstPostOrderTraversal&lt;TNode&gt; Class
## Definition

Strategy for traversing nodes in a tree depth first, post-order, i.e. depth first with parent nodes being enumerated after their children.

```c#
public sealed class DepthFirstPostOrderTraversal<TNode> : ITraversal<TNode>
   where TNode : Node<TNode>
```

### Type Parameters

| Name | Description |
| ---- | ----------- |
| TNode | The type of nodes in the tree. |

## Fields

| Name | Description |
| ---- | ----------- |
| [Instance](MrKWatkins.Ast.Traversal.DepthFirstPostOrderTraversal-1.Instance.md) | The singleton [DepthFirstPostOrderTraversal&lt;TNode&gt;](MrKWatkins.Ast.Traversal.DepthFirstPostOrderTraversal-1.md) instance. |

## Methods

| Name | Description |
| ---- | ----------- |
| [Enumerate(TNode, Boolean, Func&lt;TNode, Boolean&gt;)](MrKWatkins.Ast.Traversal.DepthFirstPostOrderTraversal-1.Enumerate.md) |  |

## See Also

[](https://en.wikipedia.org/wiki/Depth-first_search)
