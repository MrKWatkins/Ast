# Node&lt;TNode&gt;.ThisAndAncestors Property
## Definition

Lazily enumerates over this node and its [Ancestors](MrKWatkins.Ast.Node-1.Ancestors.md), i.e. this node, the [Parent](MrKWatkins.Ast.Node-1.Parent.md), grandparent, great-grandparent and so on up to the root node.

```c#
public IEnumerable<TNode> ThisAndAncestors { get; }
```

## Property Value

[IEnumerable&lt;TNode&gt;](https://learn.microsoft.com/en-gb/dotnet/api/System.Collections.Generic.IEnumerable-1)
