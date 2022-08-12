using System.Collections;
using NotNull = System.Diagnostics.CodeAnalysis.NotNullAttribute;

namespace MrKWatkins.Ast;

public sealed class NodeProperties
{
    private readonly Dictionary<string, object> properties = new();

    [Pure]
    public T GetOrThrow<T>(string key)
        where T : notnull
    {
        return GetOrThrow<T>(key, () => new KeyNotFoundException($"No value for property with key \"{key}\"."));
    }
        
    [Pure]
    public T GetOrThrow<T>(string key, [InstantHandle] Func<Exception> exceptionCreator)
        where T : notnull
    {
        if (TryGet<T>(key, out var value))
        {
            return value;
        }
        throw exceptionCreator();
    }

    [MustUseReturnValue]
    public T GetOrAdd<T>(string key, [InstantHandle] Func<T> creator)
        where T : notnull
    {
        if (TryGet<T>(key, out var value))
        {
            return value;
        }

        value = creator();
        Set(key, value);
        return value;
    }

    [Pure]
    [return: NotNullIfNotNull("default")]
    public T? GetOrDefault<T>(string key, T? @default = default)
        where T : notnull
    {
        return TryGet<T>(key, out var value) ? value : @default;
    }

    [Pure]
    public bool TryGet<T>(string key, [NotNullWhen(true)] [MaybeNullWhen(false)] out T value)
        where T : notnull
    {
        if (properties.TryGetValue(key, out var objectValue))
        {
            value = Convert<T>(key, objectValue);
            return true;
        }

        value = default;
        return false;
    }

    [Pure]
    public bool Has(string key) => properties.ContainsKey(key);

    // TODO: Type check existing on set.
    public void Set<T>(string key, [NotNull] T value)
        where T : notnull
    {
        properties[key] = value ?? throw new ArgumentNullException(nameof(value));
    }

    [Pure]
    public IReadOnlyList<T> GetMultiple<T>(string key)
        where T : notnull
    {
        if (TryGet<List<T>>(key, out var value))
        {
            return value;
        }

        return Array.Empty<T>();
    }
        
    public void SetMultiple<T>(string key, [InstantHandle] IEnumerable<T> values)
        where T : notnull
    {
        properties[key] = values.ToList();
    }

    // TODO: Remove.
    // TODO: Type check existing.
    public void AddToMultiple<T>(string key, T value)
        where T : notnull
    {
        if (!TryGet<List<T>>(key, out var list))
        {
            list = new List<T>();
            properties.Add(key, list);
        }

        list.Add(value);
    }
        
    public void RemoveFromMultiple<T>(string key, T value)
        where T : notnull
    {
        if (TryGet<List<T>>(key, out var list))
        {
            list.Remove(value);
        }
    }
        
    public void RemoveFromMultiple<T>(string key, IEnumerable<T> values)
        where T : notnull
    {
        if (TryGet<List<T>>(key, out var list))
        {
            foreach (var value in values)
            {
                list.Remove(value);
            }
        }
    }
        
    public void AddAllToMultiple<T>(string key, IEnumerable<T> values)
        where T : notnull
    {
        foreach (var value in values)
        {
            AddToMultiple(key, value);
        }
    }
        
    public void AddToMultipleIfMissing<T>(string key, T value)
        where T : notnull
    {
        if (!TryGet<List<T>>(key, out var list))
        {
            list = new List<T>();
            properties.Add(key, list);
        }

        if (!list.Contains(value))
        {
            list.Add(value);
        }
    }
        
    public void AddAllToMultipleIfMissing<T>(string key, IEnumerable<T> values)
        where T : notnull
    {
        foreach (var value in values)
        {
            AddToMultipleIfMissing(key, value);
        }
    }

    [return: NotNull]
    private static T Convert<T>(string key, object value)
        where T : notnull
    {
        if (value is T typedValue)
        {
            return typedValue;
        }

        throw new InvalidOperationException($"Property with key \"{key}\" has a value of type {value.GetType().FullName} but a type of {typeof(T).FullName} was expected.");
    }

    [Pure]
    public NodeProperties Copy()
    {
        var copy = new NodeProperties();
        foreach (var (key, value) in properties)
        {
            var type = value.GetType();
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>))
            {
                foreach (var multipleValue in (IEnumerable) value)
                {
                    copy.AddToMultiple(key, multipleValue);
                }
            }
            else
            {
                copy.Set(key, value);
            }
        }

        return copy;
    }
}