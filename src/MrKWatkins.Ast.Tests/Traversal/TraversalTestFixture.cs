using MrKWatkins.Ast.Traversal;

namespace MrKWatkins.Ast.Tests.Traversal;

[UsedImplicitly(ImplicitUseTargetFlags.WithInheritors)] // Rider doesn't recognise sub-classes with no tests.
public abstract class TraversalTestFixture : TreeTestFixture
{
    protected abstract ITraversal<TestNode> Traversal { get; }

    [Test]
    public void Enumerate_IncludeRoot() => Traversal.Enumerate(N1).Should().HaveSameOrderAs(ExpectedOrderWithRoot);

    protected virtual IEnumerable<TestNode> ExpectedOrderWithRoot => ExpectedOrderWithoutRoot.Prepend(N1);

    [Test]
    public void Enumerate_WithoutRoot() => Traversal.Enumerate(N1, false).Should().HaveSameOrderAs(ExpectedOrderWithoutRoot);

    protected abstract IEnumerable<TestNode> ExpectedOrderWithoutRoot { get; }

    [Test]
    public void Enumerate_IncludeRoot_ShouldEnumerateDescendents() => 
        Traversal.Enumerate(N1, true, c => c != N12).Should().HaveSameOrderAs(ExpectedOrderWithRoot.Except(N12.Descendents));

    [Test]
    public void Enumerate_WithoutRoot_ShouldEnumerateDescendents() => 
        Traversal.Enumerate(N1, false, c => c != N12).Should().HaveSameOrderAs(ExpectedOrderWithoutRoot.Except(N12.Descendents));

    [Test]
    public void Enumerate_IncludeRoot_ShouldEnumerateDescendents_ExcludeDescendentsOfRoot() => 
        Traversal.Enumerate(N1, true, c => c != N1).Should().HaveSameOrderAs(N1);

    [Test]
    public void Enumerate_WithoutRoot_ShouldEnumerateDescendents_ExcludeDescendentsOfRoot() =>
        Traversal.Enumerate(N1, false, c => c != N1).Should().BeEmpty();

    [Test]
    public void Enumerate_ReplaceDuringEnumeration()
    {
        var replacement = new ANode { Name = "NRep" };

        var actual = new List<TestNode>();
        foreach (var node in Traversal.Enumerate(N1))
        {
            actual.Add(node);

            if (node == N122)
            {
                node.ReplaceWith(replacement);
            }
        }

        // Replacement won't appear in the expected nodes because it was yielded before being replaced.
        actual.Should().HaveSameOrderAs(ExpectedOrderWithRoot);
        
        // Replacement will appear on a subsequent enumeration because the tree has been mutated.
        var expected = ExpectedOrderWithRoot.Select(n => n == N122 ? replacement : n);
        Traversal.Enumerate(N1).Should().HaveSameOrderAs(expected);
    }
    
    [Test]
    public void Enumerate_RemoveDuringEnumeration()
    {
        var actual = new List<TestNode>();
        foreach (var node in Traversal.Enumerate(N1))
        {
            actual.Add(node);

            if (node == N122)
            {
                node.RemoveFromParent();
            }
        }

        // Node will still appear in the expected nodes because it was yielded before being removed.
        actual.Should().HaveSameOrderAs(ExpectedOrderWithRoot);
        
        // Node will be removed on subsequent enumeration because the tree has been mutated.
        var expected = ExpectedOrderWithRoot.Where(n => n != N122);
        Traversal.Enumerate(N1).Should().HaveSameOrderAs(expected);
    }
    
    [Test]
    public void Enumerate_AddNextSiblingDuringEnumeration()
    {
        var sibling = new ANode { Name = "NSib" };

        var actual = new List<TestNode>();
        foreach (var node in Traversal.Enumerate(N1))
        {
            actual.Add(node);

            if (node == N122)
            {
                node.AddNextSibling(sibling);
            }
        }
        
        // Sibling will appear in the enumeration as we insert it after the node.
        var expected = ExpectedOrderWithRoot.SelectMany(n => n == N122 ? new [] { N122, sibling } : new [] { n }).ToList();
        actual.Should().HaveSameOrderAs(expected);
        
        // Tree has been mutated so sibling will appear in subsequent enumerations.
        Traversal.Enumerate(N1).Should().HaveSameOrderAs(expected);
    }
    
    [Test]
    public void Enumerate_RemoveNextSiblingDuringEnumeration()
    {
        var actual = new List<TestNode>();
        foreach (var node in Traversal.Enumerate(N1))
        {
            actual.Add(node);

            if (node == N122)
            {
                node.RemoveNextSibling();
            }
        }
        
        // Next sibling will not appear in the enumeration as it comes after the node.
        var expected = ExpectedOrderWithRoot.Where(n => n != N123).ToList();
        actual.Should().HaveSameOrderAs(expected);
        
        // Tree has been mutated so sibling will not appear in subsequent enumerations.
        Traversal.Enumerate(N1).Should().HaveSameOrderAs(expected);
    }
    
    [Test]
    public void Enumerate_AddPreviousSiblingDuringEnumeration()
    {
        var sibling = new ANode { Name = "PSib" };

        var actual = new List<TestNode>();
        foreach (var node in Traversal.Enumerate(N1))
        {
            actual.Add(node);

            if (node == N122)
            {
                node.AddPreviousSibling(sibling);
            }
        }
        
        // Sibling will not appear in the enumeration as we insert it before the node.
        actual.Should().HaveSameOrderAs(ExpectedOrderWithRoot);
        
        // Tree has been mutated so sibling will appear in subsequent enumerations.
        var expected = ExpectedOrderWithRoot.SelectMany(n => n == N122 ? new [] { sibling, N122 } : new [] { n }).ToList();
        Traversal.Enumerate(N1).Should().HaveSameOrderAs(expected);
    }
    
    [Test]
    public void Enumerate_RemovePreviousSiblingDuringEnumeration()
    {
        var actual = new List<TestNode>();
        foreach (var node in Traversal.Enumerate(N1))
        {
            actual.Add(node);

            if (node == N122)
            {
                node.RemovePreviousSibling();
            }
        }
        
        // Sibling will appear in the enumeration as it is removed after being enumerated.
        actual.Should().HaveSameOrderAs(ExpectedOrderWithRoot);

        // Tree has been mutated so sibling will not appear in subsequent enumerations.
        var expected = ExpectedOrderWithRoot.Where(n => n != N121).ToList();
        Traversal.Enumerate(N1).Should().HaveSameOrderAs(expected);
    }
    
    [Test]
    public void Enumerate_TooManyMutationsHaltsEnumeration()
    {
        FluentActions.Invoking(() =>
        {
            foreach (var node in Traversal.Enumerate(N1))
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
}