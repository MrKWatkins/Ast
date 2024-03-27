# Node&lt;TNode&gt;.ThisAndDescendentsWithErrors Property
## Definition

Lazily enumerates over this node and its descendents returning only those that have [Messages](MrKWatkins.Ast.Message.md) with [Level](MrKWatkins.Ast.Message.Level.md) [Error](MrKWatkins.Ast.MessageLevel.md#fields).

```c#
public IEnumerable<TNode> ThisAndDescendentsWithErrors { get; }
```

## Property Value

[IEnumerable&lt;TNode&gt;](https://learn.microsoft.com/en-gb/dotnet/api/System.Collections.Generic.IEnumerable-1)

A lazy enumeration of this node if it has errors and any descendents that have errors.
