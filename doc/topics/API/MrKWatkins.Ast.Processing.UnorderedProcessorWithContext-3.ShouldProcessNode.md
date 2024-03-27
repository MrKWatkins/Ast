# UnorderedProcessorWithContext&lt;TContext, TBaseNode, TNode&gt;.ShouldProcessNode Method
## Definition

Override this method to optionally decide whether to process the specified node or not. Defaults to processing all nodes.

```c#
protected virtual bool ShouldProcessNode(TContext? context, TNode node);
```

## Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| context | TContext | The context object. |
| node | TNode | The node. |

## Returns

[Boolean](https://learn.microsoft.com/en-gb/dotnet/api/System.Boolean)

`true` if `node` should be processed, `false` otherwise.
