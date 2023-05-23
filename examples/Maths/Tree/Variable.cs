using System.Text;

namespace MrKWatkins.Ast.Examples.Maths.Tree;

/// <summary>
/// A variable in an expression.
/// </summary>
public sealed class Variable : Expression
{
    public Variable(string name)
    {
        Name = name;
    }

    /// <summary>
    /// The name of the variable.
    /// </summary>
    public string Name
    {
        get => Properties.GetOrThrow<string>(nameof(Name));
        init => Properties.Set(nameof(Name), value);
    }

    internal override void WriteSymbolicExpression(StringBuilder expression) => expression.Append(Name);
}