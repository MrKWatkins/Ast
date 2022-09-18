using MrKWatkins.Ast.Enumeration;

namespace MrKWatkins.Ast.Tests.Enumeration;

public sealed class BreadthFirstTests : EnumerationTestFixture
{
    protected override IDescendentEnumerator<TestNode> Enumerator => BreadthFirst<TestNode>.Instance;

    protected override IEnumerable<TestNode> ExpectedOrderWithoutRoot => new[] { N11, N12, N13, N111, N121, N122, N123 };
}