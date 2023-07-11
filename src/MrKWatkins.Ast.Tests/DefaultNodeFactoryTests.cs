namespace MrKWatkins.Ast.Tests;

public sealed class DefaultNodeFactoryTests
{
    [Test]
    public void Create_CanCreateAllTestNodes()
    {
        var factory = DefaultNodeFactory<TestNode>.Instance;

        factory.Create(typeof(ANode)).Should().BeOfType<ANode>();
        factory.Create(typeof(BNode)).Should().BeOfType<BNode>();
        factory.Create(typeof(CNode)).Should().BeOfType<CNode>();
    }

    [Test]
    public void Create_TypeParameter_CanCreateAllTestNodes()
    {
        var factory = DefaultNodeFactory<TestNode>.Instance;

        factory.Create<ANode>().Should().BeOfType<ANode>();
        factory.Create<BNode>().Should().BeOfType<BNode>();
        factory.Create<CNode>().Should().BeOfType<CNode>();
    }

    [Test]
    public void Create_ThrowsIfNodeTypeDoesNotInheritFromBaseType() =>
        FluentActions.Invoking(() => DefaultNodeFactory<NoParameterlessConstructor>.Instance.Create(typeof(ANode)))
            .Should().Throw<ArgumentException>()
            .WithParameters($"{nameof(ANode)} is not a {nameof(NoParameterlessConstructor)}.", "nodeType");

    [Test]
    public void Create_ThrowsIfANodeDoesNotHaveAParameterlessConstructor() =>
        FluentActions.Invoking(() => DefaultNodeFactory<NoParameterlessConstructor>.Instance.Create<NoParameterlessConstructor>())
            .Should().Throw<ArgumentException>()
            .WithParameters($"Could not find a parameterless constructor for the node type {nameof(NoParameterlessConstructor)}.", "nodeType");

    [Test]
    public void Create_ThrowsIfANodeConstructorThrows() =>
        FluentActions.Invoking(() => DefaultNodeFactory<ThrowOnConstruction>.Instance.Create<ThrowOnConstruction>())
            .Should().Throw<InvalidOperationException>()
            .WithMessage($"Exception calling the constructor for {nameof(ThrowOnConstruction)}.");

    [UsedImplicitly]
    private sealed class NoParameterlessConstructor : Node<NoParameterlessConstructor>
    {
        public NoParameterlessConstructor(string _)
        {
        }
    }

    [UsedImplicitly]
    private sealed class ThrowOnConstruction : Node<ThrowOnConstruction>
    {
        public ThrowOnConstruction()
        {
            throw new InvalidOperationException("Test Exception");
        }
    }
}