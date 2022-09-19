namespace MrKWatkins.Ast.Processing;

public sealed class PipelineException : Exception
{
    internal PipelineException(string message, string stage, Exception innerException)
        : base(message, innerException)
    {
        Stage = stage;
    }

    public string Stage { get; }

    public override string Message => $"{base.Message} (Stage '{Stage}')";
}