namespace MrKWatkins.Ast;

public static class NodeExtensions
{
    [Pure]
    [LinqTunnel]
    public static IEnumerable<TThis> OfType<T, TThis>(this IEnumerable<TThis> nodes, T type)
        where T : Enum
        where TThis : Node<T, TThis>
    {
        return nodes.Where(n => n.NodeType.Equals(type));
    }
}