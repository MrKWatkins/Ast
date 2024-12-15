# Node&lt;TNode&gt; Constructors
## Overloads

| Name | Description |
| ---- | ----------- |
| [Node()](MrKWatkins.Ast.Node-1.-ctor.md#mrkwatkins-ast-node-1-ctor) | Initialises a new instance of the [Node&lt;TNode&gt;](MrKWatkins.Ast.Node-1.md) class. |
| [Node(IEnumerable&lt;TNode&gt;)](MrKWatkins.Ast.Node-1.-ctor.md#mrkwatkins-ast-node-1-ctor(system-collections-generic-ienumerable((-0)))) | Initialises a new instance of the [Node&lt;TNode&gt;](MrKWatkins.Ast.Node-1.md) class with the specified children. |

## Node() {id="mrkwatkins-ast-node-1-ctor"}

Initialises a new instance of the [Node&lt;TNode&gt;](MrKWatkins.Ast.Node-1.md) class.

```c#
protected Node();
```

## Node(IEnumerable&lt;TNode&gt;) {id="mrkwatkins-ast-node-1-ctor(system-collections-generic-ienumerable((-0)))"}

Initialises a new instance of the [Node&lt;TNode&gt;](MrKWatkins.Ast.Node-1.md) class with the specified children.

```c#
protected Node(params IEnumerable<TNode> children);
```

## Parameters {id="parameters-mrkwatkins-ast-node-1-ctor(system-collections-generic-ienumerable((-0)))"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| children | [IEnumerable&lt;TNode&gt;](https://learn.microsoft.com/en-gb/dotnet/api/System.Collections.Generic.IEnumerable-1) | The children to add. |

