using MrKWatkins.Ast.Traversal;

namespace MrKWatkins.Ast.Tests;

public sealed partial class NodeTests : TreeTestFixture
{
    [Test]
    public void Enumerate_BreadthFirst_IncludeRoot() =>
        TestNode.Traverse.BreadthFirst(N1)
            .Should().BeEquivalentTo(BreadthFirstTraversal<TestNode>.Instance.Enumerate(N1));

    [Test]
    public void Enumerate_BreadthFirst_WithoutRoot() =>
        TestNode.Traverse.BreadthFirst(N1, false)
            .Should().BeEquivalentTo(BreadthFirstTraversal<TestNode>.Instance.Enumerate(N1, false));

    [Test]
    public void Enumerate_BreadthFirst_WithShouldProcessChildren() =>
        TestNode.Traverse.BreadthFirst(N1, false, p => p != N11)
            .Should().BeEquivalentTo(BreadthFirstTraversal<TestNode>.Instance.Enumerate(N1, false, p => p != N11));

    [Test]
    public void Enumerate_DepthFirstPreOrder_IncludeRoot() =>
        TestNode.Traverse.DepthFirstPreOrder(N1)
            .Should().BeEquivalentTo(DepthFirstPreOrderTraversal<TestNode>.Instance.Enumerate(N1));

    [Test]
    public void Enumerate_DepthFirstPreOrder_WithoutRoot() =>
        TestNode.Traverse.DepthFirstPreOrder(N1, false)
            .Should().BeEquivalentTo(DepthFirstPreOrderTraversal<TestNode>.Instance.Enumerate(N1, false));

    [Test]
    public void Enumerate_DepthFirstPreOrder_WithShouldProcessChildren() =>
        TestNode.Traverse.DepthFirstPreOrder(N1, false, p => p != N11)
            .Should().BeEquivalentTo(DepthFirstPreOrderTraversal<TestNode>.Instance.Enumerate(N1, false, p => p != N11));

    [Test]
    public void Enumerate_DepthFirstPostOrder_IncludeRoot() =>
        TestNode.Traverse.DepthFirstPostOrder(N1)
            .Should().BeEquivalentTo(DepthFirstPostOrderTraversal<TestNode>.Instance.Enumerate(N1));

    [Test]
    public void Enumerate_DepthFirstPostOrder_WithoutRoot() =>
        TestNode.Traverse.DepthFirstPostOrder(N1, false)
            .Should().BeEquivalentTo(DepthFirstPostOrderTraversal<TestNode>.Instance.Enumerate(N1, false));

    [Test]
    public void Enumerate_DepthFirstPostOrder_WithShouldProcessChildren() =>
        TestNode.Traverse.DepthFirstPostOrder(N1, false, p => p != N11)
            .Should().BeEquivalentTo(DepthFirstPostOrderTraversal<TestNode>.Instance.Enumerate(N1, false, p => p != N11));

}