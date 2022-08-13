namespace MrKWatkins.Ast;

public static class NodeExtensions
{
    [Pure]
    [LinqTunnel]
    public static IEnumerable<TThis> OfType<TType, TThis>(this IEnumerable<TThis> nodes, TType type)
        where TType : struct, Enum
        where TThis : Node<TType, TThis>
    {
        return nodes.Where(n => n.NodeType.Equals(type));
    }
}