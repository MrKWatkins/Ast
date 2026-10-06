using MrKWatkins.Ast.Visiting;

namespace MrKWatkins.Ast.Examples.Visitors;

/// <summary>
/// Evaluates boolean <see cref="Expression" />s.
/// </summary>
public static class Evaluator
{
    // Build a composite visitor from the visitors for each node type.
    private static readonly CompositeVisitor<EvaluationContext, Expression, bool> Visitor =
        CompositeVisitor<EvaluationContext, Expression, bool>
            .Build()
            .With(new ConstantVisitor())
            .With(new VariableVisitor())
            .With(new AndVisitor())
            .With(new OrVisitor())
            .With(new NotVisitor())
            .ToVisitor();

    /// <summary>
    /// Evaluates an <see cref="Expression" />.
    /// </summary>
    /// <param name="expression">The expression.</param>
    /// <param name="variables">The values of any variables in the expression.</param>
    /// <returns>The value of the expression.</returns>
    /// <exception cref="KeyNotFoundException">If the expression uses a variable that has no value in <paramref name="variables" />.</exception>
    [Pure]
    public static bool Evaluate(Expression expression, IReadOnlyDictionary<string, bool>? variables = null)
    {
        var context = new EvaluationContext(variables ?? new Dictionary<string, bool>());

        return Visitor.Visit(context, expression);
    }
}