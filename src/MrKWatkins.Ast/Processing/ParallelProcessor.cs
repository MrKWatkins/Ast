using System.Threading.Tasks.Dataflow;

namespace MrKWatkins.Ast.Processing;

internal sealed class ParallelProcessor<TNode> : Processor<TNode>
    where TNode : Node<TNode>
{
    private readonly IReadOnlyList<Processor<TNode>> processors;

    internal ParallelProcessor([InstantHandle] IEnumerable<Processor<TNode>> processors, int? maxDegreeOfParallelism)
    {
        if (maxDegreeOfParallelism <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(maxDegreeOfParallelism), maxDegreeOfParallelism, "Value must be greater than 0.");
        }

        MaxDegreeOfParallelism = maxDegreeOfParallelism ?? Environment.ProcessorCount;

        this.processors = processors.ToList();
        if (this.processors.Count == 0)
        {
            throw new ArgumentException("Value is empty.", nameof(processors));
        }
    }

    internal int MaxDegreeOfParallelism { get; }

    internal override ProcessorState<TNode> CreateState(TNode root) =>
        MaxDegreeOfParallelism == 1
            ? CreateSerialState(root)
            : CreateParallelState(root);

    [Pure]
    private ProcessorState<TNode> CreateSerialState(TNode root)
    {
        var exceptions = new Exceptions();

        var context = new SerialContext(processors, root);

        return new ProcessorState<TNode>(
            exceptions,
            node =>
            {
                foreach (var processorState in context.ProcessorStates)
                {
                    processorState.ProcessNodeIfShould(node);
                }
            },
            context)
        {
            OnComplete = context.OnComplete
        };
    }

    [Pure]
    private ProcessorState<TNode> CreateParallelState(TNode root)
    {
        var exceptions = new Exceptions();

        var context = new ParallelContext(processors, root, MaxDegreeOfParallelism);

        return new ProcessorState<TNode>(
            exceptions,
            node =>
            {
                foreach (var processorState in context.ProcessorStates)
                {
                    context.Jobs.Post((processorState, node));
                }
            },
            context)
        {
            OnComplete = context.OnComplete
        };
    }

    private abstract class Context : IDisposable
    {
        protected Context(IReadOnlyList<Processor<TNode>> processors, TNode root)
        {
            ProcessorStates = processors.Select(p => p.CreateState(root)).ToList();
        }

        public IReadOnlyList<ProcessorState<TNode>> ProcessorStates { get; }

        public virtual void OnComplete(ProcessorState<TNode> parallelState)
        {
            foreach (var state in ProcessorStates)
            {
                parallelState.Exceptions.Add(state.Exceptions);

                // No need to invoke state.OnComplete as it will always be null. The only thing that sets an OnComplete
                // is a parallel processing state, and parallel processing states cannot be built from other parallel
                // processing states.
            }
        }

        public void Dispose()
        {
            foreach (var state in ProcessorStates)
            {
                state.Dispose();
            }
        }
    }

    private sealed class ParallelContext : Context
    {
        public ParallelContext(IReadOnlyList<Processor<TNode>> processors, TNode root, int maxDegreeOfParallelism)
            : base(processors, root)
        {
            var options = new ExecutionDataflowBlockOptions
            {
                EnsureOrdered = false,
                MaxDegreeOfParallelism = maxDegreeOfParallelism - 1, // One thread to walk the tree, the rest to process.
                SingleProducerConstrained = true
            };

            Jobs = new ActionBlock<(ProcessorState<TNode> State, TNode Node)>(x => x.State.ProcessNodeIfShould(x.Node), options);
        }

        public ActionBlock<(ProcessorState<TNode> State, TNode Node)> Jobs { get; }

        public override void OnComplete(ProcessorState<TNode> parallelState)
        {
            Jobs.Complete();
            Jobs.Completion.Wait();

            base.OnComplete(parallelState);
        }
    }

    private sealed class SerialContext(IReadOnlyList<Processor<TNode>> processors, TNode root) : Context(processors, root);
}