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
        AssertThat.Invoking(() => DefaultNodeFactory<NoParameterlessConstructor>.Instance.Create(typeof(ANode)))
            .Should().Throw<ArgumentException>().That.Should()
            .HaveMessageStartingWith($"{nameof(ANode)} is not a {nameof(NoParameterlessConstructor)}.").And
            .HaveParamName("nodeType");

    [Test]
    public void Create_ThrowsIfANodeDoesNotHaveAParameterlessConstructor() =>
        AssertThat.Invoking(() => DefaultNodeFactory<NoParameterlessConstructor>.Instance.Create<NoParameterlessConstructor>())
            .Should().Throw<ArgumentException>().That.Should()
            .HaveMessageStartingWith($"Could not find a parameterless constructor for the node type {nameof(NoParameterlessConstructor)}.").And
            .HaveParamName("nodeType");

    [Test]
    public void Create_ThrowsIfANodeConstructorThrows() =>
        AssertThat.Invoking(() => DefaultNodeFactory<ThrowOnConstruction>.Instance.Create<ThrowOnConstruction>())
            .Should().Throw<InvalidOperationException>()
            .That.Should().HaveMessage($"Exception calling the constructor for {nameof(ThrowOnConstruction)}.");

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