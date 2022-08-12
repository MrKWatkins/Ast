namespace MrKWatkins.Ast.Tests;

public sealed class NodePropertiesTests
{
    [Test]
    public void GetOrThrow()
    {
        var properties = new NodeProperties();
        properties.Set("One", 1);
        properties.Set("Two", "2");

        properties.GetOrThrow<int>("One").Should().Be(1);
        properties.GetOrThrow<string>("Two").Should().Be("2");
    }
        
    [Test]
    public void GetOrThrow_ThrowsIfNoValueFoundForKey()
    {
        var properties = new NodeProperties();
        properties.Set("One", 1);

        properties.Invoking(p => p.GetOrThrow<string>("Two"))
            .Should().Throw<KeyNotFoundException>()
            .WithMessage("No value for property with key \"Two\".");
    }
        
    [Test]
    public void GetOrThrow_ThrowsIfValueWithDifferentTypeFound()
    {
        var properties = new NodeProperties();
        properties.Set("One", 1);

        properties.Invoking(p => p.GetOrThrow<string>("One"))
            .Should().Throw<InvalidOperationException>()
            .WithMessage("Property with key \"One\" has a value of type System.Int32 but a type of System.String was expected.");
    }
        
    [Test]
    public void GetOrThrow_CanConvertValuesToBaseTypes()
    {
        var properties = new NodeProperties();
        properties.Set("One", 1);
        properties.Set("Two", "2");

        properties.GetOrThrow<object>("One").Should().Be(1);
        properties.GetOrThrow<object>("Two").Should().Be("2");
    }
        
    [Test]
    public void GetOrDefault()
    {
        var properties = new NodeProperties();
        properties.Set("One", 1);
        properties.Set("Two", "2");

        properties.GetOrDefault<int>("One").Should().Be(1);
        properties.GetOrDefault<string>("Two").Should().Be("2");
    }
        
    [Test]
    public void GetOrDefault_ReturnsTheDefaultIfNoValueFoundForKey()
    {
        var properties = new NodeProperties();
        properties.Set("One", 1);

        properties.GetOrDefault<string>("Two").Should().BeNull();
        properties.GetOrDefault("Two", "Default").Should().Be("Default");
            
        properties.GetOrDefault<int>("Two").Should().Be(default);
        properties.GetOrDefault("Two", 123).Should().Be(123);
    }
        
    [Test]
    public void GetOrDefault_ThrowsIfValueWithDifferentTypeFound()
    {
        var properties = new NodeProperties();
        properties.Set("One", 1);

        properties.Invoking(p => p.GetOrDefault<string>("One"))
            .Should().Throw<InvalidOperationException>()
            .WithMessage("Property with key \"One\" has a value of type System.Int32 but a type of System.String was expected.");
    }
        
    [Test]
    public void GetOrDefault_CanConvertValuesToBaseTypes()
    {
        var properties = new NodeProperties();
        properties.Set("One", 1);
        properties.Set("Two", "2");

        properties.GetOrDefault<object>("One").Should().Be(1);
        properties.GetOrDefault<object>("Two").Should().Be("2");
        properties.GetOrDefault<object>("Three", "Default").Should().Be("Default");
        properties.GetOrDefault<object>("Three", 123).Should().Be(123);
    }
}