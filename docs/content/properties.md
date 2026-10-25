# Properties

[`PropertyNode<TNode>`](api/MrKWatkins.Ast/PropertyNode-TNode/index.md) extends [`Node<TNode>`](api/MrKWatkins.Ast/Node-TNode/index.md) with a [`Properties`](api/MrKWatkins.Ast/Properties/index.md) collection: a keyed store of arbitrary values held against the node. Storing state there rather than in fields costs a dictionary lookup per access, but buys three things the library can then do generically — nodes can be [copied](#copying-nodes), compared by value, and enumerated without your node types having to implement anything.

## Declaring Properties

Properties are a storage mechanism, not a way of accessing state. Keep the usual C# property on your node type and back it with the collection:

```c#
public abstract class Expression : PropertyNode<Expression>
{
}

public sealed class Constant : Expression
{
    public Constant(int value)
    {
        Value = value;
    }

    public int Value
    {
        get => Properties.GetOrThrow<int>(nameof(Value));
        init => Properties.Set(nameof(Value), value);
    }
}
```

[`GetOrThrow`](api/MrKWatkins.Ast/Properties/GetOrThrow.md) throws a `KeyNotFoundException` if the property has not been set, which is usually what you want for a value the node cannot be valid without. Overloads take a function to create the exception yourself, so a missing property can be reported in terms of your own domain rather than as a dictionary miss.

For optional state use [`GetOrDefault`](api/MrKWatkins.Ast/Properties/GetOrDefault.md), [`TryGet`](api/MrKWatkins.Ast/Properties/TryGet.md) or [`GetOrAdd`](api/MrKWatkins.Ast/Properties/GetOrAdd.md), the last of which computes and stores a value on first access:

```c#
public bool IsReachable
{
    get => Properties.GetOrDefault<bool>(nameof(IsReachable));
    set => Properties.Set(nameof(IsReachable), value);
}
```

Properties are typed. Reading a property with a different type to the one it was set with throws an `InvalidOperationException` rather than silently returning a default.

## Caching

The dictionary lookup can matter for properties read in a tight loop. The `ref`/`out` overloads of [`GetOrThrow`](api/MrKWatkins.Ast/Properties/GetOrThrow.md) and [`Set`](api/MrKWatkins.Ast/Properties/Set.md) keep a field in sync with the collection so subsequent reads come straight from the field:

```c#
private int? cachedValue;

public int Value
{
    get => Properties.GetOrThrow(nameof(Value), ref cachedValue);
    init => Properties.Set(nameof(Value), value, out cachedValue);
}
```

Always pair the two; setting the property without the `out` overload will leave a stale value in the cache field.

## Multiple Values

A property can hold a list of values instead of a single one. Use [`GetMultiple`](api/MrKWatkins.Ast/Properties/GetMultiple.md) to read, and [`SetMultiple`](api/MrKWatkins.Ast/Properties/SetMultiple.md), [`AddToMultiple`](api/MrKWatkins.Ast/Properties/AddToMultiple.md), [`AddRangeToMultiple`](api/MrKWatkins.Ast/Properties/AddRangeToMultiple.md) or [`TryAddToMultiple`](api/MrKWatkins.Ast/Properties/TryAddToMultiple.md) to write. [`GetMultiple`](api/MrKWatkins.Ast/Properties/GetMultiple.md) returns an empty list for a property that has never been set, so there is no need to initialise one:

```c#
public IReadOnlyList<Attribute> Attributes => Properties.GetMultiple<Attribute>(nameof(Attributes));

public void AddAttribute(Attribute attribute) => Properties.AddToMultiple(nameof(Attributes), attribute);
```

[`TryAddToMultiple`](api/MrKWatkins.Ast/Properties/TryAddToMultiple.md) will not add a value that is already present, optionally using an `IEqualityComparer<T>` of your choosing.

Single and multiple valued properties are distinct; reading a multiple valued property as a single value, or vice versa, throws an `InvalidOperationException`.

## Copying Nodes

[`Copy`](api/MrKWatkins.Ast/PropertyNode-TNode/Copy.md) deep copies a node, its properties and all of its descendants:

```c#
var copy = expression.Copy();
```

Nodes are created by an [`INodeFactory<TNode>`](api/MrKWatkins.Ast/INodeFactory-TNode/index.md), defaulting to [`DefaultNodeFactory<TNode>`](api/MrKWatkins.Ast/DefaultNodeFactory-TNode/index.md), which needs a parameterless constructor on each node type — it may be non-public. Pass your own factory to [`Copy`](api/MrKWatkins.Ast/PropertyNode-TNode/Copy.md) if your nodes cannot be built that way.

Note that the [`SourcePosition`](api/MrKWatkins.Ast/Node-TNode/SourcePosition.md) and [`Messages`](api/MrKWatkins.Ast/Node-TNode/Messages.md) of a node are *not* copied. Copying is designed for reproducing a piece of tree or a general pattern, and the copy did not come from the original position in the source, so neither the position nor any messages raised against the original apply to it.

Property values themselves are copied by reference. Multiple valued properties get a new list, but the values in it are the same instances. Mutable values shared between a node and its copy will be seen by both.

## Comparing Nodes

[`PropertyNodeComparer<TNode>`](api/MrKWatkins.Ast/PropertyNodeComparer-TNode/index.md) is an `IEqualityComparer<TNode>` that considers two nodes equal if they have the same type and equal properties. Children, source positions and messages are not compared, so it answers "are these two nodes the same shape of thing?" rather than "are these two subtrees identical?".

```c#
var same = PropertyNodeComparer<Expression>.Instance.Equals(first, second);
```

Properties are equal if they have the same keys, each key is single or multiple valued to match, the values have the same type, and the values are equal — by `Equals` for single values and element-wise for multiple values.

## Enumerating Properties

[`EnumerateProperties`](api/MrKWatkins.Ast/PropertyNode-TNode/EnumerateProperties.md) returns the properties as untyped `KeyValuePair<string, object>`s, with a `List<T>` as the value for multiple valued properties. This is intended for debugging output, diagnostics and serialisation rather than for reading state during normal processing.
