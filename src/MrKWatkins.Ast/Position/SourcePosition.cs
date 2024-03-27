using System.Numerics;

namespace MrKWatkins.Ast.Position;

/// <summary>
/// A position in source code.
/// </summary>
public abstract class SourcePosition : IEquatable<SourcePosition>, IEqualityOperators<SourcePosition, SourcePosition, bool>
{
    /// <summary>
    /// Represents no source position, e.g. the parent node was generated programmatically.
    /// </summary>
    /// <returns>The none position.</returns>
    public static readonly SourcePosition None = new NonePosition();

    /// <summary>
    /// Combines two <see cref="SourcePosition" />s to give a new SourcePosition that includes both
    /// this position and the <paramref name="other" /> along any source in-between the two.
    /// </summary>
    /// <returns>The combined position.</returns>
    [Pure]
    public abstract SourcePosition Combine(SourcePosition other);

    /// <summary>
    /// Combines two <see cref="SourcePosition" />s to give a new SourcePosition that includes both
    /// <paramref name="x" /> and <paramref name="y" /> along with any source in-between the two.
    /// </summary>
    /// <returns>The combined position.</returns>
    [Pure]
    public static SourcePosition operator +(SourcePosition x, SourcePosition y) => x.Combine(y);

    /// <summary>
    /// Create a new <see cref="SourcePosition" /> with zero width at the start of this <see cref="SourcePosition" />.
    /// </summary>
    /// <returns>A zero width <see cref="SourcePosition" />.</returns>
    [Pure]
    public abstract SourcePosition CreateZeroWidthPrefix();

    /// <inheritdoc />
    public abstract bool Equals(SourcePosition? other);

    /// <inheritdoc />
    public sealed override bool Equals(object? obj) => Equals(obj as SourcePosition);

    /// <inheritdoc />
    public override int GetHashCode() => 0;

    /// <summary>
    /// Determines whether two specified <see cref="SourceFile"/>s have the same value.
    /// </summary>
    /// <param name="left">The first <see cref="SourceFile"/> to compare, or <c>null</c>.</param>
    /// <param name="right">The second <see cref="SourceFile"/> to compare, or <c>null</c>.</param>
    /// <returns>
    /// <c>true</c> if the value of <paramref name="left"/> is the same as the value of <paramref name="right"/>; otherwise, <c>false</c>.
    /// </returns>
    public static bool operator ==(SourcePosition? left, SourcePosition? right) => Equals(left, right);

    /// <summary>
    /// Determines whether two specified <see cref="SourceFile"/>s have different values.
    /// </summary>
    /// <param name="left">The first <see cref="SourceFile"/> to compare, or <c>null</c>.</param>
    /// <param name="right">The second <see cref="SourceFile"/> to compare, or <c>null</c>.</param>
    /// <returns>
    /// <c>true</c> if the value of <paramref name="left"/> is different to the value of <paramref name="right"/>; otherwise, <c>false</c>.
    /// </returns>
    public static bool operator !=(SourcePosition? left, SourcePosition? right) => !Equals(left, right);
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
            : throw new ArgumentException($"Value is not of type {typeof(TSelf).SimpleName()}.", nameof(other));

    /// <summary>
    /// Combines two <see cref="SourcePosition{TSelf}" />s to give a new SourcePosition that includes both
    /// this position and the <paramref name="other" /> along any source in-between the two.
    /// </summary>
    /// <returns>The combined position.</returns>
    [Pure]
    protected abstract TSelf Combine(TSelf other);
}