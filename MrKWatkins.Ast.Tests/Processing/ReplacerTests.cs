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
        var _ = new BNode(replacement) { Name = "Parent" };
        var replacer = new TestReplacer(N122, replacement);

        replacer.Invoking(p => p.Process(N12))
            .Should().Throw<ProcessingException<TestNode>>()
            .WithParameters("Exception during ProcessNode.", N122)
            .WithInnerException<InvalidOperationException>()
            .WithMessage("Replacement node Replacement already has a parent Parent.");
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
        var _ = new BNode(replacement) { Name = "Parent" };
        var replacer = new TestOriginalTypedReplacer(N122, replacement);

        replacer.Invoking(p => p.Process(N12))
            .Should().Throw<ProcessingException<TestNode>>()
            .WithParameters("Exception during ProcessNode.", N122)
            .WithInnerException<InvalidOperationException>()
            .WithMessage("Replacement node Replacement already has a parent Parent.");
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
        var _ = new BNode(replacement) { Name = "Parent" };
        var replacer = new TestOriginalAndReplacementTypedReplacer<ANode>(N122, replacement);

        replacer.Invoking(p => p.Process(N12))
            .Should().Throw<ProcessingException<TestNode>>()
            .WithParameters("Exception during ProcessNode.", N122)
            .WithInnerException<InvalidOperationException>()
            .WithMessage("Replacement node Replacement already has a parent Parent.");
    }
    
    private sealed class TestReplacer : Replacer<TestNode>
    {
        private readonly TestNode original;
        private readonly TestNode? replacement;

        public TestReplacer(TestNode original, TestNode? replacement)
        {
            this.original = original;
            this.replacement = replacement;
        }

        protected override TestNode? ReplaceNode(TestNode node) => node == original ? replacement : node;
    }
    
    private sealed class TestOriginalTypedReplacer : Replacer<TestNode, BNode>
    {
        private readonly TestNode original;
        private readonly TestNode? replacement;

        public TestOriginalTypedReplacer(TestNode original, TestNode? replacement)
        {
            this.original = original;
            this.replacement = replacement;
        }

        protected override TestNode? ReplaceNode(BNode node) => node == original ? replacement : node;
    }
    
    private sealed class TestOriginalAndReplacementTypedReplacer<TReplacement> : Replacer<TestNode, BNode, TReplacement>
        where TReplacement : TestNode
    {
        private readonly TestNode original;
        private readonly TReplacement? replacement;

        public TestOriginalAndReplacementTypedReplacer(TestNode original, TReplacement? replacement)
        {
            this.original = original;
            this.replacement = replacement;
        }

        protected override TReplacement? ReplaceNode(BNode node) => node == original ? replacement : null;
    }
}