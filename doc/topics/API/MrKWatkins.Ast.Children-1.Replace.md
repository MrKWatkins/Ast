# Children&lt;TNode&gt;.Replace Method
## Definition

Replaces a node in the collection with another node. The replacement will be removed from its parent and the node being replaced will have its parent removed.

```c#
public void Replace(TNode child, TNode replacement);
```

## Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| child | TNode | The node in the collection to replace. |
| replacement | TNode | The node to replace `child` with. |

