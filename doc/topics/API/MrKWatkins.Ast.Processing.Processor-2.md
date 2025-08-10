# Processor&lt;TContext, TBaseNode&gt; Class
## Definition

Performs some processing on a given node using a processing context in a [Pipeline&lt;TContext, TBaseNode&gt;](MrKWatkins.Ast.Processing.Pipeline-2.md).

```c#
public abstract class Processor<TContext, TBaseNode>
   where TBaseNode : Node<TBaseNode>
```

### Type Parameters

| Name | Description |
| ---- | ----------- |
| TContext | The type of the processing context. |
| TBaseNode | The type of nodes in the tree. |

## Constructors

| Name | Description |
| ---- | ----------- |
| [Processor()](MrKWatkins.Ast.Processing.Processor-2.-ctor.md) |  |

## Methods

| Name | Description |
| ---- | ----------- |
| [Process(TContext, TBaseNode)](MrKWatkins.Ast.Processing.Processor-2.Process.md) | Performs processing on the specified `node`. Does not process any descendents. |

