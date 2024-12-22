# Children&lt;TNode&gt;.UnsafeSlice Method
## Overloads

| Name | Description |
| ---- | ----------- |
| [UnsafeSlice(Int32, Int32)](MrKWatkins.Ast.Children-1.UnsafeSlice.md#mrkwatkins-ast-children-1-unsafeslice(system-int32-system-int32)) | Returns a [ReadOnlySpan&lt;T&gt;](https://learn.microsoft.com/en-gb/dotnet/api/System.ReadOnlySpan-1) of a range of nodes in the source [Children&lt;TNode&gt;](MrKWatkins.Ast.Children-1.md). Nodes should not be added or removed from the [Children&lt;TNode&gt;](MrKWatkins.Ast.Children-1.md) while the [ReadOnlySpan&lt;T&gt;](https://learn.microsoft.com/en-gb/dotnet/api/System.ReadOnlySpan-1) is in use. |
| [UnsafeSlice(Range)](MrKWatkins.Ast.Children-1.UnsafeSlice.md#mrkwatkins-ast-children-1-unsafeslice(system-range)) | Returns a [ReadOnlySpan&lt;T&gt;](https://learn.microsoft.com/en-gb/dotnet/api/System.ReadOnlySpan-1) of a range of nodes in the source [Children&lt;TNode&gt;](MrKWatkins.Ast.Children-1.md). Nodes should not be added or removed from the [Children&lt;TNode&gt;](MrKWatkins.Ast.Children-1.md) while the [ReadOnlySpan&lt;T&gt;](https://learn.microsoft.com/en-gb/dotnet/api/System.ReadOnlySpan-1) is in use. |

## UnsafeSlice(Int32, Int32) {id="mrkwatkins-ast-children-1-unsafeslice(system-int32-system-int32)"}

Returns a [ReadOnlySpan&lt;T&gt;](https://learn.microsoft.com/en-gb/dotnet/api/System.ReadOnlySpan-1) of a range of nodes in the source [Children&lt;TNode&gt;](MrKWatkins.Ast.Children-1.md). Nodes should not be added or removed from the [Children&lt;TNode&gt;](MrKWatkins.Ast.Children-1.md) while the [ReadOnlySpan&lt;T&gt;](https://learn.microsoft.com/en-gb/dotnet/api/System.ReadOnlySpan-1) is in use.

```c#
public ReadOnlySpan<TNode> UnsafeSlice(int index, int count);
```

## Parameters {id="parameters-mrkwatkins-ast-children-1-unsafeslice(system-int32-system-int32)"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| index | [Int32](https://learn.microsoft.com/en-gb/dotnet/api/System.Int32) | The zero-based index at which the range starts. |
| count | [Int32](https://learn.microsoft.com/en-gb/dotnet/api/System.Int32) | The number of nodes in the range. |

## Returns {id="returns-mrkwatkins-ast-children-1-unsafeslice(system-int32-system-int32)"}

[ReadOnlySpan&lt;TNode&gt;](https://learn.microsoft.com/en-gb/dotnet/api/System.ReadOnlySpan-1)

A [ReadOnlySpan&lt;T&gt;](https://learn.microsoft.com/en-gb/dotnet/api/System.ReadOnlySpan-1) of a range of nodes in the source [Children&lt;TNode&gt;](MrKWatkins.Ast.Children-1.md).
## UnsafeSlice(Range) {id="mrkwatkins-ast-children-1-unsafeslice(system-range)"}

Returns a [ReadOnlySpan&lt;T&gt;](https://learn.microsoft.com/en-gb/dotnet/api/System.ReadOnlySpan-1) of a range of nodes in the source [Children&lt;TNode&gt;](MrKWatkins.Ast.Children-1.md). Nodes should not be added or removed from the [Children&lt;TNode&gt;](MrKWatkins.Ast.Children-1.md) while the [ReadOnlySpan&lt;T&gt;](https://learn.microsoft.com/en-gb/dotnet/api/System.ReadOnlySpan-1) is in use.

```c#
public ReadOnlySpan<TNode> UnsafeSlice(Range range);
```

## Parameters {id="parameters-mrkwatkins-ast-children-1-unsafeslice(system-range)"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| range | [Range](https://learn.microsoft.com/en-gb/dotnet/api/System.Range) | The [Range](https://learn.microsoft.com/en-gb/dotnet/api/System.Range). |

## Returns {id="returns-mrkwatkins-ast-children-1-unsafeslice(system-range)"}

[ReadOnlySpan&lt;TNode&gt;](https://learn.microsoft.com/en-gb/dotnet/api/System.ReadOnlySpan-1)

A [ReadOnlySpan&lt;T&gt;](https://learn.microsoft.com/en-gb/dotnet/api/System.ReadOnlySpan-1) of a range of nodes in the source [Children&lt;TNode&gt;](MrKWatkins.Ast.Children-1.md).
