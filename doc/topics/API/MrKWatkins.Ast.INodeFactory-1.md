# INodeFactory&lt;TNode&gt; Interface
## Definition

Factory to create nodes for a tree.

```c#
public abstract interface INodeFactory<TNode>
   where TNode : Node<TNode>
```

### Type Parameters

| Name | Description |
| ---- | ----------- |
| TNode | The base type of nodes to create. |

## Methods

| Name | Description |
| ---- | ----------- |
| [Create](MrKWatkins.Ast.INodeFactory-1.Create.md#mrkwatkins-ast-inodefactory-1-create(system-type)) | Creates a node of the specified type. The node must inherit from `TNode`. |
| [Create](MrKWatkins.Ast.INodeFactory-1.Create.md#mrkwatkins-ast-inodefactory-1-create-1) | Creates a node of the specified type. |

