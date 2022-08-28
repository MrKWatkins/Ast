using MrKWatkins.Ast.Position;

namespace MrKWatkins.Ast.Tests.Position;

public sealed class NonePositionTests
{
    [Test]
    public void Combine() => SourcePosition.None.Combine(SourcePosition.None).Should().Be(SourcePosition.None);
}