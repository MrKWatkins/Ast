using MrKWatkins.Ast.Traversal;

namespace MrKWatkins.Ast.Tests.Traversal;

public sealed class DepthFirstPreOrderTraversalTests : TraversalTestFixture
{
    protected override ITraversal<TestNode> Traversal => DepthFirstPreOrderTraversal<TestNode>.Instance;

    protected override IEnumerable<TestNode> ExpectedOrderWithoutRoot => new[] { N11, N111, N12, N121, N122, N123, N13 };
}