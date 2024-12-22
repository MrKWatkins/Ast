# Children&lt;TNode&gt;.EnumerateSlice Method
## Overloads

| Name | Description |
| ---- | ----------- |
| [EnumerateSlice(Int32, Int32)](MrKWatkins.Ast.Children-1.EnumerateSlice.md#mrkwatkins-ast-children-1-enumerateslice(system-int32-system-int32)) | Returns a lazy enumeration of a range of nodes in the source [Children&lt;TNode&gt;](MrKWatkins.Ast.Children-1.md). |
| [EnumerateSlice(Range)](MrKWatkins.Ast.Children-1.EnumerateSlice.md#mrkwatkins-ast-children-1-enumerateslice(system-range)) | Returns a lazy enumeration of a range of nodes in the source [Children&lt;TNode&gt;](MrKWatkins.Ast.Children-1.md). |

## EnumerateSlice(Int32, Int32) {id="mrkwatkins-ast-children-1-enumerateslice(system-int32-system-int32)"}

Returns a lazy enumeration of a range of nodes in the source [Children&lt;TNode&gt;](MrKWatkins.Ast.Children-1.md).

```c#
public IEnumerable<TNode> EnumerateSlice(int index, int count);
```

## Parameters {id="parameters-mrkwatkins-ast-children-1-enumerateslice(system-int32-system-int32)"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| index | [Int32](https://learn.microsoft.com/en-gb/dotnet/api/System.Int32) | The zero-based index at which the range starts. |
| count | [Int32](https://learn.microsoft.com/en-gb/dotnet/api/System.Int32) | The number of nodes in the range. |

## Returns {id="returns-mrkwatkins-ast-children-1-enumerateslice(system-int32-system-int32)"}

[IEnumerable&lt;TNode&gt;](https://learn.microsoft.com/en-gb/dotnet/api/System.Collections.Generic.IEnumerable-1)

A lazy enumeration of a range of nodes in the source [Children&lt;TNode&gt;](MrKWatkins.Ast.Children-1.md).
## EnumerateSlice(Range) {id="mrkwatkins-ast-children-1-enumerateslice(system-range)"}

Returns a lazy enumeration of a range of nodes in the source [Children&lt;TNode&gt;](MrKWatkins.Ast.Children-1.md).

```c#
public IEnumerable<TNode> EnumerateSlice(Range range);
```

## Parameters {id="parameters-mrkwatkins-ast-children-1-enumerateslice(system-range)"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| range | [Range](https://learn.microsoft.com/en-gb/dotnet/api/System.Range) | The [Range](https://learn.microsoft.com/en-gb/dotnet/api/System.Range). |

## Returns {id="returns-mrkwatkins-ast-children-1-enumerateslice(system-range)"}

[IEnumerable&lt;TNode&gt;](https://learn.microsoft.com/en-gb/dotnet/api/System.Collections.Generic.IEnumerable-1)

A lazy enumeration of a range of nodes in the source [Children&lt;TNode&gt;](MrKWatkins.Ast.Children-1.md).
