namespace MrKWatkins.Ast.Examples.Maths.Evaluation;

/// <summary>
/// Context for evaluating expressions. Holds the values of the arguments to the function being evaluated.
/// </summary>
internal sealed class EvaluationContext
{
    public EvaluationContext(IReadOnlyDictionary<string, int> arguments)
    {
        Arguments = arguments;
    }

    internal IReadOnlyDictionary<string, int> Arguments { get; }
}