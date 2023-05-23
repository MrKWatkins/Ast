using MrKWatkins.Ast.Examples.Maths.Tree;
using MrKWatkins.Ast.Processing;

namespace MrKWatkins.Ast.Examples.Maths.Processing;

internal static class Processor
{
    private static readonly Pipeline<MathsNode> Pipeline =
        Pipeline<MathsNode>
            .Build(builder =>
                builder
                    .AddStage<Reducer>("Reduction")
                    .AddStage<DivideByZeroValidator>("Validation"));

    public static Function Process(Function function)
    {
        Pipeline.Run(function);
        return function;
    }
}