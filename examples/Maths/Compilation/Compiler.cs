using MrKWatkins.Ast.Examples.Maths.Tree;
using MrKWatkins.Ast.Listening;
using LinqExpression = System.Linq.Expressions.Expression;

namespace MrKWatkins.Ast.Examples.Maths.Compilation;

/// <summary>
/// Compiles an expression to a .NET function.
/// </summary>
[SuppressMessage("Naming", "CA1724:Type names should not match namespaces", Justification = "Just example code.")]
public static class Compiler
{
    private static readonly CompositeListener<CompilationContext, MathsNode> Listener =
        CompositeListener<CompilationContext, MathsNode>
            .Build()
            .With(new BinaryOperationListener())
            .With(new ConstantListener())
            .With(new VariableListener())
            .ToListener();

    /// <summary>
    /// Evaluates a <see cref="Function" />.
    /// </summary>
    /// <param name="function">The function.</param>
    /// <returns>The evaluated value.</returns>
    /// <exception cref="ArgumentException">
    /// If <paramref name="function"/> has errors.
    /// </exception>
    [Pure]
    public static Delegate Compile(Function function)
    {
        if (function.ThisAndDescendentsHaveErrors)
        {
            throw new ArgumentException("Value contains errors.", nameof(function));
        }

        var parameters = function.Parameters.ToDictionary(p => p.Name, p => LinqExpression.Parameter(typeof(int), p.Name));

        var context = new CompilationContext(parameters);

        Listener.Listen(context, function.Expression);

        var body = context.Values.Pop();

        var expression = LinqExpression.Lambda(body, parameters.Values);

        return expression.Compile();
    }
}