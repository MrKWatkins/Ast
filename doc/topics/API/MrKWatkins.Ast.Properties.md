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
| [AddRangeToMultiple](MrKWatkins.Ast.Properties.AddRangeToMultiple.md) | Adds values to a multiple valued property with the specified key. |
| [AddToMultiple](MrKWatkins.Ast.Properties.AddToMultiple.md) | Adds a value to a multiple valued property with the specified key. |
| [ContainsKey](MrKWatkins.Ast.Properties.ContainsKey.md) | Tests whether a property exists with the specified key. |
| [GetMultiple](MrKWatkins.Ast.Properties.GetMultiple.md) | Gets the values of a multiple valued property with the specified key. |
| [GetOrAdd](MrKWatkins.Ast.Properties.GetOrAdd.md) | Gets the value of a single valued property with the specified key or returns a default value if the property does not exist. |
| [GetOrDefault](MrKWatkins.Ast.Properties.GetOrDefault.md) | Gets the value of a single valued property with the specified key or returns a default value if the property does not exist. |
| [GetOrThrow](MrKWatkins.Ast.Properties.GetOrThrow.md#mrkwatkins-ast-properties-getorthrow-1(system-string)) | Gets the value of a single valued property with the specified key or throws an exception if the property does not exist. |
| [GetOrThrow](MrKWatkins.Ast.Properties.GetOrThrow.md#mrkwatkins-ast-properties-getorthrow-1(system-string-0@)) | Gets the value of a single valued property with the specified key or throws an exception if the property does not exist. Uses a field to cache the value for better performance. |
| [GetOrThrow](MrKWatkins.Ast.Properties.GetOrThrow.md#mrkwatkins-ast-properties-getorthrow-1(system-string-system-nullable((-0))@)) | Gets the value of a single valued property with the specified key or throws an exception if the property does not exist. Uses a field to cache the value for better performance. |
| [GetOrThrow](MrKWatkins.Ast.Properties.GetOrThrow.md#mrkwatkins-ast-properties-getorthrow-1(system-string-system-func((system-exception)))) | Gets the value of a single valued property with the specified key or throws an exception if the property does not exist. |
| [GetOrThrow](MrKWatkins.Ast.Properties.GetOrThrow.md#mrkwatkins-ast-properties-getorthrow-1(system-string-0@-system-func((system-exception)))) | Gets the value of a single valued property with the specified key or throws an exception if the property does not exist. Uses a field to cache the value for better performance. |
| [GetOrThrow](MrKWatkins.Ast.Properties.GetOrThrow.md#mrkwatkins-ast-properties-getorthrow-1(system-string-system-nullable((-0))@-system-func((system-exception)))) | Gets the value of a single valued property with the specified key or throws an exception if the property does not exist. Uses a field to cache the value for better performance. |
| [GetOrThrow](MrKWatkins.Ast.Properties.GetOrThrow.md#mrkwatkins-ast-properties-getorthrow-1(system-string-system-func((system-string-system-exception)))) | Gets the value of a single valued property with the specified key or throws an exception if the property does not exist. |
| [GetOrThrow](MrKWatkins.Ast.Properties.GetOrThrow.md#mrkwatkins-ast-properties-getorthrow-1(system-string-0@-system-func((system-string-system-exception)))) | Gets the value of a single valued property with the specified key or throws an exception if the property does not exist. Uses a field to cache the value for better performance. |
| [GetOrThrow](MrKWatkins.Ast.Properties.GetOrThrow.md#mrkwatkins-ast-properties-getorthrow-1(system-string-system-nullable((-0))@-system-func((system-string-system-exception)))) | Gets the value of a single valued property with the specified key or throws an exception if the property does not exist. Uses a field to cache the value for better performance. |
| [Set](MrKWatkins.Ast.Properties.Set.md#mrkwatkins-ast-properties-set-1(system-string-0)) | Sets the value of a single valued property with the specified key. |
| [Set](MrKWatkins.Ast.Properties.Set.md#mrkwatkins-ast-properties-set-1(system-string-0-0@)) | Sets the value of a single valued property with the specified key. Uses a field to cache the value for better performance. |
| [Set](MrKWatkins.Ast.Properties.Set.md#mrkwatkins-ast-properties-set-1(system-string-0-system-nullable((-0))@)) | Sets the value of a single valued property with the specified key. Uses a field to cache the value for better performance. |
| [SetMultiple](MrKWatkins.Ast.Properties.SetMultiple.md) | Sets the values of a multiple valued property with the specified key. Any existing values are replaced. |
| [TryAddToMultiple](MrKWatkins.Ast.Properties.TryAddToMultiple.md) | Tries to add a value to a multiple valued property with the specified key. If the value already exists in the multiple then it is not added. |
| [TryGet](MrKWatkins.Ast.Properties.TryGet.md) | Tries to get the value of a single valued property with the specified key. |

