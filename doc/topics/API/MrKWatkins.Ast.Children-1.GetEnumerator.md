# Children&lt;TNode&gt;.GetEnumerator Method
## Definition

```c#
public IEnumerator<TNode> GetEnumerator();
```

## Returns

[IEnumerator&lt;TNode&gt;](https://learn.microsoft.com/en-gb/dotnet/api/System.Collections.Generic.IEnumerator-1)
## Remarks

Enumerating over children tries to accommodate changes to the collection whilst enumerating. However not all changes can be accommodated and an [InvalidOperationException](https://learn.microsoft.com/en-gb/dotnet/api/System.InvalidOperationException) will be thrown if enumeration cannot continue.
