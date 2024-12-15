# PropertyNode&lt;TNode&gt;.EnumerateProperties Method
## Definition

Returns an [IEnumerable&lt;T&gt;](https://learn.microsoft.com/en-gb/dotnet/api/System.Collections.Generic.IEnumerable-1) that enumerates over the properties as [KeyValuePair&lt;TKey, TValue&gt;](https://learn.microsoft.com/en-gb/dotnet/api/System.Collections.Generic.KeyValuePair-2)s. Returns the name as the key and an untyped object for the value. This will be the object itself for single value properties and a [List&lt;T&gt;](https://learn.microsoft.com/en-gb/dotnet/api/System.Collections.Generic.List-1) of objects for multiple value properties.

```c#
public IEnumerable<KeyValuePair<string, object>> EnumerateProperties();
```

## Returns

[IEnumerable&lt;KeyValuePair&lt;String, Object&gt;&gt;](https://learn.microsoft.com/en-gb/dotnet/api/System.Collections.Generic.IEnumerable-1)

An [IEnumerable&lt;T&gt;](https://learn.microsoft.com/en-gb/dotnet/api/System.Collections.Generic.IEnumerable-1).
