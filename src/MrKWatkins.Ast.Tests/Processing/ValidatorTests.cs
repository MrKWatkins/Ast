using System.Collections;
using MrKWatkins.Ast.Processing;

namespace MrKWatkins.Ast.Tests.Processing;

public sealed class ValidatorTests : TreeTestFixture
{
    [Test]
    public void Process()
    {
        var validator = new TestValidator
        {
            { N12, Message.Error("N12 Error") },
            { N121, Message.Warning("N121 Warning"), Message.Error("N121 Error") }
        };

        validator.Process(N1);

        N1.ThisAndDescendentsWithMessages.Should().BeEquivalentTo(new[] { N12, N121 });
        N12.Messages.Should().BeEquivalentTo(new[] { Message.Error("N12 Error") });
        N121.Messages.Should().BeEquivalentTo(new[] { Message.Warning("N121 Warning"), Message.Error("N121 Error") });
    }

    [Test]
    public void Process_Typed()
    {
        var validator = new TestTypedValidator
        {
            { N1, Message.Error("N1 Error") },
            { N11, Message.Info("N11 Info") },
            { N121, Message.Warning("N121 Warning"), Message.Error("N121 Error") },
            { N122, Message.Info("Not an ANode") }
        };

        validator.Process(N1);

        N1.ThisAndDescendentsWithMessages.Should().BeEquivalentTo(new[] { N1, N11, N121 });
        N1.Messages.Should().BeEquivalentTo(new[] { Message.Error("N1 Error") });
        N11.Messages.Should().BeEquivalentTo(new[] { Message.Info("N11 Info") });
        N121.Messages.Should().BeEquivalentTo(new[] { Message.Warning("N121 Warning"), Message.Error("N121 Error") });
    }

    [Test]
    public void Process_Typed_OverrideShouldProcessNode()
    {
        var validator = new TestTypedValidator
        {
            { N1, Message.Error("N1 Error") },
            { N11, Message.Info("Not processing N11") },
            { N121, Message.Warning("N121 Warning"), Message.Error("N121 Error") },
            { N122, Message.Info("Not an ANode") }
        };
        validator.ShouldProcessNodeOverride = n => n != N11;

        validator.Process(N1);

        N1.ThisAndDescendentsWithMessages.Should().BeEquivalentTo(new[] { N1, N121 });
        N1.Messages.Should().BeEquivalentTo(new[] { Message.Error("N1 Error") });
        N121.Messages.Should().BeEquivalentTo(new[] { Message.Warning("N121 Warning"), Message.Error("N121 Error") });
    }

    private sealed class TestValidator : Validator<TestNode>, IEnumerable
    {
        private readonly Dictionary<TestNode, IReadOnlyList<Message>> messagesByNode = new();

        public void Add(TestNode node, params Message[] messages) => messagesByNode.Add(node, messages);

        protected override IEnumerable<Message> ValidateNode(TestNode node) =>
            messagesByNode.TryGetValue(node, out var messages) ? messages : Enumerable.Empty<Message>();

        public IEnumerator GetEnumerator() => throw new NotSupportedException();
    }

    private sealed class TestTypedValidator : Validator<TestNode, ANode>, IEnumerable
    {
        private readonly Dictionary<TestNode, IReadOnlyList<Message>> messagesByNode = new();
        public Func<TestNode, bool>? ShouldProcessNodeOverride { get; set; }

        protected override bool ShouldProcessNode(ANode node) => ShouldProcessNodeOverride?.Invoke(node) ?? base.ShouldProcessNode(node);

        public void Add(TestNode node, params Message[] messages) => messagesByNode.Add(node, messages);

        protected override IEnumerable<Message> ValidateNode(ANode node) =>
            messagesByNode.TryGetValue(node, out var messages) ? messages : Enumerable.Empty<Message>();

        public IEnumerator GetEnumerator() => throw new NotSupportedException();
    }
}