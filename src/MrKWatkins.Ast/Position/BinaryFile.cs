namespace MrKWatkins.Ast.Position;

/// <summary>
/// A binary <see cref="SourceFile" />.
/// </summary>
public sealed class BinaryFile : SourceFile
{
    /// <summary>
    /// Initialises a new instance of the <see cref="BinaryFile" /> class from a file on disk.
    /// </summary>
    /// <param name="file">
    /// A <see cref="FileInfo" /> with details a file on disk to load. The <see cref="FileInfo.FullName" /> will be used for <see cref="SourceFile.Name" />.
    /// </param>
    public BinaryFile(FileInfo file)
        : this(file.FullName, File.ReadAllBytes(file.FullName))
    {
    }

    /// <summary>
    /// Initialises a new instance of the <see cref="BinaryFile" /> class from a <see cref="Stream" /> containing the file.
    /// </summary>
    /// <param name="name">The name of the file.</param>
    /// <param name="file">A <see cref="Stream" /> containing the file. The stream will be read to the end and left open.</param>
    public BinaryFile(string name, Stream file)
        : this(name, ReadStream(file))
    {
    }

    /// <summary>
    /// Initialises a new instance of the <see cref="BinaryFile" /> class from a list of bytes containing the file.
    /// </summary>
    /// <param name="name">The name of the file.</param>
    /// <param name="bytes">The contents of the file.</param>
    public BinaryFile(string name, IReadOnlyList<byte> bytes)
        : base(name, bytes.Count)
    {
        Bytes = bytes;
    }

    /// <summary>
    /// The raw bytes of the file.
    /// </summary>
    public IReadOnlyList<byte> Bytes { get; }

    /// <summary>
    /// Creates a <see cref="BinaryFilePosition" /> from this <see cref="BinaryFile" />.
    /// </summary>
    /// <param name="startIndex">The start index of the position in the file.</param>
    /// <param name="length">The length of the file.</param>
    /// <returns>A new <see cref="BinaryFilePosition" /> instance.</returns>
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