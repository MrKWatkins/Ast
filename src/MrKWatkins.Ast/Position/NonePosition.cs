namespace MrKWatkins.Ast.Position;

internal sealed class NonePosition : SourcePosition<NonePosition>
{
    protected override NonePosition Combine(NonePosition other) => this;

    public override NonePosition CreateZeroWidthPrefix() => this;
    
    public override bool Equals(SourcePosition? other) => ReferenceEquals(this, other);
}