# Properties.GetOrThrow Method
## Overloads

| Name | Description |
| ---- | ----------- |
| [GetOrThrow&lt;T&gt;(String)](MrKWatkins.Ast.Properties.GetOrThrow.md#mrkwatkins-ast-properties-getorthrow-1(system-string)) | Gets the value of a single valued property with the specified key or throws an exception if the property does not exist. |
| [GetOrThrow&lt;T&gt;(String, T)](MrKWatkins.Ast.Properties.GetOrThrow.md#mrkwatkins-ast-properties-getorthrow-1(system-string-0@)) | Gets the value of a single valued property with the specified key or throws an exception if the property does not exist. Uses a field to cache the value for better performance. |
| [GetOrThrow&lt;T&gt;(String, T?)](MrKWatkins.Ast.Properties.GetOrThrow.md#mrkwatkins-ast-properties-getorthrow-1(system-string-system-nullable((-0))@)) | Gets the value of a single valued property with the specified key or throws an exception if the property does not exist. Uses a field to cache the value for better performance. |
| [GetOrThrow&lt;T&gt;(String, Func&lt;Exception&gt;)](MrKWatkins.Ast.Properties.GetOrThrow.md#mrkwatkins-ast-properties-getorthrow-1(system-string-system-func((system-exception)))) | Gets the value of a single valued property with the specified key or throws an exception if the property does not exist. |
| [GetOrThrow&lt;T&gt;(String, T, Func&lt;Exception&gt;)](MrKWatkins.Ast.Properties.GetOrThrow.md#mrkwatkins-ast-properties-getorthrow-1(system-string-0@-system-func((system-exception)))) | Gets the value of a single valued property with the specified key or throws an exception if the property does not exist. Uses a field to cache the value for better performance. |
| [GetOrThrow&lt;T&gt;(String, T?, Func&lt;Exception&gt;)](MrKWatkins.Ast.Properties.GetOrThrow.md#mrkwatkins-ast-properties-getorthrow-1(system-string-system-nullable((-0))@-system-func((system-exception)))) | Gets the value of a single valued property with the specified key or throws an exception if the property does not exist. Uses a field to cache the value for better performance. |
| [GetOrThrow&lt;T&gt;(String, Func&lt;String, Exception&gt;)](MrKWatkins.Ast.Properties.GetOrThrow.md#mrkwatkins-ast-properties-getorthrow-1(system-string-system-func((system-string-system-exception)))) | Gets the value of a single valued property with the specified key or throws an exception if the property does not exist. |
| [GetOrThrow&lt;T&gt;(String, T, Func&lt;String, Exception&gt;)](MrKWatkins.Ast.Properties.GetOrThrow.md#mrkwatkins-ast-properties-getorthrow-1(system-string-0@-system-func((system-string-system-exception)))) | Gets the value of a single valued property with the specified key or throws an exception if the property does not exist. Uses a field to cache the value for better performance. |
| [GetOrThrow&lt;T&gt;(String, T?, Func&lt;String, Exception&gt;)](MrKWatkins.Ast.Properties.GetOrThrow.md#mrkwatkins-ast-properties-getorthrow-1(system-string-system-nullable((-0))@-system-func((system-string-system-exception)))) | Gets the value of a single valued property with the specified key or throws an exception if the property does not exist. Uses a field to cache the value for better performance. |

## GetOrThrow&lt;T&gt;(String) {id="mrkwatkins-ast-properties-getorthrow-1(system-string)"}

Gets the value of a single valued property with the specified key or throws an exception if the property does not exist.

```c#
public T GetOrThrow<T>(string key);
```

### Type Parameters {id="type-parameters-mrkwatkins-ast-properties-getorthrow-1(system-string)"}

| Name | Description |
| ---- | ----------- |
| T | The type of the property. |

## Parameters {id="parameters-mrkwatkins-ast-properties-getorthrow-1(system-string)"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| key | [String](https://learn.microsoft.com/en-gb/dotnet/api/System.String) | The key of the property. |

## Returns {id="returns-mrkwatkins-ast-properties-getorthrow-1(system-string)"}

T

The value of the property.
## GetOrThrow&lt;T&gt;(String, T) {id="mrkwatkins-ast-properties-getorthrow-1(system-string-0@)"}

Gets the value of a single valued property with the specified key or throws an exception if the property does not exist. Uses a field to cache the value for better performance.

```c#
public T GetOrThrow<T>(string key, ref T? cached)
   where T : class;
```

### Type Parameters {id="type-parameters-mrkwatkins-ast-properties-getorthrow-1(system-string-0@)"}

| Name | Description |
| ---- | ----------- |
| T | The type of the property. |

## Parameters {id="parameters-mrkwatkins-ast-properties-getorthrow-1(system-string-0@)"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| key | [String](https://learn.microsoft.com/en-gb/dotnet/api/System.String) | The key of the property. |
| cached | T | The key of the property. |

## Returns {id="returns-mrkwatkins-ast-properties-getorthrow-1(system-string-0@)"}

T

The value of the property.
## Remarks {id="remarks-mrkwatkins-ast-properties-getorthrow-1(system-string-0@)"}

Properties are stored in a dictionary which might not have enough performance in some situations. Use this overload to get higher performance as the value will be taken from the field if it exists. Make sure to use [Set&lt;T&gt;(String, T, T)](MrKWatkins.Ast.Properties.Set.md#mrkwatkins-ast-properties-set-1(system-string-0-0@)) to update the cached field.
## GetOrThrow&lt;T&gt;(String, T?) {id="mrkwatkins-ast-properties-getorthrow-1(system-string-system-nullable((-0))@)"}

Gets the value of a single valued property with the specified key or throws an exception if the property does not exist. Uses a field to cache the value for better performance.

```c#
public T GetOrThrow<T>(string key, ref T?? cached)
   where T : struct;
```

### Type Parameters {id="type-parameters-mrkwatkins-ast-properties-getorthrow-1(system-string-system-nullable((-0))@)"}

| Name | Description |
| ---- | ----------- |
| T | The type of the property. |

## Parameters {id="parameters-mrkwatkins-ast-properties-getorthrow-1(system-string-system-nullable((-0))@)"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| key | [String](https://learn.microsoft.com/en-gb/dotnet/api/System.String) | The key of the property. |
| cached | [T?](https://learn.microsoft.com/en-gb/dotnet/api/System.Nullable-1) | The key of the property. |

## Returns {id="returns-mrkwatkins-ast-properties-getorthrow-1(system-string-system-nullable((-0))@)"}

T

The value of the property.
## Remarks {id="remarks-mrkwatkins-ast-properties-getorthrow-1(system-string-system-nullable((-0))@)"}

Properties are stored in a dictionary which might not have enough performance in some situations. Use this overload to get higher performance as the value will be taken from the field if it exists. Make sure to use [Set&lt;T&gt;(String, T, T)](MrKWatkins.Ast.Properties.Set.md#mrkwatkins-ast-properties-set-1(system-string-0-0@)) to update the cached field.
## GetOrThrow&lt;T&gt;(String, Func&lt;Exception&gt;) {id="mrkwatkins-ast-properties-getorthrow-1(system-string-system-func((system-exception)))"}

Gets the value of a single valued property with the specified key or throws an exception if the property does not exist.

```c#
public T GetOrThrow<T>(string key, Func<Exception> exceptionCreator);
```

### Type Parameters {id="type-parameters-mrkwatkins-ast-properties-getorthrow-1(system-string-system-func((system-exception)))"}

| Name | Description |
| ---- | ----------- |
| T | The type of the property. |

## Parameters {id="parameters-mrkwatkins-ast-properties-getorthrow-1(system-string-system-func((system-exception)))"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| key | [String](https://learn.microsoft.com/en-gb/dotnet/api/System.String) | The key of the property. |
| exceptionCreator | [Func&lt;Exception&gt;](https://learn.microsoft.com/en-gb/dotnet/api/System.Func-1) | Function to create an exception to throw if the no property with the specified `key` exists. |

## Returns {id="returns-mrkwatkins-ast-properties-getorthrow-1(system-string-system-func((system-exception)))"}

T

The value of the property.
## GetOrThrow&lt;T&gt;(String, T, Func&lt;Exception&gt;) {id="mrkwatkins-ast-properties-getorthrow-1(system-string-0@-system-func((system-exception)))"}

Gets the value of a single valued property with the specified key or throws an exception if the property does not exist. Uses a field to cache the value for better performance.

```c#
public T GetOrThrow<T>(string key, ref T? cached, Func<Exception> exceptionCreator)
   where T : class;
```

### Type Parameters {id="type-parameters-mrkwatkins-ast-properties-getorthrow-1(system-string-0@-system-func((system-exception)))"}

| Name | Description |
| ---- | ----------- |
| T | The type of the property. |

## Parameters {id="parameters-mrkwatkins-ast-properties-getorthrow-1(system-string-0@-system-func((system-exception)))"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| key | [String](https://learn.microsoft.com/en-gb/dotnet/api/System.String) | The key of the property. |
| cached | T | The key of the property. |
| exceptionCreator | [Func&lt;Exception&gt;](https://learn.microsoft.com/en-gb/dotnet/api/System.Func-1) | Function to create an exception to throw if the no property with the specified `key` exists. |

## Returns {id="returns-mrkwatkins-ast-properties-getorthrow-1(system-string-0@-system-func((system-exception)))"}

T

The value of the property.
## Remarks {id="remarks-mrkwatkins-ast-properties-getorthrow-1(system-string-0@-system-func((system-exception)))"}

Properties are stored in a dictionary which might not have enough performance in some situations. Use this overload to get higher performance as the value will be taken from the field if it exists. Make sure to use [Set&lt;T&gt;(String, T, T)](MrKWatkins.Ast.Properties.Set.md#mrkwatkins-ast-properties-set-1(system-string-0-0@)) to update the cached field.
## GetOrThrow&lt;T&gt;(String, T?, Func&lt;Exception&gt;) {id="mrkwatkins-ast-properties-getorthrow-1(system-string-system-nullable((-0))@-system-func((system-exception)))"}

Gets the value of a single valued property with the specified key or throws an exception if the property does not exist. Uses a field to cache the value for better performance.

```c#
public T GetOrThrow<T>(string key, ref T?? cached, Func<Exception> exceptionCreator)
   where T : struct;
```

### Type Parameters {id="type-parameters-mrkwatkins-ast-properties-getorthrow-1(system-string-system-nullable((-0))@-system-func((system-exception)))"}

| Name | Description |
| ---- | ----------- |
| T | The type of the property. |

## Parameters {id="parameters-mrkwatkins-ast-properties-getorthrow-1(system-string-system-nullable((-0))@-system-func((system-exception)))"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| key | [String](https://learn.microsoft.com/en-gb/dotnet/api/System.String) | The key of the property. |
| cached | [T?](https://learn.microsoft.com/en-gb/dotnet/api/System.Nullable-1) | The key of the property. |
| exceptionCreator | [Func&lt;Exception&gt;](https://learn.microsoft.com/en-gb/dotnet/api/System.Func-1) | Function to create an exception to throw if the no property with the specified `key` exists. |

## Returns {id="returns-mrkwatkins-ast-properties-getorthrow-1(system-string-system-nullable((-0))@-system-func((system-exception)))"}

T

The value of the property.
## Remarks {id="remarks-mrkwatkins-ast-properties-getorthrow-1(system-string-system-nullable((-0))@-system-func((system-exception)))"}

Properties are stored in a dictionary which might not have enough performance in some situations. Use this overload to get higher performance as the value will be taken from the field if it exists. Make sure to use [Set&lt;T&gt;(String, T, T)](MrKWatkins.Ast.Properties.Set.md#mrkwatkins-ast-properties-set-1(system-string-0-0@)) to update the cached field.
## GetOrThrow&lt;T&gt;(String, Func&lt;String, Exception&gt;) {id="mrkwatkins-ast-properties-getorthrow-1(system-string-system-func((system-string-system-exception)))"}

Gets the value of a single valued property with the specified key or throws an exception if the property does not exist.

```c#
public T GetOrThrow<T>(string key, Func<string, Exception> exceptionCreator);
```

### Type Parameters {id="type-parameters-mrkwatkins-ast-properties-getorthrow-1(system-string-system-func((system-string-system-exception)))"}

| Name | Description |
| ---- | ----------- |
| T | The type of the property. |

## Parameters {id="parameters-mrkwatkins-ast-properties-getorthrow-1(system-string-system-func((system-string-system-exception)))"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| key | [String](https://learn.microsoft.com/en-gb/dotnet/api/System.String) | The key of the property. |
| exceptionCreator | [Func&lt;String, Exception&gt;](https://learn.microsoft.com/en-gb/dotnet/api/System.Func-2) | Function to create an exception to throw if the no property with the specified `key` exists. |

## Returns {id="returns-mrkwatkins-ast-properties-getorthrow-1(system-string-system-func((system-string-system-exception)))"}

T

The value of the property.
## GetOrThrow&lt;T&gt;(String, T, Func&lt;String, Exception&gt;) {id="mrkwatkins-ast-properties-getorthrow-1(system-string-0@-system-func((system-string-system-exception)))"}

Gets the value of a single valued property with the specified key or throws an exception if the property does not exist. Uses a field to cache the value for better performance.

```c#
public T GetOrThrow<T>(string key, ref T? cached, Func<string, Exception> exceptionCreator)
   where T : class;
```

### Type Parameters {id="type-parameters-mrkwatkins-ast-properties-getorthrow-1(system-string-0@-system-func((system-string-system-exception)))"}

| Name | Description |
| ---- | ----------- |
| T | The type of the property. |

## Parameters {id="parameters-mrkwatkins-ast-properties-getorthrow-1(system-string-0@-system-func((system-string-system-exception)))"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| key | [String](https://learn.microsoft.com/en-gb/dotnet/api/System.String) | The key of the property. |
| cached | T | The key of the property. |
| exceptionCreator | [Func&lt;String, Exception&gt;](https://learn.microsoft.com/en-gb/dotnet/api/System.Func-2) | Function to create an exception to throw if the no property with the specified `key` exists. |

## Returns {id="returns-mrkwatkins-ast-properties-getorthrow-1(system-string-0@-system-func((system-string-system-exception)))"}

T

The value of the property.
## Remarks {id="remarks-mrkwatkins-ast-properties-getorthrow-1(system-string-0@-system-func((system-string-system-exception)))"}

Properties are stored in a dictionary which might not have enough performance in some situations. Use this overload to get higher performance as the value will be taken from the field if it exists. Make sure to use [Set&lt;T&gt;(String, T, T)](MrKWatkins.Ast.Properties.Set.md#mrkwatkins-ast-properties-set-1(system-string-0-0@)) to update the cached field.
## GetOrThrow&lt;T&gt;(String, T?, Func&lt;String, Exception&gt;) {id="mrkwatkins-ast-properties-getorthrow-1(system-string-system-nullable((-0))@-system-func((system-string-system-exception)))"}

Gets the value of a single valued property with the specified key or throws an exception if the property does not exist. Uses a field to cache the value for better performance.

```c#
public T GetOrThrow<T>(string key, ref T?? cached, Func<string, Exception> exceptionCreator)
   where T : struct;
```

### Type Parameters {id="type-parameters-mrkwatkins-ast-properties-getorthrow-1(system-string-system-nullable((-0))@-system-func((system-string-system-exception)))"}

| Name | Description |
| ---- | ----------- |
| T | The type of the property. |

## Parameters {id="parameters-mrkwatkins-ast-properties-getorthrow-1(system-string-system-nullable((-0))@-system-func((system-string-system-exception)))"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| key | [String](https://learn.microsoft.com/en-gb/dotnet/api/System.String) | The key of the property. |
| cached | [T?](https://learn.microsoft.com/en-gb/dotnet/api/System.Nullable-1) | The key of the property. |
| exceptionCreator | [Func&lt;String, Exception&gt;](https://learn.microsoft.com/en-gb/dotnet/api/System.Func-2) | Function to create an exception to throw if the no property with the specified `key` exists. |

## Returns {id="returns-mrkwatkins-ast-properties-getorthrow-1(system-string-system-nullable((-0))@-system-func((system-string-system-exception)))"}

T

The value of the property.
## Remarks {id="remarks-mrkwatkins-ast-properties-getorthrow-1(system-string-system-nullable((-0))@-system-func((system-string-system-exception)))"}

Properties are stored in a dictionary which might not have enough performance in some situations. Use this overload to get higher performance as the value will be taken from the field if it exists. Make sure to use [Set&lt;T&gt;(String, T, T)](MrKWatkins.Ast.Properties.Set.md#mrkwatkins-ast-properties-set-1(system-string-0-0@)) to update the cached field.
