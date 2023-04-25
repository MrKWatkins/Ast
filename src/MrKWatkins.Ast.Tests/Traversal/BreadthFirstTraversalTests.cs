using MrKWatkins.Ast.Traversal;

namespace MrKWatkins.Ast.Tests.Traversal;

public sealed class BreadthFirstTraversalTests : TraversalTestFixture
{
    protected override ITraversal<TestNode> Traversal => BreadthFirstTraversal<TestNode>.Instance;

    protected override IEnumerable<TestNode> ExpectedOrderWithoutRoot => new[] { N11, N12, N13, N111, N121, N122, N123 };
}