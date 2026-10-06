using MrKWatkins.Ast.Traversal;

namespace MrKWatkins.Ast.Processing;

/// <summary>
/// Implemented by processors that control the traversal of the tree, i.e. <see cref="OrderedProcessor{TContext, TBaseNode}" /> and
/// <see cref="OrderedProcessor{TBaseNode}" />, so that the pipeline can treat both the same way.
/// </summary>
internal interface IOrderedProcessor<TContext, TBaseNode>
    where TBaseNode : Node<TBaseNode>
{
    ITraversal<TBaseNode> GetTraversal(TContext context, TBaseNode root);

    bool ShouldProcessDescendants(TContext context, TBaseNode node);
}