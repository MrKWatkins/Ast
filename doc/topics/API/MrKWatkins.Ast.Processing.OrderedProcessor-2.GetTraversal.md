# OrderedProcessor&lt;TContext, TBaseNode&gt;.GetTraversal Method
## Definition

Gets the traversal that the [Pipeline&lt;TBaseNode&gt;](MrKWatkins.Ast.Processing.Pipeline-1.md) should use for this processor.

```c#
public virtual ITraversal<TBaseNode> GetTraversal(TContext? context, TBaseNode root);
```

## Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| context | TContext | The processing context. |
| root | TBaseNode | The root of the tree. |

## Returns

[ITraversal&lt;TBaseNode&gt;](MrKWatkins.Ast.Traversal.ITraversal-1.md)

The traversal to use.
