# Node&lt;TNode&gt;.AncestorsOfType Method
## Definition

Lazily enumerates over this node and its [Ancestors](MrKWatkins.Ast.Node-1.Ancestors.md), returning only ancestors of the specified type.

```c#
public IEnumerable<TAncestor> AncestorsOfType<TAncestor>()
   where TAncestor : TNode;
```

### Type Parameters

| Name | Description |
| ---- | ----------- |
| TAncestor | The type of ancestors to return. |

## Returns

[IEnumerable&lt;TAncestor&gt;](https://learn.microsoft.com/en-gb/dotnet/api/System.Collections.Generic.IEnumerable-1)
