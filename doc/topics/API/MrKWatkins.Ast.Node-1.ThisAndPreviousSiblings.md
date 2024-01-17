# Node&lt;TNode&gt;.ThisAndPreviousSiblings Property
## Definition

Lazily enumerates over this node then the previous siblings, i.e. the children from the same [Parent](MrKWatkins.Ast.Node-1.Parent.md) at precedent positional indices in descending index order.

```c#
public IEnumerable<TNode> ThisAndPreviousSiblings { get; }
```

## Property Value

[IEnumerable&lt;TNode&gt;](https://learn.microsoft.com/en-gb/dotnet/api/System.Collections.Generic.IEnumerable-1)
