using MrKWatkins.Ast.Processing;

namespace MrKWatkins.Ast.Tests.Processing;

public sealed class NodeReplacerTests : TreeTestFixture
{
    [Test]
    public void Process_ReturnOriginal()
    {
        var replacer = new TestNodeReplacer(N12);
        replacer.Process(N12);
        N1.Children.Should().SequenceEqual(N11, N12, N13);
        replacer.Process(N13);
        N1.Children.Should().SequenceEqual(N11, N12, N13);
    }

    [Test]
    public void Process_ReturnNull()
    {
        var replacer = new TestNodeReplacer(null);
        replacer.Process(N12);
        N1.Children.Should().SequenceEqual(N11, N12, N13);
        replacer.Process(N13);
        N1.Children.Should().SequenceEqual(N11, N12, N13);
    }

    [Test]
    public void Process_ReturnNewNode()
    {
        var replacement = new ANode { Name = "Replacement" };
        var replacer = new TestNodeReplacer(replacement);
        replacer.Process(N12);
        N1.Children.Should().SequenceEqual(N11, replacement, N13);
        replacer.Process(N13);
        N1.Children.Should().SequenceEqual(N11, replacement, N13);
    }

    [Test]
    public void Process_ReturnNewNodeWithParent()
    {
        var replacement = new ANode { Name = "Replacement" };
        _ = new BNode(replacement) { Name = "Parent" };
        var replacer = new TestNodeReplacer(replacement);

        replacer.Invoking(p => p.Process(N12))
            .Should().Throw<InvalidOperationException>().That.Should()
            .HaveMessage("Replacement node Replacement already has a parent Parent.");
        replacer.Invoking(p => p.Process(N13)).Should().NotThrow();
    }

    [Test]
    public void WithContext_Process_ReturnOriginal()
    {
        var context = new object();
        var replacer = new TestNodeReplacer<object>(context, N12);
        replacer.Process(context, N12);
        N1.Children.Should().SequenceEqual(N11, N12, N13);
    }

    [Test]
    public void WithContext_Process_ReturnNull()
    {
        var context = new object();
        var replacer = new TestNodeReplacer<object>(context, null);
        replacer.Process(context, N12);
        N1.Children.Should().SequenceEqual(N11, N12, N13);
    }

    [Test]
    public void WithContext_Process_ReturnNewNode()
    {
        var context = new object();
        var replacement = new ANode { Name = "Replacement" };
        var replacer = new TestNodeReplacer<object>(context, replacement);
        replacer.Process(context, N12);
        N1.Children.Should().SequenceEqual(N11, replacement, N13);
    }

    [Test]
    public void WithContext_Process_ReturnNewNodeWithParent()
    {
        var context = new object();
        var replacement = new ANode { Name = "Replacement" };
        _ = new BNode(replacement) { Name = "Parent" };
        var replacer = new TestNodeReplacer<object>(context, replacement);

        replacer.Invoking(p => p.Process(context, N12))
            .Should().Throw<InvalidOperationException>().That.Should()
            .HaveMessage("Replacement node Replacement already has a parent Parent.");
    }

    private sealed class TestNodeReplacer(TestNode? replacement) : NodeReplacer<TestNode, BNode>
    {
        protected override TestNode? Replace(BNode node) => replacement;
    }

    private sealed class TestNodeReplacer<TContext>(TContext expectedContext, TestNode? replacement) : NodeReplacer<TContext, TestNode, BNode>
    {
        protected override TestNode? Replace(TContext context, BNode node)
        {
            context.Should().BeTheSameInstanceAs(expectedContext);
            return replacement;
        }
    }
}