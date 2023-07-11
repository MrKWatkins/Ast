namespace MrKWatkins.Ast.Position;

/// <summary>
/// A text <see cref="SourceFile" />.
/// </summary>
public sealed class TextFile : SourceFile
{
    /// <summary>
    /// Initialises a new instance of the <see cref="TextFile" /> class from a file on disk.
    /// </summary>
    /// <param name="file">
    /// A <see cref="FileInfo" /> with details a file on disk to load. The <see cref="FileSystemInfo.FullName" /> will be used for <see cref="SourceFile.Name" />.
    /// </param>
    public TextFile(FileInfo file)
        : this(file.FullName, File.ReadAllText(file.FullName))
    {
    }

    /// <summary>
    /// Initialises a new instance of the <see cref="TextFile" /> class from a <see cref="Stream" /> containing the file.
    /// </summary>
    /// <param name="name">The name of the file.</param>
    /// <param name="text">A <see cref="Stream" /> containing the file. The stream will be read to the end and left open.</param>
    public TextFile(string name, Stream text)
        : this(name, ReadStream(text))
    {
    }

    /// <summary>
    /// Initialises a new instance of the <see cref="BinaryFile" /> class from a string containing the file.
    /// </summary>
    /// <param name="name">The name of the file.</param>
    /// <param name="text">The contents of the file.</param>
    public TextFile(string name, string text)
        : base(name, text.Length)
    {
        Text = text;
        // Decided not to create this lazily, even though we might only need it for logging errors,
        // so that StartLineNumber and StartColumnNumber can be validated in the constructor of
        // TextFilePosition.
        Lines = ReadLines(text);
    }

    /// <summary>
    /// The text of the file.
    /// </summary>
    public string Text { get; }

    /// <summary>
    /// The individual lines in the file.
    /// </summary>
    public IReadOnlyList<string> Lines { get; }

    /// <summary>
    /// Creates a <see cref="TextFilePosition" /> from this <see cref="TextFile" />.
    /// </summary>
    /// <param name="startIndex">The start index of the position in the file.</param>
    /// <param name="length">The length of the file.</param>
    /// <param name="startLineIndex"> Zero based index of the start line of the position in the text file.</param>
    /// <param name="startColumnIndex">Zero based index of the start column of the position in the text file.</param>
    /// <returns>A new <see cref="TextFilePosition" /> instance.</returns>
    [Pure]
    public TextFilePosition CreatePosition(int startIndex, int length, int startLineIndex, int startColumnIndex) =>
        new(this, startIndex, length, startLineIndex, startColumnIndex);

    /// <summary>
    /// Creates a <see cref="TextFilePosition" /> from this <see cref="TextFile" /> that represents the whole file.
    /// </summary>
    /// <returns>A new <see cref="TextFilePosition" /> instance.</returns>
    [Pure]
    public TextFilePosition CreateEntireFilePosition() => new(this, 0, Length, 0, 0);

    [Pure]
    private static string ReadStream(Stream stream)
    {
        using var streamReader = new StreamReader(stream);
        return streamReader.ReadToEnd();
    }

    [Pure]
    private static IReadOnlyList<string> ReadLines(string text)
    {
        var linesList = new List<string>();

        using var reader = new StringReader(text);
        while (reader.ReadLine() is { } line)
        {
            linesList.Add(line);
        }

        return linesList.ToArray();
    }
}