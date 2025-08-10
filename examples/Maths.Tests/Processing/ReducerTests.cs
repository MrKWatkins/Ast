using MrKWatkins.Ast.Examples.Maths.Processing;
using MrKWatkins.Ast.Examples.Maths.Tree;
using MrKWatkins.Ast.Processing;

namespace MrKWatkins.Ast.Examples.Maths.Tests.Processing;

public sealed class ReducerTests : TestFixture
{
    [TestCase("1", "() => 1")]
    [TestCase("1 + 2 * 3", "() => 7")]
    [TestCase("(10 - 5) * 3", "() => 15")]
    [TestCase("1 + 2 + b / 3", "(b) => (+ 3 (/ b 3))")]
    [TestCase("3 - x / (5 - 5)", "(x) => (- 3 (/ x 0))")]
    [TestCase("3 - 2 / 0", "() => (- 3 (/ 2 0))")]
    [TestCase("3 - 10 / 5", "() => 1")]
    public void Process(string expression, string reduced)
    {
        var function = ParseWithoutProcessing(expression);

        var pipeline = Pipeline<MathsNode>.Build(builder => builder.AddStage<Reducer>("Reduction"));
        pipeline.Run(function);

        function.ToString().Should().SequenceEqual(reduced);
    }
}