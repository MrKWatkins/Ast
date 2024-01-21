using MrKWatkins.Ast.Processing;

namespace MrKWatkins.Ast.Tests.Processing;

public sealed class ReplacerTests : TreeTestFixture
{
    [Test]
    public void Process_ReturnOriginal()
    {
        var replacer = new TestReplacer(N122, N122);
        replacer.Process(N12);
        N12.Children.Should().HaveSameOrderAs(N121, N122, N123);
    }

    [Test]
    public void Process_ReturnNull()
    {
        var replacer = new TestReplacer(N122, null);
        replacer.Process(N12);
        N12.Children.Should().HaveSameOrderAs(N121, N122, N123);
    }

    [Test]
    public void Process_ReturnNewNode()
    {
        var replacement = new ANode { Name = "Replacement" };
        var replacer = new TestReplacer(N122, replacement);
        replacer.Process(N12);
        N12.Children.Should().HaveSameOrderAs(N121, replacement, N123);
    }

    [Test]
    public void Process_ReturnNewNodeWithParent()
    {
        var replacement = new ANode { Name = "Replacement" };
        _ = new BNode(replacement) { Name = "Parent" };
        var replacer = new TestReplacer(N122, replacement);

        replacer.Invoking(p => p.Process(N12))
            .Should().Throw<ProcessingException<TestNode>>()
            .WithParameters("Exception during ProcessNode.", N122)
            .WithInnerException<InvalidOperationException>()
            .WithMessage("Replacement node Replacement already has a parent Parent.");
    }

    [Test]
    public void Process_DoNotProcessReplacements()
    {
        var root = new ANode(new BNode(new BNode()));
        var replacer = new TestReplacer(
            node => node is BNode,
            node =>
            {
                var replacement = new CNode();
                replacement.Children.Move(node.Children);
                return replacement;
            });

        replacer.Process(root);

        root.Children.Should().HaveCount(1);

        var child = root.Children[0];
        child.Should().BeOfType<CNode>();
        child.Children.Should().HaveCount(1);

        var grandchild = child.Children[0];
        grandchild.Should().BeOfType<BNode>();
    }

    [Test]
    public void Process_ProcessReplacements()
    {
        var root = new ANode(new BNode(new BNode()));
        var replacer = new TestReplacer(
            node => node is BNode,
            node =>
            {
                var replacement = new CNode();
                replacement.Children.Move(node.Children);
                return replacement;
            },
            true);

        replacer.Process(root);

        root.Children.Should().HaveCount(1);

        var child = root.Children[0];
        child.Should().BeOfType<CNode>();
        child.Children.Should().HaveCount(1);

        var grandchild = child.Children[0];
        grandchild.Should().BeOfType<CNode>();
    }

    [Test]
    public void Process_OriginalTyped_ReturnOriginal()
    {
        var replacer = new TestOriginalTypedReplacer(N122, N122);
        replacer.Process(N12);
        N12.Children.Should().HaveSameOrderAs(N121, N122, N123);
    }

    [Test]
    public void Process_OriginalTyped_ReturnNull()
    {
        var replacer = new TestOriginalTypedReplacer(N122, null);
        replacer.Process(N12);
        N12.Children.Should().HaveSameOrderAs(N121, N122, N123);
    }

    [Test]
    public void Process_OriginalTyped_ReturnNewNode()
    {
        var replacement = new ANode { Name = "Replacement" };
        var replacer = new TestOriginalTypedReplacer(N122, replacement);
        replacer.Process(N12);
        N12.Children.Should().HaveSameOrderAs(N121, replacement, N123);
    }

    [Test]
    public void Process_OriginalTyped_ReturnNewNodeWithParent()
    {
        var replacement = new ANode { Name = "Replacement" };
        _ = new BNode(replacement) { Name = "Parent" };
        var replacer = new TestOriginalTypedReplacer(N122, replacement);

        replacer.Invoking(p => p.Process(N12))
            .Should().Throw<ProcessingException<TestNode>>()
            .WithParameters("Exception during ProcessNode.", N122)
            .WithInnerException<InvalidOperationException>()
            .WithMessage("Replacement node Replacement already has a parent Parent.");
    }

    [Test]
    public void Process_OriginalTyped_DoNotProcessReplacements()
    {
        var root = new ANode(new BNode(new BNode()));
        var replacer = new TestOriginalTypedReplacer(
            node => node is BNode,
            node =>
            {
                var replacement = new CNode();
                replacement.Children.Move(node.Children);
                return replacement;
            });

        replacer.Process(root);

        root.Children.Should().HaveCount(1);

        var child = root.Children[0];
        child.Should().BeOfType<CNode>();
        child.Children.Should().HaveCount(1);

        var grandchild = child.Children[0];
        grandchild.Should().BeOfType<BNode>();
    }

    [Test]
    public void Process_OriginalTyped_ProcessReplacements()
    {
        var root = new ANode(new BNode(new BNode()));
        var replacer = new TestOriginalTypedReplacer(
            node => node is BNode,
            node =>
            {
                var replacement = new CNode();
                replacement.Children.Move(node.Children);
                return replacement;
            },
            true);

        replacer.Process(root);

        root.Children.Should().HaveCount(1);

        var child = root.Children[0];
        child.Should().BeOfType<CNode>();
        child.Children.Should().HaveCount(1);

        var grandchild = child.Children[0];
        grandchild.Should().BeOfType<CNode>();
    }

    [Test]
    public void Process_OriginalAndReplacementTyped_ReturnOriginalAndReplacement()
    {
        var replacer = new TestOriginalAndReplacementTypedReplacer<BNode>(N122, (BNode) N122);
        replacer.Process(N12);
        N12.Children.Should().HaveSameOrderAs(N121, N122, N123);
    }

