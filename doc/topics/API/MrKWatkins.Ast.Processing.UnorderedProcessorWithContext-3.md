# UnorderedProcessorWithContext&lt;TContext, TBaseNode, TNode&gt; Class
## Definition

A [Processor&lt;TNode&gt;](MrKWatkins.Ast.Processing.Processor-1.md) that processes the nodes of a specific type that processes the nodes in a tree without caring about the order they are processed in and gives access to a context object during processing.

```c#
public abstract class UnorderedProcessorWithContext<TContext, TBaseNode, TNode> : Processor<TBaseNode>
   where TBaseNode : Node<TBaseNode>
   where TNode : TBaseNode
```

### Type Parameters

| Name | Description |
| ---- | ----------- |
| TContext | The type of the context object. |
| TBaseNode | The base type of nodes in the tree. |
| TNode | The type of nodes to process. |

## Constructors

| Name | Description |
| ---- | ----------- |
| [UnorderedProcessorWithContext()](MrKWatkins.Ast.Processing.UnorderedProcessorWithContext-3.-ctor.md) |  |

## Methods

| Name | Description |
| ---- | ----------- |
| [CreateContext(TBaseNode)](MrKWatkins.Ast.Processing.UnorderedProcessorWithContext-3.CreateContext.md) | Override to create the context object. |
| [ProcessNode(TContext, TNode)](MrKWatkins.Ast.Processing.UnorderedProcessorWithContext-3.ProcessNode.md) | Process the specified node. |
| [ShouldProcessNode(TContext, TNode)](MrKWatkins.Ast.Processing.UnorderedProcessorWithContext-3.ShouldProcessNode.md) | Override this method to optionally decide whether to process the specified node or not. Defaults to processing all nodes. |

