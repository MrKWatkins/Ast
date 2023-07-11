using System.Text;

namespace MrKWatkins.Ast.Examples.Maths.Tree;

/// <summary>
/// Base node for expressions in the AST.
/// </summary>
public abstract class Expression : MathsNode
{
    private protected Expression()
    {
    }

    public override string ToString()
    {
        var stringBuilder = new StringBuilder();
        WriteSymbolicExpression(stringBuilder);
        return stringBuilder.ToString();
    }

    internal abstract void WriteSymbolicExpression(StringBuilder expression);
}