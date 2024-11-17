using System.Collections.Immutable;

namespace MrKWatkins.Ast;

public abstract partial class Node<TNode>
    where TNode : Node<TNode>
{
    private readonly Lock messagesLock = new();
    private ImmutableList<Message> messages = ImmutableList<Message>.Empty;

    /// <summary>
    /// The <see cref="Message">Messages</see> associated with this node.
    /// </summary>
    /// <returns>The messages.</returns>
    public IReadOnlyList<Message> Messages => messages;

    /// <summary>
    /// Returns <c>true</c> if this node has any <see cref="Message">Messages</see>, <c>false</c> otherwise.
    /// </summary>
    /// <returns>Whether this node has messages or not.</returns>
    public bool HasMessages => Messages.Any();

    /// <summary>
    /// Returns <c>true</c> if this node or any of its descendents have any <see cref="Message">Messages</see>, <c>false</c> otherwise.
    /// </summary>
    /// <returns>Whether this node or its descendents have messages or not.</returns>
    public bool ThisAndDescendentsHaveMessages => ThisAndDescendentsWithMessages.Any();

    /// <summary>
    /// Lazily enumerates over this node and its descendents returning only those that have <see cref="Message">Messages</see>.
    /// </summary>
    /// <returns>A lazy enumeration of this node if it has messages and any descendents that have messages.</returns>
    public IEnumerable<TNode> ThisAndDescendentsWithMessages => ThisAndDescendents.Where(n => n.HasMessages);

    /// <summary>
    /// Adds a <see cref="Message" /> to this node.
    /// </summary>
    /// <param name="message">The <see cref="Message" /> to add.</param>
    public void AddMessage(Message message)
    {
        // Lock around updates in case two threads add at the same time; we don't want to lose one of the adds.
        // No need to lock around gets as we'll just return the list at the time.
        lock (messagesLock)
        {
            messages = messages.Add(message);
        }
    }

    /// <summary>
    /// Adds a <see cref="Message" /> with the specified <see cref="Message.Level" /> and <see cref="Message.Text" /> to this node.
    /// </summary>
    /// <param name="level">The <see cref="Message.Level" /> of the <see cref="Message" />.</param>
    /// <param name="text">The <see cref="Message.Text" /> of the message.</param>
    public void AddMessage(MessageLevel level, string text) => AddMessage(new Message(level, text));

    /// <summary>
    /// Adds a <see cref="Message" /> with the specified <see cref="Message.Level" />, <see cref="Message.Code" /> and <see cref="Message.Text" /> to this node.
    /// </summary>
    /// <param name="level">The <see cref="Message.Level" /> of the <see cref="Message" />.</param>
    /// <param name="code">The <see cref="Message.Code" /> for the message.</param>
    /// <param name="text">The <see cref="Message.Text" /> of the message.</param>
    public void AddMessage(MessageLevel level, string code, string text) => AddMessage(new Message(level, code, text));

    /// <summary>
    /// The <see cref="Message">Messages</see> with <see cref="Message.Level" /> <see cref="MessageLevel.Error" /> associated with this node.
    /// </summary>
    /// <returns>A lazy enumeration of this node's errors.</returns>
    public IEnumerable<Message> Errors => Messages.Where(m => m.Level == MessageLevel.Error);

    /// <summary>
    /// Returns <c>true</c> if this node has any <see cref="Message">Messages</see> with <see cref="Message.Level" /> <see cref="MessageLevel.Error" />,
    /// <c>false</c> otherwise.
    /// </summary>
    /// <returns>Whether this node has errors or not.</returns>
    public bool HasErrors => Errors.Any();

    /// <summary>
    /// Returns <c>true</c> if this node or any of its descendents have any <see cref="Message">Messages</see> with <see cref="Message.Level" />
    /// <see cref="MessageLevel.Error" />, <c>false</c> otherwise.
    /// </summary>
    /// <returns>Whether this node or its descendents have errors or not.</returns>
    public bool ThisAndDescendentsHaveErrors => ThisAndDescendentsWithErrors.Any();

    /// <summary>
    /// Lazily enumerates over this node and its descendents returning only those that have <see cref="Message">Messages</see> with
    /// <see cref="Message.Level" /> <see cref="MessageLevel.Error" />.
    /// </summary>
    /// <returns>A lazy enumeration of this node if it has errors and any descendents that have errors.</returns>
    public IEnumerable<TNode> ThisAndDescendentsWithErrors => ThisAndDescendents.Where(n => n.HasErrors);

    /// <summary>
    /// Adds a <see cref="Message" /> with <see cref="Message.Level" /> <see cref="MessageLevel.Error" /> and the specified text to this node.
    /// </summary>
    /// <param name="text">The <see cref="Message.Text" /> of the message.</param>
    public void AddError(string text) => AddMessage(Message.Error(text));

    /// <summary>
    /// Adds a <see cref="Message" /> with <see cref="Message.Level" /> <see cref="MessageLevel.Error" /> and the specified text to this node.
    /// </summary>
    /// <param name="code">The <see cref="Message.Code" /> for the message.</param>
    /// <param name="text">The <see cref="Message.Text" /> of the message.</param>
    public void AddError(string code, string text) => AddMessage(Message.Error(code, text));

    /// <summary>
    /// The <see cref="Message">Messages</see> with <see cref="Message.Level" /> <see cref="MessageLevel.Warning" /> associated with this node.
    /// </summary>
    /// <returns>A lazy enumeration of this node's warnings.</returns>
    public IEnumerable<Message> Warnings => Messages.Where(m => m.Level == MessageLevel.Warning);

    /// <summary>
    /// Returns <c>true</c> if this node has any <see cref="Message">Messages</see> with <see cref="Message.Level" /> <see cref="MessageLevel.Warning" />,
    /// <c>false</c> otherwise.
    /// </summary>
    /// <returns>Whether this node has warnings or not.</returns>
    public bool HasWarnings => Warnings.Any();

    /// <summary>
    /// Returns <c>true</c> if this node or any of its descendents have any <see cref="Message">Messages</see> with <see cref="Message.Level" />
    /// <see cref="MessageLevel.Warning" />, <c>false</c> otherwise.
    /// </summary>
    /// <returns>Whether this node or its descendents have warnings or not.</returns>
    public bool ThisAndDescendentsHaveWarnings => ThisAndDescendentsWithWarnings.Any();

    /// <summary>
    /// Lazily enumerates over this node and its descendents returning only those that have <see cref="Message">Messages</see> with
    /// <see cref="Message.Level" /> <see cref="MessageLevel.Warning" />.
    /// </summary>
    /// <returns>A lazy enumeration of this node if it has warnings and any descendents that have warnings.</returns>
    public IEnumerable<TNode> ThisAndDescendentsWithWarnings => ThisAndDescendents.Where(n => n.HasWarnings);

    /// <summary>
    /// Adds a <see cref="Message" /> with <see cref="Message.Level" /> <see cref="MessageLevel.Warning" /> and the specified text to this node.
    /// </summary>
    /// <param name="text">The <see cref="Message.Text" /> of the message.</param>
    public void AddWarning(string text) => AddMessage(Message.Warning(text));

    /// <summary>
    /// Adds a <see cref="Message" /> with <see cref="Message.Level" /> <see cref="MessageLevel.Warning" /> and the specified text to this node.
    /// </summary>
    /// <param name="code">The <see cref="Message.Code" /> for the message.</param>
    /// <param name="text">The <see cref="Message.Text" /> of the message.</param>
    public void AddWarning(string code, string text) => AddMessage(Message.Warning(code, text));

    /// <summary>
    /// The <see cref="Message">Messages</see> with <see cref="Message.Level" /> <see cref="MessageLevel.Info" /> associated with this node.
    /// </summary>
    /// <returns>A lazy enumeration of this node's info messages.</returns>
    public IEnumerable<Message> Infos => Messages.Where(m => m.Level == MessageLevel.Info);

    /// <summary>
    /// Returns <c>true</c> if this node has any <see cref="Message">Messages</see> with <see cref="Message.Level" /> <see cref="MessageLevel.Info" />,
    /// <c>false</c> otherwise.
    /// </summary>
    /// <returns>Whether this node has infos or not.</returns>
    public bool HasInfos => Infos.Any();

    /// <summary>
    /// Returns <c>true</c> if this node or any of its descendents have any <see cref="Message">Messages</see> with <see cref="Message.Level" />
    /// <see cref="MessageLevel.Info" />, <c>false</c> otherwise.
    /// </summary>
    /// <returns>Whether this node or its descendents have info messages or not.</returns>
    public bool ThisAndDescendentsHaveInfos => ThisAndDescendentsWithInfos.Any();

    /// <summary>
    /// Lazily enumerates over this node and its descendents returning only those that have <see cref="Message">Messages</see> with
    /// <see cref="Message.Level" /> <see cref="MessageLevel.Info" />.
    /// </summary>
    /// <returns>A lazy enumeration of this node if it has info messages and any descendents that have info messages.</returns>
    public IEnumerable<TNode> ThisAndDescendentsWithInfos => ThisAndDescendents.Where(n => n.HasInfos);

    /// <summary>
    /// Adds a <see cref="Message" /> with <see cref="Message.Level" /> <see cref="MessageLevel.Info" /> and the specified text to this node.
    /// </summary>
    /// <param name="text">The <see cref="Message.Text" /> of the message.</param>
    public void AddInfo(string text) => AddMessage(Message.Info(text));

    /// <summary>
    /// Adds a <see cref="Message" /> with <see cref="Message.Level" /> <see cref="MessageLevel.Info" /> and the specified text to this node.
    /// </summary>
    /// <param name="code">The <see cref="Message.Code" /> for the message.</param>
    /// <param name="text">The <see cref="Message.Text" /> of the message.</param>
    public void AddInfo(string code, string text) => AddMessage(Message.Info(code, text));
}