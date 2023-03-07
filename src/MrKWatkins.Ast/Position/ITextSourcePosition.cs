using System.Text;

namespace MrKWatkins.Ast.Position;

/// <summary>
/// Contract for a <see cref="SourcePosition{TSelf}" /> that can be represented as text.
/// </summary>
public interface ITextSourcePosition
{
    /// <summary>
    /// Writes the source for the purposes of displaying a <see cref="Message" />.
    /// </summary>
    void WriteSourceForMessage(StringBuilder builder);
}