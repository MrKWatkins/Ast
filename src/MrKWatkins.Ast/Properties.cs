using System.Collections.Concurrent;
using System.Linq.Expressions;

namespace MrKWatkins.Ast;

/// <summary>
/// A collection of properties for a node. Properties allow you to store arbitrary data against a node and will be copying during
/// calls to <see cref="PropertyNode{TNode}.Copy()"/>. Properties can have a single value or multiple values.
/// </summary>
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

    /// <summary>
    /// Gets the value of a single valued property with the specified key or throws an exception if the property does not exist.
    /// </summary>
    /// <param name="key">The key of the property.</param>
    /// <typeparam name="T">The type of the property.</typeparam>
    /// <returns>The value of the property.</returns>
    /// <exception cref="KeyNotFoundException">No property with the specified <paramref name="key" /> exists.</exception>
    /// <exception cref="InvalidOperationException">
    /// The property is a multiple value property or the type of the property does not match <typeparamref name="T"/>.
    /// </exception>
    [Pure]
    public T GetOrThrow<T>(string key)
        where T : notnull =>
        GetOrThrow<T>(key, k => new KeyNotFoundException($"No value for property with key \"{k}\"."));

    /// <summary>
    /// Gets the value of a single valued property with the specified key or throws an exception if the property does not exist.
    /// Uses a field to cache the value for better performance.
    /// </summary>
    /// <remarks>
    /// Properties are stored in a dictionary which might not have enough performance in some situations. Use this overload to
    /// get higher performance as the value will be taken from the field if it exists. Make sure to use <see cref="Set{T}(string,T,out T)" />
    /// to update the cached field.
    /// </remarks>
    /// <param name="key">The key of the property.</param>
    /// <param name="cached">The key of the property.</param>
    /// <typeparam name="T">The type of the property.</typeparam>
    /// <returns>The value of the property.</returns>
    /// <exception cref="KeyNotFoundException">No property with the specified <paramref name="key" /> exists.</exception>
    /// <exception cref="InvalidOperationException">
    /// The property is a multiple value property or the type of the property does not match <typeparamref name="T" />.
    /// </exception>
    [Pure]
    public T GetOrThrow<T>(string key, ref T? cached)
        where T : class
    {
        return cached ??= GetOrThrow<T>(key, k => new KeyNotFoundException($"No value for property with key \"{k}\"."));
    }

    /// <summary>
    ///     Gets the value of a single valued property with the specified key or throws an exception if the property does not exist.
    ///     Uses a field to cache the value for better performance.
    /// </summary>
    /// <remarks>
    ///     Properties are stored in a dictionary which might not have enough performance in some situations. Use this overload to
    ///     get higher performance as the value will be taken from the field if it exists. Make sure to use <see cref="Set{T}(string,T,out T)" />
    ///     to update the cached field.
    /// </remarks>
    /// <param name="key">The key of the property.</param>
    /// <param name="cached">The key of the property.</param>
    /// <typeparam name="T">The type of the property.</typeparam>
    /// <returns>The value of the property.</returns>
    /// <exception cref="KeyNotFoundException">No property with the specified <paramref name="key" /> exists.</exception>
    /// <exception cref="InvalidOperationException">
    ///     The property is a multiple value property or the type of the property does not match <typeparamref name="T" />.
    /// </exception>
    [Pure]
    public T GetOrThrow<T>(string key, ref T? cached)
        where T : struct
    {
        return cached ??= GetOrThrow<T>(key, k => new KeyNotFoundException($"No value for property with key \"{k}\"."));
    }

    /// <summary>
    /// Gets the value of a single valued property with the specified key or throws an exception if the property does not exist.
    /// </summary>
    /// <param name="key">The key of the property.</param>
    /// <param name="exceptionCreator">Function to create an exception to throw if the no property with the specified <paramref name="key" /> exists.</param>
    /// <typeparam name="T">The type of the property.</typeparam>
    /// <returns>The value of the property.</returns>
    /// <exception cref="InvalidOperationException">
    /// The property is a multiple value property or the type of the property does not match <typeparamref name="T"/>.
    /// </exception>
    [Pure]
    public T GetOrThrow<T>(string key, [InstantHandle] Func<Exception> exceptionCreator)
        where T : notnull =>
        TryGet<T>(key, out var value) ? value : throw exceptionCreator();

    /// <summary>
    /// Gets the value of a single valued property with the specified key or throws an exception if the property does not exist.
    /// Uses a field to cache the value for better performance.
    /// </summary>
    /// <remarks>
    /// Properties are stored in a dictionary which might not have enough performance in some situations. Use this overload to
    /// get higher performance as the value will be taken from the field if it exists. Make sure to use <see cref="Set{T}(string,T,out T)" />
    /// to update the cached field.
    /// </remarks>
    /// <param name="key">The key of the property.</param>
    /// <param name="cached">The key of the property.</param>
    /// <param name="exceptionCreator">Function to create an exception to throw if the no property with the specified <paramref name="key" /> exists.</param>
    /// <typeparam name="T">The type of the property.</typeparam>
    /// <returns>The value of the property.</returns>
    /// <exception cref="InvalidOperationException">
    /// The property is a multiple value property or the type of the property does not match <typeparamref name="T" />.
    /// </exception>
    [Pure]
    public T GetOrThrow<T>(string key, ref T? cached, [InstantHandle] Func<Exception> exceptionCreator)
        where T : class
    {
        return cached ??= TryGet<T>(key, out var value) ? value : throw exceptionCreator();
    }

    /// <summary>
    /// Gets the value of a single valued property with the specified key or throws an exception if the property does not exist.
    /// Uses a field to cache the value for better performance.
    /// </summary>
    /// <remarks>
    /// Properties are stored in a dictionary which might not have enough performance in some situations. Use this overload to
    /// get higher performance as the value will be taken from the field if it exists. Make sure to use <see cref="Set{T}(string,T,out T)" />
    /// to update the cached field.
    /// </remarks>
    /// <param name="key">The key of the property.</param>
    /// <param name="cached">The key of the property.</param>
    /// <param name="exceptionCreator">Function to create an exception to throw if the no property with the specified <paramref name="key" /> exists.</param>
    /// <typeparam name="T">The type of the property.</typeparam>
    /// <returns>The value of the property.</returns>
    /// <exception cref="InvalidOperationException">
    /// The property is a multiple value property or the type of the property does not match <typeparamref name="T" />.
    /// </exception>
    [Pure]
    public T GetOrThrow<T>(string key, ref T? cached, [InstantHandle] Func<Exception> exceptionCreator)
        where T : struct
    {
        return cached ??= TryGet<T>(key, out var value) ? value : throw exceptionCreator();
    }

    /// <summary>
    /// Gets the value of a single valued property with the specified key or throws an exception if the property does not exist.
    /// </summary>
    /// <param name="key">The key of the property.</param>
    /// <param name="exceptionCreator">Function to create an exception to throw if the no property with the specified <paramref name="key" /> exists.</param>
    /// <typeparam name="T">The type of the property.</typeparam>
    /// <returns>The value of the property.</returns>
    /// <exception cref="InvalidOperationException">
    /// The property is a multiple value property or the type of the property does not match <typeparamref name="T"/>.
    /// </exception>
    [Pure]
    public T GetOrThrow<T>(string key, [InstantHandle] Func<string, Exception> exceptionCreator)
        where T : notnull =>
        TryGet<T>(key, out var value) ? value : throw exceptionCreator(key);

    /// <summary>
    /// Gets the value of a single valued property with the specified key or throws an exception if the property does not exist.
    /// Uses a field to cache the value for better performance.
    /// </summary>
    /// <remarks>
    /// Properties are stored in a dictionary which might not have enough performance in some situations. Use this overload to
    /// get higher performance as the value will be taken from the field if it exists. Make sure to use <see cref="Set{T}(string,T,out T)" />
    /// to update the cached field.
    /// </remarks>
    /// <param name="key">The key of the property.</param>
    /// <param name="cached">The key of the property.</param>
    /// <param name="exceptionCreator">Function to create an exception to throw if the no property with the specified <paramref name="key" /> exists.</param>
    /// <typeparam name="T">The type of the property.</typeparam>
    /// <returns>The value of the property.</returns>
    /// <exception cref="InvalidOperationException">
    /// The property is a multiple value property or the type of the property does not match <typeparamref name="T" />.
    /// </exception>
    [Pure]
    public T GetOrThrow<T>(string key, ref T? cached, [InstantHandle] Func<string, Exception> exceptionCreator)
        where T : class
    {
        return cached ??= TryGet<T>(key, out var value) ? value : throw exceptionCreator(key);
    }

    /// <summary>
    /// Gets the value of a single valued property with the specified key or throws an exception if the property does not exist.
    /// Uses a field to cache the value for better performance.
    /// </summary>
    /// <remarks>
    /// Properties are stored in a dictionary which might not have enough performance in some situations. Use this overload to
    /// get higher performance as the value will be taken from the field if it exists. Make sure to use <see cref="Set{T}(string,T,out T)" />
    /// to update the cached field.
    /// </remarks>
    /// <param name="key">The key of the property.</param>
    /// <param name="cached">The key of the property.</param>
    /// <param name="exceptionCreator">Function to create an exception to throw if the no property with the specified <paramref name="key" /> exists.</param>
    /// <typeparam name="T">The type of the property.</typeparam>
    /// <returns>The value of the property.</returns>
    /// <exception cref="InvalidOperationException">
    ///     The property is a multiple value property or the type of the property does not match <typeparamref name="T" />.
    /// </exception>
    [Pure]
    public T GetOrThrow<T>(string key, ref T? cached, [InstantHandle] Func<string, Exception> exceptionCreator)
        where T : struct
    {
        return cached ??= TryGet<T>(key, out var value) ? value : throw exceptionCreator(key);
    }

    /// <summary>
    /// Gets the value of a single valued property with the specified key or returns a default value if the property does not exist.
    /// </summary>
    /// <param name="key">The key of the property.</param>
    /// <param name="default">The value to return if the property does not exist.</param>
    /// <typeparam name="T">The type of the property.</typeparam>
    /// <returns>The value of the property.</returns>
    /// <exception cref="InvalidOperationException">
    /// The property is a multiple value property or the type of the property does not match <typeparamref name="T"/>.
    /// </exception>
    [Pure]
    [return: NotNullIfNotNull("default")]
    public T? GetOrDefault<T>(string key, T? @default = default)
        where T : notnull =>
        TryGet<T>(key, out var value) ? value : @default;

    /// <summary>
    /// Gets the value of a single valued property with the specified key or returns a default value if the property does not exist.
    /// </summary>
    /// <param name="key">The key of the property.</param>
    /// <param name="defaultCreator">Function to create the value to return if the property does not exist.</param>
    /// <typeparam name="T">The type of the property.</typeparam>
    /// <returns>The value of the property.</returns>
    /// <exception cref="InvalidOperationException">
    /// The property is a multiple value property or the type of the property does not match <typeparamref name="T"/>.
    /// </exception>
    [MustUseReturnValue]
    public T GetOrAdd<T>(string key, [InstantHandle] Func<string, T> defaultCreator)
        where T : notnull
    {
        if (TryGet<T>(key, out var value))
        {
            return value;
        }

        value = defaultCreator(key);
        Set(key, value);
        return value;
    }

    /// <summary>
    /// Tries to get the value of a single valued property with the specified key.
    /// </summary>
    /// <param name="key">The key of the property.</param>
    /// <param name="value">The value of the property if it exists.</param>
    /// <typeparam name="T">The type of the property.</typeparam>
    /// <returns><c>true</c> if the property exists, <c>false</c> otherwise.</returns>
    /// <exception cref="InvalidOperationException">
    /// The property is a multiple value property or the type of the property does not match <typeparamref name="T"/>.
    /// </exception>
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

    /// <summary>
    /// Tests whether a property exists with the specified key.
    /// </summary>
    /// <param name="key">The key of the property.</param>
    /// <returns><c>true</c> if the property exists, <c>false</c> otherwise.</returns>
    [Pure]
    public bool ContainsKey(string key) => properties.ContainsKey(key);

    /// <summary>
    /// The number of properties in the collection.
    /// </summary>
    [Pure]
    public int Count => properties.Count;

    /// <summary>
    /// Sets the value of a single valued property with the specified key.
    /// </summary>
    /// <param name="key">The key of the property.</param>
    /// <param name="value">The value of the property.</param>
    /// <typeparam name="T">The type of the property.</typeparam>
    /// <exception cref="InvalidOperationException">
    /// The property already has a value and is a multiple value property or the type of the property does not match <typeparamref name="T"/>.
    /// </exception>
    public void Set<T>(string key, T value)
        where T : notnull
    {
        if (properties.TryGetValue(key, out var property))
        {
            VerifySingle<T>(key, property);
        }

        properties[key] = new Property(false, typeof(T), value);
    }

    /// <summary>
    /// Sets the value of a single valued property with the specified key. Uses a field to cache the value for better performance.
    /// </summary>
    /// <remarks>
    /// Properties are stored in a dictionary which might not have enough performance in some situations. Use this overload to
    /// get higher performance as the value will be stored in the field if it exists. Make sure to use <see cref="GetOrThrow{T}(string,ref T)" />
    /// to retrieve the cached field.
    /// </remarks>
    /// <param name="key">The key of the property.</param>
    /// <param name="value">The value of the property.</param>
    /// <param name="cached">The key of the property.</param>
    /// <typeparam name="T">The type of the property.</typeparam>
    /// <exception cref="InvalidOperationException">
    /// The property already has a value and is a multiple value property or the type of the property does not match <typeparamref name="T" />.
    /// </exception>
    public void Set<T>(string key, T value, out T? cached)
        where T : class
    {
        if (properties.TryGetValue(key, out var property))
        {
            VerifySingle<T>(key, property);
        }

        cached = value;
        properties[key] = new Property(false, typeof(T), value);
    }

    /// <summary>
    /// Sets the value of a single valued property with the specified key. Uses a field to cache the value for better performance.
    /// </summary>
    /// <remarks>
    /// Properties are stored in a dictionary which might not have enough performance in some situations. Use this overload to
    /// get higher performance as the value will be stored in the field if it exists. Make sure to use <see cref="GetOrThrow{T}(string,ref T)" />
    /// to retrieve the cached field.
    /// </remarks>
    /// <param name="key">The key of the property.</param>
    /// <param name="value">The value of the property.</param>
    /// <param name="cached">The key of the property.</param>
    /// <typeparam name="T">The type of the property.</typeparam>
    /// <exception cref="InvalidOperationException">
    ///     The property already has a value and is a multiple value property or the type of the property does not match <typeparamref name="T" />.
    /// </exception>
    public void Set<T>(string key, T value, out T? cached)
        where T : struct
    {
        if (properties.TryGetValue(key, out var property))
        {
            VerifySingle<T>(key, property);
        }

        cached = value;
        properties[key] = new Property(false, typeof(T), value);
    }

    /// <summary>
    /// Gets the values of a multiple valued property with the specified key.
    /// </summary>
    /// <param name="key">The key of the property.</param>
    /// <typeparam name="T">The type of the property.</typeparam>
    /// <returns>The values of the property.</returns>
    /// <exception cref="InvalidOperationException">
    /// The property is a single value property or the type of the property does not match <typeparamref name="T"/>.
    /// </exception>
    [Pure]
    public IReadOnlyList<T> GetMultiple<T>(string key)
        where T : notnull =>
        properties.TryGetValue(key, out var property) ? VerifyMultiple<T>(key, property) : Array.Empty<T>();

    /// <summary>
    /// Sets the values of a multiple valued property with the specified key. Any existing values are replaced.
    /// </summary>
    /// <param name="key">The key of the property.</param>
    /// <param name="values">The values of the property.</param>
    /// <typeparam name="T">The type of the property.</typeparam>
    /// <exception cref="InvalidOperationException">
    /// The property already has a value and is a single value property or the type of the property does not match <typeparamref name="T"/>.
    /// </exception>
    public void SetMultiple<T>(string key, [InstantHandle] IEnumerable<T> values)
        where T : notnull
    {
        if (properties.TryGetValue(key, out var property))
        {
            VerifyMultiple<T>(key, property);
        }

        properties[key] = new Property(true, typeof(T), values.ToList());
    }

    /// <summary>
    /// Adds a value to a multiple valued property with the specified key.
    /// </summary>
    /// <param name="key">The key of the property.</param>
    /// <param name="value">The value to add to the property.</param>
    /// <typeparam name="T">The type of the property.</typeparam>
    /// <exception cref="InvalidOperationException">
    /// The property already has a value and is a single value property or the type of the property does not match <typeparamref name="T"/>.
    /// </exception>
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

    /// <summary>
    /// Tries to add a value to a multiple valued property with the specified key. If the value already exists in the multiple
    /// then it is not added.
    /// </summary>
    /// <param name="key">The key of the property.</param>
    /// <param name="value">The value to add to the property.</param>
    /// <param name="valueComparer">An equality comparer to compare values or <c>null</c> for the default comparer.</param>
    /// <returns><c>true</c> if the value was added, <c>false</c> otherwise.</returns>
    /// <typeparam name="T">The type of the property.</typeparam>
    /// <exception cref="InvalidOperationException">
    /// The property already has a value and is a single value property or the type of the property does not match <typeparamref name="T"/>.
    /// </exception>
    public bool TryAddToMultiple<T>(string key, T value, IEqualityComparer<T>? valueComparer = null)
        where T : notnull
    {
        List<T> list;
        if (properties.TryGetValue(key, out var property))
        {
            list = VerifyMultiple<T>(key, property);
            if (!list.Contains(value, valueComparer))
            {
                list.Add(value);
                return true;
            }

            return false;
        }

        list = new List<T>();
        properties.Add(key, new Property(true, typeof(T), list));

        list.Add(value);
        return true;
    }

    /// <summary>
    /// Adds values to a multiple valued property with the specified key.
    /// </summary>
    /// <param name="key">The key of the property.</param>
    /// <param name="values">The values to add to the property.</param>
    /// <typeparam name="T">The type of the property.</typeparam>
    /// <exception cref="InvalidOperationException">
    /// The property already has a value and is a single value property or the type of the property does not match <typeparamref name="T"/>.
    /// </exception>
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
    internal Properties Copy()
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