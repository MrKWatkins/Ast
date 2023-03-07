namespace MrKWatkins.Ast.Processing;

internal sealed class PipelineStage<TNode>
    where TNode : Node<TNode>
{
    private readonly Func<TNode, bool> shouldContinue;

    internal PipelineStage(string name, IReadOnlyList<Processor<TNode>> processors, Func<TNode, bool> shouldContinue)
    {
        if (processors.Count == 0)
        {
            throw new ArgumentException("Value is empty.", nameof(processors));
        }
        
        Name = name;
        Processors = processors;
        this.shouldContinue = shouldContinue;
    }
    
    internal string Name { get; }
    
    internal IReadOnlyList<Processor<TNode>> Processors { get; }
    
    internal bool Run(TNode root)
    {
        foreach (var processor in Processors)
        {
            try
            {
                processor.Process(root);
            }
            catch (Exception exception)
            {
                throw new PipelineException($"Exception occurred executing processor {processor.GetType().Name}.", Name, exception);
            }
        }

        try
        {
            return shouldContinue(root);
        }
        catch (Exception exception)
        {
            throw new PipelineException("Exception occurred executing should continue function.", Name, exception);
        }
    }
}