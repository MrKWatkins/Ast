namespace MrKWatkins.Ast.Position;

public abstract class SourcePosition
{
    public static readonly SourcePosition None = new NonePosition();
        
    /// <summary>
    /// Combines two <see cref="SourcePosition" />s to give a new SourcePosition that includes both
    /// this position and the <paramref name="other" /> along any source in-between the two.
    /// </summary>
    [Pure]
    public abstract SourcePosition Combine(SourcePosition other);
}

public abstract class SourcePosition<TSelf> : SourcePosition
    where TSelf : SourcePosition<TSelf>
{
    public sealed override TSelf Combine(SourcePosition other) => 
        other is TSelf typedOther 
            ? Combine(typedOther) 
            : throw new ArgumentException($"Value is not of type {typeof(TSelf).Name}.", nameof(other));

    [Pure]
    protected abstract TSelf Combine(TSelf other);
}