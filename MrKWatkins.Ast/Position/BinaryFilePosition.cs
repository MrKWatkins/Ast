namespace MrKWatkins.Ast.Position;

public sealed class BinaryFilePosition : SourceFilePosition<BinaryFilePosition, BinaryFile>
{
    internal BinaryFilePosition(BinaryFile file, int startIndex, int length) 
        : base(file, startIndex, length)
    {
    }

    protected override BinaryFilePosition CreateCombination(BinaryFilePosition other)
    {
        var startIndex = StartIndex < other.StartIndex ? StartIndex : other.StartIndex;
        var endIndex = EndIndex > other.EndIndex ? EndIndex : other.EndIndex;

        return new BinaryFilePosition(File, startIndex, endIndex - startIndex + 1);
    }
}