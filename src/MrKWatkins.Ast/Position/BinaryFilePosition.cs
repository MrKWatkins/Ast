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
    /// Create a new <see cref="BinaryFilePosition" /> with zero width at the start of this <see cref="BinaryFilePosition" />.
    /// </summary>
    public override BinaryFilePosition CreateZeroWidthPrefix() => new(File, StartIndex, 0);
}