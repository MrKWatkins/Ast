# Properties.Set Method
## Overloads

| Name | Description |
| ---- | ----------- |
| [Set&lt;T&gt;(String, T)](MrKWatkins.Ast.Properties.Set.md#mrkwatkins-ast-properties-set-1(system-string-0)) | Sets the value of a single valued property with the specified key. |
| [Set&lt;T&gt;(String, T, T)](MrKWatkins.Ast.Properties.Set.md#mrkwatkins-ast-properties-set-1(system-string-0-0@)) | Sets the value of a single valued property with the specified key. Uses a field to cache the value for better performance. |
| [Set&lt;T&gt;(String, T, T?)](MrKWatkins.Ast.Properties.Set.md#mrkwatkins-ast-properties-set-1(system-string-0-system-nullable((-0))@)) | Sets the value of a single valued property with the specified key. Uses a field to cache the value for better performance. |

## Set&lt;T&gt;(String, T) {id="mrkwatkins-ast-properties-set-1(system-string-0)"}

Sets the value of a single valued property with the specified key.

```c#
public void Set<T>(string key, T value);
```

### Type Parameters {id="type-parameters-mrkwatkins-ast-properties-set-1(system-string-0)"}

| Name | Description |
| ---- | ----------- |
| T | The type of the property. |

## Parameters {id="parameters-mrkwatkins-ast-properties-set-1(system-string-0)"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| key | [String](https://learn.microsoft.com/en-gb/dotnet/api/System.String) | The key of the property. |
| value | T | The value of the property. |

## Set&lt;T&gt;(String, T, T) {id="mrkwatkins-ast-properties-set-1(system-string-0-0@)"}

Sets the value of a single valued property with the specified key. Uses a field to cache the value for better performance.

```c#
public void Set<T>(string key, T value, out T? cached)
   where T : class;
```

### Type Parameters {id="type-parameters-mrkwatkins-ast-properties-set-1(system-string-0-0@)"}

| Name | Description |
| ---- | ----------- |
| T | The type of the property. |

## Parameters {id="parameters-mrkwatkins-ast-properties-set-1(system-string-0-0@)"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| key | [String](https://learn.microsoft.com/en-gb/dotnet/api/System.String) | The key of the property. |
| value | T | The value of the property. |
| cached | T | The key of the property. |

## Remarks {id="remarks-mrkwatkins-ast-properties-set-1(system-string-0-0@)"}

Properties are stored in a dictionary which might not have enough performance in some situations. Use this overload to get higher performance as the value will be stored in the field if it exists. Make sure to use [GetOrThrow&lt;T&gt;(String, T)](MrKWatkins.Ast.Properties.GetOrThrow.md#mrkwatkins-ast-properties-getorthrow-1(system-string-0@)) to retrieve the cached field.
## Set&lt;T&gt;(String, T, T?) {id="mrkwatkins-ast-properties-set-1(system-string-0-system-nullable((-0))@)"}

Sets the value of a single valued property with the specified key. Uses a field to cache the value for better performance.

```c#
public void Set<T>(string key, T value, out T?? cached)
   where T : struct;
```

### Type Parameters {id="type-parameters-mrkwatkins-ast-properties-set-1(system-string-0-system-nullable((-0))@)"}

| Name | Description |
| ---- | ----------- |
| T | The type of the property. |

## Parameters {id="parameters-mrkwatkins-ast-properties-set-1(system-string-0-system-nullable((-0))@)"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| key | [String](https://learn.microsoft.com/en-gb/dotnet/api/System.String) | The key of the property. |
| value | T | The value of the property. |
| cached | [T?](https://learn.microsoft.com/en-gb/dotnet/api/System.Nullable-1) | The key of the property. |

## Remarks {id="remarks-mrkwatkins-ast-properties-set-1(system-string-0-system-nullable((-0))@)"}

Properties are stored in a dictionary which might not have enough performance in some situations. Use this overload to get higher performance as the value will be stored in the field if it exists. Make sure to use [GetOrThrow&lt;T&gt;(String, T)](MrKWatkins.Ast.Properties.GetOrThrow.md#mrkwatkins-ast-properties-getorthrow-1(system-string-0@)) to retrieve the cached field.
