using MrKWatkins.Ast.Processing;

namespace MrKWatkins.Ast.Tests.Processing;

public abstract class PipelineStageBuilderTestFixture<TBuilder, TProcessor> : TreeTestFixture
    where TBuilder : PipelineStageBuilder<TBuilder, TProcessor, TestNode>
    where TProcessor : Processor<TestNode>
{
    [Pure]
    protected abstract TProcessor CreateProcessor();
    
    [Pure]
    protected abstract TBuilder CreateBuilder(int number);
    
    [Test]
    public void WithName()
    {
        var processor = CreateProcessor();

        var stage = CreateBuilder(123)
            .Add(processor)
            .WithName("Test Name")
            .Build();

        stage.Name.Should().Be("Test Name");
    }
    
    [Test]
    public void WithShouldContinue()
    {
        var processor = CreateProcessor();

        var stage = CreateBuilder(123)
            .Add(processor)
            .WithShouldContinue(_ => false)
            .Build();

        stage.Run(N1).Should().BeFalse();
    }
    
    [Test]
    public void WithAlwaysContinue()
    {
        var processor = CreateProcessor();

        var stage = CreateBuilder(123)
            .Add(processor)
            .WithAlwaysContinue()
            .Build();

        N12.AddError("Default should continue checks the tree for errors.");
        stage.Run(N1).Should().BeTrue();
    }
}