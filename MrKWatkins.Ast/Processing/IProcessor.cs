namespace MrKWatkins.Ast.Processing;

internal interface IProcessor<in TNode>
    where TNode : Node<TNode>
{
    void Process(TNode root);
}