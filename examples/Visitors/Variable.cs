namespace MrKWatkins.Ast.Examples.Visitors;

/// <summary>
/// A named variable whose value is supplied when the expression is evaluated.
/// </summary>
public sealed class Variable : Expression
{
    public Variable(string name)
    {
        Name = name;
    }

    public string Name
    {
        get => Properties.GetOrThrow<string>(nameof(Name));
        init => Properties.Set(nameof(Name), value);
    }
}