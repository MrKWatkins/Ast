using MrKWatkins.Ast.Processing;

namespace MrKWatkins.Ast.Tests.Processing;

public sealed class ProcessingExceptionTests
{
    [Test]
    public void Constructor()
    {
        var node = new ANode { Name = "TestNode" };

        var exception = new ProcessingException<TestNode>("Test Message", node);
        exception.Message.Should().Be("Test Message (Node 'TestNode')");
        exception.Node.Should().BeSameAs(node);
        exception.InnerException.Should().BeNull();
    }
    
    [Test]
    public void Constructor_InnerException()
    {
        var node = new ANode { Name = "TestNode" };
        var innerException = new InvalidOperationException("Inner Exception");

        var exception = new ProcessingException<TestNode>("Test Message", innerException, node);
        exception.Message.Should().Be("Test Message (Node 'TestNode')");
        exception.Node.Should().BeSameAs(node);
        exception.InnerException.Should().BeSameAs(innerException);
    }
}