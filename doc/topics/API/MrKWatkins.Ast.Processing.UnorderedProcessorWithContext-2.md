# UnorderedProcessorWithContext&lt;TContext, TNode&gt; Class
## Definition

A [Processor&lt;TNode&gt;](MrKWatkins.Ast.Processing.Processor-1.md) that processes the nodes in a tree without caring about the order they are processed in and gives access to a context object during processing.

```c#
public abstract class UnorderedProcessorWithContext<TContext, TNode> : Processor<TNode>
   where TContext
   where TNode : Node<TNode>
```

### Type Parameters

| Name | Description |
| ---- | ----------- |
| TContext | The type of the context object. |
| TNode | The type of nodes in the tree. |

## Constructors

| Name | Description |
| ---- | ----------- |
| [UnorderedProcessorWithContext()](MrKWatkins.Ast.Processing.UnorderedProcessorWithContext-2.-ctor.md) |  |

## Methods

| Name | Description |
| ---- | ----------- |
| [CreateContext(TNode)](MrKWatkins.Ast.Processing.UnorderedProcessorWithContext-2.CreateContext.md) | Override to create the context object. |
| [ProcessNode(TContext, TNode)](MrKWatkins.Ast.Processing.UnorderedProcessorWithContext-2.ProcessNode.md) | Process the specified node. |
| [ShouldProcessNode(TContext, TNode)](MrKWatkins.Ast.Processing.UnorderedProcessorWithContext-2.ShouldProcessNode.md) | Override this method to optionally decide whether to process the specified node or not. Defaults to processing all nodes. |

