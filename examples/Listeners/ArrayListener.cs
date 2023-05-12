namespace MrKWatkins.Ast.Examples.Listeners;

/// <summary>
/// Listener for arrays. Wraps the output in square brackets.
/// </summary>
internal sealed class ArrayListener : ExpressionListener<Array>
{
    protected override void BeforeListenToNode(FormattingContext formattingContext, Array array)
    {
        base.BeforeListenToNode(formattingContext, array);
        formattingContext.Output.Append('[');
    }

    protected override void AfterListenToNode(FormattingContext formattingContext, Array _) => formattingContext.Output.Append(']');
}