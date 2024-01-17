# Node&lt;TNode&gt;.HasParent Property
## Definition

Returns `true` if this node has a [Parent](MrKWatkins.Ast.Node-1.Parent.md), `false` otherwise. Nodes will not have parents if they are the root node, or they have just been constructed and not yet added to a parent.

```c#
public bool HasParent { get; }
```

## Property Value

[Boolean](https://learn.microsoft.com/en-gb/dotnet/api/System.Boolean)