    [Test]
    public void Process_OriginalAndReplacementTyped_ReturnNull()
    {
        var replacer = new TestOriginalAndReplacementTypedReplacer<ANode>(N122, null);
        replacer.Process(N12);
        N12.Children.Should().HaveSameOrderAs(N121, N122, N123);
    }

    [Test]
    public void Process_OriginalAndReplacementTyped_ReturnNewNode()
    {
        var replacement = new ANode { Name = "Replacement" };
        var replacer = new TestOriginalAndReplacementTypedReplacer<ANode>(N122, replacement);
        replacer.Process(N12);
        N12.Children.Should().HaveSameOrderAs(N121, replacement, N123);
    }

    [Test]
    public void Process_OriginalAndReplacementTyped_ReturnNewNodeWithParent()
    {
        var replacement = new ANode { Name = "Replacement" };
        _ = new BNode(replacement) { Name = "Parent" };
        var replacer = new TestOriginalAndReplacementTypedReplacer<ANode>(N122, replacement);

        replacer.Invoking(p => p.Process(N12))
            .Should().Throw<ProcessingException<TestNode>>()
            .WithParameters("Exception during ProcessNode.", N122)
            .WithInnerException<InvalidOperationException>()
            .WithMessage("Replacement node Replacement already has a parent Parent.");
    }

    [Test]
    public void Process_OriginalAndReplacementTyped_DoNotProcessReplacements()
    {
        var root = new ANode(new BNode(new BNode()));
        var replacer = new TestOriginalAndReplacementTypedReplacer<CNode>(
            node => node is BNode,
            node =>
            {
                var replacement = new CNode();
                replacement.Children.Move(node.Children);
                return replacement;
            });

        replacer.Process(root);

        root.Children.Should().HaveCount(1);

        var child = root.Children[0];
        child.Should().BeOfType<CNode>();
        child.Children.Should().HaveCount(1);

        var grandchild = child.Children[0];
        grandchild.Should().BeOfType<BNode>();
    }

    [Test]
    public void Process_OriginalAndReplacementTyped_ProcessReplacements()
    {
        var root = new ANode(new BNode(new BNode()));
        var replacer = new TestOriginalAndReplacementTypedReplacer<CNode>(
            node => node is BNode,
            node =>
            {
                var replacement = new CNode();
                replacement.Children.Move(node.Children);
                return replacement;
            },
            true);

        replacer.Process(root);

        root.Children.Should().HaveCount(1);

        var child = root.Children[0];
        child.Should().BeOfType<CNode>();
        child.Children.Should().HaveCount(1);

        var grandchild = child.Children[0];
        grandchild.Should().BeOfType<CNode>();
    }

    private sealed class TestReplacer : Replacer<TestNode>
    {
        private readonly Func<TestNode, bool> shouldReplace;
        private readonly Func<TestNode, TestNode?> createReplacement;
        private readonly bool? processReplacements;

        public TestReplacer(TestNode original, TestNode? replacement, bool? processReplacements = null)
            : this(node => node == original, _ => replacement, processReplacements)
        {
        }

        public TestReplacer(Func<TestNode, bool> shouldReplace, Func<TestNode, TestNode?> createReplacement, bool? processReplacements = null)
        {
            this.shouldReplace = shouldReplace;
            this.createReplacement = createReplacement;
            this.processReplacements = processReplacements;
        }

        protected override TestNode? ReplaceNode(TestNode node) => shouldReplace(node) ? createReplacement(node) : node;

        protected override bool ProcessReplacements => processReplacements ?? base.ProcessReplacements;
    }

    private sealed class TestOriginalTypedReplacer : Replacer<TestNode, BNode>
    {
        private readonly Func<TestNode, bool> shouldReplace;
        private readonly Func<TestNode, TestNode?> createReplacement;
        private readonly bool? processReplacements;

        public TestOriginalTypedReplacer(TestNode original, TestNode? replacement, bool? processReplacements = null)
            : this(node => node == original, _ => replacement, processReplacements)
        {
        }

        public TestOriginalTypedReplacer(Func<TestNode, bool> shouldReplace, Func<TestNode, TestNode?> createReplacement, bool? processReplacements = null)
        {
            this.shouldReplace = shouldReplace;
            this.createReplacement = createReplacement;
            this.processReplacements = processReplacements;
        }

        protected override TestNode? ReplaceNode(BNode node) => shouldReplace(node) ? createReplacement(node) : node;

        protected override bool ProcessReplacements => processReplacements ?? base.ProcessReplacements;
    }

    private sealed class TestOriginalAndReplacementTypedReplacer<TReplacement> : Replacer<TestNode, BNode, TReplacement>
        where TReplacement : TestNode
    {
        private readonly Func<TestNode, bool> shouldReplace;
        private readonly Func<TestNode, TReplacement?> createReplacement;
        private readonly bool? processReplacements;

        public TestOriginalAndReplacementTypedReplacer(TestNode original, TReplacement? replacement, bool? processReplacements = null)
            : this(node => node == original, _ => replacement, processReplacements)
        {
        }

        public TestOriginalAndReplacementTypedReplacer(Func<TestNode, bool> shouldReplace, Func<TestNode, TReplacement?> createReplacement, bool? processReplacements = null)
        {
            this.shouldReplace = shouldReplace;
            this.createReplacement = createReplacement;
            this.processReplacements = processReplacements;
        }

        protected override TReplacement? ReplaceNode(BNode node) => shouldReplace(node) ? createReplacement(node) : null;

        protected override bool ProcessReplacements => processReplacements ?? base.ProcessReplacements;
    }
}