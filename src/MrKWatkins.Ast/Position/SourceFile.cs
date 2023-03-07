namespace MrKWatkins.Ast.Position;

/// <summary>
/// A file of source code.
/// </summary>
public abstract class SourceFile : IEquatable<SourceFile>
{
    protected SourceFile(string name, int length)
    {
        if (length <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(length), length, "Value must be greater than 0.");
        }
        Name = name;
        Length = length;
    }

    public string Name { get; }
    
    public int Length { get; }

    public sealed override string ToString() => Name;

    public bool Equals(SourceFile? other)
    {
        if (ReferenceEquals(null, other))
        {
            return false;
        }

        if (ReferenceEquals(this, other))
        {
            return true;
        }

        if (other.GetType() != GetType())
        {
            return false;
        }
        
        return Name == other.Name;
    }

    public sealed override bool Equals(object? obj) => Equals(obj as SourceFile);

    public override int GetHashCode() => Name.GetHashCode();

    public static bool operator ==(SourceFile? left, SourceFile? right) => Equals(left, right);

    public static bool operator !=(SourceFile? left, SourceFile? right) => !Equals(left, right);
}