# Replacer&lt;TContext, TBaseNode&gt;.Replace Method
## Definition

Optionally replace the specified node.

```c#
protected abstract TBaseNode? Replace(TContext? context, TBaseNode node);
```

## Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| context | TContext | The processing context. |
| node | TBaseNode | The node to potentially replace. |

## Returns

TBaseNode

A new node to replace `node` in the tree. Return `node` or `null` to leave `node` in the tree.
