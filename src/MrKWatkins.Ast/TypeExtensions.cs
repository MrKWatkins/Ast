namespace MrKWatkins.Ast;

internal static class TypeExtensions
{
    // Not caching as it's only used for exception messages.
    [Pure]
    internal static string SimpleName(this Type type) =>
        type.IsGenericType
            ? $"{type.Name[..^2]}<{string.Join(", ", type.GetGenericArguments().Select(SimpleName))}>"
            : type.Name;
}