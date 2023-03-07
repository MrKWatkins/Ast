using MrKWatkins.Ast.Enumeration;

namespace MrKWatkins.Ast.Tests.Enumeration;

public sealed class DepthFirstPreOrderTests : EnumerationTestFixture
{
    protected override IDescendentEnumerator<TestNode> Enumerator => DepthFirstPreOrder<TestNode>.Instance;

    protected override IEnumerable<TestNode> ExpectedOrderWithoutRoot => new[] { N11, N111, N12, N121, N122, N123, N13 };
}
