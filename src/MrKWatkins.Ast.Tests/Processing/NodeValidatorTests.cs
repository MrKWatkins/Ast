using MrKWatkins.Ast.Processing;

namespace MrKWatkins.Ast.Tests.Processing;

public sealed class NodeValidatorTests : TreeTestFixture
{
    [Test]
    public void Process()
    {
        var validator = new TestNodeValidator();

        validator.Process(N11);
        N12.Messages.Should().BeEmpty();

        validator.Process(N12);
        N12.Messages.Should().BeEmpty();

        validator.Add(N11, Message.Error("N11 Error"));
        validator.Add(N12, Message.Error("N12 Error")); // Checking that validate isn't called for the wrong type.

        validator.Process(N11);
        N11.Messages.Should().SequenceEqual(Message.Error("N11 Error"));

        validator.Process(N12);
        N12.Messages.Should().BeEmpty();
    }

    [Test]
    public void WithContext_Process()
    {
        var context = new object();
        var validator = new TestNodeValidator<object>(context);

        validator.Process(context, N11);
        N12.Messages.Should().BeEmpty();

        validator.Process(context, N12);
        N12.Messages.Should().BeEmpty();

        validator.Add(N11, Message.Error("N11 Error"));
        validator.Add(N12, Message.Error("N12 Error")); // Checking that validate isn't called for the wrong type.

        validator.Process(context, N11);
        N11.Messages.Should().SequenceEqual(Message.Error("N11 Error"));

        validator.Process(context, N12);
        N12.Messages.Should().BeEmpty();
    }

    private sealed class TestNodeValidator : NodeValidator<TestNode, ANode>
    {
        private readonly Dictionary<TestNode, IReadOnlyList<Message>> messagesByNode = new();

        public void Add(TestNode node, params Message[] messages) => messagesByNode.Add(node, messages);

        protected override IEnumerable<Message> Validate(ANode node) =>
            messagesByNode.TryGetValue(node, out var messages) ? messages : Enumerable.Empty<Message>();
    }

    private sealed class TestNodeValidator<TContext>(TContext expectedContext) : NodeValidator<TContext, TestNode, ANode>
    {
        private readonly Dictionary<TestNode, IReadOnlyList<Message>> messagesByNode = new();

        public void Add(TestNode node, params Message[] messages) => messagesByNode.Add(node, messages);

        protected override IEnumerable<Message> Validate(TContext context, ANode node)
        {
            context.Should().BeTheSameInstanceAs(expectedContext);
            return messagesByNode.TryGetValue(node, out var messages) ? messages : Enumerable.Empty<Message>();
        }
    }
}