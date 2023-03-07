namespace MrKWatkins.Ast.Position;

public abstract class SourceFilePosition<TSelf, TFile> : SourcePosition<TSelf>
    where TSelf : SourceFilePosition<TSelf, TFile>
    where TFile : SourceFile 
{
    protected SourceFilePosition(TFile file, int startIndex, int length)
    {
        if (startIndex < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(startIndex), startIndex, "Value must be 0 or greater.");
        }
        
        if (length < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(length), length, "Value must be 0 or greater.");
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

    public TFile File { get; }

    public int StartIndex { get; }

    public int Length { get; }

    public int EndIndex => StartIndex + Length - 1;
    
    protected sealed override TSelf Combine(TSelf other) => 
        File == other.File 
            ? CreateCombination(other) 
            : throw new ArgumentException("Value is for a different file.", nameof(other));

    [Pure]
    protected abstract TSelf CreateCombination(TSelf other);
    
    /// <summary>
    /// Returns <c>true</c> if two positions for the same file overlap. Zero length positions will overlap only if they are inside
    /// the other position. If they are at the start or end index of the other position they will not overlap. Two zero length positions
    /// never overlap.
    /// </summary>
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
            return other.StartIndex < StartIndex && other.EndIndex > StartIndex;
        }
        
        // Other is zero length.
        if (other.Length == 0)
        {
            return StartIndex < other.StartIndex && EndIndex > other.StartIndex;
        }

        if (StartIndex <= other.StartIndex)
        {
            return EndIndex >= other.StartIndex;
        }

        return StartIndex <= other.EndIndex;
    }

    public override string ToString() => $"{File.Name} ({StartIndex}, {Length})";
}