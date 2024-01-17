# UnorderedProcessor&lt;TBaseNode, TNode&gt; Class
## Definition

A [Processor&lt;TNode&gt;](MrKWatkins.Ast.Processing.Processor-1.md) that processes the nodes of a specific type in a tree without caring about the order they are processed in.

```c#
public abstract class UnorderedProcessor<TBaseNode, TNode> : Processor<TBaseNode>
   where TBaseNode : Node<TBaseNode>
   where TNode : TBaseNode
```

### Type Parameters

| Name | Description |
| ---- | ----------- |
| TBaseNode | The base type of nodes in the tree. |
| TNode | The type of nodes to process. |

## Constructors

| Name | Description |
| ---- | ----------- |
| [UnorderedProcessor()](MrKWatkins.Ast.Processing.UnorderedProcessor-2.-ctor.md) |  |

## Methods

| Name | Description |
| ---- | ----------- |
| [ProcessNode(TNode)](MrKWatkins.Ast.Processing.UnorderedProcessor-2.ProcessNode.md) | Process the specified node. |
| [ShouldProcessNode(TNode)](MrKWatkins.Ast.Processing.UnorderedProcessor-2.ShouldProcessNode.md) | Override this method to optionally decide whether to process the specified node or not. Defaults to processing all nodes. |

