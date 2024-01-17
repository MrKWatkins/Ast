# Properties.GetOrDefault Method
## Definition

Gets the value of a single valued property with the specified key or returns a default value if the property does not exist.

```c#
public T GetOrDefault<T>(string key, T @default = null)
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
| default | T | The value to return if the property does not exist. |

## Returns

T

The value of the property.
