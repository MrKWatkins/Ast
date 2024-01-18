# UnorderedProcessor&lt;TNode&gt; Class
## Definition

A [Processor&lt;TNode&gt;](MrKWatkins.Ast.Processing.Processor-1.md) that processes the nodes in a tree without caring about the order they are processed in.

```c#
public abstract class UnorderedProcessor<TNode> : Processor<TNode>
   where TNode : Node<TNode>
```

### Type Parameters

| Name | Description |
| ---- | ----------- |
| TNode | The type of nodes in the tree. |

## Constructors

| Name | Description |
| ---- | ----------- |
| [UnorderedProcessor()](MrKWatkins.Ast.Processing.UnorderedProcessor-1.-ctor.md) |  |

## Methods

| Name | Description |
| ---- | ----------- |
| [ProcessNode](MrKWatkins.Ast.Processing.UnorderedProcessor-1.ProcessNode.md) | Process the specified node. |
| [ShouldProcessNode](MrKWatkins.Ast.Processing.UnorderedProcessor-1.ShouldProcessNode.md) | Override this method to optionally decide whether to process the specified node or not. Defaults to processing all nodes. |

