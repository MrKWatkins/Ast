using MrKWatkins.Ast.Processing;
using MrKWatkins.Ast.Traversal;

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
        var replacer = new TestNodeReplacer((TestNode?) null);
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
    public void Process_ReturnNewNode_RootNode_Throws()
    {
        var replacement = new ANode { Name = "Replacement" };
        var replacer = new TestNodeReplacer(replacement);
        var root = new BNode { Name = "Root" };
        replacer.Invoking(r => r.Process(root))
            .Should().Throw<InvalidOperationException>().That.Should()
            .HaveMessage("The root node can only be replaced by running the replacer in a pipeline.");
    }

    [Test]
    public void Pipeline_ReplacesNodes()
    {
        // Only BNodes are replaced; N1 is an ANode so is left alone and the root is unchanged.
        var replacer = new TestNodeReplacer(() => new ANode { Name = "Replacement" });

        var stage = new SerialPipelineStage<TestNode>("Test Stage", _ => true, DepthFirstPreOrderTraversal<TestNode>.Instance, [replacer]);

        var result = stage.Run(N1);

        result.Root.Should().BeTheSameInstanceAs(N1);
        N1.Children.Select(c => c.Name).Should().SequenceEqual("N11", "Replacement", "N13");
    }

    [Test]
    public void Pipeline_ReplacesRoot()
    {
        var replacement = new ANode { Name = "Replacement" };
        var replacer = new TestNodeReplacer(replacement);
        var root = new BNode { Name = "Root" };

        var stage = new SerialPipelineStage<TestNode>("Test Stage", _ => true, DepthFirstPreOrderTraversal<TestNode>.Instance, [replacer]);

        var result = stage.Run(root);

        result.Root.Should().BeTheSameInstanceAs(replacement);
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
        var replacer = new TestNodeReplacer<object>(context, (TestNode?) null);
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

    [Test]
    public void WithContext_Process_ReturnNewNode_RootNode_Throws()
    {
        var context = new object();
        var replacement = new ANode { Name = "Replacement" };
        var replacer = new TestNodeReplacer<object>(context, replacement);
        var root = new BNode { Name = "Root" };
        replacer.Invoking(r => r.Process(context, root))
            .Should().Throw<InvalidOperationException>().That.Should()
            .HaveMessage("The root node can only be replaced by running the replacer in a pipeline.");
    }

    private sealed class TestNodeReplacer(Func<TestNode?> replacement) : NodeReplacer<TestNode, BNode>
    {
        public TestNodeReplacer(TestNode? replacement)
            : this(() => replacement)
        {
        }

        protected override TestNode? Replace(BNode node) => replacement();
    }

    [Test]
    public void WithContext_Pipeline_ReplacesNodes()
    {
        var context = new object();
        var replacer = new TestNodeReplacer<object>(context, () => new ANode { Name = "Replacement" });

        var stage = new SerialPipelineStage<object, TestNode>("Test Stage", (_, _) => true, DepthFirstPreOrderTraversal<TestNode>.Instance, [replacer]);

        var result = stage.Run(context, N1);

        result.Root.Should().BeTheSameInstanceAs(N1);
        N1.Children.Select(c => c.Name).Should().SequenceEqual("N11", "Replacement", "N13");
    }

    [Test]
    public void WithContext_Pipeline_ReplacesRoot()
    {
        var context = new object();
        var replacement = new ANode { Name = "Replacement" };
        var replacer = new TestNodeReplacer<object>(context, replacement);
        var root = new BNode { Name = "Root" };

        var stage = new SerialPipelineStage<object, TestNode>("Test Stage", (_, _) => true, DepthFirstPreOrderTraversal<TestNode>.Instance, [replacer]);

        var result = stage.Run(context, root);

        result.Root.Should().BeTheSameInstanceAs(replacement);
    }

    private sealed class TestNodeReplacer<TContext>(TContext expectedContext, Func<TestNode?> replacement) : NodeReplacer<TContext, TestNode, BNode>
    {
        public TestNodeReplacer(TContext expectedContext, TestNode? replacement)
            : this(expectedContext, () => replacement)
        {
        }

        protected override TestNode? Replace(TContext context, BNode node)
        {
            context.Should().BeTheSameInstanceAs(expectedContext);
            return replacement();
        }
    }
}