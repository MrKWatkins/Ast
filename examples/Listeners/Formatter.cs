using MrKWatkins.Ast.Listening;

namespace MrKWatkins.Ast.Examples.Listeners;

public static class Formatter
{
    [Pure]
    public static string Format(Expression expression)
    {
        // Build a listener from our constant and array listeners.
        var listener = CompositeListener<Expression>
            .BuildWithContext<FormattingContext>()
            .With(new ConstantListener())
            .With(new ArrayListener())
            .ToListener();

        var context = new FormattingContext();
        
        listener.Listen(context, expression);

        return context.Output.ToString();
    }
}