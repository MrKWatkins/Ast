namespace MrKWatkins.Ast.Processing;

/// <summary>
/// Fluent builder for a pipeline stage.
/// </summary>
/// <typeparam name="TBaseNode">The base type of nodes in the tree.</typeparam>
public sealed class PipelineBuilder<TBaseNode>
    where TBaseNode : Node<TBaseNode>
{
    private readonly List<PipelineStage<TBaseNode>> stages = new();

    [Pure]
    internal Pipeline<TBaseNode> Build() => new(stages);

    /// <summary>
    /// Adds a stage to the pipeline that runs <see cref="Processor{TNode}">Processors</see> serially. Its name will be the number of the stage.
    /// </summary>
    /// <param name="build">An action to perform on a <see cref="SerialPipelineStageBuilder{TNode}"/> to build the pipeline.</param>
    /// <returns>The fluent builder.</returns>
    public PipelineBuilder<TBaseNode> AddStage(Action<SerialPipelineStageBuilder<TBaseNode>> build)
    {
        var builder = new SerialPipelineStageBuilder<TBaseNode>(stages.Count + 1);
        build(builder);
        stages.Add(builder.Build());
        return this;
    }

    /// <summary>
    /// Adds a stage to the pipeline with a single <see cref="Processor{TNode}"/>. Its name will be the number of the stage.
    /// </summary>
    /// <typeparam name="TProcessor">The type of the <see cref="Processor{TNode}"/>.</typeparam>
    /// <returns>The fluent builder.</returns>
    public PipelineBuilder<TBaseNode> AddStage<TProcessor>()
        where TProcessor : Processor<TBaseNode>, new() =>
        AddStage(b => b.Add<TProcessor>());

    /// <summary>
    /// Adds a stage with the specified name to the pipeline with a single <see cref="Processor{TNode}"/>.
    /// </summary>
    /// <typeparam name="TProcessor">The type of the <see cref="Processor{TNode}"/>.</typeparam>
    /// <param name="name">The name of the stage.</param>
    /// <returns>The fluent builder.</returns>
    public PipelineBuilder<TBaseNode> AddStage<TProcessor>(string name)
        where TProcessor : Processor<TBaseNode>, new() =>
        AddStage(b => b.WithName(name).Add<TProcessor>());

    /// <summary>
    /// Adds a stage to the pipeline with the specified <see cref="Processor{TNode}">Processors</see> to be run serially.
    /// Its name will be the number of the stage.
    /// </summary>
    /// <param name="processors">The processors to add.</param>
    /// <returns>The fluent builder.</returns>
    public PipelineBuilder<TBaseNode> AddStage([InstantHandle] params IEnumerable<Processor<TBaseNode>> processors) =>
        AddStage(b => b.Add(processors));

    /// <summary>
    /// Adds a stage with the specified name to the pipeline with the specified <see cref="Processor{TNode}">Processors</see> to be run serially.
    /// </summary>
    /// <param name="name">The name of the stage.</param>
    /// <param name="processors">The processors to add.</param>
    /// <returns>The fluent builder.</returns>
    public PipelineBuilder<TBaseNode> AddStage(string name, [InstantHandle] params IEnumerable<Processor<TBaseNode>> processors) =>
        AddStage(b => b.WithName(name).Add(processors));

    /// <summary>
    /// Adds a stage to the pipeline that runs <see cref="Processor{TNode}">Processors</see> in parallel.
    /// Its name will be the number of the stage.
    /// </summary>
    /// <param name="build">An action to perform on a <see cref="ParallelPipelineStageBuilder{TNode}"/> to build the pipeline.</param>
    /// <returns>The fluent builder.</returns>
    public PipelineBuilder<TBaseNode> AddParallelStage(Action<ParallelPipelineStageBuilder<TBaseNode>> build)
    {
        var builder = new ParallelPipelineStageBuilder<TBaseNode>(stages.Count + 1);
        build(builder);
        stages.Add(builder.Build());
        return this;
    }

    /// <summary>
    /// Adds a stage to the pipeline with the specified <see cref="Processor{TNode}">Processors</see> to be run in parallel.
    /// Its name will be the number of the stage.
    /// </summary>
    /// <param name="processors">The processors to add.</param>
    /// <returns>The fluent builder.</returns>
    public PipelineBuilder<TBaseNode> AddParallelStage([InstantHandle] params IEnumerable<Processor<TBaseNode>> processors) =>
        AddParallelStage(b => b.Add(processors));

    /// <summary>
    /// Adds a stage with the specified name to the pipeline with the specified <see cref="Processor{TNode}">Processors</see> to be run in parallel.
    /// Its name will be the number of the stage.
    /// </summary>
    /// <param name="name">The name of the stage.</param>
    /// <param name="processors">The processors to add.</param>
    /// <returns>The fluent builder.</returns>
    public PipelineBuilder<TBaseNode> AddParallelStage(string name, [InstantHandle] params IEnumerable<Processor<TBaseNode>> processors) =>
        AddParallelStage(b => b.WithName(name).Add(processors));

    /// <summary>
    /// Adds a stage to the pipeline with the specified <see cref="Processor{TNode}">Processors</see> to be run in parallel with the specified maximum
    /// degree of parallelism. Its name will be the number of the stage.
    /// </summary>
    /// <param name="maxDegreeOfParallelism">
    /// The maximum degree of parallelism. If set to one, then the stage will proceed in serial. If greater than one, then one thread
    /// will be used to walk the tree and the other threads will be used to process the nodes.
    /// </param>
    /// <param name="processors">The processors to add.</param>
    /// <returns>The fluent builder.</returns>
    public PipelineBuilder<TBaseNode> AddParallelStage(int maxDegreeOfParallelism, [InstantHandle] params IEnumerable<Processor<TBaseNode>> processors) =>
        AddParallelStage(b => b.Add(processors).WithMaxDegreeOfParallelism(maxDegreeOfParallelism));

    /// <summary>
    /// Adds a stage with the specified name to the pipeline with the specified <see cref="Processor{TNode}">Processors</see> to be run in parallel
    /// with the specified maximum degree of parallelism.
    /// </summary>
    /// <param name="name">The name of the stage.</param>
    /// <param name="maxDegreeOfParallelism">
    /// The maximum degree of parallelism. If set to one, then the stage will proceed in serial. If greater than one, then one thread
    /// will be used to walk the tree and the other threads will be used to process the nodes.
    /// </param>
    /// <param name="processors">The processors to add.</param>
    /// <returns>The fluent builder.</returns>
    public PipelineBuilder<TBaseNode> AddParallelStage(string name, int maxDegreeOfParallelism, [InstantHandle] params IEnumerable<Processor<TBaseNode>> processors) =>
        AddParallelStage(b => b.WithName(name).Add(processors).WithMaxDegreeOfParallelism(maxDegreeOfParallelism));
}

