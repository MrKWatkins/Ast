using System.Collections.Concurrent;
using System.Threading.Tasks.Dataflow;
using MrKWatkins.Ast.Enumeration;

namespace MrKWatkins.Ast.Processing;

public sealed class ParallelProcessor<TNode> : Processor, IProcessor<TNode>
    where TNode : Node<TNode>
{
    private readonly IReadOnlyList<Processor<TNode>> processors;

    public ParallelProcessor(params Processor<TNode>[] processors)
        : this(Environment.ProcessorCount, processors)
    {
    }
    
    public ParallelProcessor(int maxDegreeOfParallelism, params Processor<TNode>[] processors)
        : this(maxDegreeOfParallelism, (IEnumerable<Processor<TNode>>) processors)
    {
    }
    
    public ParallelProcessor([InstantHandle] IEnumerable<Processor<TNode>> processors)
        : this(Environment.ProcessorCount, processors)
    {
    }
    
    public ParallelProcessor(int maxDegreeOfParallelism, [InstantHandle] IEnumerable<Processor<TNode>> processors)
    {
        this.processors = ValidateProcessors(processors);
        if (maxDegreeOfParallelism <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(maxDegreeOfParallelism), maxDegreeOfParallelism, "Value must be greater than 0.");
        }
        MaxDegreeOfParallelism = maxDegreeOfParallelism;
    }
    
    internal int MaxDegreeOfParallelism { get; }

    private static IReadOnlyList<Processor<TNode>> ValidateProcessors([InstantHandle] IEnumerable<Processor<TNode>> processors)
    {
        var processorsList = new List<Processor<TNode>>();
        foreach (var processor in processors)
        {
            if (processor.Enumerator != DepthFirstPreOrder<TNode>.Instance)
            {
                throw new ArgumentException(
                    $"Value contains processor {processor.GetType().Name} that overrides {nameof(processor.Enumerator)}; only DepthFirstPreOrder processors can be used in parallel.",
                    nameof(processors));
            }

            processorsList.Add(processor);
        }
        
        if (processorsList.Count == 0)
        {
            throw new ArgumentException("Value is empty.", nameof(processors));
        }

        return processorsList;
    }

    public void Process(TNode root)
    {
        if (MaxDegreeOfParallelism == 1)
        {
            ProcessSerially(root);
        }
        else
        {
            ProcessInParallel(root);
        }
    }

    private void ProcessInParallel(TNode root)
    {
        var options = new ExecutionDataflowBlockOptions
        {
            EnsureOrdered = false,
            MaxDegreeOfParallelism = MaxDegreeOfParallelism - 1, // One thread to walk the tree, the rest to process.
            SingleProducerConstrained = true
        };

        var exceptions = new ConcurrentStack<Exception>();
        var actionBlock = new ActionBlock<(Processor<TNode> Processor, TNode Node, Action<Exception> OnException)>(ProcessNode, options);
        foreach (var node in DepthFirstPreOrder<TNode>.Instance.Enumerate(root))
        {
            foreach (var processor in processors)
            {
                actionBlock.Post((processor, node, exceptions.Push));
            }
        }
        actionBlock.Complete();
        actionBlock.Completion.Wait();

        if (exceptions.Any())
        {
            throw new AggregateException("One or more errors occurred processing the tree in parallel.", exceptions);
        }
    }

    private void ProcessSerially(TNode root)
    {
        var exceptions = new List<Exception>();
        foreach (var node in DepthFirstPreOrder<TNode>.Instance.Enumerate(root))
        {
            foreach (var processor in processors)
            {
                ProcessNode((processor, node, exceptions.Add));
            }
        }

        if (exceptions.Any())
        {
            throw new AggregateException("One or more errors occurred processing the tree serially.", exceptions);
        }
    }

    private static void ProcessNode((Processor<TNode> Processor, TNode Node, Action<Exception> OnException) input)
    {
        var (processor, node, onException) = input;
        try
        {
            if (CatchAndRethrowExceptions(node, nameof(processor.ShouldProcessNode), processor.ShouldProcessNode))
            {
                CatchAndRethrowExceptions(node, nameof(processor.ProcessNode), processor.ProcessNode);
            }

            if (!processor.ShouldProcessChildren(node))
            {
                throw new ProcessingException<TNode>(
                    $"Processor {processor.GetType().Name}.{nameof(processor.ShouldProcessChildren)} returned false; child nodes cannot be skipped when running in parallel.", node);
            }
        }
        catch (Exception exception)
        {
            onException(exception);
        }
    }
}