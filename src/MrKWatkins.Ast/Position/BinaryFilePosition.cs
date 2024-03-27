namespace MrKWatkins.Ast.Position;

/// <summary>
/// A <see cref="SourcePosition" /> in a binary source code file.
/// </summary>
public sealed class BinaryFilePosition : SourceFilePosition<BinaryFilePosition, BinaryFile>
{
    internal BinaryFilePosition(BinaryFile file, int startIndex, int length)
        : base(file, startIndex, length)
    {
    }

    /// <inheritdoc />
    protected override BinaryFilePosition CreateCombination(BinaryFilePosition other)
    {
        var startIndex = StartIndex < other.StartIndex ? StartIndex : other.StartIndex;
        var endIndex = EndIndex > other.EndIndex ? EndIndex : other.EndIndex;

        return new BinaryFilePosition(File, startIndex, endIndex - startIndex);
    }

    /// <summary>
    /// Combines two <see cref="BinaryFilePosition" />s to give a new SourcePosition that includes both
    /// <paramref name="x" /> and <paramref name="y" /> along with any source in-between the two.
    /// </summary>
    /// <returns>A combined <see cref="BinaryFilePosition" />.</returns>
    [Pure]
    public static BinaryFilePosition operator +(BinaryFilePosition x, BinaryFilePosition y) => x.Combine(y);

    /// <summary>
    /// Create a new <see cref="BinaryFilePosition" /> with zero width at the start of this <see cref="BinaryFilePosition" />.
    /// </summary>
    /// <returns>A zero width <see cref="BinaryFilePosition" />.</returns>
    public override BinaryFilePosition CreateZeroWidthPrefix() => new(File, StartIndex, 0);
}