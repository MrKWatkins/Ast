# Node&lt;TNode&gt;.ThisAndAncestorsOfType Method
## Definition

Lazily enumerates over the [Ancestors](MrKWatkins.Ast.Node-1.Ancestors.md) of this node, returning only ancestors of the specified type.

```c#
public IEnumerable<TAncestor> ThisAndAncestorsOfType<TAncestor>()
   where TAncestor : TNode;
```

### Type Parameters

| Name | Description |
| ---- | ----------- |
| TAncestor | The type of ancestors to return. |

## Returns

[IEnumerable&lt;TAncestor&gt;](https://learn.microsoft.com/en-gb/dotnet/api/System.Collections.Generic.IEnumerable-1)
