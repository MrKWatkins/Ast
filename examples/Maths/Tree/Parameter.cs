namespace MrKWatkins.Ast.Examples.Maths.Tree;

/// <summary>
/// A parameter in a <see cref="Function" />.
/// </summary>
public sealed class Parameter : MathsNode
{
    public Parameter(string name)
    {
        Name = name;
    }

    /// <summary>
    /// The name of the parameter.
    /// </summary>
    public string Name
    {
        get => Properties.GetOrThrow<string>(nameof(Name));
        init => Properties.Set(nameof(Name), value);
    }
}