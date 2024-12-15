# Properties.GetEnumerator Method
## Definition

Returns an enumerator that enumerates over the properties. Returns the name as the key and an untyped object for the value. This will be the object itself for single value properties and a [List&lt;T&gt;](https://learn.microsoft.com/en-gb/dotnet/api/System.Collections.Generic.List-1) of objects for multiple value properties.

```c#
public IEnumerator<KeyValuePair<string, object>> GetEnumerator();
```

## Returns

[IEnumerator&lt;KeyValuePair&lt;String, Object&gt;&gt;](https://learn.microsoft.com/en-gb/dotnet/api/System.Collections.Generic.IEnumerator-1)

An enumerator.
