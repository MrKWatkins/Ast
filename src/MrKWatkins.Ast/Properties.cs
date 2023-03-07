using System.Collections.Concurrent;
using System.Linq.Expressions;

namespace MrKWatkins.Ast;

public sealed class Properties
{
    private static readonly ConcurrentDictionary<Type, Func<object, object>> ListCopiers = new();
    private readonly Dictionary<string, Property> properties;

    internal Properties()
        : this(new Dictionary<string, Property>())
    {
    }

    private Properties(Dictionary<string, Property> properties)
    {
        this.properties = properties;
    }

    [Pure]
    public T GetOrThrow<T>(string key)
        where T : notnull =>
        GetOrThrow<T>(key, k => new KeyNotFoundException($"No value for property with key \"{k}\"."));

    [Pure]
    public T GetOrThrow<T>(string key, [InstantHandle] Func<string, Exception> exceptionCreator)
        where T : notnull =>
        TryGet<T>(key, out var value) ? value : throw exceptionCreator(key);

    [Pure]
    [return: NotNullIfNotNull("default")]
    public T? GetOrDefault<T>(string key, T? @default = default)
        where T : notnull =>
        TryGet<T>(key, out var value) ? value : @default;

    [MustUseReturnValue]
    public T GetOrAdd<T>(string key, [InstantHandle] Func<string, T> creator)
        where T : notnull
    {
        if (TryGet<T>(key, out var value))
        {
            return value;
        }

        value = creator(key);
        Set(key, value);
        return value;
    }

    [Pure]
    public bool TryGet<T>(string key, [MaybeNullWhen(false)] out T value)
        where T : notnull
    {
        if (properties.TryGetValue(key, out var property))
        {
            value = VerifySingle<T>(key, property);
            return true;
        }

        value = default;
        return false;
    }

    [Pure]
    public bool ContainsKey(string key) => properties.ContainsKey(key);

    [Pure]
    public int Count => properties.Count;

    public void Set<T>(string key, T value)
        where T : notnull
    {
        if (properties.TryGetValue(key, out var property))
        {
            VerifySingle<T>(key, property);
        }

        properties[key] = new Property(false, typeof(T), value);
    }

    [Pure]
    public IReadOnlyList<T> GetMultiple<T>(string key)
        where T : notnull =>
        properties.TryGetValue(key, out var property) ? VerifyMultiple<T>(key, property) : Array.Empty<T>();

    public void SetMultiple<T>(string key, [InstantHandle] IEnumerable<T> values)
        where T : notnull
    {
        if (properties.TryGetValue(key, out var property))
        {
            VerifyMultiple<T>(key, property);
        }
        
        properties[key] =  new Property(true, typeof(T), values.ToList());
    }

    public void AddToMultiple<T>(string key, T value)
        where T : notnull
    {
        List<T> list;
        if (properties.TryGetValue(key, out var property))
        {
            list = VerifyMultiple<T>(key, property);
        }
        else
        {
            list = new List<T>();
            properties.Add(key, new Property(true, typeof(T), list));
        }

        list.Add(value);
    }
        
    public void AddRangeToMultiple<T>(string key, IEnumerable<T> values)
        where T : notnull
    {
        List<T> list;
        if (properties.TryGetValue(key, out var property))
        {
            list = VerifyMultiple<T>(key, property);
        }
        else
        {
            list = new List<T>();
            properties.Add(key, new Property(true, typeof(T), list));
        }

        list.AddRange(values);
    }

    [Pure]
    public Properties Copy()
    {
        var newProperties = new Dictionary<string, Property>(properties.Count);
        foreach (var (key, property) in properties)
        {
            if (property.Multiple)
            {
                newProperties[key] = new Property(true, property.Type, ListCopiers.GetOrAdd(property.Type, BuildListCopier)(property.Value));
            }
            else
            {
                newProperties[key] = property;
            }
        }

        return new Properties(newProperties);
    }

    [Pure]
    private static Func<object, object> BuildListCopier(Type itemType)
    {
        var parameter = Expression.Parameter(typeof(object), "value");

        var listType = typeof(List<>).MakeGenericType(itemType);
        var list = Expression.Convert(parameter, listType);

        var constructor = listType.GetConstructor(new[] { typeof(IEnumerable<>).MakeGenericType(itemType) })!;
        var newList = Expression.New(constructor, list);

        var lambda = Expression.Lambda<Func<object, object>>(newList, parameter);

        return lambda.Compile();
    }

    private static T VerifySingle<T>(string key, Property property)
    {
        if (property.Multiple)
        {
            throw new InvalidOperationException($"Property \"{key}\" has multiple values.");
        }

        if (property.Type != typeof(T))
        {
            throw new InvalidOperationException($"Property \"{key}\" has a value of type {property.Type.SimpleName()}; cannot change to {typeof(T).SimpleName()}.");
        }

        return (T) property.Value;
    }

    private static List<T> VerifyMultiple<T>(string key, Property property)
    {
        if (!property.Multiple)
        {
            throw new InvalidOperationException($"Property \"{key}\" is a single value.");
        }

        if (property.Type != typeof(T))
        {
            throw new InvalidOperationException($"Property \"{key}\" has values of type {property.Type.SimpleName()}; cannot change to {typeof(T).SimpleName()}.");
        }
        return (List<T>) property.Value;
    }

    private readonly struct Property
    {
        public Property(bool multiple, Type type, object value)
        {
            Multiple = multiple;
            Type = type;
            Value = value;
        }

        public bool Multiple { get; }
        
        public Type Type { get; }
        
        public object Value { get; }
    }
}