namespace MrKWatkins.Ast.Tests;

public sealed class NodeFactoryTests
{
    [Test]
    public void Create_CanCreateAllTestNodes()
    {
        var factory = NodeFactory<TestNodeType, TestNode>.Default;

        factory.Create(TestNodeType.A).Should().BeOfType<ANode>();
        factory.Create(TestNodeType.B).Should().BeOfType<BNode>();
        factory.Create(TestNodeType.C).Should().BeOfType<CNode>();
    }

    [Test]
    public void Create_ThrowsIfAClassHasNotBeenImplementedForTheSpecifiedNodeType() =>
        FluentActions.Invoking(() => NodeFactory<TestNodeType, TestNode>.Default.Create((TestNodeType)10000))
            .Should().Throw<InvalidOperationException>()
            .WithMessage($"A TestNode for {nameof(TestNodeType)}.10000 could not be found in the same assembly as {nameof(TestNode)}.");

    [Test]
    public void Create_ThrowsIfANodeDoesNotHaveAParameterlessConstructor() =>
        FluentActions.Invoking(() => NodeFactory<TestNodeType, NoParameterlessConstructor>.Default.Create(TestNodeType.A))
            .Should().Throw<InvalidOperationException>()
            .WithMessage($"Could not find a parameterless constructor for the node type {nameof(NoParameterlessConstructor)}.");
        
    [Test]
    public void Create_ThrowsIfANodeConstructorThrows() =>
        FluentActions.Invoking(() => NodeFactory<TestNodeType, ThrowOnConstruction>.Default.Create(TestNodeType.A))
            .Should().Throw<InvalidOperationException>()
            .WithMessage($"Exception calling the constructor for {nameof(ThrowOnConstruction)}.");
        
    [Test]
    public void Create_ThrowsIfTypeThrows() =>
        FluentActions.Invoking(() => NodeFactory<TestNodeType, TypeThrows>.Default.Create(TestNodeType.A))
            .Should().Throw<InvalidOperationException>()
            .WithMessage($"The node of type {nameof(TypeThrows)} threw when calling the {nameof(TypeThrows.NodeType)} property.");
        
    [Test]
    public void Create_ThrowsMultipleTypesGiveTheSameNodeType() =>
        FluentActions.Invoking(() => NodeFactory<TestNodeType, MultipleOfSameNodeType>.Default.Create(TestNodeType.A))
            .Should().Throw<InvalidOperationException>()
            .WithMessage($"Multiple types return a {nameof(TestNodeType)} value of {TestNodeType.A}.");
        
    [Test]
    public void Create_ThrowsIfNoImplementationsCouldBeFound() =>
        FluentActions.Invoking(() => NodeFactory<TestNodeType, NoImplementationsFound>.Default.Create(TestNodeType.A))
            .Should().Throw<InvalidOperationException>()
            .WithMessage($"No implementations of {nameof(NoImplementationsFound)} found.");

    [UsedImplicitly]
    private sealed class NoParameterlessConstructor : Node<TestNodeType, NoParameterlessConstructor>
    {
        public NoParameterlessConstructor(string _)
        {
        }
            
        public override TestNodeType NodeType => TestNodeType.A;
    }
        
    [UsedImplicitly]
    private sealed class ThrowOnConstruction : Node<TestNodeType, ThrowOnConstruction>
    {
        public ThrowOnConstruction()
        {
            throw new InvalidOperationException("Test Exception");
        }
            
        public override TestNodeType NodeType => TestNodeType.A;
    }
        
    [UsedImplicitly]
    private sealed class TypeThrows : Node<TestNodeType, TypeThrows>
    {
        public override TestNodeType NodeType => throw new InvalidOperationException("Test Exception");
    }
        
    [UsedImplicitly]
    private abstract class MultipleOfSameNodeType : Node<TestNodeType, MultipleOfSameNodeType>
    {
        public override TestNodeType NodeType => TestNodeType.A;
    }
        
    [UsedImplicitly]
    private sealed class MultipleOfSameNodeType1 : MultipleOfSameNodeType
    {
    }
        
    [UsedImplicitly]
    private sealed class MultipleOfSameNodeType2 : MultipleOfSameNodeType
    {
    }
        
    [UsedImplicitly]
    private abstract class NoImplementationsFound : Node<TestNodeType, NoImplementationsFound>
    {
    }
}