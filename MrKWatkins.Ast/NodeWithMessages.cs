using System.Collections.Concurrent;

namespace MrKWatkins.Ast;

public abstract class NodeWithMessages<TType, TNode> : Node<TType, TNode>
    where TType : struct, Enum
    where TNode : NodeWithMessages<TType, TNode>
{
    private readonly ConcurrentBag<Message> messages = new();

    protected NodeWithMessages()
    {
    }

    protected NodeWithMessages([InstantHandle] IEnumerable<TNode> children)
        : base(children)
    {
    }

    public IReadOnlyCollection<Message> Messages => messages;
    
    public bool HasMessages => Messages.Any();

    public bool ThisOrDescendentsHaveMessages => ThisAndDescendents.Any(n => n.HasMessages);

    public void AddMessage(MessageLevel level, string text) => messages.Add(new Message(level, text));
    
    public void AddMessage(MessageLevel level, string code, string text) => messages.Add(new Message(level, text, code));

    public IEnumerable<Message> Errors => Messages.Where(m => m.Level == MessageLevel.Error);
    
    public bool HasErrors => Errors.Any();

    public bool ThisOrDescendentsHaveErrors => ThisAndDescendents.Any(n => n.HasErrors);
    
    public void AddError(string text) => AddMessage(MessageLevel.Error, text);
    
    public void AddError(string code, string text) => AddMessage(MessageLevel.Error, code, text);
    
    public IEnumerable<Message> Warnings => Messages.Where(m => m.Level == MessageLevel.Warning);

    public bool HasWarnings => Warnings.Any();

    public bool ThisOrDescendentsHaveWarnings => ThisAndDescendents.Any(n => n.HasWarnings);
    
    public void AddWarning(string text) => AddMessage(MessageLevel.Warning, text);
    
    public void AddWarning(string code, string text) => AddMessage(MessageLevel.Warning, code, text);

    public void AddInfo(string text) => AddMessage(MessageLevel.Info, text);
    
    public void AddInfo(string code, string text) => AddMessage(MessageLevel.Info, code, text);

    public IEnumerable<Message> Infos => Messages.Where(m => m.Level == MessageLevel.Info);

    public bool HasInfos => Infos.Any();

    public bool ThisOrDescendentsHaveInfos => ThisAndDescendents.Any(n => n.HasInfos);
}