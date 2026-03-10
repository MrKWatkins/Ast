# OrderedNodeProcessor&lt;TContext, TBaseNode, TNode&gt;.Process Method
## Overloads

| Name | Description |
| ---- | ----------- |
| [Process(TContext, TBaseNode)](MrKWatkins.Ast.Processing.OrderedNodeProcessor-3.Process.md#mrkwatkins-ast-processing-orderednodeprocessor-3-process(-0-1)) |  |
| [Process(TContext, TNode)](MrKWatkins.Ast.Processing.OrderedNodeProcessor-3.Process.md#mrkwatkins-ast-processing-orderednodeprocessor-3-process(-0-2)) | Performs processing on the specified `node`. Does not process any descendents. |

## Process(TContext, TBaseNode) {id="mrkwatkins-ast-processing-orderednodeprocessor-3-process(-0-1)"}

```c#
public sealed override TBaseNode Process(TContext context, TBaseNode node);
```

## Parameters {id="parameters-mrkwatkins-ast-processing-orderednodeprocessor-3-process(-0-1)"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| context | TContext |  |
| node | TBaseNode |  |

## Returns {id="returns-mrkwatkins-ast-processing-orderednodeprocessor-3-process(-0-1)"}

TBaseNode

## Process(TContext, TNode) {id="mrkwatkins-ast-processing-orderednodeprocessor-3-process(-0-2)"}

Performs processing on the specified `node`. Does not process any descendents.

```c#
protected new abstract TBaseNode Process(TContext context, TNode node);
```

## Parameters {id="parameters-mrkwatkins-ast-processing-orderednodeprocessor-3-process(-0-2)"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| context | TContext | The processing context. |
| node | TNode | The node to process. |

## Returns {id="returns-mrkwatkins-ast-processing-orderednodeprocessor-3-process(-0-2)"}

TBaseNode
