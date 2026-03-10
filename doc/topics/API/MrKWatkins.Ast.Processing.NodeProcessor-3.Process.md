# NodeProcessor&lt;TContext, TBaseNode, TNode&gt;.Process Method
## Overloads

| Name | Description |
| ---- | ----------- |
| [Process(TContext, TBaseNode)](MrKWatkins.Ast.Processing.NodeProcessor-3.Process.md#mrkwatkins-ast-processing-nodeprocessor-3-process(-0-1)) | Performs processing on the specified `node` if it is of type `TNode`. Does not process any descendents. |
| [Process(TContext, TNode)](MrKWatkins.Ast.Processing.NodeProcessor-3.Process.md#mrkwatkins-ast-processing-nodeprocessor-3-process(-0-2)) | Performs processing on the specified `node`. Does not process any descendents. |

## Process(TContext, TBaseNode) {id="mrkwatkins-ast-processing-nodeprocessor-3-process(-0-1)"}

Performs processing on the specified `node` if it is of type `TNode`. Does not process any descendents.

```c#
public sealed override TBaseNode Process(TContext context, TBaseNode node);
```

## Parameters {id="parameters-mrkwatkins-ast-processing-nodeprocessor-3-process(-0-1)"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| context | TContext | The processing context. |
| node | TBaseNode | The node to process. |

## Returns {id="returns-mrkwatkins-ast-processing-nodeprocessor-3-process(-0-1)"}

TBaseNode

The root node of the tree, which may have been replaced.

## Process(TContext, TNode) {id="mrkwatkins-ast-processing-nodeprocessor-3-process(-0-2)"}

Performs processing on the specified `node`. Does not process any descendents.

```c#
protected new abstract TBaseNode Process(TContext context, TNode node);
```

## Parameters {id="parameters-mrkwatkins-ast-processing-nodeprocessor-3-process(-0-2)"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| context | TContext | The processing context. |
| node | TNode | The node to process. |

## Returns {id="returns-mrkwatkins-ast-processing-nodeprocessor-3-process(-0-2)"}

TBaseNode

The root node of the tree, which may have been replaced.
