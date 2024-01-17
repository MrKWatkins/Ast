# INodeFactory&lt;TNode&gt;.Create Method
## Overloads

| Name | Description |
| ---- | ----------- |
| [Create(Type)](MrKWatkins.Ast.INodeFactory-1.Create.md#mrkwatkins-ast-inodefactory-1-create(system-type)) | Creates a node of the specified type. The node must inherit from `TNode`. |
| [Create()](MrKWatkins.Ast.INodeFactory-1.Create.md#mrkwatkins-ast-inodefactory-1-create-1) | Creates a node of the specified type. |

## Create(Type) {id="mrkwatkins-ast-inodefactory-1-create(system-type)"}

Creates a node of the specified type. The node must inherit from `TNode`.

```c#
public abstract TNode Create(Type nodeType);
```

## Parameters {id="parameters-mrkwatkins-ast-inodefactory-1-create(system-type)"}

| Name | Type | Description |
| ---- | ---- | ----------- |
| nodeType | [Type](https://learn.microsoft.com/en-gb/dotnet/api/System.Type) | The type of node to create. |

## Returns {id="returns-mrkwatkins-ast-inodefactory-1-create(system-type)"}

TNode

The new node.
## Create&lt;T&gt;() {id="mrkwatkins-ast-inodefactory-1-create-1"}

Creates a node of the specified type.

```c#
public virtual T Create<T>()
   where T : TNode;
```

### Type Parameters {id="type-parameters-mrkwatkins-ast-inodefactory-1-create-1"}

| Name | Description |
| ---- | ----------- |
| T | The type of node to create. |

## Returns {id="returns-mrkwatkins-ast-inodefactory-1-create-1"}

T

The new node.
