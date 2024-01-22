# Properties.GetOrAdd Method
## Definition

Gets the value of a single valued property with the specified key or returns a default value if the property does not exist.

```c#
public T GetOrAdd<T>(string key, Func<String, T> defaultCreator);
```

### Type Parameters

| Name | Description |
| ---- | ----------- |
| T | The type of the property. |

## Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| key | [String](https://learn.microsoft.com/en-gb/dotnet/api/System.String) | The key of the property. |
| defaultCreator | [Func&lt;String, T&gt;](https://learn.microsoft.com/en-gb/dotnet/api/System.Func-2) | Function to create the value to return if the property does not exist. |

## Returns

T

The value of the property.
