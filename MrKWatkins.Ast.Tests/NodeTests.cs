using MrKWatkins.Ast.Position;

namespace MrKWatkins.Ast.Tests;

public sealed partial class NodeTests
{
    [Test]
    public void Constructor()
    {
        var root = new ANode();
        root.HasParent.Should().BeFalse();
        root.Invoking(r => r.Parent).Should().Throw<InvalidOperationException>();

        root.Children.Should().BeEmpty();
    }

    [Test]
    public void Constructor_IEnumerable()
    {
        IEnumerable<TestNode> children = new TestNode[] { new ANode(), new BNode(), new CNode() };

        var root = new ANode(children);
        root.HasParent.Should().BeFalse();
        root.Invoking(r => r.Parent).Should().Throw<InvalidOperationException>();

        root.Children.Should().BeEquivalentTo(children, options => options.WithStrictOrdering());
        root.Children.Should().OnlyContain(c => c.HasParent);
        root.Children.Should().OnlyContain(c => ReferenceEquals(c.Parent, root));
    }

    [Test]
    public void Constructor_Array()
    {
        var children = new TestNode[] { new ANode(), new BNode(), new CNode() };

        var root = new ANode(children);
        root.HasParent.Should().BeFalse();

        root.Children.Should().BeEquivalentTo(children, options => options.WithStrictOrdering());
        root.Children.Should().OnlyContain(c => c.HasParent);
        root.Children.Should().OnlyContain(c => ReferenceEquals(c.Parent, root));
    }

    [Test]
    public void Properties()
    {
        var node = new ANode();
        node.Invoking(n => n.TestProperty).Should().Throw<KeyNotFoundException>();
        node.TestProperty = "Test";
        node.TestProperty.Should().Be("Test");
    }

    [Test]
    public void Constructor_CannotAddNodesThatAlreadyHaveParents()
    {
        var root = new ANode(new BNode());

        FluentActions.Invoking(() => new ANode(root.Children[0]))
            .Should().Throw<InvalidOperationException>()
            .WithMessage("Node is already the child of another node.");
    }

    [Test]
    public void Copy()
    {
        var grandchild11 = new CNode { TestProperty = "GC11" };
        var grandchild12 = new CNode { TestProperty = "GC12" };
        var child1 = new BNode(grandchild11, grandchild12) { TestProperty = "C1" };

        var grandchild21 = new CNode { TestProperty = "GC21" };
        var child2 = new BNode(grandchild21) { TestProperty = "C2" };

        var child3 = new BNode(); // Test TestProperty not set.

        var parent = new ANode(child1, child2, child3) { TestProperty = "P" };

        var copy = parent.Copy();

        copy.Should().NotBeSameAs(parent);
        copy.TestProperty.Should().Be("P");
        copy.Children.Should().HaveCount(3);

        var child1Copy = copy.Children[0];
        child1Copy.Should().NotBeSameAs(child1);
        child1Copy.TestProperty.Should().Be("C1");
        child1Copy.Children.Should().HaveCount(2);

        var grandchild11Copy = child1Copy.Children[0];
        grandchild11Copy.Should().NotBeSameAs(grandchild11);
        grandchild11Copy.TestProperty.Should().Be("GC11");
        grandchild11Copy.Children.Should().BeEmpty();

        var grandchild12Copy = child1Copy.Children[1];
        grandchild12Copy.Should().NotBeSameAs(grandchild12);
        grandchild12Copy.TestProperty.Should().Be("GC12");
        grandchild12Copy.Children.Should().BeEmpty();

        var child2Copy = copy.Children[1];
        child2Copy.Should().NotBeSameAs(child2);
        child2Copy.TestProperty.Should().Be("C2");
        child2Copy.Children.Should().HaveCount(1);

        var grandchild21Copy = child2Copy.Children[0];
        grandchild21Copy.Should().NotBeSameAs(grandchild21);
        grandchild21Copy.TestProperty.Should().Be("GC21");
        grandchild21Copy.Children.Should().BeEmpty();

        var child3Copy = copy.Children[2];
        child3Copy.Should().NotBeSameAs(child3);
        child3Copy.Invoking(c => c.TestProperty).Should().Throw<KeyNotFoundException>();
        child3Copy.Children.Should().BeEmpty();
    }

    [Test]
    public void Copy_INodeFactory()
    {
        var child = new BNode { TestProperty = "Child" };
        var parent = new ANode(child);

        var copy = parent.Copy(new CustomNodeFactory());

        copy.Should().BeOfType<CNode>();
        copy.Invoking(c => c.TestProperty).Should().Throw<KeyNotFoundException>();
        copy.Children.Should().HaveCount(1);

        var childCopy = copy.Children[0];
        childCopy.Should().BeOfType<CNode>();
        childCopy.TestProperty.Should().Be("Child");
        childCopy.Children.Should().BeEmpty();
    }

    [Test]
    public void SourcePosition()
    {
        var node = new ANode();
        node.SourcePosition.Should().BeSameAs(MrKWatkins.Ast.Position.SourcePosition.None);

        var position = new BinaryFilePosition(new BinaryFile("Test File", new byte[] { 1, 2, 3 }), 0, 1);
        node.SourcePosition = position;
        node.SourcePosition.Should().BeSameAs(position);
    }

    [Test]
    public void ToStringTest()
    {
        new ANode().ToString().Should().Be("ANode");
        new BNode().ToString().Should().Be("BNode");
        new CNode().ToString().Should().Be("CNode");
    }

    private sealed class CustomNodeFactory : INodeFactory<TestNode>
    {
        public TestNode Create(Type nodeType) => new CNode();
    }
}