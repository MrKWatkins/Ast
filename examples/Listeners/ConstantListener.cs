namespace MrKWatkins.Ast.Examples.Listeners;

/// <summary>
/// Listener for constants. Writes the value to the output.
/// </summary>
internal sealed class ConstantListener : ExpressionListener<Constant>
{
    protected override void ListenToNode(FormattingContext formattingContext, Constant constant) => formattingContext.Output.Append(constant.Value);
}