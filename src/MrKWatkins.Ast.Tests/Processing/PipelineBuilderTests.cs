using MrKWatkins.Ast.Processing;

namespace MrKWatkins.Ast.Tests.Processing;

public sealed class PipelineBuilderTests
{
    [Test]
    public void Build_ThrowsForNoStages() => AssertThat.Invoking(() => Pipeline<TestNode>.Build(_ => { })).Should().Throw<ArgumentException>();

    [Test]
    public void AddStage_Builder()
    {
        var processor = new TestProcessor();
        var pipeline = Pipeline<TestNode>.Build(p => p.AddStage(b => b.Add(processor)).Should().BeTheSameInstanceAs(p));
        pipeline.Stages.Should().HaveCount(1);

        var stage = pipeline.Stages[0].Should().BeOfType<SerialPipelineStage<TestNode>>().Value;
        stage.Name.Should().Equal("1");
        stage.Processors.Should().SequenceEqual(processor);
    }

    [Test]
    public void AddStage_ConstructableProcessor()
    {
        var pipeline = Pipeline<TestNode>.Build(p => p.AddStage<TestProcessor>().Should().BeTheSameInstanceAs(p));
        pipeline.Stages.Should().HaveCount(1);

        var stage = pipeline.Stages[0].Should().BeOfType<SerialPipelineStage<TestNode>>().Value;
        stage.Name.Should().Equal("1");
        stage.Processors.Should().HaveCount(1);
        stage.Processors[0].Should().BeOfType<TestProcessor>();
    }

    [Test]
    public void AddStage_ConstructableProcessor_Name()
    {
        var pipeline = Pipeline<TestNode>.Build(p => p.AddStage<TestProcessor>("TestName").Should().BeTheSameInstanceAs(p));
        pipeline.Stages.Should().HaveCount(1);

        var stage = pipeline.Stages[0].Should().BeOfType<SerialPipelineStage<TestNode>>().Value;
        stage.Name.Should().Equal("TestName");
        stage.Processors.Should().HaveCount(1);
        stage.Processors[0].Should().BeOfType<TestProcessor>();
    }

    [Test]
    public void AddStage_Processors()
    {
        var processors = new[] { new TestProcessor(), new TestProcessor() };
        var pipeline = Pipeline<TestNode>.Build(p => p.AddStage(processors).Should().BeTheSameInstanceAs(p));
        pipeline.Stages.Should().HaveCount(1);

        var stage = pipeline.Stages[0].Should().BeOfType<SerialPipelineStage<TestNode>>().Value;
        stage.Name.Should().Equal("1");
        stage.Processors.Should().SequenceEqual(processors);
    }

    [Test]
    public void AddStage_Name_Processors()
    {
        var processors = new[] { new TestProcessor(), new TestProcessor() };
        var pipeline = Pipeline<TestNode>.Build(p => p.AddStage("TestName", processors).Should().BeTheSameInstanceAs(p));
        pipeline.Stages.Should().HaveCount(1);

        var stage = pipeline.Stages[0].Should().BeOfType<SerialPipelineStage<TestNode>>().Value;
        stage.Name.Should().Equal("TestName");
        stage.Processors.Should().SequenceEqual(processors);
    }

    [Test]
    public void AddParallelStage_Builder()
    {
        var processor = new TestProcessor();
        var pipeline = Pipeline<TestNode>.Build(p => p.AddParallelStage(b => b.Add(processor)).Should().BeTheSameInstanceAs(p));
        pipeline.Stages.Should().HaveCount(1);

        var stage = pipeline.Stages[0].Should().BeOfType<ParallelPipelineStage<TestNode>>().Value;
        stage.Name.Should().Equal("1");
        stage.MaxDegreeOfParallelism.Should().Equal(Environment.ProcessorCount);
        stage.Processors.Should().SequenceEqual(processor);
    }

    [Test]
    public void AddParallelStage_Processors()
    {
        var processors = new[] { new TestProcessor(), new TestProcessor() };
        var pipeline = Pipeline<TestNode>.Build(p => p.AddParallelStage(processors).Should().BeTheSameInstanceAs(p));
        pipeline.Stages.Should().HaveCount(1);

        var stage = pipeline.Stages[0].Should().BeOfType<ParallelPipelineStage<TestNode>>().Value;
        stage.Name.Should().Equal("1");
        stage.MaxDegreeOfParallelism.Should().Equal(Environment.ProcessorCount);
        stage.Processors.Should().SequenceEqual(processors);
    }

