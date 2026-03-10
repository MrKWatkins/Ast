# OrderedNodeProcessor&lt;TBaseNode, TNode&gt;.Process Method
## Overloads

| Name | Description |
| ---- | ----------- |
| [Process(TBaseNode)](MrKWatkins.Ast.Processing.OrderedNodeProcessor-2.Process.md#mrkwatkins-ast-processing-orderednodeprocessor-2-process(-0)) |  |
| [Process(TNode)](MrKWatkins.Ast.Processing.OrderedNodeProcessor-2.Process.md#mrkwatkins-ast-processing-orderednodeprocessor-2-process(-1)) | Performs processing on the specified `node`. Does not process any descendents. |

## Process(TBaseNode) {id="mrkwatkins-ast-processing-orderednodeprocessor-2-process(-0)"}

```c#
public sealed override TBaseNode Process(TBaseNode node);
```

## Parameters {id="parameters-mrkwatkins-ast-processing-orderednodeprocessor-2-process(-0)"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| node | TBaseNode |  |

## Returns {id="returns-mrkwatkins-ast-processing-orderednodeprocessor-2-process(-0)"}

TBaseNode

## Process(TNode) {id="mrkwatkins-ast-processing-orderednodeprocessor-2-process(-1)"}

Performs processing on the specified `node`. Does not process any descendents.

```c#
protected new abstract TBaseNode Process(TNode node);
```

## Parameters {id="parameters-mrkwatkins-ast-processing-orderednodeprocessor-2-process(-1)"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| node | TNode | The node to process. |

## Returns {id="returns-mrkwatkins-ast-processing-orderednodeprocessor-2-process(-1)"}

TBaseNode
