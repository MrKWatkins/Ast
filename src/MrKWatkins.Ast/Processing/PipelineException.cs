namespace MrKWatkins.Ast.Processing;

/// <summary>
/// Exception thrown by a <see cref="Pipeline{TNode}" /> when one or more <see cref="Exception">Exceptions</see> occur during processing.
/// <see cref="Exception.InnerException" /> will contain specifics of the <see cref="Exception" />, and will be an <see cref="AggregateException" />
/// if multiple exceptions occurred.
/// </summary>
public sealed class PipelineException : Exception
{
    internal PipelineException(string message, string stage, Exception innerException)
        : base(message, innerException)
    {
        Stage = stage;
    }

    /// <summary>
    /// The name of the stage the exception occurred in.
    /// </summary>
    public string Stage { get; }

    /// <inheritdoc />
    public override string Message => $"{base.Message} (Stage '{Stage}')";
}