using MrKWatkins.Ast.Listening;

namespace MrKWatkins.Ast.Examples.Listeners;

public static class Formatter
{
    // Build a composite listener from our constant and array listeners.
    private static readonly CompositeListener<FormattingContext, Expression> Listener =
        CompositeListener<FormattingContext, Expression>
            .Build()
            .With(new ConstantListener())
            .With(new ArrayListener())
            .ToListener();

    [Pure]
    public static string Format(Expression expression)
    {
        var context = new FormattingContext();

        Listener.Listen(context, expression);

        return context.Output.ToString();
    }
}