using System.Text;

namespace MrKWatkins.Ast.Position;

/// <summary>
/// A <see cref="SourcePosition" /> in a text source code file.
/// </summary>
public sealed class TextFilePosition : SourceFilePosition<TextFilePosition, TextFile>, ITextSourcePosition
{
    internal TextFilePosition(TextFile file, int startIndex, int length, int startLineIndex, int startColumnIndex) 
        : base(file, startIndex, length)
    {
        if (startLineIndex < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(startLineIndex), startLineIndex, "Value must be 0 or greater.");
        }
        
        if (startColumnIndex < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(startColumnIndex), startColumnIndex, "Value must be 0 or greater.");
        }
        
        if (startLineIndex >= File.Lines.Count)
        {
            throw new ArgumentOutOfRangeException(nameof(startLineIndex), startLineIndex, $"Value must be less than the number of lines in {nameof(file)}. ({file.Lines.Count})");
        }

        var startLine = file.Lines[startLineIndex];
        if (startLine.Length == 0)
        {
            if (startColumnIndex != 0)
            {
                throw new ArgumentOutOfRangeException(nameof(startColumnIndex), startColumnIndex, "Value must be 0 for a start line of 0 length.");
            }
        }
        else if (startColumnIndex >= startLine.Length)
        {
            throw new ArgumentOutOfRangeException(nameof(startColumnIndex), startColumnIndex, $"Value must be less than the length of the start line. ({startLine.Length})");
        }
        
        // Not validating that line/column match index/length as it's vaguely complicated due to line endings...

        StartLineIndex = startLineIndex;
        StartColumnIndex = startColumnIndex;
    }

    /// <summary>
    /// Zero based index of the start line of the position in the text file.
    /// </summary>
    public int StartLineIndex { get; }
    
    /// <summary>
    /// Number, i.e. 1 based index, of the start line of the position in the text file.
    /// </summary>
    public int StartLineNumber => StartLineIndex + 1;

    /// <summary>
    /// Zero based index of the start column of the position in the text file.
    /// </summary>
    public int StartColumnIndex { get; }
    
    /// <summary>
    /// Number, i.e. 1 based index, of the start column of the position in the text file.
    /// </summary>
    public int StartColumnNumber => StartColumnIndex + 1;

    /// <summary>
    /// The start line of the text source.
    /// </summary>
    public string StartLine => File.Lines[StartLineIndex];

    /// <summary>
    /// The text source.
    /// </summary>
    public string Text => File.Text.Substring(StartIndex, Length);

    /// <inheritdoc />
    protected override TextFilePosition CreateCombination(TextFilePosition other)
    {
        int startIndex, startLineIndex, startColumnIndex;
        if (StartIndex < other.StartIndex)
        {
            startIndex = StartIndex;
            startLineIndex = StartLineIndex;
            startColumnIndex = StartColumnIndex;
        }
        else
        {
            startIndex = other.StartIndex;
            startLineIndex = other.StartLineIndex;
            startColumnIndex = other.StartColumnIndex;
        }

        var endIndex = EndIndex > other.EndIndex ? EndIndex : other.EndIndex;

        return new TextFilePosition(File, startIndex, endIndex - startIndex, startLineIndex, startColumnIndex);
    }

    // Matches the C# compiler, with some extra spacing.
    /// <inheritdoc />
    public override string ToString() => $"{File.Name} ({StartLineNumber}, {StartColumnNumber})";

    /// <inheritdoc />
    public void WriteSourceForMessage(StringBuilder builder)
    {
        var line = StartLine;
        builder.AppendLine(line);
        
        // Build an underscore line. We want to preserve any tabs from the original line so everything lines up.
        // Will still mess up if the underscore section contains tabs of course, but hopefully that's rare...
        for (var f = 0; f < StartColumnIndex; f++)
        {
            builder.Append(line[f] == '\t' ? '\t' : ' ');
        }

        var length = Math.Min(Length, line.Length - StartColumnNumber + 1);
        builder.Append('-', length);
    }
}