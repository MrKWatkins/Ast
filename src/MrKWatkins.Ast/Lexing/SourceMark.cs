namespace MrKWatkins.Ast.Lexing;

/// <summary>
/// A position in a <see cref="SourceReader" />, created by <see cref="SourceReader.Mark" /> and typically used to mark the start of a
/// token before scanning to its end.
/// </summary>
public readonly record struct SourceMark
{
    internal SourceMark(int index, int lineIndex, int columnIndex)
    {
        Index = index;
        LineIndex = lineIndex;
        ColumnIndex = columnIndex;
    }

    /// <summary>
    /// The index of the position in the file.
    /// </summary>
    /// <returns>The index.</returns>
    public int Index { get; }

    /// <summary>
    /// Zero based index of the line of the position in the file.
    /// </summary>
    /// <returns>The index of the line.</returns>
    public int LineIndex { get; }

    /// <summary>
    /// Zero based index of the column of the position in the file.
    /// </summary>
    /// <returns>The index of the column.</returns>
    public int ColumnIndex { get; }
}