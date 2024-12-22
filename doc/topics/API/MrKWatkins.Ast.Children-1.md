# Children&lt;TNode&gt; Class
## Definition

Collection of child nodes for a [Node&lt;TNode&gt;](MrKWatkins.Ast.Node-1.md).

```c#
public sealed class Children<TNode> : IList<TNode>, ICollection<TNode>, IEnumerable<TNode>, IEnumerable
   where TNode : Node<TNode>
```

### Type Parameters

| Name | Description |
| ---- | ----------- |
| TNode | The base node type for the collection. |

## Properties

| Name | Description |
| ---- | ----------- |
| [Capacity](MrKWatkins.Ast.Children-1.Capacity.md) | Current capacity of the collection. The capacity is the size of the internal array used to hold items. When set, the internal array of the list is reallocated to the given capacity. |
| [Count](MrKWatkins.Ast.Children-1.Count.md) | The number of nodes in the collection. |
| [First](MrKWatkins.Ast.Children-1.First.md) | Gets the first child in the collection. |
| [FirstOrNull](MrKWatkins.Ast.Children-1.FirstOrNull.md) | Gets the first child in the collection or null if the collection is empty. |
| [Item[Int32]](MrKWatkins.Ast.Children-1.Item.md) | Gets or sets the node at the specified index. Parents will be updated accordingly on set. |
| [Last](MrKWatkins.Ast.Children-1.Last.md) | Gets the last child in the collection. |
| [LastOrNull](MrKWatkins.Ast.Children-1.LastOrNull.md) | Gets the last child in the collection or null if the collection is empty. |
| [UnsafeFirst](MrKWatkins.Ast.Children-1.UnsafeFirst.md) | Gets the first child in the collection without array bounds checks. For high performance scenarios. WARNING: Do not use unless you are certain of the number of children! |
| [UnsafeLast](MrKWatkins.Ast.Children-1.UnsafeLast.md) | Gets the last child in the collection without array bounds checks. For high performance scenarios. WARNING: Do not use unless you are certain of the number of children! |

## Methods

