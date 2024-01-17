# Node&lt;TNode&gt;.ThisAndNextSiblings Property
## Definition

Lazily enumerates over this node then the next siblings, i.e. the children from the same [Parent](MrKWatkins.Ast.Node-1.Parent.md) at subsequent positional indices in ascending index order.

```c#
public IEnumerable<TNode> ThisAndNextSiblings { get; }
```

## Property Value

[IEnumerable&lt;TNode&gt;](https://learn.microsoft.com/en-gb/dotnet/api/System.Collections.Generic.IEnumerable-1)
