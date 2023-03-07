using System.Text;
using MrKWatkins.Ast.Position;

namespace MrKWatkins.Ast;

public static class MessageFormatter
{
    [Pure]
    public static IEnumerable<string> FormatErrors<TNode>(Node<TNode> node, bool includeSource = true)
        where TNode : Node<TNode> => 
        Format(node, MessageLevel.Error, includeSource);

    [Pure]
    public static IEnumerable<string> Format<TNode>(Node<TNode> node, MessageLevel level, bool includeSource = true)
        where TNode : Node<TNode> =>
        node.ThisAndDescendents
            .SelectMany(n => n.Messages.Where(m => m.Level == level).Select(m => FormatMessage(n, m, includeSource)));

    [Pure]
	public static IEnumerable<IGrouping<MessageLevel, string>> Format<TNode>(Node<TNode> node, bool includeSource = true)
        where TNode : Node<TNode> =>
        node.ThisAndDescendents
            .SelectMany(n => n.Messages.Select(m => (m.Level, Message: FormatMessage(n, m, includeSource))))
            .GroupBy(x => x.Level, x => x.Message)
            .OrderByDescending(g => g.Key); // Error then Warning then Info.

    [Pure]
    private static string FormatMessage<TNode>(Node<TNode> node, Message message, bool includeSource)
        where TNode : Node<TNode>
    {
        if (node.SourcePosition == SourcePosition.None)
        {
            return message.ToString();
        }

        var builder = new StringBuilder(node.SourcePosition.ToString())
            .Append(": ")
            .Append(message);

        if (includeSource && node.SourcePosition is ITextSourcePosition textSourcePosition)
        {
            builder.AppendLine();
            textSourcePosition.WriteSourceForMessage(builder);
        }

        return builder.ToString();
    }
}