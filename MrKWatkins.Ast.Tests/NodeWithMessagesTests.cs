namespace MrKWatkins.Ast.Tests;

public sealed class NodeWithMessagesTests
{
    [Test]
    public void AddMessage()
    {
        var node = new MessageNode();
        node.HasMessages.Should().BeFalse();
        node.Messages.Should().BeEmpty();
        
        node.AddMessage(MessageLevel.Info, "First Message");
        node.HasMessages.Should().BeTrue();
        node.Messages.Should().BeEquivalentTo(new[]
        {
            new Message(MessageLevel.Info, "First Message")
        });
        
        node.AddMessage(MessageLevel.Error, "M2", "Second Message");
        node.HasMessages.Should().BeTrue();
        node.Messages.Should().BeEquivalentTo(new[]
        {
            new Message(MessageLevel.Info, "First Message"),
            new Message(MessageLevel.Error, "Second Message", "M2")
        });
    }
    
    [Test]
    public void ThisOrDescendentsHaveMessages()
    {
        var node = new MessageNode();
        node.ThisOrDescendentsHaveMessages.Should().BeFalse();
        
        node.AddMessage(MessageLevel.Info, "First Message");
        node.ThisOrDescendentsHaveMessages.Should().BeTrue();

        node.AddMessage(MessageLevel.Error, "M2", "Second Message");
        node.ThisOrDescendentsHaveMessages.Should().BeTrue();
        
        var parent = new MessageNode();
        parent.ThisOrDescendentsHaveMessages.Should().BeFalse();

        parent.Children.Add(node);
        parent.ThisOrDescendentsHaveMessages.Should().BeTrue();
    }

    [Test]
    public void AddError()
    {
        var node = new MessageNode();
        node.HasErrors.Should().BeFalse();
        node.Errors.Should().BeEmpty();
        
        node.AddMessage(MessageLevel.Info, "First Message");
        node.HasErrors.Should().BeFalse();
        node.Errors.Should().BeEmpty();
        
        node.AddError("M2", "Second Message");
        node.HasErrors.Should().BeTrue();
        node.Errors.Should().BeEquivalentTo(new[]
        {
            new Message(MessageLevel.Error, "Second Message", "M2")
        });
    }
    
    [Test]
    public void ThisOrDescendentsHaveErrors()
    {
        var node = new MessageNode();
        node.ThisOrDescendentsHaveErrors.Should().BeFalse();
        
        node.AddMessage(MessageLevel.Info, "First Message");
        node.ThisOrDescendentsHaveErrors.Should().BeFalse();

        node.AddMessage(MessageLevel.Error, "M2", "Second Message");
        node.ThisOrDescendentsHaveErrors.Should().BeTrue();
        
        var parent = new MessageNode();
        parent.ThisOrDescendentsHaveErrors.Should().BeFalse();

        parent.Children.Add(node);
        parent.ThisOrDescendentsHaveErrors.Should().BeTrue();

        var grandchild = new MessageNode();
        var grandparent = new MessageNode(new MessageNode(grandchild));
        grandparent.ThisOrDescendentsHaveErrors.Should().BeFalse();
        
        grandchild.AddError("Grandchild Error");
        grandparent.ThisOrDescendentsHaveErrors.Should().BeTrue();
    }
    
    [Test]
    public void AddWarning()
    {
        var node = new MessageNode();
        node.HasWarnings.Should().BeFalse();
        node.Warnings.Should().BeEmpty();
        
        node.AddMessage(MessageLevel.Info, "First Message");
        node.HasWarnings.Should().BeFalse();
        node.Warnings.Should().BeEmpty();
        
        node.AddWarning("M2", "Second Message");
        node.HasWarnings.Should().BeTrue();
        node.Warnings.Should().BeEquivalentTo(new[]
        {
            new Message(MessageLevel.Warning, "Second Message", "M2")
        });
    }
    
    [Test]
    public void ThisOrDescendentsHaveWarnings()
    {
        var node = new MessageNode();
        node.ThisOrDescendentsHaveWarnings.Should().BeFalse();
        
        node.AddMessage(MessageLevel.Info, "First Message");
        node.ThisOrDescendentsHaveWarnings.Should().BeFalse();

        node.AddMessage(MessageLevel.Warning, "M2", "Second Message");
        node.ThisOrDescendentsHaveWarnings.Should().BeTrue();
        
        var parent = new MessageNode();
        parent.ThisOrDescendentsHaveWarnings.Should().BeFalse();

        parent.Children.Add(node);
        parent.ThisOrDescendentsHaveWarnings.Should().BeTrue();

        var grandchild = new MessageNode();
        var grandparent = new MessageNode(new MessageNode(grandchild));
        grandparent.ThisOrDescendentsHaveWarnings.Should().BeFalse();
        
        grandchild.AddWarning("Grandchild Warning");
        grandparent.ThisOrDescendentsHaveWarnings.Should().BeTrue();
    }
    
    [Test]
    public void AddInfo()
    {
        var node = new MessageNode();
        node.HasInfos.Should().BeFalse();
        node.Infos.Should().BeEmpty();
        
        node.AddMessage(MessageLevel.Error, "First Message");
        node.HasInfos.Should().BeFalse();
        node.Infos.Should().BeEmpty();
        
        node.AddInfo("M2", "Second Message");
        node.HasInfos.Should().BeTrue();
        node.Infos.Should().BeEquivalentTo(new[]
        {
            new Message(MessageLevel.Info, "Second Message", "M2")
        });
    }
    
    [Test]
    public void ThisOrDescendentsHaveInfos()
    {
        var node = new MessageNode();
        node.ThisOrDescendentsHaveInfos.Should().BeFalse();
        
        node.AddMessage(MessageLevel.Error, "First Message");
        node.ThisOrDescendentsHaveInfos.Should().BeFalse();

        node.AddInfo("Second Message");
        node.ThisOrDescendentsHaveInfos.Should().BeTrue();
        
        var parent = new MessageNode();
        parent.ThisOrDescendentsHaveInfos.Should().BeFalse();

        parent.Children.Add(node);
        parent.ThisOrDescendentsHaveInfos.Should().BeTrue();

        var grandchild = new MessageNode();
        var grandparent = new MessageNode(new MessageNode(grandchild));
        grandparent.ThisOrDescendentsHaveInfos.Should().BeFalse();
        
        grandchild.AddInfo("Grandchild Message");
        grandparent.ThisOrDescendentsHaveInfos.Should().BeTrue();
    }
    
    private enum MessageNodeType
    {
        Message
    }

    private sealed class MessageNode : NodeWithMessages<MessageNodeType, MessageNode>
    {
        public MessageNode()
        {
        }
        
        public MessageNode(params MessageNode[] children)
            : base(children)
        {
        }
        
        public override MessageNodeType NodeType => MessageNodeType.Message;
    }
}