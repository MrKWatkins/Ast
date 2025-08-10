using MrKWatkins.Ast.Processing;
using MrKWatkins.Ast.Traversal;

namespace MrKWatkins.Ast.Tests.Processing;

public sealed class ParallelPipelineStageBuilderTests
{
    [Test]
    public void Build_DefaultOptions()
    {
        var builder = new ParallelPipelineStageBuilder<TestNode>(5).Add(new TestProcessor());
        var stage = builder.Build();
        stage.Name.Should().Equal("5");
        stage.DefaultTraversal.Should().BeTheSameInstanceAs(DepthFirstPreOrderTraversal<TestNode>.Instance);

        // Default should continue will return false if this or descendents have errors.
        var hasErrors = new ANode();
        hasErrors.AddError("Test");

        stage.Run(new ANode()).Should().BeTrue();
        stage.Run(hasErrors).Should().BeFalse();
        stage.Run(new ANode(hasErrors)).Should().BeFalse();
    }

    [Test]
    public void WithName()
    {
        var builder = new ParallelPipelineStageBuilder<TestNode>(5).Add(new TestProcessor()).WithName("Test");
        var stage = builder.Build();
        stage.Name.Should().Equal("Test");
    }

    [Test]
    public void Add()
    {
        var builder = new ParallelPipelineStageBuilder<TestNode>(5);
        builder.Add<TestProcessor>();
        var stage = builder.Build();
        stage.Processors.Should().HaveCount(1);
        stage.Processors[0].Should().BeOfType<TestProcessor>();
    }

    [Test]
    public void Add_ThrowsForOrderedProcessor()
    {
        var builder = new ParallelPipelineStageBuilder<TestNode>(5);
        builder.Invoking(b => b.Add<TestOrderedProcessor>()).Should().Throw<ArgumentException>();
    }

    [Test]
    public void Add_Params()
    {
        var processors = new[] { new TestProcessor(), new TestProcessor(), new TestProcessor() };
        var builder = new ParallelPipelineStageBuilder<TestNode>(5);
        builder.Add(processors);
        var stage = builder.Build();
        stage.Processors.Should().SequenceEqual(processors);
    }

    [Test]
    public void Add_Params_ThrowsForOrderedProcessor()
    {
        var builder = new ParallelPipelineStageBuilder<TestNode>(5);
        builder.Invoking(b => b.Add(new TestProcessor(), new TestOrderedProcessor(), new TestProcessor())).Should().Throw<ArgumentException>();
    }

    [Test]
    public void WithAlwaysContinue()
    {
        var builder = new ParallelPipelineStageBuilder<TestNode>(5).Add(new TestProcessor()).WithAlwaysContinue();
        var stage = builder.Build();
        var hasErrors = new ANode();
        hasErrors.AddError("Test");

        stage.Run(new ANode()).Should().BeTrue();
        stage.Run(hasErrors).Should().BeTrue();
    }

    [Test]
    public void WithDefaultTraversal()
    {
        var builder = new ParallelPipelineStageBuilder<TestNode>(5).Add(new TestProcessor()).WithDefaultTraversal(BreadthFirstTraversal<TestNode>.Instance);
        var stage = builder.Build();
        stage.DefaultTraversal.Should().BeTheSameInstanceAs(BreadthFirstTraversal<TestNode>.Instance);
    }

    [Test]
    public void WithMaxDegreeOfParallelism()
    {
        var builder = new ParallelPipelineStageBuilder<TestNode>(5).Add(new TestProcessor()).WithMaxDegreeOfParallelism(123456);
        var stage = builder.Build();
        stage.MaxDegreeOfParallelism.Should().Equal(123456);
    }

    [TestCase(0)]
    [TestCase(-1)]
    public void WithMaxDegreeOfParallelism_ThrowsForInvalidValue(int maxDegreeOfParallelism)
    {
        var builder = new ParallelPipelineStageBuilder<TestNode>(5);
        builder.Invoking(b => b.WithMaxDegreeOfParallelism(maxDegreeOfParallelism)).Should().Throw<ArgumentOutOfRangeException>();
    }

