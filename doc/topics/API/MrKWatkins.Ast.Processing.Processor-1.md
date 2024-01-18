# Processor&lt;TNode&gt; Class
## Definition

A processor to traverse a tree of nodes and perform some processing on some or all of them.

```c#
public abstract class Processor<TNode>
   where TNode : Node<TNode>
```

### Type Parameters

| Name | Description |
| ---- | ----------- |
| TNode | The type of nodes in the tree. |

## Methods

| Name | Description |
| ---- | ----------- |
| [Process](MrKWatkins.Ast.Processing.Processor-1.Process.md) | Processes a tree of nodes from the specified root node. |

