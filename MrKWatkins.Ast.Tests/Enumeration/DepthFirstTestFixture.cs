namespace MrKWatkins.Ast.Tests.Enumeration;

public abstract class DepthFirstTestFixture : EnumerationTestFixture
{
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
}