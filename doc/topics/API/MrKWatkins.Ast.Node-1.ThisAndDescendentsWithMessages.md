# Node&lt;TNode&gt;.ThisAndDescendentsWithMessages Property
## Definition

Lazily enumerates over this node and its descendents returning only those that have [Messages](MrKWatkins.Ast.Message.md).

```c#
public IEnumerable<TNode> ThisAndDescendentsWithMessages { get; }
```

## Property Value

[IEnumerable&lt;TNode&gt;](https://learn.microsoft.com/en-gb/dotnet/api/System.Collections.Generic.IEnumerable-1)

A lazy enumeration of this node if it has messages and any descendents that have messages.
