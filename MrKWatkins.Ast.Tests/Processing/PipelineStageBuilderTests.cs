using MrKWatkins.Ast.Processing;

namespace MrKWatkins.Ast.Tests.Processing;

public sealed class PipelineStageBuilderTests : TreeTestFixture
{
    [Test]
    public void Build_DefaultOptions()
    {
        var processor = new TestProcessor();

        var stage = new PipelineStageBuilder<TestNode>(123)
            .Add(processor)
            .Build();

        stage.Name.Should().Be("123");

        stage.Run(N1).Should().BeTrue();
        processor.Processed.Should().HaveSameOrderAs(TestNode.Enumerate.DepthFirstPreOrder(N1));
        
        N12.AddError("Default should continue checks the tree for errors.");
        stage.Run(N1).Should().BeFalse();
        processor.Processed.Should().HaveSameOrderAs(TestNode.Enumerate.DepthFirstPreOrder(N1).Concat(TestNode.Enumerate.DepthFirstPreOrder(N1)));
    }
    
    [Test]
    public void Add_ByType()
    {
        var stage = new PipelineStageBuilder<TestNode>(123)
            .Add<TestProcessor>()
            .Build();

        stage.Processors.Should().HaveCount(1);
        stage.Processors[0].Should().BeOfType<TestProcessor>();
    }
    
    [Test]
    public void Add_Multiple()
    {
        var processors = new [] { new TestProcessor(), new TestProcessor(), new TestProcessor() };

        var stage = new PipelineStageBuilder<TestNode>(123)
            .Add(processors[0], processors[1], processors[2])
            .Build();

        stage.Processors.Should().BeEquivalentTo(processors);
    }
    
    [Test]
    public void WithName()
    {
        var processor = new TestProcessor();

        var stage = new PipelineStageBuilder<TestNode>(123)
            .Add(processor)
            .WithName("Test Name")
            .Build();

        stage.Name.Should().Be("Test Name");
    }
    
    [Test]
    public void WithShouldContinue()
    {
        var processor = new TestProcessor();

        var stage = new PipelineStageBuilder<TestNode>(123)
            .Add(processor)
            .WithShouldContinue(_ => false)
            .Build();

        stage.Run(N1).Should().BeFalse();
    }
    
    [Test]
    public void WithAlwaysContinue()
    {
        var processor = new TestProcessor();

        var stage = new PipelineStageBuilder<TestNode>(123)
            .Add(processor)
            .WithAlwaysContinue()
            .Build();

        N12.AddError("Default should continue checks the tree for errors.");
        stage.Run(N1).Should().BeTrue();
    }

    [TestCase(5)]
    [TestCase(null)]
    public void WithParallelProcessing(int? maxDegreeOfParallelism)
    {
        var processors = new [] { new TestProcessor(), new TestProcessor(), new TestProcessor() };

        var stage = new PipelineStageBuilder<TestNode>(123)
            .Add(processors[0], processors[1], processors[2])
            .WithParallelProcessing(maxDegreeOfParallelism)
            .Build();

        stage.Processors.Should().HaveCount(1);
        stage.Processors[0].Should().BeOfType<ParallelProcessor<TestNode>>()
            .Which.MaxDegreeOfParallelism.Should().Be(maxDegreeOfParallelism ?? Environment.ProcessorCount);
        
        stage.Run(N1).Should().BeTrue();
        processors[0].Processed.Should().HaveCount(NodeCount);
        processors[1].Processed.Should().HaveCount(NodeCount);
        processors[2].Processed.Should().HaveCount(NodeCount);
    }
}