/// <summary>
/// Fluent builder for a pipeline stage.
/// </summary>
/// <typeparam name="TContext">The type of the context.</typeparam>
/// <typeparam name="TBaseNode">The base type of nodes in the tree.</typeparam>
public sealed class PipelineBuilder<TContext, TBaseNode>
    where TBaseNode : Node<TBaseNode>
{
    private readonly List<PipelineStage<TContext, TBaseNode>> stages = new();

    [Pure]
    internal Pipeline<TContext, TBaseNode> Build() => new(stages);

    /// <summary>
    /// Adds a stage to the pipeline that runs <see cref="Processor{TContext, TNode}">Processors</see> serially. Its name will be the number of the stage.
    /// </summary>
    /// <param name="build">An action to perform on a <see cref="SerialPipelineStageBuilder{TContext, TNode}"/> to build the pipeline.</param>
    /// <returns>The fluent builder.</returns>
    public PipelineBuilder<TContext, TBaseNode> AddStage(Action<SerialPipelineStageBuilder<TContext, TBaseNode>> build)
    {
        var builder = new SerialPipelineStageBuilder<TContext, TBaseNode>(stages.Count + 1);
        build(builder);
        stages.Add(builder.Build());
        return this;
    }

    /// <summary>
    /// Adds a stage to the pipeline with a single <see cref="Processor{TContext, TNode}"/>. Its name will be the number of the stage.
    /// </summary>
    /// <typeparam name="TProcessor">The type of the <see cref="Processor{TContext, TNode}"/>.</typeparam>
    /// <returns>The fluent builder.</returns>
    public PipelineBuilder<TContext, TBaseNode> AddStage<TProcessor>()
        where TProcessor : Processor<TContext, TBaseNode>, new() =>
        AddStage(b => b.Add<TProcessor>());

    /// <summary>
    /// Adds a stage with the specified name to the pipeline with a single <see cref="Processor{TContext, TNode}"/>.
    /// </summary>
    /// <typeparam name="TProcessor">The type of the <see cref="Processor{TContext, TNode}"/>.</typeparam>
    /// <param name="name">The name of the stage.</param>
    /// <returns>The fluent builder.</returns>
    public PipelineBuilder<TContext, TBaseNode> AddStage<TProcessor>(string name)
        where TProcessor : Processor<TContext, TBaseNode>, new() =>
        AddStage(b => b.WithName(name).Add<TProcessor>());

    /// <summary>
    /// Adds a stage to the pipeline with the specified <see cref="Processor{TContext, TNode}">Processors</see> to be run serially.
    /// Its name will be the number of the stage.
    /// </summary>
    /// <param name="processors">The processors to add.</param>
    /// <returns>The fluent builder.</returns>
    public PipelineBuilder<TContext, TBaseNode> AddStage([InstantHandle] params IEnumerable<Processor<TContext, TBaseNode>> processors) =>
        AddStage(b => b.Add(processors));

    /// <summary>
    /// Adds a stage with the specified name to the pipeline with the specified <see cref="Processor{TContext, TNode}">Processors</see> to be run serially.
    /// </summary>
    /// <param name="name">The name of the stage.</param>
    /// <param name="processors">The processors to add.</param>
    /// <returns>The fluent builder.</returns>
    public PipelineBuilder<TContext, TBaseNode> AddStage(string name, [InstantHandle] params IEnumerable<Processor<TContext, TBaseNode>> processors) =>
        AddStage(b => b.WithName(name).Add(processors));

    /// <summary>
    /// Adds a stage to the pipeline that runs <see cref="Processor{TContext, TNode}">Processors</see> in parallel.
    /// Its name will be the number of the stage.
    /// </summary>
    /// <param name="build">An action to perform on a <see cref="ParallelPipelineStageBuilder{TContext, TNode}"/> to build the pipeline.</param>
    /// <returns>The fluent builder.</returns>
    public PipelineBuilder<TContext, TBaseNode> AddParallelStage(Action<ParallelPipelineStageBuilder<TContext, TBaseNode>> build)
    {
        var builder = new ParallelPipelineStageBuilder<TContext, TBaseNode>(stages.Count + 1);
        build(builder);
        stages.Add(builder.Build());
        return this;
    }

    /// <summary>
    /// Adds a stage to the pipeline with the specified <see cref="Processor{TContext, TNode}">Processors</see> to be run in parallel.
    /// Its name will be the number of the stage.
    /// </summary>
    /// <param name="processors">The processors to add.</param>
    /// <returns>The fluent builder.</returns>
    public PipelineBuilder<TContext, TBaseNode> AddParallelStage([InstantHandle] params IEnumerable<Processor<TContext, TBaseNode>> processors) =>
        AddParallelStage(b => b.Add(processors));

    /// <summary>
    /// Adds a stage with the specified name to the pipeline with the specified <see cref="Processor{TContext, TNode}">Processors</see> to be run in parallel.
    /// Its name will be the number of the stage.
    /// </summary>
    /// <param name="name">The name of the stage.</param>
    /// <param name="processors">The processors to add.</param>
    /// <returns>The fluent builder.</returns>
    public PipelineBuilder<TContext, TBaseNode> AddParallelStage(string name, [InstantHandle] params IEnumerable<Processor<TContext, TBaseNode>> processors) =>
        AddParallelStage(b => b.WithName(name).Add(processors));

    /// <summary>
    /// Adds a stage to the pipeline with the specified <see cref="Processor{TNode}">Processors</see> to be run in parallel with the specified maximum
    /// degree of parallelism. Its name will be the number of the stage.
    /// </summary>
    /// <param name="maxDegreeOfParallelism">
    /// The maximum degree of parallelism. If set to one, then the stage will proceed in serial. If greater than one, then one thread
    /// will be used to walk the tree and the other threads will be used to process the nodes.
    /// </param>
    /// <param name="processors">The processors to add.</param>
    /// <returns>The fluent builder.</returns>
    public PipelineBuilder<TContext, TBaseNode> AddParallelStage(int maxDegreeOfParallelism, [InstantHandle] params IEnumerable<Processor<TContext, TBaseNode>> processors) =>
        AddParallelStage(b => b.Add(processors).WithMaxDegreeOfParallelism(maxDegreeOfParallelism));

    /// <summary>
    /// Adds a stage with the specified name to the pipeline with the specified <see cref="Processor{TNode}">Processors</see> to be run in parallel
    /// with the specified maximum degree of parallelism.
    /// </summary>
    /// <param name="name">The name of the stage.</param>
    /// <param name="maxDegreeOfParallelism">
    /// The maximum degree of parallelism. If set to one, then the stage will proceed in serial. If greater than one, then one thread
    /// will be used to walk the tree and the other threads will be used to process the nodes.
    /// </param>
    /// <param name="processors">The processors to add.</param>
    /// <returns>The fluent builder.</returns>
    public PipelineBuilder<TContext, TBaseNode> AddParallelStage(string name, int maxDegreeOfParallelism, [InstantHandle] params IEnumerable<Processor<TContext, TBaseNode>> processors) =>
        AddParallelStage(b => b.WithName(name).Add(processors).WithMaxDegreeOfParallelism(maxDegreeOfParallelism));
}