# Replacer&lt;TBaseNode, TNode, TReplacementNode&gt;.ReplaceNode Method
## Definition

Optionally replace the specified node.

```c#
protected abstract TReplacementNode ReplaceNode(TNode node);
```

## Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| node | TNode | The node to potentially replace. |

## Returns

TReplacementNode

A new node to replace `node` in the tree. Return `node` or `null` to leave `node` in the tree.
