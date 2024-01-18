# PropertyNode&lt;TNode&gt;.Copy Method
## Overloads

| Name | Description |
| ---- | ----------- |
| [Copy](MrKWatkins.Ast.PropertyNode-1.Copy.md#mrkwatkins-ast-propertynode-1-copy) | Copies this node and its [Properties](MrKWatkins.Ast.PropertyNode-1.Properties.md) using the [DefaultNodeFactory&lt;TNode&gt;](MrKWatkins.Ast.DefaultNodeFactory-1.md). |
| [Copy](MrKWatkins.Ast.PropertyNode-1.Copy.md#mrkwatkins-ast-propertynode-1-copy(mrkwatkins-ast-inodefactory((-0)))) | Copies this node and its [Properties](MrKWatkins.Ast.PropertyNode-1.Properties.md) using the specified [INodeFactory&lt;TNode&gt;](MrKWatkins.Ast.INodeFactory-1.md). |

## Copy() {id="mrkwatkins-ast-propertynode-1-copy"}

Copies this node and its [Properties](MrKWatkins.Ast.PropertyNode-1.Properties.md) using the [DefaultNodeFactory&lt;TNode&gt;](MrKWatkins.Ast.DefaultNodeFactory-1.md).

```c#
public TNode Copy();
```

## Returns {id="returns-mrkwatkins-ast-propertynode-1-copy"}

TNode

A copy of this node.
## Remarks {id="remarks-mrkwatkins-ast-propertynode-1-copy"}

[SourcePosition](MrKWatkins.Ast.Node-1.SourcePosition.md) and [Messages](MrKWatkins.Ast.Node-1.Messages.md) are not copied. Copying is designed for reproducing parts of a tree or a general pattern. As such it doesn&#39;t make sense to copy [SourcePosition](MrKWatkins.Ast.Node-1.SourcePosition.md) because the new nodes will not come from the original place. Similarly any [Messages](MrKWatkins.Ast.Node-1.Messages.md) associated with the originals will not apply to the copy.
## Copy(INodeFactory&lt;TNode&gt;) {id="mrkwatkins-ast-propertynode-1-copy(mrkwatkins-ast-inodefactory((-0)))"}

Copies this node and its [Properties](MrKWatkins.Ast.PropertyNode-1.Properties.md) using the specified [INodeFactory&lt;TNode&gt;](MrKWatkins.Ast.INodeFactory-1.md).

```c#
public TNode Copy(INodeFactory<TNode> nodeFactory);
```

## Parameters {id="parameters-mrkwatkins-ast-propertynode-1-copy(mrkwatkins-ast-inodefactory((-0)))"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| nodeFactory | [INodeFactory&lt;TNode&gt;](MrKWatkins.Ast.INodeFactory-1.md) |  |

## Returns {id="returns-mrkwatkins-ast-propertynode-1-copy(mrkwatkins-ast-inodefactory((-0)))"}

TNode

A copy of this node.
## Remarks {id="remarks-mrkwatkins-ast-propertynode-1-copy(mrkwatkins-ast-inodefactory((-0)))"}

[SourcePosition](MrKWatkins.Ast.Node-1.SourcePosition.md) and [Messages](MrKWatkins.Ast.Node-1.Messages.md) are not copied. Copying is designed for reproducing parts of a tree or a general pattern. As such it doesn&#39;t make sense to copy [SourcePosition](MrKWatkins.Ast.Node-1.SourcePosition.md) because the new nodes will not come from the original place. Similarly any [Messages](MrKWatkins.Ast.Node-1.Messages.md) associated with the originals will not apply to the copy.
