# OrderedProcessor&lt;TNode&gt;.Traversal Property
## Definition

Override this property to specify the [ITraversal&lt;TNode&gt;](MrKWatkins.Ast.Traversal.ITraversal-1.md) to use to traverse the tree. Defaults to [DepthFirstPreOrderTraversal&lt;TNode&gt;](MrKWatkins.Ast.Traversal.DepthFirstPreOrderTraversal-1.md).

```c#
protected virtual ITraversal<TNode> Traversal { }
```

## Property Value

[ITraversal&lt;TNode&gt;](MrKWatkins.Ast.Traversal.ITraversal-1.md)
