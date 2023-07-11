using MrKWatkins.Ast.Listening;

namespace MrKWatkins.Ast.Examples.Listeners;

/// <summary>
/// Base class for listening to expressions.
/// </summary>
internal abstract class ExpressionListener<TExpression> : ListenerWithContext<FormattingContext, Expression, TExpression>
    where TExpression : Expression
{
    protected override void BeforeListenToNode(FormattingContext formattingContext, TExpression node)
    {
        // If our node has a previous sibling then we need a separator.
        if (node.HasPreviousSibling)
        {
            formattingContext.Output.Append(", ");
        }
    }
}