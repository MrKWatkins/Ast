namespace MrKWatkins.Ast.Position;

/// <summary>
/// A position in source code.
/// </summary>
public abstract class SourcePosition
{
    /// <summary>
    /// Represents no source position, e.g. the parent node was generated programmatically.
    /// </summary>
    public static readonly SourcePosition None = new NonePosition();
        
    /// <summary>
    /// Combines two <see cref="SourcePosition" />s to give a new SourcePosition that includes both
    /// this position and the <paramref name="other" /> along any source in-between the two.
    /// </summary>
    [Pure]
    public abstract SourcePosition Combine(SourcePosition other);
}

/// <summary>
/// Self generic extension of <see cref="SourcePosition" />.
/// </summary>
public abstract class SourcePosition<TSelf> : SourcePosition
    where TSelf : SourcePosition<TSelf>
{
    /// <inheritdoc />
    public sealed override TSelf Combine(SourcePosition other) => 
        other is TSelf typedOther 
            ? Combine(typedOther) 
            : throw new ArgumentException($"Value is not of type {typeof(TSelf).Name}.", nameof(other));
    
    /// <summary>
    /// Combines two <see cref="SourcePosition" />s to give a new SourcePosition that includes both
    /// this position and the <paramref name="other" /> along any source in-between the two.
    /// </summary>
    [Pure]
    protected abstract TSelf Combine(TSelf other);
}