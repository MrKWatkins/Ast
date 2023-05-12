namespace MrKWatkins.Ast.Examples.Listeners;

/// <summary>
/// An array of expressions.
/// </summary>
public sealed class Array : Expression
{
    public Array(params Expression[] items)
    {
        Children.Add(items);
    }
}