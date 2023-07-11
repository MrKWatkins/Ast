namespace MrKWatkins.Ast.Tests;

public sealed class MessageTests
{
    [Test]
    public void Constructor_WithCode()
    {
        var message = new Message(MessageLevel.Error, "Code", "Text");
        message.Level.Should().Be(MessageLevel.Error);
        message.Code.Should().Be("Code");
        message.Text.Should().Be("Text");
    }

    [Test]
    public void Constructor_WithoutCode()
    {
        var message = new Message(MessageLevel.Warning, "Text");
        message.Level.Should().Be(MessageLevel.Warning);
        message.Code.Should().BeNull();
        message.Text.Should().Be("Text");
    }

    [Test]
    public void Error_WithoutCode()
    {
        var error = Message.Error("Code", "Text");
        error.Level.Should().Be(MessageLevel.Error);
        error.Code.Should().Be("Code");
        error.Text.Should().Be("Text");
    }

    [Test]
    public void Error_WithCode()
    {
        var error = Message.Error("Text");
        error.Level.Should().Be(MessageLevel.Error);
        error.Code.Should().BeNull();
        error.Text.Should().Be("Text");
    }

    [Test]
    public void Warning_WithoutCode()
    {
        var warning = Message.Warning("Code", "Text");
        warning.Level.Should().Be(MessageLevel.Warning);
        warning.Code.Should().Be("Code");
        warning.Text.Should().Be("Text");
    }

    [Test]
    public void Warning_WithCode()
    {
        var warning = Message.Warning("Text");
        warning.Level.Should().Be(MessageLevel.Warning);
        warning.Code.Should().BeNull();
        warning.Text.Should().Be("Text");
    }

    [Test]
    public void Info_WithoutCode()
    {
        var info = Message.Info("Code", "Text");
        info.Level.Should().Be(MessageLevel.Info);
        info.Code.Should().Be("Code");
        info.Text.Should().Be("Text");
    }

    [Test]
    public void Info_WithCode()
    {
        var info = Message.Info("Text");
        info.Level.Should().Be(MessageLevel.Info);
        info.Code.Should().BeNull();
        info.Text.Should().Be("Text");
    }

    [Test]
    public void ToString_WithCode() => new Message(MessageLevel.Error, "Code", "Text").ToString().Should().Be("Error Code: Text");

    [Test]
    public void ToString_WithoutCode() => new Message(MessageLevel.Error, "Text").ToString().Should().Be("Error: Text");
}