using MrKWatkins.Ast.Processing;

namespace MrKWatkins.Ast.Tests.Processing;

public sealed class SerialPipelineStageBuilderTests : PipelineStageBuilderTestFixture<SerialPipelineStageBuilder<TestNode>, Processor<TestNode>>
{
    protected override Processor<TestNode> CreateProcessor() => new TestProcessor();

    protected override SerialPipelineStageBuilder<TestNode> CreateBuilder(int number) => new(number);

    [Test]
    public void Build_DefaultOptions()
    {
        var processor = new TestProcessor();

        var stage = CreateBuilder(123)
            .Add(processor)
            .Build();

        stage.Name.Should().Be("123");

        stage.Run(N1).Should().BeTrue();
        processor.Processed.Should().HaveSameOrderAs(TestNode.Traverse.DepthFirstPreOrder(N1));

        N12.AddError("Default should continue checks the tree for errors.");
        stage.Run(N1).Should().BeFalse();
        processor.Processed.Should().HaveSameOrderAs(TestNode.Traverse.DepthFirstPreOrder(N1).Concat(TestNode.Traverse.DepthFirstPreOrder(N1)));
    }

    [Test]
    public void Add_ByType()
    {
        var stage = CreateBuilder(123)
            .Add<TestProcessor>()
            .Build();

        stage.Processors.Should().HaveCount(1);
        stage.Processors[0].Should().BeOfType<TestProcessor>();
    }

    [Test]
    public void Add_Multiple()
    {
        var processors = new[] { CreateProcessor(), CreateProcessor(), CreateProcessor() };

        var stage = CreateBuilder(123)
            .Add(processors[0], processors[1], processors[2])
            .Build();

        stage.Processors.Should().BeEquivalentTo(processors);
    }

    private sealed class TestProcessor : OrderedProcessor<TestNode>
    {
        private readonly List<TestNode> processed = new();

        public IReadOnlyList<TestNode> Processed => processed;

        protected override void ProcessNode(TestNode node) => processed.Add(node);
    }
}