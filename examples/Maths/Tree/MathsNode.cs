namespace MrKWatkins.Ast.Examples.Maths.Tree;

/// <summary>
/// Base type for nodes in the AST.
/// </summary>
public abstract class MathsNode : Node<MathsNode>
{
    private protected MathsNode()
    {
    }
}