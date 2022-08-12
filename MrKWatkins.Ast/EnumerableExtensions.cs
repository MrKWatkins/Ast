namespace MrKWatkins.Ast;

internal static class EnumerableExtensions
{
    public static TSource FirstOrThrow<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate, string message) =>
        source.FirstOrThrow(predicate, () => new InvalidOperationException(message));

    public static TSource FirstOrThrow<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate, Func<string> createMessage) =>
        source.FirstOrThrow(predicate, () => new InvalidOperationException(createMessage()));

    public static TSource FirstOrThrow<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate, Func<Exception> createException)
    {
        foreach (var element in source)
        {
            if (predicate(element))
            {
                return element;
            }
        }

        throw createException();
    }

    public static TSource FirstOrThrow<TSource>(this IEnumerable<TSource> source, string message) =>
        source.FirstOrThrow(() => new InvalidOperationException(message));
        
    public static TSource FirstOrThrow<TSource>(this IEnumerable<TSource> source, Func<string> createMessage) =>
        source.FirstOrThrow(() => new InvalidOperationException(createMessage()));
        
    public static TSource FirstOrThrow<TSource>(this IEnumerable<TSource> source, Func<Exception> createException)
    { 
        if (source is IReadOnlyList<TSource> { Count: > 0 } list)
        {
            return list[0];
        }

        using var e = source.GetEnumerator();
        if (e.MoveNext())
        {
            return e.Current;
        }

        throw createException();
    }

    [Pure]
    public static IEnumerable<IReadOnlyList<TSource>> DuplicateGroupsBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector) =>
        source.GroupBy(keySelector).Where(g => g.Count() > 1).Select(g => g.ToList());
        
    [Pure]
    public static IEnumerable<TSource> DuplicatesBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector) =>
        source.GroupBy(keySelector).Where(g => g.Count() > 1).SelectMany(g => g);
}