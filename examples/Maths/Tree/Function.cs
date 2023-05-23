using System.Text;

namespace MrKWatkins.Ast.Examples.Maths.Tree;

/// <summary>
/// A function consisting of an expression and several parameters.
/// </summary>
public sealed class Function : MathsNode
{
    internal Function([InstantHandle] IEnumerable<Parameter> parameters, Expression expression)
    {
        Children.Add(parameters);
        Children.Add(expression);
    }

    /// <summary>
    /// The parameters to the function.
    /// </summary>
    public IEnumerable<Parameter> Parameters => Children.OfType<Parameter>();

    /// <summary>
    /// The body of the function.
    /// </summary>
    public Expression Expression => Children.LastOfType<Expression>();

    public override string ToString()
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.Append('(');
        foreach (var parameter in Parameters)
        {
            if (stringBuilder.Length > 1)
            {
                stringBuilder.Append(", ");
            }

            stringBuilder.Append(parameter.Name);
        }
        stringBuilder.Append(") => ");
        Expression.WriteSymbolicExpression(stringBuilder);
        return stringBuilder.ToString();
    }
}