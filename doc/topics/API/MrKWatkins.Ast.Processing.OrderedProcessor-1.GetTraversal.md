# OrderedProcessor&lt;TBaseNode&gt;.GetTraversal Method
## Definition

Gets the traversal that the [Pipeline&lt;TBaseNode&gt;](MrKWatkins.Ast.Processing.Pipeline-1.md) should use for this processor.

```c#
public virtual ITraversal<TBaseNode> GetTraversal(TBaseNode root);
```

## Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| root | TBaseNode | The root of the tree. |

## Returns

[ITraversal&lt;TBaseNode&gt;](MrKWatkins.Ast.Traversal.ITraversal-1.md)

The traversal to use.
