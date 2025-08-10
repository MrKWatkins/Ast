using MrKWatkins.Ast.Processing;

namespace MrKWatkins.Ast.Tests.Processing;

public sealed class ValidatorTests : TreeTestFixture
{
    [Test]
    public void Process()
    {
        var validator = new TestValidator();

        validator.Process(N1);
        N1.Messages.Should().BeEmpty();

        validator.Add(N1, Message.Error("N1 Error"));

        validator.Process(N1);
        N1.Messages.Should().SequenceEqual(Message.Error("N1 Error"));
    }

    [Test]
    public void WithContext_Process()
    {
        var context = new object();
        var validator = new TestValidator<object>(context);

        validator.Process(context, N1);
        N1.Messages.Should().BeEmpty();

        validator.Add(N1, Message.Error("N1 Error"));

        validator.Process(context, N1);
        N1.Messages.Should().SequenceEqual(Message.Error("N1 Error"));
    }

    private sealed class TestValidator : Validator<TestNode>
    {
        private readonly Dictionary<TestNode, IReadOnlyList<Message>> messagesByNode = new();

        public void Add(TestNode node, params Message[] messages) => messagesByNode.Add(node, messages);

        protected override IEnumerable<Message> Validate(TestNode node) =>
            messagesByNode.TryGetValue(node, out var messages) ? messages : Enumerable.Empty<Message>();
    }

    private sealed class TestValidator<TContext>(TContext expectedContext) : Validator<TContext, TestNode>
    {
        private readonly Dictionary<TestNode, IReadOnlyList<Message>> messagesByNode = new();

        public void Add(TestNode node, params Message[] messages) => messagesByNode.Add(node, messages);

        protected override IEnumerable<Message> Validate(TContext context, TestNode node)
        {
            context.Should().BeTheSameInstanceAs(expectedContext);
            return messagesByNode.TryGetValue(node, out var messages) ? messages : Enumerable.Empty<Message>();
        }
    }
}