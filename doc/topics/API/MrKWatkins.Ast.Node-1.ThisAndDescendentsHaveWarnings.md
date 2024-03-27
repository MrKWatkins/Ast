# Node&lt;TNode&gt;.ThisAndDescendentsHaveWarnings Property
## Definition

Returns `true` if this node or any of its descendents have any [Messages](MrKWatkins.Ast.Message.md) with [Level](MrKWatkins.Ast.Message.Level.md) [Warning](MrKWatkins.Ast.MessageLevel.md#fields), `false` otherwise.

```c#
public bool ThisAndDescendentsHaveWarnings { get; }
```

## Property Value

[Boolean](https://learn.microsoft.com/en-gb/dotnet/api/System.Boolean)

Whether this node or its descendents have warnings or not.
