using MrKWatkins.Ast.Enumeration;

namespace MrKWatkins.Ast.Tests;

public sealed partial class NodeTests
{
    private static readonly TestNode N1 = new ANode { Name = "N1" };
    private static readonly TestNode N11 = new BNode { Name = "N11" };
    private static readonly TestNode N12 = new CNode { Name = "N12" };
    private static readonly TestNode N111 = new BNode { Name = "N111" };
    private static readonly TestNode N121 = new CNode { Name = "N121" };

    static NodeTests()
    {
        N1.Children.Add(N11, N12);
        N11.Children.Add(N111);
        N12.Children.Add(N121);
    }

    [Test]
    public void Enumerate_BreadthFirst_IncludeRoot() => 
        TestNode.Enumerate.BreadthFirst(N1)
            .Should().BeEquivalentTo(BreadthFirst<TestNode>.Instance.Enumerate(N1));
    
    [Test]
    public void Enumerate_BreadthFirst_WithoutRoot() => 
        TestNode.Enumerate.BreadthFirst(N1, false)
            .Should().BeEquivalentTo(BreadthFirst<TestNode>.Instance.Enumerate(N1, false));
    
    [Test]
    public void Enumerate_BreadthFirst_WithShouldProcessChildren() => 
        TestNode.Enumerate.BreadthFirst(N1, false, p => p != N11)
            .Should().BeEquivalentTo(BreadthFirst<TestNode>.Instance.Enumerate(N1, false, p => p != N11));
    
    [Test]
    public void Enumerate_DepthFirstPreOrder_IncludeRoot() => 
        TestNode.Enumerate.DepthFirstPreOrder(N1)
            .Should().BeEquivalentTo(DepthFirstPreOrder<TestNode>.Instance.Enumerate(N1));
    
    [Test]
    public void Enumerate_DepthFirstPreOrder_WithoutRoot() => 
        TestNode.Enumerate.DepthFirstPreOrder(N1, false)
            .Should().BeEquivalentTo(DepthFirstPreOrder<TestNode>.Instance.Enumerate(N1, false));

    [Test]
    public void Enumerate_DepthFirstPreOrder_WithShouldProcessChildren() => 
        TestNode.Enumerate.DepthFirstPreOrder(N1, false, p => p != N11)
            .Should().BeEquivalentTo(DepthFirstPreOrder<TestNode>.Instance.Enumerate(N1, false, p => p != N11));

    [Test]
    public void Enumerate_DepthFirstPostOrder_IncludeRoot() => 
        TestNode.Enumerate.DepthFirstPostOrder(N1)
            .Should().BeEquivalentTo(DepthFirstPostOrder<TestNode>.Instance.Enumerate(N1));
    
    [Test]
    public void Enumerate_DepthFirstPostOrder_WithoutRoot() => 
        TestNode.Enumerate.DepthFirstPostOrder(N1, false)
            .Should().BeEquivalentTo(DepthFirstPostOrder<TestNode>.Instance.Enumerate(N1, false));
    
    [Test]
    public void Enumerate_DepthFirstPostOrder_WithShouldProcessChildren() => 
        TestNode.Enumerate.DepthFirstPostOrder(N1, false, p => p != N11)
            .Should().BeEquivalentTo(DepthFirstPostOrder<TestNode>.Instance.Enumerate(N1, false, p => p != N11));

}