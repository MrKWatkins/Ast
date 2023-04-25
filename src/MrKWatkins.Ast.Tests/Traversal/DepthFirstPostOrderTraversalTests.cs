using MrKWatkins.Ast.Traversal;

namespace MrKWatkins.Ast.Tests.Traversal;

public sealed class DepthFirstPostOrderTraversalTests : TraversalTestFixture
{
    protected override ITraversal<TestNode> Traversal => DepthFirstPostOrderTraversal<TestNode>.Instance;

    protected override IEnumerable<TestNode> ExpectedOrderWithRoot => new[] { N111, N11, N121, N122, N123, N12, N13, N1 };
    
    protected override IEnumerable<TestNode> ExpectedOrderWithoutRoot => new[] { N111, N11, N121, N122, N123, N12, N13 };
}
