# DefaultNodeFactory&lt;TNode&gt; Class
## Definition

Default implementation of [INodeFactory&lt;TNode&gt;](MrKWatkins.Ast.INodeFactory-1.md). Nodes to be created must have a parameterless constructor which can be public or non-public.

```c#
public sealed class DefaultNodeFactory<TNode> : INodeFactory<TNode>
   where TNode : Node<TNode>
```

### Type Parameters

| Name | Description |
| ---- | ----------- |
| TNode | The base type of nodes to create. |

## Fields

| Name | Description |
| ---- | ----------- |
| [Instance](MrKWatkins.Ast.DefaultNodeFactory-1.Instance.md) | Singleton instance of [DefaultNodeFactory&lt;TNode&gt;](MrKWatkins.Ast.DefaultNodeFactory-1.md). |

## Methods

| Name | Description |
| ---- | ----------- |
| [Create(Type)](MrKWatkins.Ast.DefaultNodeFactory-1.Create.md) | Creates a node of the specified type. The node must inherit from `TNode`. |