| Name | Description |
| ---- | ----------- |
| [Add(TNode)](MrKWatkins.Ast.Children-1.Add.md#mrkwatkins-ast-children-1-add(-0)) | Adds a node to the collection and assigns its [Parent](MrKWatkins.Ast.Node-1.Parent.md) property. |
| [Add(IEnumerable&lt;TNode&gt;)](MrKWatkins.Ast.Children-1.Add.md#mrkwatkins-ast-children-1-add(system-collections-generic-ienumerable((-0)))) | Adds nodes to the collection and assigns their [Parent](MrKWatkins.Ast.Node-1.Parent.md) properties. |
| [Add(ICollection&lt;TNode&gt;)](MrKWatkins.Ast.Children-1.Add.md#mrkwatkins-ast-children-1-add(system-collections-generic-icollection((-0)))) | Adds nodes to the collection and assigns their [Parent](MrKWatkins.Ast.Node-1.Parent.md) properties. |
| [Add(TNode\[\])](MrKWatkins.Ast.Children-1.Add.md#mrkwatkins-ast-children-1-add(-0())) | Adds nodes to the collection and assigns their [Parent](MrKWatkins.Ast.Node-1.Parent.md) properties. |
| [Clear()](MrKWatkins.Ast.Children-1.Clear.md) | Removes all nodes from the collection and resets their [Parent](MrKWatkins.Ast.Node-1.Parent.md) properties to `null`. |
| [Contains(TNode)](MrKWatkins.Ast.Children-1.Contains.md) | Determines if the specified node is in the collection or not. |
| [EnumerateSlice(Int32, Int32)](MrKWatkins.Ast.Children-1.EnumerateSlice.md#mrkwatkins-ast-children-1-enumerateslice(system-int32-system-int32)) | Returns a lazy enumeration of a range of nodes in the source [Children&lt;TNode&gt;](MrKWatkins.Ast.Children-1.md). |
| [EnumerateSlice(Range)](MrKWatkins.Ast.Children-1.EnumerateSlice.md#mrkwatkins-ast-children-1-enumerateslice(system-range)) | Returns a lazy enumeration of a range of nodes in the source [Children&lt;TNode&gt;](MrKWatkins.Ast.Children-1.md). |
| [ExceptOfType&lt;TChild&gt;()](MrKWatkins.Ast.Children-1.ExceptOfType.md) | Lazily enumerates over all nodes in this collection not of the specified type. |
| [FirstIfTypeOrDefault&lt;TChild&gt;(TChild)](MrKWatkins.Ast.Children-1.FirstIfTypeOrDefault.md) | Returns the first node in the collection if it is of the specified type or a specified default if the collection is empty or the first node is a different type. |
| [FirstOfType&lt;TChild&gt;()](MrKWatkins.Ast.Children-1.FirstOfType.md) | Returns the first node in the collection of the specified type or throws otherwise. |
| [FirstOfTypeOrDefault&lt;TChild&gt;(TChild)](MrKWatkins.Ast.Children-1.FirstOfTypeOrDefault.md) | Returns the first node in the collection of the specified type or a specified default if it doesn&#39;t contain any nodes of the specified type. |
| [GetEnumerator()](MrKWatkins.Ast.Children-1.GetEnumerator.md) |  |
| [IndexOf(TNode)](MrKWatkins.Ast.Children-1.IndexOf.md) | Gets the index of the specified node in this collection. |
| [Insert(Int32, TNode)](MrKWatkins.Ast.Children-1.Insert.md) | Inserts a node into the collection at the specified position and assigns its [Parent](MrKWatkins.Ast.Node-1.Parent.md) property. |
| [LastIfTypeOrDefault&lt;TChild&gt;(TChild)](MrKWatkins.Ast.Children-1.LastIfTypeOrDefault.md) | Returns the last node in the collection if it is of the specified type or a specified default if the collection is empty or the last node is a different type. |
| [LastOfType&lt;TChild&gt;()](MrKWatkins.Ast.Children-1.LastOfType.md) | Returns the last node in the collection of the specified type or throws otherwise. |
| [LastOfTypeOrDefault&lt;TChild&gt;(TChild)](MrKWatkins.Ast.Children-1.LastOfTypeOrDefault.md) | Returns the last node in the collection of the specified type or a specified default if it doesn&#39;t contain any nodes of the specified type. |
| [Move(TNode)](MrKWatkins.Ast.Children-1.Move.md#mrkwatkins-ast-children-1-move(-0)) | Moves a node from it&#39;s current parent (if it has one) and into this collection. |
| [Move(IEnumerable&lt;TNode&gt;)](MrKWatkins.Ast.Children-1.Move.md#mrkwatkins-ast-children-1-move(system-collections-generic-ienumerable((-0)))) | Moves nodes from their current parents (if they have one) into this collection. |
| [Move(TNode\[\])](MrKWatkins.Ast.Children-1.Move.md#mrkwatkins-ast-children-1-move(-0())) | Moves nodes from their current parents (if they have one) into this collection. |
| [OfType&lt;TChild&gt;()](MrKWatkins.Ast.Children-1.OfType.md) | Lazily enumerates over all nodes in this collection of the specified type. |
| [Remove(TNode)](MrKWatkins.Ast.Children-1.Remove.md) | Tries to remove a node from the collection and reset its [Parent](MrKWatkins.Ast.Node-1.Parent.md) property to `null`. |
| [RemoveAt(Int32)](MrKWatkins.Ast.Children-1.RemoveAt.md) | Removes the node at the specified position from the collection and reset its [Parent](MrKWatkins.Ast.Node-1.Parent.md) property to `null`. |
| [Replace(TNode, TNode)](MrKWatkins.Ast.Children-1.Replace.md) | Replaces a node in the collection with another node. The replacement will be removed from its parent and the node being replaced will have its parent removed. |
| [SingleOfType&lt;TChild&gt;()](MrKWatkins.Ast.Children-1.SingleOfType.md) | Returns the only node in the collection of the specified type. Throws if there is not exactly one node in the collection of the specified type. |
| [SingleOfTypeOrDefault&lt;TChild&gt;(TChild)](MrKWatkins.Ast.Children-1.SingleOfTypeOrDefault.md) | Returns the only node in the collection of the specified type. Returns the specified default if there are no nodes in the collection of the specified type. Throws if there are multiple nodes in the collection of the specified type. |
| [Slice(Int32, Int32)](MrKWatkins.Ast.Children-1.Slice.md) | Creates a shallow copy of a range of nodes in the source [Children&lt;TNode&gt;](MrKWatkins.Ast.Children-1.md). |
| [UnsafeGet(Int32)](MrKWatkins.Ast.Children-1.UnsafeGet.md) | Gets the child at the specified index in the collection without array bounds checks. For high performance scenarios. WARNING: Do not use unless you are certain of the number of children! |
| [UnsafeSlice(Int32, Int32)](MrKWatkins.Ast.Children-1.UnsafeSlice.md#mrkwatkins-ast-children-1-unsafeslice(system-int32-system-int32)) | Returns a [ReadOnlySpan&lt;T&gt;](https://learn.microsoft.com/en-gb/dotnet/api/System.ReadOnlySpan-1) of a range of nodes in the source [Children&lt;TNode&gt;](MrKWatkins.Ast.Children-1.md). Nodes should not be added or removed from the [Children&lt;TNode&gt;](MrKWatkins.Ast.Children-1.md) while the [ReadOnlySpan&lt;T&gt;](https://learn.microsoft.com/en-gb/dotnet/api/System.ReadOnlySpan-1) is in use. |
| [UnsafeSlice(Range)](MrKWatkins.Ast.Children-1.UnsafeSlice.md#mrkwatkins-ast-children-1-unsafeslice(system-range)) | Returns a [ReadOnlySpan&lt;T&gt;](https://learn.microsoft.com/en-gb/dotnet/api/System.ReadOnlySpan-1) of a range of nodes in the source [Children&lt;TNode&gt;](MrKWatkins.Ast.Children-1.md). Nodes should not be added or removed from the [Children&lt;TNode&gt;](MrKWatkins.Ast.Children-1.md) while the [ReadOnlySpan&lt;T&gt;](https://learn.microsoft.com/en-gb/dotnet/api/System.ReadOnlySpan-1) is in use. |

