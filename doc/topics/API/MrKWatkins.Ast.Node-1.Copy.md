# Node&lt;TNode&gt;.Copy Method
## Overloads

| Name | Description |
| ---- | ----------- |
| [Copy()](MrKWatkins.Ast.Node-1.Copy.md#mrkwatkins-ast-node-1-copy) | Copies this node using the [DefaultNodeFactory&lt;TNode&gt;](MrKWatkins.Ast.DefaultNodeFactory-1.md). |
| [Copy(INodeFactory&lt;TNode&gt;)](MrKWatkins.Ast.Node-1.Copy.md#mrkwatkins-ast-node-1-copy(mrkwatkins-ast-inodefactory((-0)))) | Copies this node using the specified [INodeFactory&lt;TNode&gt;](MrKWatkins.Ast.INodeFactory-1.md). |

## Copy() {id="mrkwatkins-ast-node-1-copy"}

Copies this node using the [DefaultNodeFactory&lt;TNode&gt;](MrKWatkins.Ast.DefaultNodeFactory-1.md).

```c#
public TNode Copy();
```

## Returns {id="returns-mrkwatkins-ast-node-1-copy"}

TNode

A copy of this node.
## Copy(INodeFactory&lt;TNode&gt;) {id="mrkwatkins-ast-node-1-copy(mrkwatkins-ast-inodefactory((-0)))"}

Copies this node using the specified [INodeFactory&lt;TNode&gt;](MrKWatkins.Ast.INodeFactory-1.md).

```c#
public TNode Copy(INodeFactory<TNode> nodeFactory);
```

## Parameters {id="parameters-mrkwatkins-ast-node-1-copy(mrkwatkins-ast-inodefactory((-0)))"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| nodeFactory | [INodeFactory&lt;TNode&gt;](MrKWatkins.Ast.INodeFactory-1.md) |  |

## Returns {id="returns-mrkwatkins-ast-node-1-copy(mrkwatkins-ast-inodefactory((-0)))"}

TNode

A copy of this node.
