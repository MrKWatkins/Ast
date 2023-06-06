using System.Numerics;

namespace MrKWatkins.Ast.Position;

/// <summary>
/// A file of source code.
/// </summary>
public abstract class SourceFile : IEquatable<SourceFile>, IEqualityOperators<SourceFile, SourceFile, bool>
{
    /// <summary>
    /// Initialises a new instance of the <see cref="SourceFile" /> class.
    /// </summary>
    /// <param name="name">The name of the source file.</param>
    /// <param name="length">The length of the source file.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// If <paramref name="length"/> is less than 0.
    /// </exception>
    protected SourceFile(string name, int length)
    {
        if (length <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(length), length, "Value must be greater than 0.");
        }
        Name = name;
        Length = length;
    }

    /// <summary>
    /// The name of the source file.
    /// </summary>
    public string Name { get; }
    
    /// <summary>
    /// The length of the source file.
    /// </summary>
    public int Length { get; }

    /// <inheritdoc />
    public sealed override string ToString() => Name;

    /// <inheritdoc />
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

    /// <inheritdoc />
    public sealed override bool Equals(object? obj) => Equals(obj as SourceFile);

    /// <inheritdoc />
    public override int GetHashCode() => Name.GetHashCode();

    /// <summary>
    /// Determines whether two specified <see cref="SourceFile"/>s have the same value.
    /// </summary>
    /// <param name="left">The first <see cref="SourceFile"/> to compare, or <c>null</c>.</param>
    /// <param name="right">The second <see cref="SourceFile"/> to compare, or <c>null</c>.</param>
    /// <returns>
    /// <c>true</c> if the value of <paramref name="left"/> is the same as the value of <paramref name="right"/>; otherwise, <c>false</c>.
    /// </returns>
    public static bool operator ==(SourceFile? left, SourceFile? right) => Equals(left, right);

    /// <summary>
    /// Determines whether two specified <see cref="SourceFile"/>s have different values.
    /// </summary>
    /// <param name="left">The first <see cref="SourceFile"/> to compare, or <c>null</c>.</param>
    /// <param name="right">The second <see cref="SourceFile"/> to compare, or <c>null</c>.</param>
    /// <returns>
    /// <c>true</c> if the value of <paramref name="left"/> is different to the value of <paramref name="right"/>; otherwise, <c>false</c>.
    /// </returns>
    public static bool operator !=(SourceFile? left, SourceFile? right) => !Equals(left, right);
}