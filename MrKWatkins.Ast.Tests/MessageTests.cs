namespace MrKWatkins.Ast.Tests;

public sealed class MessageTests
{
    [TestCase(MessageLevel.Error, "Text", "Code")]
    [TestCase(MessageLevel.Warning, "Text", null)]
    public void Constructor(MessageLevel level, string text, string? code)
    {
        var message = new Message(level, text, code);
        message.Level.Should().Be(level);
        message.Text.Should().Be(text);
        message.Code.Should().Be(code);
    }
    
    [Test]
    public void Constructor_WithoutCode()
    {
        var message = new Message(MessageLevel.Warning, "Text");
        message.Level.Should().Be(MessageLevel.Warning);
        message.Code.Should().BeNull();
        message.Text.Should().Be("Text");
    }
    
    [TestCase(MessageLevel.Error, "Text", "Code", "[Error] Code: Text")]
    [TestCase(MessageLevel.Error, "Text", null, "[Error] Text")]
    public void ToString_Tests(MessageLevel level, string text, string? code, string expected)
    {
        var message = new Message(level, text, code);
        message.ToString().Should().Be(expected);
    }
}