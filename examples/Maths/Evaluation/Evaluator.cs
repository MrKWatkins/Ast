using MrKWatkins.Ast.Examples.Maths.Tree;
using MrKWatkins.Ast.Listening;

namespace MrKWatkins.Ast.Examples.Maths.Evaluation;

/// <summary>
/// Evaluates a <see cref="Function" />.
/// </summary>
public static class Evaluator
{
    private static readonly CompositeListenerWithContext<EvaluationContext, MathsNode> Listener =
        CompositeListener<MathsNode>
            .BuildWithContext<EvaluationContext>()
            .With(new BinaryOperationListener())
            .With(new ConstantListener())
            .With(new VariableListener())
            .ToListener();
    
    /// <summary>
    /// Evaluates a <see cref="Function" />.
    /// </summary>
    /// <param name="function">The function.</param>
    /// <param name="arguments">The arguments to the function.</param>
    /// <returns>The evaluated value.</returns>
    /// <exception cref="ArgumentException">
    /// If <paramref name="function"/> has errors or the number of <paramref name="arguments"/> does not match the parameter count.
    /// </exception>
    [Pure]
    public static int Evaluate(Function function, params int[] arguments)
    {
        if (function.ThisAndDescendentsHaveErrors)
        {
            throw new ArgumentException("Value contains errors.", nameof(function));
        }
        
        var parameters = function.Parameters.ToList();
        if (parameters.Count != arguments.Length)
        {
            throw new ArgumentException($"Value contains {arguments.Length} arguments; {parameters.Count} are required.", nameof(arguments));
        }

        var context = new EvaluationContext(parameters.Zip(arguments).ToDictionary(x => x.First.Name, x => x.Second));

        Listener.Listen(context, function.Expression);

        return context.Values.Pop();
    }
}