namespace MrKWatkins.Ast.Processing;

/// <summary>
/// Exception thrown when a <see cref="Pipeline{TBaseNode}" /> or <see cref="Pipeline{TContext, TBaseNode}" /> fails.
/// </summary>
public sealed class PipelineException : Exception
{
    internal PipelineException(string message, string stage, Exception innerException)
        : base(message, innerException)
    {
        Stage = stage;
    }

    /// <summary>
    /// The name of the stage that failed.
    /// </summary>
    public string Stage { get; }

    /// <inheritdoc />
    public override string Message => $"{base.Message} (Stage '{Stage}')";
}