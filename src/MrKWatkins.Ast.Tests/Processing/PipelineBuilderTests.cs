using MrKWatkins.Ast.Processing;

namespace MrKWatkins.Ast.Tests.Processing;

public sealed class PipelineBuilderTests : TreeTestFixture
{
    [Test]
    public void AddStage_Builder()
    {
        var stage1Processors = new[] { new TestUnorderedProcessor() };
        var stage2Processors = new[] { new TestUnorderedProcessor(), new TestUnorderedProcessor(), new TestUnorderedProcessor() };

        var pipeline = Pipeline<TestNode>.Build(p => p
            .AddStage(s => s.Add(stage1Processors[0]).WithName("Test Name"))
            .AddStage(s => s.Add(stage2Processors[0], stage2Processors[1], stage2Processors[2])));

        pipeline.Stages.Should().HaveCount(2);

        pipeline.Stages[0].Name.Should().Be("Test Name");
        pipeline.Stages[0].Processors.Should().BeEquivalentTo(stage1Processors);

        pipeline.Stages[1].Name.Should().Be("2");
        pipeline.Stages[1].Processors.Should().BeEquivalentTo(stage2Processors);
    }

    [Test]
    public void AddStage_Processors()
    {
        var stage1Processors = new[] { new TestUnorderedProcessor() };
        var stage2Processors = new[] { new TestUnorderedProcessor(), new TestUnorderedProcessor(), new TestUnorderedProcessor() };

        var pipeline = Pipeline<TestNode>.Build(p => p
            .AddStage(stage1Processors[0])
            .AddStage(stage2Processors[0], stage2Processors[1], stage2Processors[2]));

        pipeline.Stages.Should().HaveCount(2);

        pipeline.Stages[0].Name.Should().Be("1");
        pipeline.Stages[0].Processors.Should().BeEquivalentTo(stage1Processors);

        pipeline.Stages[1].Name.Should().Be("2");
        pipeline.Stages[1].Processors.Should().BeEquivalentTo(stage2Processors);
    }

    [Test]
    public void AddStage_Name_Processors()
    {
        var stage1Processors = new[] { new TestUnorderedProcessor() };
        var stage2Processors = new[] { new TestUnorderedProcessor(), new TestUnorderedProcessor() };

        var pipeline = Pipeline<TestNode>.Build(p => p
            .AddStage("Test Stage", stage1Processors[0])
            .AddStage(stage2Processors[0], stage2Processors[1]));

        pipeline.Stages.Should().HaveCount(2);

        pipeline.Stages[0].Name.Should().Be("Test Stage");
        pipeline.Stages[0].Processors.Should().BeEquivalentTo(stage1Processors);

        pipeline.Stages[1].Name.Should().Be("2");
        pipeline.Stages[1].Processors.Should().BeEquivalentTo(stage2Processors);
    }

    [Test]
    public void AddStage_ByType()
    {
        var pipeline = Pipeline<TestNode>.Build(p => p
            .AddStage<TestUnorderedProcessor>()
            .AddStage<TestUnorderedProcessor>());

        pipeline.Stages.Should().HaveCount(2);

        pipeline.Stages[0].Name.Should().Be("1");
        pipeline.Stages[0].Processors.Should().HaveCount(1);
        pipeline.Stages[0].Processors[0].Should().BeOfType<TestUnorderedProcessor>();

        pipeline.Stages[1].Name.Should().Be("2");
        pipeline.Stages[1].Processors.Should().HaveCount(1);
        pipeline.Stages[1].Processors[0].Should().BeOfType<TestUnorderedProcessor>();
    }

    [Test]
    public void AddStage_Name_ByType()
    {
        var pipeline = Pipeline<TestNode>.Build(p => p
            .AddStage<TestUnorderedProcessor>("Test Stage")
            .AddStage<TestUnorderedProcessor>());

        pipeline.Stages.Should().HaveCount(2);

        pipeline.Stages[0].Name.Should().Be("Test Stage");
        pipeline.Stages[0].Processors.Should().HaveCount(1);
        pipeline.Stages[0].Processors[0].Should().BeOfType<TestUnorderedProcessor>();

        pipeline.Stages[1].Name.Should().Be("2");
        pipeline.Stages[1].Processors.Should().HaveCount(1);
        pipeline.Stages[1].Processors[0].Should().BeOfType<TestUnorderedProcessor>();
    }