    [Test]
    public void WithContext_Build_DefaultOptions()
    {
        var builder = new ParallelPipelineStageBuilder<object, TestNode>(5).Add(new TestProcessor<object>());
        var stage = builder.Build();
        stage.Name.Should().Equal("5");
        stage.DefaultTraversal.Should().BeTheSameInstanceAs(DepthFirstPreOrderTraversal<TestNode>.Instance);

        // Default should continue will return false if this or descendents have errors.
        var hasErrors = new ANode();
        hasErrors.AddError("Test");

        stage.Run(new object(), new ANode()).Should().BeTrue();
        stage.Run(new object(), hasErrors).Should().BeFalse();
        stage.Run(new object(), new ANode(hasErrors)).Should().BeFalse();
    }

    [Test]
    public void WithContext_WithName()
    {
        var builder = new ParallelPipelineStageBuilder<object, TestNode>(5).Add(new TestProcessor<object>()).WithName("Test");
        var stage = builder.Build();
        stage.Name.Should().Equal("Test");
    }

    [Test]
    public void WithContext_Add()
    {
        var builder = new ParallelPipelineStageBuilder<object, TestNode>(5);
        builder.Add<TestProcessor<object>>();
        var stage = builder.Build();
        stage.Processors.Should().HaveCount(1);
        stage.Processors[0].Should().BeOfType<TestProcessor<object>>();
    }

    [Test]
    public void WithContext_Add_ThrowsForOrderedProcessor()
    {
        var builder = new ParallelPipelineStageBuilder<object, TestNode>(5);
        builder.Invoking(b => b.Add<TestOrderedProcessor<object>>()).Should().Throw<ArgumentException>();
    }

    [Test]
    public void WithContext_Add_Params()
    {
        var processors = new[] { new TestProcessor<object>(), new TestProcessor<object>(), new TestProcessor<object>() };
        var builder = new ParallelPipelineStageBuilder<object, TestNode>(5);
        builder.Add(processors);
        var stage = builder.Build();
        stage.Processors.Should().SequenceEqual(processors);
    }

    [Test]
    public void WithContext_Add_Params_ThrowsForOrderedProcessor()
    {
        var builder = new ParallelPipelineStageBuilder<object, TestNode>(5);
        builder.Invoking(b => b.Add(new TestProcessor<object>(), new TestOrderedProcessor<object>(), new TestProcessor<object>())).Should().Throw<ArgumentException>();
    }

    [Test]
    public void WithContext_WithAlwaysContinue()
    {
        var builder = new ParallelPipelineStageBuilder<object, TestNode>(5).Add(new TestProcessor<object>()).WithAlwaysContinue();
        var stage = builder.Build();
        var hasErrors = new ANode();
        hasErrors.AddError("Test");

        stage.Run(new object(), new ANode()).Should().BeTrue();
        stage.Run(new object(), hasErrors).Should().BeTrue();
    }

    [Test]
    public void WithContext_WithDefaultTraversal()
    {
        var builder = new ParallelPipelineStageBuilder<object, TestNode>(5).Add(new TestProcessor<object>()).WithDefaultTraversal(BreadthFirstTraversal<TestNode>.Instance);
        var stage = builder.Build();
        stage.DefaultTraversal.Should().BeTheSameInstanceAs(BreadthFirstTraversal<TestNode>.Instance);
    }

    [Test]
    public void WithContext_WithMaxDegreeOfParallelism()
    {
        var builder = new ParallelPipelineStageBuilder<object, TestNode>(5).Add(new TestProcessor<object>()).WithMaxDegreeOfParallelism(123456);
        var stage = builder.Build();
        stage.MaxDegreeOfParallelism.Should().Equal(123456);
    }

    [TestCase(0)]
    [TestCase(-1)]
    public void WithContext_WithMaxDegreeOfParallelism_ThrowsForInvalidValue(int maxDegreeOfParallelism)
    {
        var builder = new ParallelPipelineStageBuilder<object, TestNode>(5);
        builder.Invoking(b => b.WithMaxDegreeOfParallelism(maxDegreeOfParallelism)).Should().Throw<ArgumentOutOfRangeException>();
    }
}