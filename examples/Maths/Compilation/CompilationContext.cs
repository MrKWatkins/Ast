using System.Linq.Expressions;

namespace MrKWatkins.Ast.Examples.Maths.Compilation;

/// <summary>
/// Context for compiling expressions. The evaluation proceeds downwards into the expression tree and maintains a stack of expressions as it goes.
/// </summary>
internal sealed class CompilationContext
{
    public CompilationContext(IReadOnlyDictionary<string, ParameterExpression> parameters)
    {
        Parameters = parameters;
    }

    internal Stack<Expression> Values { get; } = new();

    internal IReadOnlyDictionary<string, ParameterExpression> Parameters { get; }
}