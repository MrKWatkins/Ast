namespace MrKWatkins.Ast.Position;

public sealed class TextFile : SourceFile
{
    public TextFile(FileInfo file)
        : this(file.FullName, File.ReadAllText(file.FullName))
    {
    }
    
    public TextFile(string name, Stream text) 
        : this(name, ReadStream(text))
    {
    }
    
    public TextFile(string name, string text) 
        : base(name, text.Length)
    {
        Text = text;
        // Decided not to create this lazily, even though we might only need it for logging errors,
        // so that StartLineNumber and StartColumnNumber can be validated in the constructor of
        // TextFilePosition.
        Lines = ReadLines(text);
    }
    
    public string Text { get; }

    public IReadOnlyList<string> Lines { get; }

    [Pure]
    public TextFilePosition CreatePosition(int startIndex, int length, int startLineIndex, int startColumnIndex) =>
        new(this, startIndex, length, startLineIndex, startColumnIndex);

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