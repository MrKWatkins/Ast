# Properties Class
## Definition

A collection of properties for a node. Properties allow you to store arbitrary data against a node and will be copying during calls to [Copy()](MrKWatkins.Ast.PropertyNode-1.Copy.md#mrkwatkins-ast-propertynode-1-copy). Properties can have a single value or multiple values.

```c#
public sealed class Properties
```

## Properties

| Name | Description |
| ---- | ----------- |
| [Count](MrKWatkins.Ast.Properties.Count.md) | The number of properties in the collection. |

## Methods

| Name | Description |
| ---- | ----------- |
| [AddRangeToMultiple(String, IEnumerable&lt;T&gt;)](MrKWatkins.Ast.Properties.AddRangeToMultiple.md) | Adds values to a multiple valued property with the specified key. |
| [AddToMultiple(String, T)](MrKWatkins.Ast.Properties.AddToMultiple.md) | Adds a value to a multiple valued property with the specified key. |
| [ContainsKey(String)](MrKWatkins.Ast.Properties.ContainsKey.md) | Tests whether a property exists with the specified key. |
| [GetMultiple(String)](MrKWatkins.Ast.Properties.GetMultiple.md) | Gets the values of a multiple valued property with the specified key. |
| [GetOrAdd(String, Func&lt;String, T&gt;)](MrKWatkins.Ast.Properties.GetOrAdd.md) | Gets the value of a single valued property with the specified key or returns a default value if the property does not exist. |
| [GetOrDefault(String, T)](MrKWatkins.Ast.Properties.GetOrDefault.md) | Gets the value of a single valued property with the specified key or returns a default value if the property does not exist. |
| [GetOrThrow(String)](MrKWatkins.Ast.Properties.GetOrThrow.md#mrkwatkins-ast-properties-getorthrow-1(system-string)) | Gets the value of a single valued property with the specified key or throws an exception if the property does not exist. |
| [GetOrThrow(String, T)](MrKWatkins.Ast.Properties.GetOrThrow.md#mrkwatkins-ast-properties-getorthrow-1(system-string-0@)) | Gets the value of a single valued property with the specified key or throws an exception if the property does not exist. Uses a field to cache the value for better performance. |
| [GetOrThrow(String, T?)](MrKWatkins.Ast.Properties.GetOrThrow.md#mrkwatkins-ast-properties-getorthrow-1(system-string-system-nullable((-0))@)) | Gets the value of a single valued property with the specified key or throws an exception if the property does not exist. Uses a field to cache the value for better performance. |
| [GetOrThrow(String, Func&lt;Exception&gt;)](MrKWatkins.Ast.Properties.GetOrThrow.md#mrkwatkins-ast-properties-getorthrow-1(system-string-system-func((system-exception)))) | Gets the value of a single valued property with the specified key or throws an exception if the property does not exist. |
| [GetOrThrow(String, T, Func&lt;Exception&gt;)](MrKWatkins.Ast.Properties.GetOrThrow.md#mrkwatkins-ast-properties-getorthrow-1(system-string-0@-system-func((system-exception)))) | Gets the value of a single valued property with the specified key or throws an exception if the property does not exist. Uses a field to cache the value for better performance. |
| [GetOrThrow(String, T?, Func&lt;Exception&gt;)](MrKWatkins.Ast.Properties.GetOrThrow.md#mrkwatkins-ast-properties-getorthrow-1(system-string-system-nullable((-0))@-system-func((system-exception)))) | Gets the value of a single valued property with the specified key or throws an exception if the property does not exist. Uses a field to cache the value for better performance. |
| [GetOrThrow(String, Func&lt;String, Exception&gt;)](MrKWatkins.Ast.Properties.GetOrThrow.md#mrkwatkins-ast-properties-getorthrow-1(system-string-system-func((system-string-system-exception)))) | Gets the value of a single valued property with the specified key or throws an exception if the property does not exist. |
| [GetOrThrow(String, T, Func&lt;String, Exception&gt;)](MrKWatkins.Ast.Properties.GetOrThrow.md#mrkwatkins-ast-properties-getorthrow-1(system-string-0@-system-func((system-string-system-exception)))) | Gets the value of a single valued property with the specified key or throws an exception if the property does not exist. Uses a field to cache the value for better performance. |
| [GetOrThrow(String, T?, Func&lt;String, Exception&gt;)](MrKWatkins.Ast.Properties.GetOrThrow.md#mrkwatkins-ast-properties-getorthrow-1(system-string-system-nullable((-0))@-system-func((system-string-system-exception)))) | Gets the value of a single valued property with the specified key or throws an exception if the property does not exist. Uses a field to cache the value for better performance. |
| [Set(String, T)](MrKWatkins.Ast.Properties.Set.md#mrkwatkins-ast-properties-set-1(system-string-0)) | Sets the value of a single valued property with the specified key. |
| [Set(String, T, T)](MrKWatkins.Ast.Properties.Set.md#mrkwatkins-ast-properties-set-1(system-string-0-0@)) | Sets the value of a single valued property with the specified key. Uses a field to cache the value for better performance. |
| [Set(String, T, T?)](MrKWatkins.Ast.Properties.Set.md#mrkwatkins-ast-properties-set-1(system-string-0-system-nullable((-0))@)) | Sets the value of a single valued property with the specified key. Uses a field to cache the value for better performance. |
| [SetMultiple(String, IEnumerable&lt;T&gt;)](MrKWatkins.Ast.Properties.SetMultiple.md) | Sets the values of a multiple valued property with the specified key. Any existing values are replaced. |
| [TryAddToMultiple(String, T, IEqualityComparer&lt;T&gt;)](MrKWatkins.Ast.Properties.TryAddToMultiple.md) | Tries to add a value to a multiple valued property with the specified key. If the value already exists in the multiple then it is not added. |
| [TryGet(String, T)](MrKWatkins.Ast.Properties.TryGet.md) | Tries to get the value of a single valued property with the specified key. |

