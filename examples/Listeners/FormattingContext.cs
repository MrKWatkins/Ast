using System.Text;

namespace MrKWatkins.Ast.Examples.Listeners;

/// <summary>
/// Context object for the listeners.
/// </summary>
internal sealed class FormattingContext
{
    public StringBuilder Output { get; } = new();
}