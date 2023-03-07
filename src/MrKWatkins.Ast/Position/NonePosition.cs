namespace MrKWatkins.Ast.Position;

internal sealed class NonePosition : SourcePosition<NonePosition>
{
    protected override NonePosition Combine(NonePosition other) => this;
}