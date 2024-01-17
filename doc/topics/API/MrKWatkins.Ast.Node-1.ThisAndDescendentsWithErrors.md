# Node&lt;TNode&gt;.ThisAndDescendentsWithErrors Property
## Definition

Lazily enumerates over this node and its descendents returning only those that have [Messages](MrKWatkins.Ast.Message.md) with [Level](MrKWatkins.Ast.Message.Level.md) [Error](MrKWatkins.Ast.MessageLevel.Error.md).

```c#
public IEnumerable<TNode> ThisAndDescendentsWithErrors { get; }
```

## Property Value

[IEnumerable&lt;TNode&gt;](https://learn.microsoft.com/en-gb/dotnet/api/System.Collections.Generic.IEnumerable-1)
