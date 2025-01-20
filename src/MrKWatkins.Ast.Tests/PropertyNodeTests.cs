namespace MrKWatkins.Ast.Tests;

public sealed class PropertyNodeTests
{
    [Test]
    public void Copy()
    {
        var grandchild11 = new CNode { Name = "GC11" };
        var grandchild12 = new CNode { Name = "GC12" };
        var child1 = new BNode(grandchild11, grandchild12) { Name = "C1" };

        var grandchild21 = new CNode { Name = "GC21" };
        var child2 = new BNode(grandchild21) { Name = "C2" };

        var child3 = new BNode(); // Test TestProperty not set.

        var parent = new ANode(child1, child2, child3) { Name = "P" };

        var copy = parent.Copy();

        copy.Should().NotBeTheSameInstanceAs(parent);
        copy.Name.Should().Equal("P");
        copy.Children.Should().HaveCount(3);

        var child1Copy = copy.Children[0];
        child1Copy.Should().NotBeTheSameInstanceAs(child1);
        child1Copy.Name.Should().Equal("C1");
        child1Copy.Children.Should().HaveCount(2);

        var grandchild11Copy = child1Copy.Children[0];
        grandchild11Copy.Should().NotBeTheSameInstanceAs(grandchild11);
        grandchild11Copy.Name.Should().Equal("GC11");
        grandchild11Copy.Children.Should().BeEmpty();

        var grandchild12Copy = child1Copy.Children[1];
        grandchild12Copy.Should().NotBeTheSameInstanceAs(grandchild12);
        grandchild12Copy.Name.Should().Equal("GC12");
        grandchild12Copy.Children.Should().BeEmpty();

        var child2Copy = copy.Children[1];
        child2Copy.Should().NotBeTheSameInstanceAs(child2);
        child2Copy.Name.Should().Equal("C2");
        child2Copy.Children.Should().HaveCount(1);

        var grandchild21Copy = child2Copy.Children[0];
        grandchild21Copy.Should().NotBeTheSameInstanceAs(grandchild21);
        grandchild21Copy.Name.Should().Equal("GC21");
        grandchild21Copy.Children.Should().BeEmpty();

        var child3Copy = copy.Children[2];
        child3Copy.Should().NotBeTheSameInstanceAs(child3);
        child3Copy.Invoking(c => c.Name).Should().Throw<KeyNotFoundException>();
        child3Copy.Children.Should().BeEmpty();
    }

    [Test]
    public void Copy_INodeFactory()
    {
        var child = new BNode { Name = "Child" };
        var parent = new ANode(child);

        var copy = parent.Copy(new CustomNodeFactory());

        copy.Should().BeOfType<CNode>();
        copy.Invoking(c => c.Name).Should().Throw<KeyNotFoundException>();
        copy.Children.Should().HaveCount(1);

        var childCopy = copy.Children[0];
        childCopy.Should().BeOfType<CNode>();
        childCopy.Name.Should().Equal("Child");
        childCopy.Children.Should().BeEmpty();
    }

    [Test]
    public void EnumerateProperties()
    {
        var node = new ANode { Name = "Child" };
        var expected = new KeyValuePair<string, object>[]
        {
            new ("Name", "Child")
        };

        node.EnumerateProperties().Should().SequenceEqual(expected);
    }

    private sealed class CustomNodeFactory : INodeFactory<TestNode>
    {
        public TestNode Create(Type nodeType) => new CNode();
    }
}