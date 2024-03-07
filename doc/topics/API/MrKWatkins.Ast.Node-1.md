# Node&lt;TNode&gt; Class
## Definition

Abstract base class for nodes in a tree.

```c#
public abstract class Node<TNode>
   where TNode : Node<TNode>
```

### Type Parameters

| Name | Description |
| ---- | ----------- |
| TNode | Self generic node parameter. |

## Constructors

| Name | Description |
| ---- | ----------- |
| [Node()](MrKWatkins.Ast.Node-1.-ctor.md#mrkwatkins-ast-node-1-ctor) | Initialises a new instance of the [Node&lt;TNode&gt;](MrKWatkins.Ast.Node-1.md) class. |
| [Node(IEnumerable&lt;TNode&gt;)](MrKWatkins.Ast.Node-1.-ctor.md#mrkwatkins-ast-node-1-ctor(system-collections-generic-ienumerable((-0)))) | Initialises a new instance of the [Node&lt;TNode&gt;](MrKWatkins.Ast.Node-1.md) class with the specified children. |

## Properties

| Name | Description |
| ---- | ----------- |
| [Ancestors](MrKWatkins.Ast.Node-1.Ancestors.md) | Lazily enumerates over the ancestors of this node, i.e. the [Parent](MrKWatkins.Ast.Node-1.Parent.md), grandparent, great-grandparent and so on up to the root node. |
| [Children](MrKWatkins.Ast.Node-1.Children.md) | The children of this node. |
| [Descendents](MrKWatkins.Ast.Node-1.Descendents.md) | Enumerates all descendents of this node in depth first pre-order. |
| [Errors](MrKWatkins.Ast.Node-1.Errors.md) | The [Messages](MrKWatkins.Ast.Message.md) with [Level](MrKWatkins.Ast.Message.Level.md) [Error](MrKWatkins.Ast.MessageLevel.md#fields) associated with this node. |
| [HasChildren](MrKWatkins.Ast.Node-1.HasChildren.md) | Returns `true` if this node has any [Children](MrKWatkins.Ast.Node-1.Children.md), `false` otherwise. |
| [HasErrors](MrKWatkins.Ast.Node-1.HasErrors.md) | Returns `true` if this node has any [Messages](MrKWatkins.Ast.Message.md) with [Level](MrKWatkins.Ast.Message.Level.md) [Error](MrKWatkins.Ast.MessageLevel.md#fields), `false` otherwise. |
| [HasInfos](MrKWatkins.Ast.Node-1.HasInfos.md) | Returns `true` if this node has any [Messages](MrKWatkins.Ast.Message.md) with [Level](MrKWatkins.Ast.Message.Level.md) [Info](MrKWatkins.Ast.MessageLevel.md#fields), `false` otherwise. |
| [HasMessages](MrKWatkins.Ast.Node-1.HasMessages.md) | Returns `true` if this node has any [Messages](MrKWatkins.Ast.Message.md), `false` otherwise. |
| [HasNextSibling](MrKWatkins.Ast.Node-1.HasNextSibling.md) | Returns `true` if this node has a [NextSibling](MrKWatkins.Ast.Node-1.NextSibling.md), `false` otherwise. |
| [HasParent](MrKWatkins.Ast.Node-1.HasParent.md) | Returns `true` if this node has a [Parent](MrKWatkins.Ast.Node-1.Parent.md), `false` otherwise. Nodes will not have parents if they are the root node, or they have just been constructed and not yet added to a parent. |
| [HasPreviousSibling](MrKWatkins.Ast.Node-1.HasPreviousSibling.md) | Returns `true` if this node has a [PreviousSibling](MrKWatkins.Ast.Node-1.PreviousSibling.md), `false` otherwise. |
| [HasWarnings](MrKWatkins.Ast.Node-1.HasWarnings.md) | Returns `true` if this node has any [Messages](MrKWatkins.Ast.Message.md) with [Level](MrKWatkins.Ast.Message.Level.md) [Warning](MrKWatkins.Ast.MessageLevel.md#fields), `false` otherwise. |
| [IndexInParent](MrKWatkins.Ast.Node-1.IndexInParent.md) | The index of this node in the [Parent](MrKWatkins.Ast.Node-1.Parent.md) or -1 if this node has no [Parent](MrKWatkins.Ast.Node-1.Parent.md). |
| [Infos](MrKWatkins.Ast.Node-1.Infos.md) | The [Messages](MrKWatkins.Ast.Message.md) with [Level](MrKWatkins.Ast.Message.Level.md) [Info](MrKWatkins.Ast.MessageLevel.md#fields) associated with this node. |
| [IsFirstChild](MrKWatkins.Ast.Node-1.IsFirstChild.md) | `true` if this node is the first child in [Parent](MrKWatkins.Ast.Node-1.Parent.md), `false` if not or if the node has no [Parent](MrKWatkins.Ast.Node-1.Parent.md). |
| [IsLastChild](MrKWatkins.Ast.Node-1.IsLastChild.md) | `true` if this node is the last child in [Parent](MrKWatkins.Ast.Node-1.Parent.md), `false` if not or if the node has no [Parent](MrKWatkins.Ast.Node-1.Parent.md). |
| [Messages](MrKWatkins.Ast.Node-1.Messages.md) | The [Messages](MrKWatkins.Ast.Message.md) associated with this node. |
| [NextSibling](MrKWatkins.Ast.Node-1.NextSibling.md) | The next sibling, i.e. the child from the same [Parent](MrKWatkins.Ast.Node-1.Parent.md) at the next positional index. Returns `null` if this node is the last child. |
| [NextSiblings](MrKWatkins.Ast.Node-1.NextSiblings.md) | Lazily enumerates over the next siblings, i.e. the children from the same [Parent](MrKWatkins.Ast.Node-1.Parent.md) at subsequent positional indices in ascending index order. |
| [Parent](MrKWatkins.Ast.Node-1.Parent.md) | The parent of this node. |
| [PreviousSibling](MrKWatkins.Ast.Node-1.PreviousSibling.md) | The previous sibling, i.e. the child from the same [Parent](MrKWatkins.Ast.Node-1.Parent.md) at the previous positional index. Returns `null` if this node is the first child. |
| [PreviousSiblings](MrKWatkins.Ast.Node-1.PreviousSiblings.md) | Lazily enumerates over the previous siblings, i.e. the children from the same [Parent](MrKWatkins.Ast.Node-1.Parent.md) at precedent positional indices in descending index order. |
| [Root](MrKWatkins.Ast.Node-1.Root.md) | The root node, i.e. the highest parent above this node. Returns this node if it is the root, i.e. it has no parents. |
| [SourcePosition](MrKWatkins.Ast.Node-1.SourcePosition.md) | The position of the node in the source code. |
| [ThisAndAncestors](MrKWatkins.Ast.Node-1.ThisAndAncestors.md) | Lazily enumerates over this node and its [Ancestors](MrKWatkins.Ast.Node-1.Ancestors.md), i.e. this node, the [Parent](MrKWatkins.Ast.Node-1.Parent.md), grandparent, great-grandparent and so on up to the root node. |
| [ThisAndDescendents](MrKWatkins.Ast.Node-1.ThisAndDescendents.md) | Enumerates this node then all descendents of this node in depth first pre-order. |
| [ThisAndDescendentsHaveErrors](MrKWatkins.Ast.Node-1.ThisAndDescendentsHaveErrors.md) | Returns `true` if this node or any of its descendents have any [Messages](MrKWatkins.Ast.Message.md) with [Level](MrKWatkins.Ast.Message.Level.md) [Error](MrKWatkins.Ast.MessageLevel.md#fields), `false` otherwise. |
| [ThisAndDescendentsHaveInfos](MrKWatkins.Ast.Node-1.ThisAndDescendentsHaveInfos.md) | Returns `true` if this node or any of its descendents have any [Messages](MrKWatkins.Ast.Message.md) with [Level](MrKWatkins.Ast.Message.Level.md) [Info](MrKWatkins.Ast.MessageLevel.md#fields), `false` otherwise. |
| [ThisAndDescendentsHaveMessages](MrKWatkins.Ast.Node-1.ThisAndDescendentsHaveMessages.md) | Returns `true` if this node or any of its descendents have any [Messages](MrKWatkins.Ast.Message.md), `false` otherwise. |
| [ThisAndDescendentsHaveWarnings](MrKWatkins.Ast.Node-1.ThisAndDescendentsHaveWarnings.md) | Returns `true` if this node or any of its descendents have any [Messages](MrKWatkins.Ast.Message.md) with [Level](MrKWatkins.Ast.Message.Level.md) [Warning](MrKWatkins.Ast.MessageLevel.md#fields), `false` otherwise. |
| [ThisAndDescendentsWithErrors](MrKWatkins.Ast.Node-1.ThisAndDescendentsWithErrors.md) | Lazily enumerates over this node and its descendents returning only those that have [Messages](MrKWatkins.Ast.Message.md) with [Level](MrKWatkins.Ast.Message.Level.md) [Error](MrKWatkins.Ast.MessageLevel.md#fields). |
| [ThisAndDescendentsWithInfos](MrKWatkins.Ast.Node-1.ThisAndDescendentsWithInfos.md) | Lazily enumerates over this node and its descendents returning only those that have [Messages](MrKWatkins.Ast.Message.md) with [Level](MrKWatkins.Ast.Message.Level.md) [Info](MrKWatkins.Ast.MessageLevel.md#fields). |
| [ThisAndDescendentsWithMessages](MrKWatkins.Ast.Node-1.ThisAndDescendentsWithMessages.md) | Lazily enumerates over this node and its descendents returning only those that have [Messages](MrKWatkins.Ast.Message.md). |
| [ThisAndDescendentsWithWarnings](MrKWatkins.Ast.Node-1.ThisAndDescendentsWithWarnings.md) | Lazily enumerates over this node and its descendents returning only those that have [Messages](MrKWatkins.Ast.Message.md) with [Level](MrKWatkins.Ast.Message.Level.md) [Warning](MrKWatkins.Ast.MessageLevel.md#fields). |
| [ThisAndNextSiblings](MrKWatkins.Ast.Node-1.ThisAndNextSiblings.md) | Lazily enumerates over this node then the next siblings, i.e. the children from the same [Parent](MrKWatkins.Ast.Node-1.Parent.md) at subsequent positional indices in ascending index order. |
| [ThisAndPreviousSiblings](MrKWatkins.Ast.Node-1.ThisAndPreviousSiblings.md) | Lazily enumerates over this node then the previous siblings, i.e. the children from the same [Parent](MrKWatkins.Ast.Node-1.Parent.md) at precedent positional indices in descending index order. |
| [Warnings](MrKWatkins.Ast.Node-1.Warnings.md) | The [Messages](MrKWatkins.Ast.Message.md) with [Level](MrKWatkins.Ast.Message.Level.md) [Warning](MrKWatkins.Ast.MessageLevel.md#fields) associated with this node. |

## Methods

| Name | Description |
| ---- | ----------- |
| [AddError(String)](MrKWatkins.Ast.Node-1.AddError.md#mrkwatkins-ast-node-1-adderror(system-string)) | Adds a [Message](MrKWatkins.Ast.Message.md) with [Level](MrKWatkins.Ast.Message.Level.md) [Error](MrKWatkins.Ast.MessageLevel.md#fields) and the specified text to this node. |
| [AddError(String, String)](MrKWatkins.Ast.Node-1.AddError.md#mrkwatkins-ast-node-1-adderror(system-string-system-string)) | Adds a [Message](MrKWatkins.Ast.Message.md) with [Level](MrKWatkins.Ast.Message.Level.md) [Error](MrKWatkins.Ast.MessageLevel.md#fields) and the specified text to this node. |
| [AddInfo(String)](MrKWatkins.Ast.Node-1.AddInfo.md#mrkwatkins-ast-node-1-addinfo(system-string)) | Adds a [Message](MrKWatkins.Ast.Message.md) with [Level](MrKWatkins.Ast.Message.Level.md) [Info](MrKWatkins.Ast.MessageLevel.md#fields) and the specified text to this node. |
| [AddInfo(String, String)](MrKWatkins.Ast.Node-1.AddInfo.md#mrkwatkins-ast-node-1-addinfo(system-string-system-string)) | Adds a [Message](MrKWatkins.Ast.Message.md) with [Level](MrKWatkins.Ast.Message.Level.md) [Info](MrKWatkins.Ast.MessageLevel.md#fields) and the specified text to this node. |
| [AddMessage(Message)](MrKWatkins.Ast.Node-1.AddMessage.md#mrkwatkins-ast-node-1-addmessage(mrkwatkins-ast-message)) | Adds a [Message](MrKWatkins.Ast.Message.md) to this node. |
| [AddMessage(MessageLevel, String)](MrKWatkins.Ast.Node-1.AddMessage.md#mrkwatkins-ast-node-1-addmessage(mrkwatkins-ast-messagelevel-system-string)) | Adds a [Message](MrKWatkins.Ast.Message.md) with the specified [Level](MrKWatkins.Ast.Message.Level.md) and [Text](MrKWatkins.Ast.Message.Text.md) to this node. |
| [AddMessage(MessageLevel, String, String)](MrKWatkins.Ast.Node-1.AddMessage.md#mrkwatkins-ast-node-1-addmessage(mrkwatkins-ast-messagelevel-system-string-system-string)) | Adds a [Message](MrKWatkins.Ast.Message.md) with the specified [Level](MrKWatkins.Ast.Message.Level.md), [Code](MrKWatkins.Ast.Message.Code.md) and [Text](MrKWatkins.Ast.Message.Text.md) to this node. |
| [AddNextSibling(TNode)](MrKWatkins.Ast.Node-1.AddNextSibling.md) | Adds the specified node as the [NextSibling](MrKWatkins.Ast.Node-1.NextSibling.md) to this node. Existing next siblings will be moved on index to the right to accommodate. |
| [AddPreviousSibling(TNode)](MrKWatkins.Ast.Node-1.AddPreviousSibling.md) | Adds the specified node as the [PreviousSibling](MrKWatkins.Ast.Node-1.PreviousSibling.md) to this node. This and any next siblings will be moved one index to the right to accommodate. |
| [AddWarning(String)](MrKWatkins.Ast.Node-1.AddWarning.md#mrkwatkins-ast-node-1-addwarning(system-string)) | Adds a [Message](MrKWatkins.Ast.Message.md) with [Level](MrKWatkins.Ast.Message.Level.md) [Warning](MrKWatkins.Ast.MessageLevel.md#fields) and the specified text to this node. |
| [AddWarning(String, String)](MrKWatkins.Ast.Node-1.AddWarning.md#mrkwatkins-ast-node-1-addwarning(system-string-system-string)) | Adds a [Message](MrKWatkins.Ast.Message.md) with [Level](MrKWatkins.Ast.Message.Level.md) [Warning](MrKWatkins.Ast.MessageLevel.md#fields) and the specified text to this node. |
| [AncestorsOfType&lt;TAncestor&gt;()](MrKWatkins.Ast.Node-1.AncestorsOfType.md) | Lazily enumerates over this node and its [Ancestors](MrKWatkins.Ast.Node-1.Ancestors.md), returning only ancestors of the specified type. |
| [MoveTo(TNode)](MrKWatkins.Ast.Node-1.MoveTo.md) | Moves this node to a new parent. |
| [RemoveFromParent()](MrKWatkins.Ast.Node-1.RemoveFromParent.md) | Removes this node from its [Parent](MrKWatkins.Ast.Node-1.Parent.md). |
| [RemoveNextSibling()](MrKWatkins.Ast.Node-1.RemoveNextSibling.md) | Removes the [NextSibling](MrKWatkins.Ast.Node-1.NextSibling.md) from [Parent](MrKWatkins.Ast.Node-1.Parent.md) if it exists. |
| [RemovePreviousSibling()](MrKWatkins.Ast.Node-1.RemovePreviousSibling.md) | Removes the [PreviousSibling](MrKWatkins.Ast.Node-1.PreviousSibling.md) from [Parent](MrKWatkins.Ast.Node-1.Parent.md) if it exists. |
| [ReplaceWith(Node&lt;TNode&gt;)](MrKWatkins.Ast.Node-1.ReplaceWith.md) | Removes this node from it&#39;s parent and puts another node in its place. |
| [ThisAnd(IEnumerable&lt;TNode&gt;)](MrKWatkins.Ast.Node-1.ThisAnd.md) | Lazily enumerates over this node and then the specified enumeration of nodes. |
| [ThisAndAncestorsOfType&lt;TAncestor&gt;()](MrKWatkins.Ast.Node-1.ThisAndAncestorsOfType.md) | Lazily enumerates over the [Ancestors](MrKWatkins.Ast.Node-1.Ancestors.md) of this node, returning only ancestors of the specified type. |
| [ToString()](MrKWatkins.Ast.Node-1.ToString.md) | Returns a string that represents the current node. Defaults to the name of the type of the node. |

