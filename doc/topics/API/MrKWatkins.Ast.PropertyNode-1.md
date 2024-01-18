# PropertyNode&lt;TNode&gt; Class
## Definition

Abstract base class for nodes in a tree with a collection of arbitrary properties that can be copied.

```c#
public abstract class PropertyNode<TNode> : Node<TNode>
   where TNode : PropertyNode<TNode>
```

### Type Parameters

| Name | Description |
| ---- | ----------- |
| TNode | Self generic node parameter. |

## Constructors

| Name | Description |
| ---- | ----------- |
| [PropertyNode()](MrKWatkins.Ast.PropertyNode-1.-ctor.md#mrkwatkins-ast-propertynode-1-ctor) | Initialises a new instance of the [Node&lt;TNode&gt;](MrKWatkins.Ast.Node-1.md) class. |
| [PropertyNode(IEnumerable&lt;TNode&gt;)](MrKWatkins.Ast.PropertyNode-1.-ctor.md#mrkwatkins-ast-propertynode-1-ctor(system-collections-generic-ienumerable((-0)))) | Initialises a new instance of the [Node&lt;TNode&gt;](MrKWatkins.Ast.Node-1.md) class with the specified children. |

## Properties

| Name | Description |
| ---- | ----------- |
| [Properties](MrKWatkins.Ast.PropertyNode-1.Properties.md) | The [Properties](MrKWatkins.Ast.Properties.md) associated with this node. |

## Methods

| Name | Description |
| ---- | ----------- |
| [Copy](MrKWatkins.Ast.PropertyNode-1.Copy.md#mrkwatkins-ast-propertynode-1-copy) | Copies this node and its [Properties](MrKWatkins.Ast.PropertyNode-1.Properties.md) using the [DefaultNodeFactory&lt;TNode&gt;](MrKWatkins.Ast.DefaultNodeFactory-1.md). |
| [Copy](MrKWatkins.Ast.PropertyNode-1.Copy.md#mrkwatkins-ast-propertynode-1-copy(mrkwatkins-ast-inodefactory((-0)))) | Copies this node and its [Properties](MrKWatkins.Ast.PropertyNode-1.Properties.md) using the specified [INodeFactory&lt;TNode&gt;](MrKWatkins.Ast.INodeFactory-1.md). |

