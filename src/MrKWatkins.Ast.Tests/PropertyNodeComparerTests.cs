namespace MrKWatkins.Ast.Tests;

public sealed class PropertyNodeComparerTests
{
    [Test]
    public void Equals_EqualNodes()
    {
        var x = new ANode { Name = "Test" };
        var y = new ANode { Name = "Test" };

        PropertyNodeComparer<TestNode>.Instance.Equals(x, y).Should().BeTrue();
    }

    [Test]
    public void Equals_SameInstance()
    {
        var node = new ANode { Name = "Test" };

        PropertyNodeComparer<TestNode>.Instance.Equals(node, node).Should().BeTrue();
    }

    [Test]
    public void Equals_Null()
    {
        var node = new ANode { Name = "Test" };

        PropertyNodeComparer<TestNode>.Instance.Equals(null, null).Should().BeTrue();
        PropertyNodeComparer<TestNode>.Instance.Equals(node, null).Should().BeFalse();
        PropertyNodeComparer<TestNode>.Instance.Equals(null, node).Should().BeFalse();
    }

    [Test]
    public void Equals_NodeTypesDiffer()
    {
        var x = new ANode { Name = "Test" };
        var y = new BNode { Name = "Test" };

        PropertyNodeComparer<TestNode>.Instance.Equals(x, y).Should().BeFalse();
    }

    [Test]
    public void Equals_PropertiesDiffer()
    {
        var x = new ANode { Name = "Test" };
        var y = new ANode { Name = "Other" };

        PropertyNodeComparer<TestNode>.Instance.Equals(x, y).Should().BeFalse();
    }

    [Test]
    public void Equals_PropertiesNeverCreated()
    {
        var x = new ANode();
        var y = new ANode();

        PropertyNodeComparer<TestNode>.Instance.Equals(x, y).Should().BeTrue();

        var z = new ANode { Name = "Test" };

        PropertyNodeComparer<TestNode>.Instance.Equals(x, z).Should().BeFalse();
    }

    [Test]
    public void GetHashCode_EqualForEqualNodes()
    {
        var x = new ANode { Name = "Test" };
        var y = new ANode { Name = "Test" };

        PropertyNodeComparer<TestNode>.Instance.GetHashCode(x).Should().Equal(PropertyNodeComparer<TestNode>.Instance.GetHashCode(y));
    }

    [Test]
    public void GetHashCode_PropertiesNeverCreated()
    {
        var x = new ANode();
        var y = new ANode();

        PropertyNodeComparer<TestNode>.Instance.GetHashCode(x).Should().Equal(PropertyNodeComparer<TestNode>.Instance.GetHashCode(y));
    }

    [Test]
    public void UsableWithLinq()
    {
        var nodes = new TestNode[] { new ANode { Name = "Test" }, new ANode { Name = "Test" }, new ANode { Name = "Other" }, new BNode { Name = "Test" } };

        var distinct = nodes.Distinct(PropertyNodeComparer<TestNode>.Instance).ToList();
        distinct.Should().SequenceEqual(nodes[0], nodes[2], nodes[3]);
    }
}