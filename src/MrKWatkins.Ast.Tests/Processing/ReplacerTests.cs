using MrKWatkins.Ast.Processing;

namespace MrKWatkins.Ast.Tests.Processing;

public sealed class ReplacerTests : TreeTestFixture
{
    [Test]
    public void Process_ReturnOriginal()
    {
        var replacer = new TestReplacer(N12);
        replacer.Process(N12);
        N1.Children.Should().SequenceEqual(N11, N12, N13);
    }

    [Test]
    public void Process_ReturnNull()
    {
        var replacer = new TestReplacer(null);
        replacer.Process(N12);
        N1.Children.Should().SequenceEqual(N11, N12, N13);
    }

    [Test]
    public void Process_ReturnNewNode()
    {
        var replacement = new ANode { Name = "Replacement" };
        var replacer = new TestReplacer(replacement);
        replacer.Process(N12);
        N1.Children.Should().SequenceEqual(N11, replacement, N13);
    }

    [Test]
    public void Process_ReturnNewNodeWithParent()
    {
        var replacement = new ANode { Name = "Replacement" };
        _ = new BNode(replacement) { Name = "Parent" };
        var replacer = new TestReplacer(replacement);

        replacer.Invoking(p => p.Process(N12))
            .Should().Throw<InvalidOperationException>().That.Should()
            .HaveMessage("Replacement node Replacement already has a parent Parent.");
    }

    [Test]
    public void Process_ReplaceRootNode()
    {
        var replacement = new ANode { Name = "Replacement" };
        var replacer = new TestReplacer(replacement);
        var result = replacer.Process(N1);
        result.Should().BeTheSameInstanceAs(replacement);
    }

    [Test]
    public void Process_ReplaceRootNode_ReturnOriginal()
    {
        var replacer = new TestReplacer(N1);
        var result = replacer.Process(N1);
        result.Should().BeTheSameInstanceAs(N1);
    }

    [Test]
    public void Process_ReplaceRootNode_ReturnNull()
    {
        var replacer = new TestReplacer(null);
        var result = replacer.Process(N1);
        result.Should().BeTheSameInstanceAs(N1);
    }

    [Test]
    public void WithContext_Process_ReturnOriginal()
    {
        var context = new object();
        var replacer = new TestReplacer<object>(context, N12);
        replacer.Process(context, N12);
        N1.Children.Should().SequenceEqual(N11, N12, N13);
    }

    [Test]
    public void WithContext_Process_ReturnNull()
    {
        var context = new object();
        var replacer = new TestReplacer<object>(context, null);
        replacer.Process(context, N12);
        N1.Children.Should().SequenceEqual(N11, N12, N13);
    }

    [Test]
    public void WithContext_Process_ReturnNewNode()
    {
        var context = new object();
        var replacement = new ANode { Name = "Replacement" };
        var replacer = new TestReplacer<object>(context, replacement);
        replacer.Process(context, N12);
        N1.Children.Should().SequenceEqual(N11, replacement, N13);
    }

    [Test]
    public void WithContext_Process_ReturnNewNodeWithParent()
    {
        var context = new object();
        var replacement = new ANode { Name = "Replacement" };
        _ = new BNode(replacement) { Name = "Parent" };
        var replacer = new TestReplacer<object>(context, replacement);

        replacer.Invoking(p => p.Process(context, N12))
            .Should().Throw<InvalidOperationException>().That.Should()
            .HaveMessage("Replacement node Replacement already has a parent Parent.");
    }

    [Test]
    public void WithContext_Process_ReplaceRootNode()
    {
        var context = new object();
        var replacement = new ANode { Name = "Replacement" };
        var replacer = new TestReplacer<object>(context, replacement);
        var result = replacer.Process(context, N1);
        result.Should().BeTheSameInstanceAs(replacement);
    }

    [Test]
    public void WithContext_Process_ReplaceRootNode_ReturnOriginal()
    {
        var context = new object();
        var replacer = new TestReplacer<object>(context, N1);
        var result = replacer.Process(context, N1);
        result.Should().BeTheSameInstanceAs(N1);
    }

    [Test]
    public void WithContext_Process_ReplaceRootNode_ReturnNull()
    {
        var context = new object();
        var replacer = new TestReplacer<object>(context, null);
        var result = replacer.Process(context, N1);
        result.Should().BeTheSameInstanceAs(N1);
    }

    private sealed class TestReplacer(TestNode? replacement) : Replacer<TestNode>
    {
        protected override TestNode? Replace(TestNode node) => replacement;
    }

    private sealed class TestReplacer<TContext>(TContext expectedContext, TestNode? replacement) : Replacer<TContext, TestNode>
    {
        protected override TestNode? Replace(TContext context, TestNode node)
        {
            context.Should().BeTheSameInstanceAs(expectedContext);
            return replacement;
        }
    }
}
