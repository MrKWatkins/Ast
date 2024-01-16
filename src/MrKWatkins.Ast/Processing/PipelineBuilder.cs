namespace MrKWatkins.Ast.Processing;

/// <summary>
/// Fluent builder for a pipeline stage.
/// </summary>
public sealed class PipelineBuilder<TNode>
    where TNode : Node<TNode>
{
    private readonly List<PipelineStage<TNode>> stages = new();

    [Pure]
    internal Pipeline<TNode> Build() => new(stages);

    /// <summary>
    /// Adds a stage to the pipeline that runs <see cref="Processor{TNode}">Processors</see> serially. Its name will be the number of the stage.
    /// </summary>
    /// <param name="build">An action to perform on a <see cref="SerialPipelineStageBuilder{TNode}"/> to build the pipeline.</param>
    /// <returns>The fluent builder.</returns>
    public PipelineBuilder<TNode> AddStage(Action<SerialPipelineStageBuilder<TNode>> build)
    {
        var builder = new SerialPipelineStageBuilder<TNode>(stages.Count + 1);
        build(builder);
        stages.Add(builder.Build());
        return this;
    }

    /// <summary>
    /// Adds a stage to the pipeline with a single <see cref="Processor{TNode}"/>. Its name will be the number of the stage.
    /// </summary>
    /// <typeparam name="TProcessor">The type of the <see cref="Processor{TNode}"/>.</typeparam>
    /// <returns>The fluent builder.</returns>
    public PipelineBuilder<TNode> AddStage<TProcessor>()
        where TProcessor : Processor<TNode>, new() =>
        AddStage(b => b.Add<TProcessor>());

    /// <summary>
    /// Adds a stage with the specified name to the pipeline with a single <see cref="Processor{TNode}"/>.
    /// </summary>
    /// <typeparam name="TProcessor">The type of the <see cref="Processor{TNode}"/>.</typeparam>
    /// <param name="name">The name of the stage.</param>
    /// <returns>The fluent builder.</returns>
    public PipelineBuilder<TNode> AddStage<TProcessor>(string name)
        where TProcessor : Processor<TNode>, new() =>
        AddStage(b => b.WithName(name).Add<TProcessor>());

    /// <summary>
    /// Adds a stage to the pipeline with the specified <see cref="Processor{TNode}">Processors</see> to be run serially.
    /// Its name will be the number of the stage.
    /// </summary>
    /// <param name="processor">The first processor to add.</param>
    /// <param name="others">Other processors to add.</param>
    /// <returns>The fluent builder.</returns>
    public PipelineBuilder<TNode> AddStage(Processor<TNode> processor, params Processor<TNode>[] others) =>
        AddStage(b => b.Add(processor, others));

    /// <summary>
    /// Adds a stage with the specified name to the pipeline with the specified <see cref="Processor{TNode}">Processors</see> to be run serially.
    /// </summary>
    /// <param name="name">The name of the stage.</param>
    /// <param name="processor">The first processor to add.</param>
    /// <param name="others">Other processors to add.</param>
    /// <returns>The fluent builder.</returns>
    public PipelineBuilder<TNode> AddStage(string name, Processor<TNode> processor, params Processor<TNode>[] others) =>
        AddStage(b => b.WithName(name).Add(processor, others));

    /// <summary>
    /// Adds a stage to the pipeline that runs <see cref="UnorderedProcessor{TNode}">UnorderedProcessors</see> in parallel.
    /// Its name will be the number of the stage.
    /// </summary>
    /// <param name="build">An action to perform on a <see cref="ParallelPipelineStageBuilder{TNode}"/> to build the pipeline.</param>
    /// <returns>The fluent builder.</returns>
    public PipelineBuilder<TNode> AddParallelStage(Action<ParallelPipelineStageBuilder<TNode>> build)
    {
        var builder = new ParallelPipelineStageBuilder<TNode>(stages.Count + 1);
        build(builder);
        stages.Add(builder.Build());
        return this;
    }

    /// <summary>
    /// Adds a stage to the pipeline with the specified <see cref="Processor{TNode}">Processors</see> to be run in parallel.
    /// Its name will be the number of the stage.
    /// </summary>
    /// <param name="processor1">The first processor to add.</param>
    /// <param name="processor2">The second processor to add.</param>
    /// <param name="others">Other processors to add.</param>
    /// <returns>The fluent builder.</returns>
    public PipelineBuilder<TNode> AddParallelStage(UnorderedProcessor<TNode> processor1, UnorderedProcessor<TNode> processor2, params UnorderedProcessor<TNode>[] others) =>
        AddParallelStage(b => b.Add(processor1).Add(processor2, others));

    /// <summary>
    /// Adds a stage with the specified name to the pipeline with the specified <see cref="Processor{TNode}">Processors</see> to be run in parallel.
    /// Its name will be the number of the stage.
    /// </summary>
    /// <param name="name">The name of the stage.</param>
    /// <param name="processor1">The first processor to add.</param>
    /// <param name="processor2">The second processor to add.</param>
    /// <param name="others">Other processors to add.</param>
    /// <returns>The fluent builder.</returns>
    public PipelineBuilder<TNode> AddParallelStage(string name, UnorderedProcessor<TNode> processor1, UnorderedProcessor<TNode> processor2, params UnorderedProcessor<TNode>[] others) =>
        AddParallelStage(b => b.WithName(name).Add(processor1).Add(processor2, others));

    /// <summary>
    /// Adds a stage to the pipeline with the specified <see cref="Processor{TNode}">Processors</see> to be run in parallel with the specified maximum
    /// degree of parallelism. Its name will be the number of the stage.
    /// </summary>
    /// <param name="maxDegreeOfParallelism">
    /// The maximum degree of parallelism. If set to 1 then the stage will proceed in serial. If greater than 1 then 1 thread
    /// will be used to walk the tree and the other threads will be used to process the nodes.
    /// </param>
    /// <param name="processor1">The first processor to add.</param>
    /// <param name="processor2">The second processor to add.</param>
    /// <param name="others">Other processors to add.</param>
    /// <returns>The fluent builder.</returns>
    public PipelineBuilder<TNode> AddParallelStage(int maxDegreeOfParallelism, UnorderedProcessor<TNode> processor1, UnorderedProcessor<TNode> processor2, params UnorderedProcessor<TNode>[] others) =>
        AddParallelStage(b => b.Add(processor1).Add(processor2, others).WithMaxDegreeOfParallelism(maxDegreeOfParallelism));

    /// <summary>
    /// Adds a stage with the specified name to the pipeline with the specified <see cref="Processor{TNode}">Processors</see> to be run in parallel
    /// with the specified maximum degree of parallelism.
    /// </summary>
    /// <param name="name">The name of the stage.</param>
    /// <param name="maxDegreeOfParallelism">
    /// The maximum degree of parallelism. If set to 1 then the stage will proceed in serial. If greater than 1 then 1 thread
    /// will be used to walk the tree and the other threads will be used to process the nodes.
    /// </param>
    /// <param name="processor1">The first processor to add.</param>
    /// <param name="processor2">The second processor to add.</param>
    /// <param name="others">Other processors to add.</param>
    /// <returns>The fluent builder.</returns>
    public PipelineBuilder<TNode> AddParallelStage(string name, int maxDegreeOfParallelism, UnorderedProcessor<TNode> processor1, UnorderedProcessor<TNode> processor2,
        params UnorderedProcessor<TNode>[] others) =>
        AddParallelStage(b => b.WithName(name).Add(processor1).Add(processor2, others).WithMaxDegreeOfParallelism(maxDegreeOfParallelism));
}