using System.Globalization;

namespace MrKWatkins.Ast.Processing;

/// <summary>
/// Fluent builder for a pipeline stage.
/// </summary>
[SuppressMessage("ReSharper", "ParameterHidesMember")]
public abstract class PipelineStageBuilder<TSelf, TProcessor, TNode>
    where TSelf : PipelineStageBuilder<TSelf, TProcessor, TNode>
    where TProcessor : Processor<TNode>
    where TNode : Node<TNode>
{
    private protected PipelineStageBuilder(int number)
    {
        Name = number.ToString(NumberFormatInfo.InvariantInfo);
    }

    private protected TSelf Self => (TSelf) this;
    private protected List<TProcessor> Processors { get; } = new();
    private protected string Name { get; private set; }
    private protected Func<TNode, bool> ShouldContinue { get; private set; } = root => !root.ThisAndDescendentsHaveErrors;

    [Pure]
    internal abstract PipelineStage<TNode> Build();

    /// <summary>
    /// Adds a <see cref="Processor{TNode}" /> of the specified type to the current stage.
    /// </summary>
    /// <typeparam name="TConstructableProcessor">The type of the processor to add.</typeparam>
    /// <returns>The fluent builder.</returns>
    public TSelf Add<TConstructableProcessor>()
        where TConstructableProcessor : TProcessor, new()
    {
        Processors.Add(new TConstructableProcessor());
        return Self;
    }

    /// <summary>
    /// Adds <see cref="Processor{TNode}">Processors</see> to the current stage.
    /// </summary>
    /// <param name="processor">The first processor to add.</param>
    /// <param name="others">Other processors to add.</param>
    /// <returns>The fluent builder.</returns>
    public TSelf Add(TProcessor processor, params TProcessor[] others)
    {
        Processors.Add(processor);
        Processors.AddRange(others);
        return Self;
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
    public TSelf WithShouldContinue(Func<TNode, bool> shouldContinue)
    {
        ShouldContinue = shouldContinue;
        return Self;
    }

    /// <summary>
    /// Specifies that processing should always continue after this stage. By default, processing will not continue if there are any errors in the tree.
    /// </summary>
    /// <returns>The fluent builder.</returns>
    public TSelf WithAlwaysContinue() => WithShouldContinue(_ => true);
}