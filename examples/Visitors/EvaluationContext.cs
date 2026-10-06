namespace MrKWatkins.Ast.Examples.Visitors;

/// <summary>
/// Context object for the visitors. Holds the values of the variables in the expression being evaluated.
/// </summary>
internal sealed class EvaluationContext
{
    public EvaluationContext(IReadOnlyDictionary<string, bool> variables)
    {
        Variables = variables;
    }

    public IReadOnlyDictionary<string, bool> Variables { get; }

    public bool GetVariable(string name) =>
        Variables.TryGetValue(name, out var value)
            ? value
            : throw new KeyNotFoundException($"No value has been supplied for the variable {name}.");
}