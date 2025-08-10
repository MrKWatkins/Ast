# OrderedNodeProcessor&lt;TBaseNode, TNode&gt;.ShouldProcessDescendents Method
## Overloads

| Name | Description |
| ---- | ----------- |
| [ShouldProcessDescendents(TBaseNode)](MrKWatkins.Ast.Processing.OrderedNodeProcessor-2.ShouldProcessDescendents.md#mrkwatkins-ast-processing-orderednodeprocessor-2-shouldprocessdescendents(-0)) |  |
| [ShouldProcessDescendents(TNode)](MrKWatkins.Ast.Processing.OrderedNodeProcessor-2.ShouldProcessDescendents.md#mrkwatkins-ast-processing-orderednodeprocessor-2-shouldprocessdescendents(-1)) | Whether descendents of this node should be processed by the [Pipeline&lt;TBaseNode&gt;](MrKWatkins.Ast.Processing.Pipeline-1.md) or not. Defaults to `true`. |

## ShouldProcessDescendents(TBaseNode) {id="mrkwatkins-ast-processing-orderednodeprocessor-2-shouldprocessdescendents(-0)"}

```c#
public sealed override bool ShouldProcessDescendents(TBaseNode node);
```

## Parameters {id="parameters-mrkwatkins-ast-processing-orderednodeprocessor-2-shouldprocessdescendents(-0)"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| node | TBaseNode |  |

## Returns {id="returns-mrkwatkins-ast-processing-orderednodeprocessor-2-shouldprocessdescendents(-0)"}

[Boolean](https://learn.microsoft.com/en-gb/dotnet/api/System.Boolean)
## ShouldProcessDescendents(TNode) {id="mrkwatkins-ast-processing-orderednodeprocessor-2-shouldprocessdescendents(-1)"}

Whether descendents of this node should be processed by the [Pipeline&lt;TBaseNode&gt;](MrKWatkins.Ast.Processing.Pipeline-1.md) or not. Defaults to `true`.

```c#
protected new virtual bool ShouldProcessDescendents(TNode node);
```

## Parameters {id="parameters-mrkwatkins-ast-processing-orderednodeprocessor-2-shouldprocessdescendents(-1)"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| node | TNode | The node. |

## Returns {id="returns-mrkwatkins-ast-processing-orderednodeprocessor-2-shouldprocessdescendents(-1)"}

[Boolean](https://learn.microsoft.com/en-gb/dotnet/api/System.Boolean)

`true` if descendents of `node` should be processed, `false` otherwise.
