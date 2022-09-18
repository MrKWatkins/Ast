namespace MrKWatkins.Ast.Processing;

public interface IProcessor<TNode> where TNode : Node<TNode>
{
    void Process(TNode root);
}