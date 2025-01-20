using System.Collections;

namespace MrKWatkins.Ast.Tests;

public sealed partial class ChildrenTests
{
    private TestNode P { get; set; } = null!;
    private TestNode C1 { get; set; } = null!;
    private TestNode C2 { get; set; } = null!;
    private TestNode C3 { get; set; } = null!;
    private TestNode New1 { get; set; } = null!;
    private TestNode New2 { get; set; } = null!;
    private IReadOnlyList<TestNode> Children { get; set; } = null!;

    [SetUp]
    public void SetUp()
    {
        // Reset the nodes each test so we can mutate the tree to our heart's content.
        P = new ANode { Name = nameof(P) };
        C1 = new ANode { Name = nameof(C1) };
        C2 = new BNode { Name = nameof(C2) };
        C3 = new CNode { Name = nameof(C3) };
        New1 = new CNode { Name = nameof(New1) };
        New2 = new CNode { Name = nameof(New2) };
        Children = [C1, C2, C3];
        P.Children.Add(Children);
    }

    [Test]
    public void Enumeration() => TestEnumeration(_ => { }, [C1, C2, C3]);

    [TestCase(nameof(C1))]
    [TestCase(nameof(C2))]
    [TestCase(nameof(C3))]
    public void Enumeration_RemoveChildDuringEnumeration(string toRemove) => TestEnumeration(
        child =>
        {
            if (child.Name == toRemove)
            {
                P.Children.Remove(child);
            }
        },
        // First enumeration contains all children as we remove after enumerating it.
        Children,
        // Second enumeration will be missing the removed child.
        Children.Where(c => c.Name != toRemove));

    [TestCase(nameof(C1))]
    [TestCase(nameof(C2))]
    [TestCase(nameof(C3))]
    public void Enumeration_ReplaceChildDuringEnumeration(string toRemove) => TestEnumeration(
        child =>
        {
            if (child.Name == toRemove)
            {
                child.ReplaceWith(New1);
            }
        },
        // First enumeration will contain toRemove and not New as we replace after enumerating it.
        Children,
        // Second enumeration will have the replaced child.
        Children.Select(c => c.Name == toRemove ? New1 : c));

    [TestCase(nameof(C1))]
    [TestCase(nameof(C2))]
    [TestCase(nameof(C3))]
    public void Enumeration_AddNextSiblingDuringEnumeration(string toAdd) => TestEnumeration(
        child =>
        {
            if (child.Name == toAdd)
            {
                child.AddNextSibling(New1);
            }
        },
        // First and second enumerations will contain the new sibling as we add it after the child.
        Children.SelectMany(c => c.Name == toAdd ? [c, New1] : new[] { c }));

    [TestCase(nameof(C1))]
    [TestCase(nameof(C2))]
    [TestCase(nameof(C3))]
    public void Enumeration_AddMultipleNextSiblingsDuringEnumeration(string toAdd) => TestEnumeration(
        child =>
        {
            if (child.Name == toAdd)
            {
                child.AddNextSibling(New2);
                child.AddNextSibling(New1);
            }
        },
        // First and second enumerations will contain the new siblings as we add them after the child.
        Children.SelectMany(c => c.Name == toAdd ? [c, New1, New2] : new[] { c }));

    [TestCase(nameof(C1))]
    [TestCase(nameof(C2))]
    [TestCase(nameof(C3))]
    public void Enumeration_AddPreviousSiblingDuringEnumeration(string toAdd) => TestEnumeration(
        child =>
        {
            if (child.Name == toAdd)
            {
                child.AddPreviousSibling(New1);
            }
        },
        // First enumerations will not contain the new sibling as we add it before the child.
        Children,
        // Second enumerations will contain the new sibling.
        Children.SelectMany(c => c.Name == toAdd ? [New1, c] : new[] { c }));

    [TestCase(nameof(C1))]
    [TestCase(nameof(C2))]
    [TestCase(nameof(C3))]
    public void Enumeration_AddMultiplePreviousSiblingsDuringEnumeration(string toAdd) => TestEnumeration(
        child =>
        {
            if (child.Name == toAdd)
            {
                child.AddPreviousSibling(New2);
                child.AddPreviousSibling(New1);
            }
        },
        // First enumerations will not contain the new sibling sas we add them before the child.
        Children,
        // Second enumerations will contain the new siblings.
        Children.SelectMany(c => c.Name == toAdd ? [New2, New1, c] : new[] { c }));

