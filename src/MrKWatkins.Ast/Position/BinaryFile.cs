namespace MrKWatkins.Ast.Position;

public sealed class BinaryFile : SourceFile
{
    public BinaryFile(FileInfo file)
        : this(file.FullName, File.ReadAllBytes(file.FullName))
    {
    }
    
    public BinaryFile(string name, Stream file) 
        : this(name, ReadStream(file))
    {
    }
    
    public BinaryFile(string name, IReadOnlyList<byte> bytes) 
        : base(name, bytes.Count)
    {
        Bytes = bytes;
    }
    
    public IReadOnlyList<byte> Bytes { get; }

    [Pure]
    public BinaryFilePosition CreatePosition(int startIndex, int length) =>
        new(this, startIndex, length);

    [Pure]
    private static IReadOnlyList<byte> ReadStream(Stream stream)
    {
        using var memoryStream = new MemoryStream();
        stream.CopyTo(memoryStream);
        return memoryStream.ToArray();
    }
}