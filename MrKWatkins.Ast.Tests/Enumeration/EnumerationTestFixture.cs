using MrKWatkins.Ast.Enumeration;
using NUnit.Framework.Internal;

namespace MrKWatkins.Ast.Tests.Enumeration;

[UsedImplicitly(ImplicitUseTargetFlags.WithInheritors)] // Rider doesn't recognise sub-classes with no tests.
public abstract class EnumerationTestFixture
{
    protected TestNode N1 { get; private set; } = null!;
    protected TestNode N11 { get; private set; } = null!;
    protected TestNode N12 { get; private set; } = null!;
    protected TestNode N13 { get; private set; } = null!;
    protected TestNode N111 { get; private set; } = null!;
    protected TestNode N121 { get; private set; } = null!;
    protected TestNode N122 { get; private set; } = null!;
    protected TestNode N123 { get; private set; } = null!;

    [SetUp]
    public void SetUp()
    {
        // Reset the nodes each test so we can mutate the tree to our heart's content.
        N1 = new ANode { Name = nameof(N1) };
        N11 = new BNode { Name = nameof(N11) };
        N12 = new CNode { Name = nameof(N12) };
        N13 = new ANode { Name = nameof(N13) };
        N111 = new BNode { Name = nameof(N111) };
        N121 = new CNode { Name = nameof(N121) };
        N122 = new ANode { Name = nameof(N122) };
        N123 = new ANode { Name = nameof(N123) };
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

    [Test]
    public void Enumerate_IncludeRoot_ShouldEnumerateDescendents() => 
        AssertOrder(ExpectedOrderWithRoot.Except(N12.Descendents), Enumerator.Enumerate(N1, true, c => c != N12));

    [Test]
    public void Enumerate_WithoutRoot_ShouldEnumerateDescendents() => 
        AssertOrder(ExpectedOrderWithoutRoot.Except(N12.Descendents), Enumerator.Enumerate(N1, false, c => c != N12));

    [Test]
    public void Enumerate_IncludeRoot_ShouldEnumerateDescendents_ExcludeDescendentsOfRoot() => 
        AssertOrder(new [] { N1 }, Enumerator.Enumerate(N1, true, c => c != N1));

    [Test]
    public void Enumerate_WithoutRoot_ShouldEnumerateDescendents_ExcludeDescendentsOfRoot() => 
        AssertOrder(Enumerable.Empty<TestNode>(), Enumerator.Enumerate(N1, false, c => c != N1));

    [Test]
    public void Enumerate_ReplaceDuringEnumeration()
    {
        var replacement = new ANode { Name = "NRep" };

        var actual = new List<TestNode>();
        foreach (var node in Enumerator.Enumerate(N1))
        {
            actual.Add(node);

            if (node == N122)
            {
                node.ReplaceWith(replacement);
            }
        }

        // Replacement won't appear in the expected nodes because it was yielded before being replaced.
        AssertOrder(ExpectedOrderWithRoot, actual);
        
        // Replacement will appear on a subsequent enumeration because the tree has been mutated.
        var expected = ExpectedOrderWithRoot.Select(n => n == N122 ? replacement : n);

        AssertOrder(expected, Enumerator.Enumerate(N1));
    }
    
    [Test]
    public void Enumerate_RemoveDuringEnumeration()
    {
        var actual = new List<TestNode>();
        foreach (var node in Enumerator.Enumerate(N1))
        {
            actual.Add(node);

            if (node == N122)
            {
                node.RemoveFromParent();
            }
        }

        // Node will still appear in the expected nodes because it was yielded before being removed.
        AssertOrder(ExpectedOrderWithRoot, actual);
        
        // Node will be removed on subsequent enumeration because the tree has been mutated.
        var expected = ExpectedOrderWithRoot.Where(n => n != N122);

        AssertOrder(expected, Enumerator.Enumerate(N1));
    }
    
    [Test]
    public void Enumerate_AddNextSiblingDuringEnumeration()
    {
        var sibling = new ANode { Name = "NSib" };

        var actual = new List<TestNode>();
        foreach (var node in Enumerator.Enumerate(N1))
        {
            actual.Add(node);

            if (node == N122)
            {
                node.AddNextSibling(sibling);
            }
        }
        
        // Sibling will appear in the enumeration as we insert it after the node.
        var expected = ExpectedOrderWithRoot.SelectMany(n => n == N122 ? new [] { N122, sibling } : new [] { n }).ToList();
        AssertOrder(expected, actual);
        
        // Tree has been mutated so sibling will appear in subsequent enumerations.
        AssertOrder(expected, Enumerator.Enumerate(N1));
    }
    
    [Test]
    public void Enumerate_RemoveNextSiblingDuringEnumeration()
    {
        var actual = new List<TestNode>();
        foreach (var node in Enumerator.Enumerate(N1))
        {
            actual.Add(node);

            if (node == N122)
            {
                node.RemoveNextSibling();
            }
        }
        
        // Next sibling will not appear in the enumeration as it comes after the node.
        var expected = ExpectedOrderWithRoot.Where(n => n != N123).ToList();
        AssertOrder(expected, actual);
        
        // Tree has been mutated so sibling will not appear in subsequent enumerations.
        AssertOrder(expected, Enumerator.Enumerate(N1));
    }
    
    [Test]
    public void Enumerate_AddPreviousSiblingDuringEnumeration()
    {
        var sibling = new ANode { Name = "PSib" };

        var actual = new List<TestNode>();
        foreach (var node in Enumerator.Enumerate(N1))
        {
            actual.Add(node);

            if (node == N122)
            {
                node.AddPreviousSibling(sibling);
            }
        }
        
        // Sibling will not appear in the enumeration as we insert it before the node.
        AssertOrder(ExpectedOrderWithRoot, actual);
        
        // Tree has been mutated so sibling will appear in subsequent enumerations.
        var expected = ExpectedOrderWithRoot.SelectMany(n => n == N122 ? new [] { sibling, N122 } : new [] { n }).ToList();
        AssertOrder(expected, Enumerator.Enumerate(N1));
    }
    
    [Test]
    public void Enumerate_RemovePreviousSiblingDuringEnumeration()
    {
        var actual = new List<TestNode>();
        foreach (var node in Enumerator.Enumerate(N1))
        {
            actual.Add(node);

            if (node == N122)
            {
                node.RemovePreviousSibling();
            }
        }
        
        // Sibling will appear in the enumeration as it is removed after being enumerated.
        AssertOrder(ExpectedOrderWithRoot, actual);
        
        // Tree has been mutated so sibling will not appear in subsequent enumerations.
        var expected = ExpectedOrderWithRoot.Where(n => n != N121).ToList();
        AssertOrder(expected, Enumerator.Enumerate(N1));
    }
    
    [Test]
    public void Enumerate_TooManyMutationsHaltsEnumeration()
    {
        FluentActions.Invoking(() =>
        {
            foreach (var node in Enumerator.Enumerate(N1))
            {
                if (node == N122)
                {
                    node.RemovePreviousSibling();
                    node.RemoveFromParent();
                }
            }
        })
            .Should().Throw<InvalidOperationException>()
            .WithMessage("Node N122 has been removed from parent during enumeration at the same time as other mutations. Cannot determine sensible place to continue enumeration.");
    }
    
    // Custom assertion to give a better error message.
    private static void AssertOrder([InstantHandle] IEnumerable<TestNode> expected, [InstantHandle] IEnumerable<TestNode> actual)
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