    [Test]
    public void AddParallelStage_Name_Processors()
    {
        var processors = new[] { new TestProcessor(), new TestProcessor() };
        var pipeline = Pipeline<TestNode>.Build(p => p.AddParallelStage("TestName", processors).Should().BeTheSameInstanceAs(p));
        pipeline.Stages.Should().HaveCount(1);

        var stage = pipeline.Stages[0].Should().BeOfType<ParallelPipelineStage<TestNode>>().Value;
        stage.Name.Should().Equal("TestName");
        stage.MaxDegreeOfParallelism.Should().Equal(Environment.ProcessorCount);
        stage.Processors.Should().SequenceEqual(processors);
    }

    [Test]
    public void AddParallelStage_MaxDegreeOfParallelism_Processors()
    {
        var processors = new[] { new TestProcessor(), new TestProcessor() };
        var pipeline = Pipeline<TestNode>.Build(p => p.AddParallelStage(123456, processors).Should().BeTheSameInstanceAs(p));
        pipeline.Stages.Should().HaveCount(1);

        var stage = pipeline.Stages[0].Should().BeOfType<ParallelPipelineStage<TestNode>>().Value;
        stage.Name.Should().Equal("1");
        stage.MaxDegreeOfParallelism.Should().Equal(123456);
        stage.Processors.Should().SequenceEqual(processors);
    }

    [Test]
    public void AddParallelStage_Name_MaxDegreeOfParallelism_Processors()
    {
        var processors = new[] { new TestProcessor(), new TestProcessor() };
        var pipeline = Pipeline<TestNode>.Build(p => p.AddParallelStage("TestName", 123456, processors).Should().BeTheSameInstanceAs(p));
        pipeline.Stages.Should().HaveCount(1);

        var stage = pipeline.Stages[0].Should().BeOfType<ParallelPipelineStage<TestNode>>().Value;
        stage.Name.Should().Equal("TestName");
        stage.MaxDegreeOfParallelism.Should().Equal(123456);
        stage.Processors.Should().SequenceEqual(processors);
    }

    [Test]
    public void WithContext_Build_ThrowsForNoStages() => AssertThat.Invoking(() => Pipeline<object, TestNode>.Build(_ => { })).Should().Throw<ArgumentException>();

    [Test]
    public void WithContext_AddStage_Builder()
    {
        var processor = new TestProcessor<object>();
        var pipeline = Pipeline<object, TestNode>.Build(p => p.AddStage(b => b.Add(processor)).Should().BeTheSameInstanceAs(p));
        pipeline.Stages.Should().HaveCount(1);

        var stage = pipeline.Stages[0].Should().BeOfType<SerialPipelineStage<object, TestNode>>().Value;
        stage.Name.Should().Equal("1");
        stage.Processors.Should().SequenceEqual(processor);
    }

    [Test]
    public void WithContext_AddStage_ConstructableProcessor()
    {
        var pipeline = Pipeline<object, TestNode>.Build(p => p.AddStage<TestProcessor<object>>().Should().BeTheSameInstanceAs(p));
        pipeline.Stages.Should().HaveCount(1);

        var stage = pipeline.Stages[0].Should().BeOfType<SerialPipelineStage<object, TestNode>>().Value;
        stage.Name.Should().Equal("1");
        stage.Processors.Should().HaveCount(1);
        stage.Processors[0].Should().BeOfType<TestProcessor<object>>();
    }

    [Test]
    public void WithContext_AddStage_ConstructableProcessor_Name()
    {
        var pipeline = Pipeline<object, TestNode>.Build(p => p.AddStage<TestProcessor<object>>("TestName").Should().BeTheSameInstanceAs(p));
        pipeline.Stages.Should().HaveCount(1);

        var stage = pipeline.Stages[0].Should().BeOfType<SerialPipelineStage<object, TestNode>>().Value;
        stage.Name.Should().Equal("TestName");
        stage.Processors.Should().HaveCount(1);
        stage.Processors[0].Should().BeOfType<TestProcessor<object>>();
    }

    [Test]
    public void WithContext_AddStage_Processors()
    {
        var processors = new[] { new TestProcessor<object>(), new TestProcessor<object>() };
        var pipeline = Pipeline<object, TestNode>.Build(p => p.AddStage(processors).Should().BeTheSameInstanceAs(p));
        pipeline.Stages.Should().HaveCount(1);

        var stage = pipeline.Stages[0].Should().BeOfType<SerialPipelineStage<object, TestNode>>().Value;
        stage.Name.Should().Equal("1");
        stage.Processors.Should().SequenceEqual(processors);
    }

