namespace MrKWatkins.Ast;

public abstract partial class Node<TNode>
    where TNode : Node<TNode>
{
    private List<Message>? messages;
    
    public IReadOnlyList<Message> Messages => messages != null ? messages : Array.Empty<Message>();

    public bool HasMessages => Messages.Any();

    public bool ThisAndDescendentsHaveMessages => ThisAndDescendentsWithMessages.Any();

    public IEnumerable<TNode> ThisAndDescendentsWithMessages => ThisAndDescendents.Where(n => n.HasMessages);

    public void AddMessage(Message message) => (messages ??= new List<Message>()).Add(message);

    public void AddMessage(MessageLevel level, string text) => AddMessage(new Message(level, text));
    
    public void AddMessage(MessageLevel level, string code, string text) => AddMessage(new Message(level, code, text));

    public IEnumerable<Message> Errors => Messages.Where(m => m.Level == MessageLevel.Error);
    
    public bool HasErrors => Errors.Any();

    public bool ThisAndDescendentsHaveErrors => ThisAndDescendentsWithErrors.Any();
    
    public IEnumerable<TNode> ThisAndDescendentsWithErrors => ThisAndDescendents.Where(n => n.HasErrors);
    
    public void AddError(string text) => AddMessage(Message.Error(text));
    
    public void AddError(string code, string text) => AddMessage(Message.Error(code, text));
    
    public IEnumerable<Message> Warnings => Messages.Where(m => m.Level == MessageLevel.Warning);

    public bool HasWarnings => Warnings.Any();

    public bool ThisAndDescendentsHaveWarnings => ThisAndDescendentsWithWarnings.Any();
    
    public IEnumerable<TNode> ThisAndDescendentsWithWarnings => ThisAndDescendents.Where(n => n.HasWarnings);
    
    public void AddWarning(string text) => AddMessage(Message.Warning(text));
    
    public void AddWarning(string code, string text) => AddMessage(Message.Warning(code, text));

    public void AddInfo(string text) => AddMessage(Message.Info(text));
    
    public void AddInfo(string code, string text) => AddMessage(Message.Info(code, text));

    public IEnumerable<Message> Infos => Messages.Where(m => m.Level == MessageLevel.Info);

    public bool HasInfos => Infos.Any();

    public bool ThisAndDescendentsHaveInfos => ThisAndDescendentsWithInfos.Any();
    
    public IEnumerable<TNode> ThisAndDescendentsWithInfos => ThisAndDescendents.Where(n => n.HasInfos);
}