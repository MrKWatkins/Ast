namespace MrKWatkins.Ast.Processing;

public interface IProcessor<in TNode>
    where TNode : Node<TNode>
{
    void Process(TNode root);
}