    [Test]
    public void WithContext_AddStage_Name_Processors()
    {
        var processors = new[] { new TestProcessor<object>(), new TestProcessor<object>() };
        var pipeline = Pipeline<object, TestNode>.Build(p => p.AddStage("TestName", processors).Should().BeTheSameInstanceAs(p));
        pipeline.Stages.Should().HaveCount(1);

        var stage = pipeline.Stages[0].Should().BeOfType<SerialPipelineStage<object, TestNode>>().Value;
        stage.Name.Should().Equal("TestName");
        stage.Processors.Should().SequenceEqual(processors);
    }

    [Test]
    public void WithContext_AddParallelStage_Builder()
    {
        var processor = new TestProcessor<object>();
        var pipeline = Pipeline<object, TestNode>.Build(p => p.AddParallelStage(b => b.Add(processor)).Should().BeTheSameInstanceAs(p));
        pipeline.Stages.Should().HaveCount(1);

        var stage = pipeline.Stages[0].Should().BeOfType<ParallelPipelineStage<object, TestNode>>().Value;
        stage.Name.Should().Equal("1");
        stage.MaxDegreeOfParallelism.Should().Equal(Environment.ProcessorCount);
        stage.Processors.Should().SequenceEqual(processor);
    }

    [Test]
    public void WithContext_AddParallelStage_Processors()
    {
        var processors = new[] { new TestProcessor<object>(), new TestProcessor<object>() };
        var pipeline = Pipeline<object, TestNode>.Build(p => p.AddParallelStage(processors).Should().BeTheSameInstanceAs(p));
        pipeline.Stages.Should().HaveCount(1);

        var stage = pipeline.Stages[0].Should().BeOfType<ParallelPipelineStage<object, TestNode>>().Value;
        stage.Name.Should().Equal("1");
        stage.MaxDegreeOfParallelism.Should().Equal(Environment.ProcessorCount);
        stage.Processors.Should().SequenceEqual(processors);
    }

    [Test]
    public void WithContext_AddParallelStage_Name_Processors()
    {
        var processors = new[] { new TestProcessor<object>(), new TestProcessor<object>() };
        var pipeline = Pipeline<object, TestNode>.Build(p => p.AddParallelStage("TestName", processors).Should().BeTheSameInstanceAs(p));
        pipeline.Stages.Should().HaveCount(1);

        var stage = pipeline.Stages[0].Should().BeOfType<ParallelPipelineStage<object, TestNode>>().Value;
        stage.Name.Should().Equal("TestName");
        stage.MaxDegreeOfParallelism.Should().Equal(Environment.ProcessorCount);
        stage.Processors.Should().SequenceEqual(processors);
    }

    [Test]
    public void WithContext_AddParallelStage_MaxDegreeOfParallelism_Processors()
    {
        var processors = new[] { new TestProcessor<object>(), new TestProcessor<object>() };
        var pipeline = Pipeline<object, TestNode>.Build(p => p.AddParallelStage(123456, processors).Should().BeTheSameInstanceAs(p));
        pipeline.Stages.Should().HaveCount(1);

        var stage = pipeline.Stages[0].Should().BeOfType<ParallelPipelineStage<object, TestNode>>().Value;
        stage.Name.Should().Equal("1");
        stage.MaxDegreeOfParallelism.Should().Equal(123456);
        stage.Processors.Should().SequenceEqual(processors);
    }

    [Test]
    public void WithContext_AddParallelStage_Name_MaxDegreeOfParallelism_Processors()
    {
        var processors = new[] { new TestProcessor<object>(), new TestProcessor<object>() };
        var pipeline = Pipeline<object, TestNode>.Build(p => p.AddParallelStage("TestName", 123456, processors).Should().BeTheSameInstanceAs(p));
        pipeline.Stages.Should().HaveCount(1);

        var stage = pipeline.Stages[0].Should().BeOfType<ParallelPipelineStage<object, TestNode>>().Value;
        stage.Name.Should().Equal("TestName");
        stage.MaxDegreeOfParallelism.Should().Equal(123456);
        stage.Processors.Should().SequenceEqual(processors);
    }
}