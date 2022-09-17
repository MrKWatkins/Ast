using MrKWatkins.Ast.Enumeration;
using NUnit.Framework.Internal;

namespace MrKWatkins.Ast.Tests.Enumeration;

[UsedImplicitly(ImplicitUseTargetFlags.WithInheritors)] // Rider doesn't recognise sub-classes with no tests.
public abstract class EnumerationTestFixture
{
    protected TestNode N1 { get; private set; } = new ANode { Name = "N1" };
    protected TestNode N11 { get; private set; } = new BNode { Name = "N11" };
    protected TestNode N12 { get; private set; } = new CNode { Name = "N12" };
    protected TestNode N13 { get; private set; } = new ANode { Name = "N13" };
    protected TestNode N111 { get; private set; } = new BNode { Name = "N111" };
    protected TestNode N121 { get; private set; } = new CNode { Name = "N121" };
    protected TestNode N122 { get; private set; } = new ANode { Name = "N122" };
    protected TestNode N123 { get; private set; } = new ANode { Name = "N123" };

    [SetUp]
    public void SetUp()
    {
        // Reset the nodes each test so we can mutate the tree to our heart's content.
        N1 = new ANode { Name = "N1" };
        N11 = new BNode { Name = "N11" };
        N12 = new CNode { Name = "N12" };
        N13 = new ANode { Name = "N13" };
        N111 = new BNode { Name = "N111" };
        N121 = new CNode { Name = "N121" };
        N122 = new ANode { Name = "N122" };
        N123 = new ANode { Name = "N123" };
        N1.Children.Add(N11, N12, N13);
        N11.Children.Add(N111);
        N12.Children.Add(N121, N122, N123);
    }

    protected abstract IDescendentEnumerator<TestNode> Enumerator { get; }

    [Test]
    public void Enumerate_IncludeRoot() => AssertOrder(ExpectedOrderWithRoot, Enumerator.Enumerate(N1));

    protected virtual IEnumerable<TestNode> ExpectedOrderWithRoot => ExpectedOrderWithoutRoot.Prepend(N1);

    [Test]
    public void Enumerate_WithoutRoot() => AssertOrder(ExpectedOrderWithoutRoot, Enumerator.Enumerate(N1, false));

    protected abstract IEnumerable<TestNode> ExpectedOrderWithoutRoot { get; }

    // Custom assertion to give a better error message.
    protected static void AssertOrder([InstantHandle] IEnumerable<TestNode> expected, [InstantHandle] IEnumerable<TestNode> actual)
    {
        var expectedList = expected.ToList();
        var actualList = actual.ToList();

        [Pure]
        static string NodesText(IEnumerable<TestNode> nodes) => string.Join(' ', nodes.Select(n => n.Name.PadRight(4, ' ')));
        
        if (!expectedList.SequenceEqual(actualList))
        {
            throw new NUnitException(
                $"Order did not match expectation.{Environment.NewLine}" +
                $"Expected: {NodesText(expectedList)}{Environment.NewLine}" +
                $"Actual:   {NodesText(actualList)}");
        }
    }
}