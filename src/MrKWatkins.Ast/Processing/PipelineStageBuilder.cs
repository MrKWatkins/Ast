using System.Globalization;
using MrKWatkins.Ast.Traversal;

namespace MrKWatkins.Ast.Processing;

/// <summary>
/// Fluent builder for a pipeline stage.
/// </summary>
public abstract class PipelineStageBuilder<TSelf, TStage, TBaseNode, TProcessor, TShouldContinue>
    where TSelf : PipelineStageBuilder<TSelf, TStage, TBaseNode, TProcessor, TShouldContinue>
    where TBaseNode : Node<TBaseNode>
{
    private readonly List<TProcessor> processors = new();

    private protected PipelineStageBuilder(int number, TShouldContinue shouldContinue)
    {
        Name = number.ToString(NumberFormatInfo.InvariantInfo);
        ShouldContinue = shouldContinue;
    }

    private protected TSelf Self => (TSelf) this;

    private protected IReadOnlyList<TProcessor> Processors => processors;

    private protected string Name { get; private set; }

    private protected TShouldContinue ShouldContinue { get; private set; }

    private protected ITraversal<TBaseNode> DefaultTraversal { get; private set; } = DepthFirstPreOrderTraversal<TBaseNode>.Instance;

    [Pure]
    internal abstract TStage Build();

    /// <summary>
    /// Adds a <see cref="Processor{TNode}" /> of the specified type to the current stage.
    /// </summary>
    /// <typeparam name="TConstructableProcessor">The type of the processor to add.</typeparam>
    /// <returns>The fluent builder.</returns>
    public TSelf Add<TConstructableProcessor>()
        where TConstructableProcessor : TProcessor, new() => Add(new TConstructableProcessor());

    /// <summary>
    /// Adds <see cref="Processor{TNode}">Processors</see> to the current stage.
    /// </summary>
    /// <param name="processors">The processors to add.</param>
    /// <returns>The fluent builder.</returns>
    // ReSharper disable once ParameterHidesMember
    public TSelf Add([InstantHandle] params IEnumerable<TProcessor> processors)
    {
        foreach (var processor in processors)
        {
            VerifyProcessorCanBeAdded(processor);
            this.processors.Add(processor);
        }
        return Self;
    }

    private protected virtual void VerifyProcessorCanBeAdded(TProcessor processor)
    {
    }

    /// <summary>
    /// Sets the name of the stage.
    /// </summary>
    /// <param name="name">The name.</param>
    /// <returns>The fluent builder.</returns>
    public TSelf WithName(string name)
    {
        Name = name;
        return Self;
    }

    /// <summary>
    /// Sets a function to determine whether processing should continue after this stage or not. Any previously registered function
    /// will be replaced. By default, processing will not continue if there are any errors in the tree.
    /// </summary>
    /// <param name="shouldContinue">
    /// A function that takes the root node and returns <c>true</c> if processing should continue, <c>false</c> otherwise.
    /// </param>
    /// <returns>The fluent builder.</returns>
    public TSelf WithShouldContinue(TShouldContinue shouldContinue)
    {
        ShouldContinue = shouldContinue;
        return Self;
    }

    /// <summary>
    /// Specifies that processing should always continue after this stage. By default, processing will not continue if there are any errors in the tree.
    /// </summary>
    /// <returns>The fluent builder.</returns>
    public abstract TSelf WithAlwaysContinue();

    /// <summary>
    /// The default <see cref="ITraversal{TNode}" /> to use to walk through the tree. <see cref="OrderedProcessor{TNode}" />s will specify their own
    /// traversal to use. Defaults to <see cref="DepthFirstPreOrderTraversal{TNode}"/>
    /// </summary>
    /// <param name="defaultTraversal">The default <see cref="ITraversal{TNode}" />.</param>
    /// <returns>The fluent builder.</returns>
    public TSelf WithDefaultTraversal(ITraversal<TBaseNode> defaultTraversal)
    {
        DefaultTraversal = defaultTraversal;

        return Self;
    }
}