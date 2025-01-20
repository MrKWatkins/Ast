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
    [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
    public void Constructor_IEnumerable()
    {
        IEnumerable<TestNode> children = [new ANode(), new BNode(), new CNode()];

        var root = new ANode(children);
        root.HasParent.Should().BeFalse();
        root.Invoking(r => r.Parent).Should().Throw<InvalidOperationException>();

        root.Children.Should().SequenceEqual(children);
        root.Children.Should().OnlyContain(c => c.HasParent);
        root.Children.Should().OnlyContain(c => ReferenceEquals(c.Parent, root));
    }

    [Test]
    public void Constructor_Array()
    {
        var children = new TestNode[] { new ANode(), new BNode(), new CNode() };

        var root = new ANode(children);
        root.HasParent.Should().BeFalse();

        root.Children.Should().SequenceEqual(children);
        root.Children.Should().OnlyContain(c => c.HasParent);
        root.Children.Should().OnlyContain(c => ReferenceEquals(c.Parent, root));
    }

    [Test]
    public void Properties()
    {
        var node = new ANode();
        node.Invoking(n => n.Name).Should().Throw<KeyNotFoundException>();
        node.Name = "Test";
        node.Name.Should().Equal("Test");
    }

    [Test]
    public void Constructor_CannotAddNodesThatAlreadyHaveParents()
    {
        var root = new ANode(new BNode());

        AssertThat.Invoking(() => new ANode(root.Children[0]))
            .Should().Throw<InvalidOperationException>()
            .That.Should().HaveMessage("Node is already the child of another node.");
    }

    [Test]
    public void SourcePosition()
    {
        var node = new ANode();
        node.SourcePosition.Should().BeTheSameInstanceAs(MrKWatkins.Ast.Position.SourcePosition.None);

        var position = new BinaryFilePosition(new BinaryFile("Test File", [1, 2, 3]), 0, 1);
        node.SourcePosition = position;
        node.SourcePosition.Should().BeTheSameInstanceAs(position);
    }

    [Test]
    public void ToStringTest()
    {
        new ANode().ToString().Should().Equal("ANode");
        new BNode().ToString().Should().Equal("BNode");
        new CNode().ToString().Should().Equal("CNode");
    }
}