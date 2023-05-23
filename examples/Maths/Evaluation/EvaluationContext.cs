namespace MrKWatkins.Ast.Examples.Maths.Evaluation;

/// <summary>
/// Context for evaluating expressions. The evaluation proceeds downwards into the expression tree and maintains a stack of values as it goes.
/// </summary>
internal sealed class EvaluationContext
{
    public EvaluationContext(IReadOnlyDictionary<string, int> arguments)
    {
        Arguments = arguments;
    }

    internal Stack<int> Values { get; } = new();
    
    internal IReadOnlyDictionary<string, int> Arguments { get; }
}