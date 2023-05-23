using MrKWatkins.Ast.Examples.Maths.Tree;
using MrKWatkins.Ast.Processing;
using MrKWatkins.Ast.Traversal;

namespace MrKWatkins.Ast.Examples.Maths.Processing;

/// <summary>
/// Simple <see cref="Replacer{TBaseNode,TNode}"/> that reduces constant expressions by evaluating up front. 
/// </summary>
/// <remarks>
/// Not by any means exhaustive! For example it will not take associativity into account and rearrange as necessary
/// to perform further reductions; 1 + 2 + x will reduce to 3 + x but 1 + x + 2 will not reduce.
/// </remarks>
internal sealed class Reducer : Replacer<MathsNode, BinaryOperation>
{
    protected override ITraversal<MathsNode> Traversal => DepthFirstPostOrderTraversal<MathsNode>.Instance;

    protected override MathsNode? ReplaceNode(BinaryOperation node)
    {
        if (node is { FirstChild: Constant left, LastChild: Constant right })
        {
            switch (node.Operator)
            {
                case '+':
                    return new Constant(left.Value + right.Value);
                
                case '-':
                    return new Constant(left.Value - right.Value);
                
                case '*':
                    return new Constant(left.Value * right.Value);
                
                case '/':
                    if (right.Value != 0)
                    {
                        return new Constant(left.Value / right.Value);
                    }

                    break;
            }
        }

        return null;
    }
}