    [Test]
    public void AddParallelStage_Builder()
    {
        var processors = new[] { new TestUnorderedProcessor(), new TestUnorderedProcessor(), new TestUnorderedProcessor() };

        var pipeline = Pipeline<TestNode>.Build(p => p
            .AddParallelStage(s => s.Add(processors[0], processors[1], processors[2]).WithName("Test Name")));

        pipeline.Stages.Should().HaveCount(1);

        pipeline.Stages[0].Name.Should().Be("Test Name");
        pipeline.Stages[0].Processors[0].Should().BeOfType<ParallelProcessor<TestNode>>()
            .Which.MaxDegreeOfParallelism.Should().Be(Environment.ProcessorCount);

        pipeline.Run(N1).Should().BeTrue();
        processors[0].Processed.Should().HaveCount(NodeCount);
        processors[1].Processed.Should().HaveCount(NodeCount);
        processors[2].Processed.Should().HaveCount(NodeCount);
    }

    [Test]
    public void AddParallelStage_Processors()
    {
        var processors = new[] { new TestUnorderedProcessor(), new TestUnorderedProcessor(), new TestUnorderedProcessor() };

        var pipeline = Pipeline<TestNode>.Build(p => p.AddParallelStage(processors[0], processors[1], processors[2]));

        pipeline.Stages.Should().HaveCount(1);

        pipeline.Stages[0].Name.Should().Be("1");
        pipeline.Stages[0].Processors[0].Should().BeOfType<ParallelProcessor<TestNode>>()
            .Which.MaxDegreeOfParallelism.Should().Be(Environment.ProcessorCount);

        pipeline.Run(N1).Should().BeTrue();
        processors[0].Processed.Should().HaveCount(NodeCount);
        processors[1].Processed.Should().HaveCount(NodeCount);
        processors[2].Processed.Should().HaveCount(NodeCount);
    }

    [Test]
    public void AddParallelStage_Name_Processors()
    {
        var processors = new[] { new TestUnorderedProcessor(), new TestUnorderedProcessor(), new TestUnorderedProcessor() };

        var pipeline = Pipeline<TestNode>.Build(p => p.AddParallelStage("Test Stage", processors[0], processors[1], processors[2]));

        pipeline.Stages.Should().HaveCount(1);

        pipeline.Stages[0].Name.Should().Be("Test Stage");
        pipeline.Stages[0].Processors[0].Should().BeOfType<ParallelProcessor<TestNode>>()
            .Which.MaxDegreeOfParallelism.Should().Be(Environment.ProcessorCount);

        pipeline.Run(N1).Should().BeTrue();
        processors[0].Processed.Should().HaveCount(NodeCount);
        processors[1].Processed.Should().HaveCount(NodeCount);
        processors[2].Processed.Should().HaveCount(NodeCount);
    }

    [Test]
    public void AddParallelStage_MaxDegreeOfParallelism_Processors()
    {
        var maxDegreeOfParallelism = Environment.ProcessorCount + 1;
        var processors = new[] { new TestUnorderedProcessor(), new TestUnorderedProcessor(), new TestUnorderedProcessor() };

        var pipeline = Pipeline<TestNode>.Build(p => p.AddParallelStage(maxDegreeOfParallelism, processors[0], processors[1], processors[2]));

        pipeline.Stages.Should().HaveCount(1);

        pipeline.Stages[0].Name.Should().Be("1");
        pipeline.Stages[0].Processors[0].Should().BeOfType<ParallelProcessor<TestNode>>()
            .Which.MaxDegreeOfParallelism.Should().Be(maxDegreeOfParallelism);

        pipeline.Run(N1).Should().BeTrue();
        processors[0].Processed.Should().HaveCount(NodeCount);
        processors[1].Processed.Should().HaveCount(NodeCount);
        processors[2].Processed.Should().HaveCount(NodeCount);
    }

    [Test]
    public void AddParallelStage_Name_MaxDegreeOfParallelism_Processors()
    {
        var maxDegreeOfParallelism = Environment.ProcessorCount + 1;
        var processors = new[] { new TestUnorderedProcessor(), new TestUnorderedProcessor(), new TestUnorderedProcessor() };

        var pipeline = Pipeline<TestNode>.Build(p => p.AddParallelStage("Test Stage", maxDegreeOfParallelism, processors[0], processors[1], processors[2]));

        pipeline.Stages.Should().HaveCount(1);

        pipeline.Stages[0].Name.Should().Be("Test Stage");
        pipeline.Stages[0].Processors[0].Should().BeOfType<ParallelProcessor<TestNode>>()
            .Which.MaxDegreeOfParallelism.Should().Be(maxDegreeOfParallelism);

        pipeline.Run(N1).Should().BeTrue();
        processors[0].Processed.Should().HaveCount(NodeCount);
        processors[1].Processed.Should().HaveCount(NodeCount);
        processors[2].Processed.Should().HaveCount(NodeCount);
    }
}