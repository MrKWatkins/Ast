# Properties.TryAddToMultiple Method
## Definition

Tries to add a value to a multiple valued property with the specified key. If the value already exists in the multiple then it is not added.

```c#
public bool TryAddToMultiple<T>(string key, T value, IEqualityComparer<T> valueComparer = null);
```

### Type Parameters

| Name | Description |
| ---- | ----------- |
| T | The type of the property. |

## Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| key | [String](https://learn.microsoft.com/en-gb/dotnet/api/System.String) | The key of the property. |
| value | T | The value to add to the property. |
| valueComparer | [IEqualityComparer&lt;T&gt;](https://learn.microsoft.com/en-gb/dotnet/api/System.Collections.Generic.IEqualityComparer-1) | An equality comparer to compare values or `null` for the default comparer. |

## Returns

[Boolean](https://learn.microsoft.com/en-gb/dotnet/api/System.Boolean)

`true` if the value was added, `false` otherwise.