    [TestCase(nameof(C1))]
    [TestCase(nameof(C2))]
    public void Enumeration_RemoveNextSiblingDuringEnumeration(string toRemove)
    {
        TestNode? removed = null;
        TestEnumeration(
            child =>
            {
                if (child.Name == toRemove)
                {
                    removed = child.RemoveNextSibling();
                }
            },
            // First and second enumerations will not contain the removed sibling as we remove it after the child.
            Children.Where(c => c != (removed ?? throw new InvalidOperationException("No node removed"))));
    }

    [Test]
    public void Enumeration_RemoveMultipleNextSiblingsDuringEnumeration() =>
        TestEnumeration(
            child =>
            {
                if (child == C1)
                {
                    child.RemoveNextSibling();
                    child.RemoveNextSibling();
                }
            },
            // First and second enumerations will not contain the removed sibling as we remove it after the child.
            [C1]);

    [TestCase(nameof(C2))]
    [TestCase(nameof(C3))]
    public void Enumeration_RemovePreviousSiblingDuringEnumeration(string toRemove)
    {
        TestNode? removed = null;
        TestEnumeration(
            child =>
            {
                if (child.Name == toRemove)
                {
                    removed = child.RemovePreviousSibling();
                }
            },
            // First enumeration will contain the removed sibling as it was removed after enumerating.
            Children,
            // Second enumeration will not contain the removed sibling.
            Children.Where(c => c != (removed ?? throw new InvalidOperationException("No node removed"))));
    }

    [Test]
    public void Enumeration_RemoveMultiplePreviousSiblingsDuringEnumeration() =>
        TestEnumeration(
            child =>
            {
                if (child == C3)
                {
                    child.RemovePreviousSibling();
                    child.RemovePreviousSibling();
                }
            },
            // First enumeration will contain the removed siblings as they were removed after enumerating.
            Children,
            // Second enumeration will not contain the removed siblings.
            [C3]);

    private void TestEnumeration(Action<TestNode> action, [InstantHandle] IEnumerable<TestNode> expectedFirstEnumeration, [InstantHandle] IEnumerable<TestNode>? expectedSecondEnumeration = null)
    {
        var firstEnumeration = new List<TestNode>(4);
        foreach (var child in P.Children)
        {
            firstEnumeration.Add(child);
            action(child);
        }

        firstEnumeration.Should().SequenceEqual(expectedFirstEnumeration);
        P.Children.Should().SequenceEqual(expectedSecondEnumeration ?? expectedFirstEnumeration);
    }

    [Test]
    public void Enumerate_TooManyMutationsHaltsEnumeration()
    {
        AssertThat.Invoking(
                () =>
                {
                    foreach (var node in P.Children)
                    {
                        if (node == C2)
                        {
                            node.RemovePreviousSibling();
                            node.RemoveFromParent();
                        }
                    }
                })
            .Should().Throw<InvalidOperationException>()
            .That.Should().HaveMessage("Node C2 has been removed from parent during enumeration at the same time as other mutations. Cannot determine sensible place to continue enumeration.");
    }

    [Test]
    public void GetEnumerator()
    {
        using var enumerator = P.Children.GetEnumerator();

        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().BeTheSameInstanceAs(C1);

        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().BeTheSameInstanceAs(C2);

        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().BeTheSameInstanceAs(C3);

        enumerator.MoveNext().Should().BeFalse();
    }

    [Test]
    public void GetEnumerator_Reset()
    {
        using var enumerator = P.Children.GetEnumerator();

        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().BeTheSameInstanceAs(C1);

        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().BeTheSameInstanceAs(C2);

        enumerator.Reset();

        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().BeTheSameInstanceAs(C1);

        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().BeTheSameInstanceAs(C2);

        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().BeTheSameInstanceAs(C3);

        enumerator.MoveNext().Should().BeFalse();
    }

    [Test]
    public void GetEnumerator_Untyped()
    {
        var enumerator = ((IEnumerable) P.Children).GetEnumerator();

        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().BeTheSameInstanceAs(C1);

        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().BeTheSameInstanceAs(C2);

        enumerator.MoveNext().Should().BeTrue();
        enumerator.Current.Should().BeTheSameInstanceAs(C3);

        enumerator.MoveNext().Should().BeFalse();

        (enumerator as IDisposable)?.Dispose();
    }
}