using MrKWatkins.Ast.Examples.Maths.Tree;
using MrKWatkins.Ast.Visiting;

namespace MrKWatkins.Ast.Examples.Maths.Evaluation;

/// <summary>
/// Evaluates a <see cref="Function" />.
/// </summary>
public static class Evaluator
{
    private static readonly CompositeVisitor<EvaluationContext, MathsNode, int> Visitor =
        CompositeVisitor<EvaluationContext, MathsNode, int>
            .Build()
            .With(new BinaryOperationVisitor())
            .With(new ConstantVisitor())
            .With(new VariableVisitor())
            .ToVisitor();

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
        if (function.ThisAndDescendantsHaveErrors)
        {
            throw new ArgumentException("Value contains errors.", nameof(function));
        }

        var parameters = function.Parameters.ToList();
        if (parameters.Count != arguments.Length)
        {
            throw new ArgumentException($"Value contains {arguments.Length} arguments; {parameters.Count} are required.", nameof(arguments));
        }

        var context = new EvaluationContext(parameters.Zip(arguments).ToDictionary(x => x.First.Name, x => x.Second));

        return Visitor.Visit(context, function.Expression);
    }
}