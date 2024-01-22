# Properties.GetMultiple Method
## Definition

Gets the values of a multiple valued property with the specified key.

```c#
public IReadOnlyList<T> GetMultiple<T>(string key);
```

### Type Parameters

| Name | Description |
| ---- | ----------- |
| T | The type of the property. |

## Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| key | [String](https://learn.microsoft.com/en-gb/dotnet/api/System.String) | The key of the property. |

## Returns

[IReadOnlyList&lt;T&gt;](https://learn.microsoft.com/en-gb/dotnet/api/System.Collections.Generic.IReadOnlyList-1)

The values of the property.
