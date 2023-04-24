using MrKWatkins.Ast.Traversal;

namespace MrKWatkins.Ast.Processing;

public abstract class OrderedProcessor<TNode> : Processor<TNode>
    where TNode : Node<TNode>
{    
    internal override ProcessorState<TNode> CreateState(TNode root) => ProcessorState<TNode>.Create(ShouldProcessNode, ProcessNode);
    
    protected virtual ITraversal<TNode> Enumerator => DepthFirstPreOrderTraversal<TNode>.Instance;

    private protected sealed override IEnumerable<TNode> EnumerateNodes(ProcessorState<TNode> state, TNode root) =>
        Enumerator.Enumerate(
            root, 
            true, 
            node => state.Exceptions.Trap(node, nameof(ShouldProcessChildren), ShouldProcessChildren));

    [Pure]
    protected virtual bool ShouldProcessChildren(TNode node) => true;

    [Pure]
    protected virtual bool ShouldProcessNode(TNode node) => true;
    
    protected abstract void ProcessNode(TNode node);
}

public abstract class OrderedProcessor<TBaseNode, TNode> : Processor<TBaseNode>
    where TBaseNode : Node<TBaseNode>
    where TNode : TBaseNode
{
    internal override ProcessorState<TBaseNode> CreateState(TBaseNode root) => ProcessorState<TBaseNode>.Create<TNode>(ShouldProcessNode, ProcessNode);

    protected virtual ITraversal<TBaseNode> Enumerator => DepthFirstPreOrderTraversal<TBaseNode>.Instance;

    private protected sealed override IEnumerable<TBaseNode> EnumerateNodes(ProcessorState<TBaseNode> state, TBaseNode root) =>
        Enumerator.Enumerate(
            root,
            true,
            node => state.Exceptions.Trap<TBaseNode, TBaseNode>(node, nameof(ShouldProcessChildren), ShouldProcessChildren));

    [Pure]
    protected virtual bool ShouldProcessChildren(TBaseNode node) => true;
    
    [Pure]
    protected virtual bool ShouldProcessNode(TNode node) => true;

    protected abstract void ProcessNode(TNode node);
}