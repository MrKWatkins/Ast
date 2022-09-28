namespace MrKWatkins.Ast.Processing;

[SuppressMessage("ReSharper", "ParameterHidesMember")]
public abstract class PipelineStageBuilder<TThis, TProcessor, TNode>
    where TThis : PipelineStageBuilder<TThis, TProcessor, TNode>
    where TProcessor : Processor<TNode>
    where TNode : Node<TNode>
{
    private protected PipelineStageBuilder(int number)
    {
        Name = number.ToString();
    }

    private protected TThis This => (TThis) this;
    private protected List<TProcessor> Processors { get; } = new();
    private protected string Name { get; private set; }
    private protected Func<TNode, bool> ShouldContinue { get; private set; } = root => !root.ThisAndDescendentsHaveErrors;

    [Pure]
    internal abstract PipelineStage<TNode> Build();

    public TThis Add<TConstructableProcessor>()
        where TConstructableProcessor : TProcessor, new()
    {
        Processors.Add(new TConstructableProcessor());
        return This;
    }

    public TThis Add(TProcessor processor, params TProcessor[] others)
    {
        Processors.Add(processor);
        Processors.AddRange(others);
        return This;
    }

    public TThis WithName(string name)
    {
        Name = name;
        return This;
    }

    public TThis WithShouldContinue(Func<TNode, bool> shouldContinue)
    {
        ShouldContinue = shouldContinue;
        return This;
    }

    public TThis WithAlwaysContinue() => WithShouldContinue(_ => true);
}