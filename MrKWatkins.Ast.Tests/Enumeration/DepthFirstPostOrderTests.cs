using MrKWatkins.Ast.Enumeration;

namespace MrKWatkins.Ast.Tests.Enumeration;

public sealed class DepthFirstPostOrderTests : DepthFirstTestFixture
{
    protected override IDescendentEnumerator<TestNode> Enumerator => DepthFirstPostOrder<TestNode>.Instance;

    protected override IEnumerable<TestNode> ExpectedOrderWithRoot => new[] { N111, N11, N121, N122, N123, N12, N13, N1 };
    
    protected override IEnumerable<TestNode> ExpectedOrderWithoutRoot => new[] { N111, N11, N121, N122, N123, N12, N13 };
}
