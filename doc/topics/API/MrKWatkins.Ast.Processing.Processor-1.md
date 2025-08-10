# Processor&lt;TBaseNode&gt; Class
## Definition

Performs some processing on a given node in a [Pipeline&lt;TBaseNode&gt;](MrKWatkins.Ast.Processing.Pipeline-1.md).

```c#
public abstract class Processor<TBaseNode>
   where TBaseNode : Node<TBaseNode>
```

### Type Parameters

| Name | Description |
| ---- | ----------- |
| TBaseNode | The type of nodes in the tree. |

## Constructors

| Name | Description |
| ---- | ----------- |
| [Processor()](MrKWatkins.Ast.Processing.Processor-1.-ctor.md) |  |

## Methods

| Name | Description |
| ---- | ----------- |
| [Process(TBaseNode)](MrKWatkins.Ast.Processing.Processor-1.Process.md) | Performs processing on the specified `node`. Does not process any descendents. |

