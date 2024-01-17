# Properties.SetMultiple Method
## Definition

Sets the values of a multiple valued property with the specified key. Any existing values are replaced.

```c#
public void SetMultiple<T>(string key, IEnumerable<T> values)
   where T;
```

### Type Parameters

| Name | Description |
| ---- | ----------- |
| T | The type of the property. |

## Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| key | [String](https://learn.microsoft.com/en-gb/dotnet/api/System.String) | The key of the property. |
| values | [IEnumerable&lt;T&gt;](https://learn.microsoft.com/en-gb/dotnet/api/System.Collections.Generic.IEnumerable-1) | The values of the property. |

