# Properties.TryGet Method
## Definition

Tries to get the value of a single valued property with the specified key.

```c#
public bool TryGet<T>(string key, out T value)
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
| value | T | The value of the property if it exists. |

## Returns

[Boolean](https://learn.microsoft.com/en-gb/dotnet/api/System.Boolean)

`true` if the property exists, `false` otherwise.
