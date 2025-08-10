# NodeProcessor&lt;TBaseNode, TNode&gt;.Process Method
## Overloads

| Name | Description |
| ---- | ----------- |
| [Process(TBaseNode)](MrKWatkins.Ast.Processing.NodeProcessor-2.Process.md#mrkwatkins-ast-processing-nodeprocessor-2-process(-0)) | Performs processing on the specified `node` if it is of type `TNode`. Does not process any descendents. |
| [Process(TNode)](MrKWatkins.Ast.Processing.NodeProcessor-2.Process.md#mrkwatkins-ast-processing-nodeprocessor-2-process(-1)) | Performs processing on the specified `node`. Does not process any descendents. |

## Process(TBaseNode) {id="mrkwatkins-ast-processing-nodeprocessor-2-process(-0)"}

Performs processing on the specified `node` if it is of type `TNode`. Does not process any descendents.

```c#
public sealed override void Process(TBaseNode node);
```

## Parameters {id="parameters-mrkwatkins-ast-processing-nodeprocessor-2-process(-0)"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| node | TBaseNode | The node to process. |

## Process(TNode) {id="mrkwatkins-ast-processing-nodeprocessor-2-process(-1)"}

Performs processing on the specified `node`. Does not process any descendents.

```c#
protected new abstract void Process(TNode node);
```

## Parameters {id="parameters-mrkwatkins-ast-processing-nodeprocessor-2-process(-1)"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| node | TNode | The node to process. |

