# OrderedNodeProcessor&lt;TContext, TBaseNode, TNode&gt;.ShouldProcessDescendents Method
## Overloads

| Name | Description |
| ---- | ----------- |
| [ShouldProcessDescendents(TContext, TBaseNode)](MrKWatkins.Ast.Processing.OrderedNodeProcessor-3.ShouldProcessDescendents.md#mrkwatkins-ast-processing-orderednodeprocessor-3-shouldprocessdescendents(-0-1)) |  |
| [ShouldProcessDescendents(TContext, TNode)](MrKWatkins.Ast.Processing.OrderedNodeProcessor-3.ShouldProcessDescendents.md#mrkwatkins-ast-processing-orderednodeprocessor-3-shouldprocessdescendents(-0-2)) | Whether descendents of this node should be processed by the [Pipeline&lt;TContext, TBaseNode&gt;](MrKWatkins.Ast.Processing.Pipeline-2.md) or not. Defaults to `true`. |

## ShouldProcessDescendents(TContext, TBaseNode) {id="mrkwatkins-ast-processing-orderednodeprocessor-3-shouldprocessdescendents(-0-1)"}

```c#
public sealed override bool ShouldProcessDescendents(TContext? context, TBaseNode node);
```

## Parameters {id="parameters-mrkwatkins-ast-processing-orderednodeprocessor-3-shouldprocessdescendents(-0-1)"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| context | TContext |  |
| node | TBaseNode |  |

## Returns {id="returns-mrkwatkins-ast-processing-orderednodeprocessor-3-shouldprocessdescendents(-0-1)"}

[Boolean](https://learn.microsoft.com/en-gb/dotnet/api/System.Boolean)
## ShouldProcessDescendents(TContext, TNode) {id="mrkwatkins-ast-processing-orderednodeprocessor-3-shouldprocessdescendents(-0-2)"}

Whether descendents of this node should be processed by the [Pipeline&lt;TContext, TBaseNode&gt;](MrKWatkins.Ast.Processing.Pipeline-2.md) or not. Defaults to `true`.

```c#
protected new virtual bool ShouldProcessDescendents(TContext? context, TNode node);
```

## Parameters {id="parameters-mrkwatkins-ast-processing-orderednodeprocessor-3-shouldprocessdescendents(-0-2)"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| context | TContext | The processing context. |
| node | TNode | The node. |

## Returns {id="returns-mrkwatkins-ast-processing-orderednodeprocessor-3-shouldprocessdescendents(-0-2)"}

[Boolean](https://learn.microsoft.com/en-gb/dotnet/api/System.Boolean)

`true` if descendents of `node` should be processed, `false` otherwise.
