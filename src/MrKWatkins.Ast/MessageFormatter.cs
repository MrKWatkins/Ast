using System.Text;
using MrKWatkins.Ast.Position;

namespace MrKWatkins.Ast;

/// <summary>
/// Utility methods to format <see cref="Message">Messages</see> as strings. Formatting optionally includes the original source code.
/// </summary>
public static class MessageFormatter
{
    /// <summary>
    /// Lazily enumerates over all <see cref="MessageLevel.Error">Errors</see> in the specified node.
    /// </summary>
    /// <param name="node">The node.</param>
    /// <param name="options">The <see cref="MessageFormatterOptions" /> to use. If not specified then <see cref="MessageFormatterOptions.Default"/> is used.</param>
    /// <typeparam name="TNode">The type of the node.</typeparam>
    /// <returns>A lazy enumeration of formatted errors.</returns>
    [Pure]
    public static IEnumerable<string> FormatErrors<TNode>(Node<TNode> node, MessageFormatterOptions? options = null)
        where TNode : Node<TNode> =>
        Format(node, MessageLevel.Error, options);

    /// <summary>
    /// Lazily enumerates over all <see cref="Message">Messages</see> of the specified <see cref="MessageLevel"/> in the specified node.
    /// </summary>
    /// <param name="node">The node.</param>
    /// <param name="level">The <see cref="MessageLevel"/>.</param>
    /// <param name="options">The <see cref="MessageFormatterOptions" /> to use. If not specified then <see cref="MessageFormatterOptions.Default"/> is used.</param>
    /// <typeparam name="TNode">The type of the node.</typeparam>
    /// <returns>A lazy enumeration of formatted <see cref="Message">Messages</see>.</returns>
    [Pure]
    public static IEnumerable<string> Format<TNode>(Node<TNode> node, MessageLevel level, MessageFormatterOptions? options = null)
        where TNode : Node<TNode> =>
        node.ThisAndDescendents
            .SelectMany(n => n.Messages.Where(m => m.Level == level).Select(m => FormatMessage(n, m, options)));

    /// <summary>
    /// Lazily enumerates over all <see cref="Message">Messages</see> in the specified node, grouping by <see cref="Message.Level" />
    /// in descending order. I.e. <see cref="MessageLevel.Error" /> then <see cref="MessageLevel.Warning" /> and then <see cref="MessageLevel.Info" />.
    /// </summary>
    /// <param name="node">The node.</param>
    /// <param name="options">The <see cref="MessageFormatterOptions" /> to use. If not specified then <see cref="MessageFormatterOptions.Default"/> is used.</param>
    /// <typeparam name="TNode">The type of the node.</typeparam>
    /// <returns>A lazy enumeration of formatted <see cref="Message">Messages</see> grouped by <see cref="Message.Level" />.</returns>
    [Pure]
    public static IEnumerable<IGrouping<MessageLevel, string>> Format<TNode>(Node<TNode> node, MessageFormatterOptions? options = null)
        where TNode : Node<TNode> =>
        node.ThisAndDescendents
            .SelectMany(n => n.Messages.Select(m => (m.Level, Message: FormatMessage(n, m, options))))
            .GroupBy(x => x.Level, x => x.Message)
            .OrderByDescending(g => g.Key); // Error then Warning then Info.

    [Pure]
    private static string FormatMessage<TNode>(Node<TNode> node, Message message, MessageFormatterOptions? options)
        where TNode : Node<TNode>
    {
        options ??= MessageFormatterOptions.Default;

        if (node.SourcePosition == SourcePosition.None)
        {
            return message.ToString();
        }

        var builder = new StringBuilder();

        if (options.PrefixWithSourcePosition)
        {
            builder.Append(node.SourcePosition);
            builder.Append(": ");
        }

        builder.Append(message);

        if (options.HighlightSourcePosition && node.SourcePosition is ITextSourcePosition textSourcePosition)
        {
            builder.AppendLine();
            textSourcePosition.WriteSourceForMessage(builder);
        }

        return builder.ToString();
    }
}