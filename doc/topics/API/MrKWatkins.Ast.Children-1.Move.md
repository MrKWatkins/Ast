# Children&lt;TNode&gt;.Move Method
## Overloads

| Name | Description |
| ---- | ----------- |
| [Move](MrKWatkins.Ast.Children-1.Move.md#mrkwatkins-ast-children-1-move(-0)) | Moves a node from it&#39;s current parent (if it has one) and into this collection. |
| [Move](MrKWatkins.Ast.Children-1.Move.md#mrkwatkins-ast-children-1-move(system-collections-generic-ienumerable((-0)))) | Moves nodes from their current parents (if they have one) into this collection. |
| [Move](MrKWatkins.Ast.Children-1.Move.md#mrkwatkins-ast-children-1-move(-0())) | Moves nodes from their current parents (if they have one) into this collection. |

## Move(TNode) {id="mrkwatkins-ast-children-1-move(-0)"}

Moves a node from it&#39;s current parent (if it has one) and into this collection.

```c#
public void Move(TNode node);
```

## Parameters {id="parameters-mrkwatkins-ast-children-1-move(-0)"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| node | TNode | The node to move. |

## Move(IEnumerable&lt;TNode&gt;) {id="mrkwatkins-ast-children-1-move(system-collections-generic-ienumerable((-0)))"}

Moves nodes from their current parents (if they have one) into this collection.

```c#
public void Move(IEnumerable<TNode> nodes);
```

## Parameters {id="parameters-mrkwatkins-ast-children-1-move(system-collections-generic-ienumerable((-0)))"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| nodes | [IEnumerable&lt;TNode&gt;](https://learn.microsoft.com/en-gb/dotnet/api/System.Collections.Generic.IEnumerable-1) | The nodes to move. |

## Move(TNode\[\]) {id="mrkwatkins-ast-children-1-move(-0())"}

Moves nodes from their current parents (if they have one) into this collection.

```c#
public void Move(params TNode[] nodes);
```

## Parameters {id="parameters-mrkwatkins-ast-children-1-move(-0())"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| nodes | TNode\[\] | The nodes to move. |

