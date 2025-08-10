# OrderedProcessor&lt;TBaseNode&gt;.ShouldProcessDescendents Method
## Definition

Whether descendents of this node should be processed by the [Pipeline&lt;TBaseNode&gt;](MrKWatkins.Ast.Processing.Pipeline-1.md) or not. Defaults to `true`.

```c#
public virtual bool ShouldProcessDescendents(TBaseNode node);
```

## Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| node | TBaseNode | The node. |

## Returns

[Boolean](https://learn.microsoft.com/en-gb/dotnet/api/System.Boolean)

`true` if descendents of `node` should be processed, `false` otherwise.
