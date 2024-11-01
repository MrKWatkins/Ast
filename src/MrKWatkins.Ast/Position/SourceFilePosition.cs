namespace MrKWatkins.Ast.Position;

/// <summary>
/// A <see cref="SourcePosition" /> in a source code file.
/// </summary>
public abstract class SourceFilePosition<TSelf, TFile> : SourcePosition<TSelf>
    where TSelf : SourceFilePosition<TSelf, TFile>
    where TFile : SourceFile
{
    /// <summary>
    /// Initialises a new instance of the <see cref="SourceFilePosition{TSelf,TFile}" /> class.
    /// </summary>
    /// <param name="file">The source file.</param>
    /// <param name="startIndex">The start index of the position in the source file.</param>
    /// <param name="length">The length of the position in the source file.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// If <paramref name="startIndex" /> or <paramref name="length" /> are less than 0, <paramref name="startIndex" /> or
    /// <paramref name="startIndex" /> + <paramref name="length" /> are greater than <paramref name="file" />'s
    /// <see cref="SourceFile.Length" />
    /// </exception>
    protected SourceFilePosition(TFile file, int startIndex, int length)
    {
        if (startIndex < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(startIndex), startIndex, "Value must be greater than 0.");
        }

        if (length < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(length), length, "Value must be greater than 0.");
        }

        if (startIndex >= file.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(startIndex), startIndex, $"Value must be less than {nameof(file)}'s length. ({file.Length})");
        }

        if (startIndex + length > file.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(length), length, $"Value plus {nameof(startIndex)} ({startIndex}) must be less than {nameof(file)}'s length. ({file.Length})");
        }

        File = file;
        StartIndex = startIndex;
        Length = length;
    }

    /// <summary>
    /// The <see cref="SourceFile" />.
    /// </summary>
    /// <returns>The file.</returns>
    public TFile File { get; }

    /// <summary>
    /// The inclusive start index of the position in the source file.
    /// </summary>
    /// <returns>The inclusive start index.</returns>
    public int StartIndex { get; }

    /// <summary>
    /// The length of the position in the source file.
    /// </summary>
    /// <returns>The length.</returns>
    public int Length { get; }

    /// <summary>
    /// The exclusive end index of the position in the source file.
    /// </summary>
    /// <remarks>
    /// As the end index is exclusive it will be the index of the first character *after* the position. If the position is zero
    /// length then it will equal <see cref="StartIndex" />.
    /// </remarks>
    /// <returns>The exclusive end index.</returns>
    public int EndIndex => StartIndex + Length;

    /// <inheritdoc />
    protected sealed override TSelf Combine(TSelf other) =>
        File == other.File
            ? CreateCombination(other)
            : throw new ArgumentException("Value is for a different file.", nameof(other));

    /// <summary>
    /// Creates a combination of this <see cref="SourceFilePosition{TSelf,TFile}" /> and another. Used to create a combination for <see cref="Combine" />.
    /// </summary>
    /// <param name="other">The other <see cref="SourceFilePosition{TSelf,TFile}" /> to combine with.</param>
    /// <returns>The combination.</returns>
    [Pure]
    protected abstract TSelf CreateCombination(TSelf other);

    /// <summary>
    /// Returns <c>true</c> if two positions for the same file overlap. Zero length positions will overlap only if they are inside
    /// the other position. If they are at the start or end index of the other position they will not overlap. Two zero length positions
    /// never overlap.
    /// </summary>
    /// <returns><c>true</c> if the two positions overlap, <c>false</c> otherwise.</returns>
    [Pure]
    public bool Overlaps(SourceFilePosition<TSelf, TFile> other)
    {
        if (File != other.File)
        {
            return false;
        }

        // We are zero length.
        if (Length == 0)
        {
            return other.StartIndex < StartIndex && other.EndIndex > StartIndex + 1;
        }

        // Other is zero length.
        if (other.Length == 0)
        {
            return StartIndex < other.StartIndex && EndIndex > other.StartIndex + 1;
        }

        if (StartIndex <= other.StartIndex)
        {
            return EndIndex > other.StartIndex;
        }

        return StartIndex < other.EndIndex;
    }

    /// <inheritdoc />
    public override string ToString() => $"{File.Name} ({StartIndex}, {Length})";

    /// <inheritdoc />
    public sealed override bool Equals(SourcePosition? other)
    {
        if (ReferenceEquals(null, other))
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        if (other is not SourceFilePosition<TSelf, TFile> otherFilePosition)
        {
            return false;
        }

        return File == otherFilePosition.File && StartIndex == otherFilePosition.StartIndex && Length == otherFilePosition.Length;
    }

    /// <inheritdoc />
    public sealed override int GetHashCode() => HashCode.Combine(File, StartIndex, Length);
}