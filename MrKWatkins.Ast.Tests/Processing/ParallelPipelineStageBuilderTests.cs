using MrKWatkins.Ast.Processing;

namespace MrKWatkins.Ast.Tests.Processing;

public sealed class ParallelPipelineStageBuilderTests : PipelineStageBuilderTestFixture<ParallelPipelineStageBuilder<TestNode>, UnorderedProcessor<TestNode>>
{
    protected override UnorderedProcessor<TestNode> CreateProcessor() => new TestUnorderedProcessor();
    
    protected override ParallelPipelineStageBuilder<TestNode> CreateBuilder(int number) => new(number);
    
    [Test]
    public void Build_DefaultOptions()
    {
        var processors = new [] { new TestUnorderedProcessor(), new TestUnorderedProcessor(), new TestUnorderedProcessor() };

        var stage = CreateBuilder(123)
            .Add(processors[0], processors[1], processors[2])
            .Build();

        stage.Processors.Should().HaveCount(1);
        stage.Processors[0].Should().BeOfType<ParallelProcessor<TestNode>>()
            .Which.MaxDegreeOfParallelism.Should().Be(Environment.ProcessorCount);
        
        stage.Run(N1).Should().BeTrue();
        processors[0].Processed.Should().HaveCount(NodeCount);
        processors[1].Processed.Should().HaveCount(NodeCount);
        processors[2].Processed.Should().HaveCount(NodeCount);
    }
    
    [Test]
    public void WithMaxDegreeOfParallelism()
    {
        var processors = new [] { new TestUnorderedProcessor(), new TestUnorderedProcessor(), new TestUnorderedProcessor() };

        var stage = CreateBuilder(123)
            .Add(processors[0], processors[1], processors[2])
            .WithMaxDegreeOfParallelism(5)
            .Build();

        stage.Processors[0].Should().BeOfType<ParallelProcessor<TestNode>>()
            .Which.MaxDegreeOfParallelism.Should().Be(5);
    }
    
    [TestCase(0)]
    [TestCase(-1)]
    public void WithMaxDegreeOfParallelism_ThrowsInvalidValue(int maxDegreeOfParallelism) =>
        CreateBuilder(123)
            .Add(new TestUnorderedProcessor())
            .Invoking(b => b.WithMaxDegreeOfParallelism(maxDegreeOfParallelism))
            .Should().Throw<ArgumentOutOfRangeException>()
            .WithParameters("maxDegreeOfParallelism", maxDegreeOfParallelism, "Value must be greater than 0.");
}