# Nodes and Trees

Trees are built from [`Node<TNode>`](api/MrKWatkins.Ast/Node-TNode/index.md), an abstract base class with a self generic parameter. The generic parameter is the base type of the nodes in *your* tree, which means navigation, children and processing are all strongly typed against your own node hierarchy rather than against `Node`.

## Creating Node Types

Start by creating a base node type for your tree:

```c#
public abstract class Expression : Node<Expression>
{
}
```

Then create more specific node types that inherit from it:

```c#
public sealed class ConstantNumber : Expression
{
    public ConstantNumber(int value)
    {
        Value = value;
    }

    public int Value { get; }
}

public sealed class Addition : Expression
{
    public Addition(Expression left, Expression right)
    {
        Children.Add(left);
        Children.Add(right);
    }
}
```

Node state can be held in ordinary C# properties as above, or in a [`Properties`](api/MrKWatkins.Ast/Properties/index.md) collection by inheriting from [`PropertyNode<TNode>`](api/MrKWatkins.Ast/PropertyNode-TNode/index.md) instead. See [Properties](properties.md) for the trade-offs.

## Children

The [`Children`](api/MrKWatkins.Ast/Node-TNode/Children.md) property is a [`Children<TNode>`](api/MrKWatkins.Ast/Children-TNode/index.md) collection, an `IList<TNode>` that also maintains the [`Parent`](api/MrKWatkins.Ast/Node-TNode/Parent.md) of every node it contains. A node can only ever have one parent, so [`Add`](api/MrKWatkins.Ast/Children-TNode/Add.md) will throw if the node already belongs to another parent; use [`Move`](api/MrKWatkins.Ast/Children-TNode/Move.md) to reparent an existing node instead.

```c#
var expression = new Addition(fifty, sixty);

expression.Children.Add(seventy);            // Throws if seventy already has a parent.
expression.Children.Move(seventy);           // Removes seventy from its old parent first.
expression.Children.Replace(sixty, fifty5);  // Swaps a child for a new node.
expression.Children.Remove(fifty);
```

As well as the usual list operations there are typed accessors for pulling specific kinds of node out of the collection:

| Member | Description |
| ------ | ----------- |
| [`OfType<TChild>`](api/MrKWatkins.Ast/Children-TNode/OfType.md) | Lazily enumerates the children of the specified type. |
| [`ExceptOfType<TChild>`](api/MrKWatkins.Ast/Children-TNode/ExceptOfType.md) | Lazily enumerates the children that are *not* of the specified type. |
| [`FirstOfType<TChild>`](api/MrKWatkins.Ast/Children-TNode/FirstOfType.md) | The first child of the specified type, throwing if there isn't one. |
| [`FirstIfTypeOrDefault<TChild>`](api/MrKWatkins.Ast/Children-TNode/FirstIfTypeOrDefault.md) | The first child if it is of the specified type, a default otherwise. |
| [`SingleOfType<TChild>`](api/MrKWatkins.Ast/Children-TNode/SingleOfType.md) | The only child of the specified type, throwing if there isn't exactly one. |

`Last` and `OrDefault` variants exist for each of the above. [`First`](api/MrKWatkins.Ast/Children-TNode/First.md), [`Last`](api/MrKWatkins.Ast/Children-TNode/Last.md), [`FirstOrNull`](api/MrKWatkins.Ast/Children-TNode/FirstOrNull.md) and [`LastOrNull`](api/MrKWatkins.Ast/Children-TNode/LastOrNull.md) give direct access to the ends of the collection.

The `Unsafe` members — [`UnsafeFirst`](api/MrKWatkins.Ast/Children-TNode/UnsafeFirst.md), [`UnsafeLast`](api/MrKWatkins.Ast/Children-TNode/UnsafeLast.md), [`UnsafeGet`](api/MrKWatkins.Ast/Children-TNode/UnsafeGet.md) and [`UnsafeSlice`](api/MrKWatkins.Ast/Children-TNode/UnsafeSlice.md) — skip bounds checking for use in hot paths where the caller has already established the collection is big enough. They will read past the end of the collection rather than throw if it is not.

## Navigating the Tree

Nodes know where they sit in the tree. Every relationship comes in a lazily enumerated form, and most have a `ThisAnd` variant that includes the node itself:

| Member | Description |
| ------ | ----------- |
| [`Parent`](api/MrKWatkins.Ast/Node-TNode/Parent.md) | The parent node; throws if the node is a root. Check [`HasParent`](api/MrKWatkins.Ast/Node-TNode/HasParent.md) first. |
| [`Root`](api/MrKWatkins.Ast/Node-TNode/Root.md) | The highest parent above this node, or this node if it is the root. |
| [`Ancestors`](api/MrKWatkins.Ast/Node-TNode/Ancestors.md) | The parent and all remaining ancestors up to the root. |
| [`Descendants`](api/MrKWatkins.Ast/Node-TNode/Descendants.md) | All descendants, depth first pre-order. |
| [`NextSibling`](api/MrKWatkins.Ast/Node-TNode/NextSibling.md) / [`PreviousSibling`](api/MrKWatkins.Ast/Node-TNode/PreviousSibling.md) | The adjacent children of the same parent, or `null` at the ends. |
| [`NextSiblings`](api/MrKWatkins.Ast/Node-TNode/NextSiblings.md) / [`PreviousSiblings`](api/MrKWatkins.Ast/Node-TNode/PreviousSiblings.md) | All siblings in each direction. |
| [`IndexInParent`](api/MrKWatkins.Ast/Node-TNode/IndexInParent.md) | The index of this node in its parent, or `-1` if it has no parent. |
| [`IsFirstChild`](api/MrKWatkins.Ast/Node-TNode/IsFirstChild.md) / [`IsLastChild`](api/MrKWatkins.Ast/Node-TNode/IsLastChild.md) | Whether this node sits at either end of its parent. |

[`AncestorsOfType<TAncestor>`](api/MrKWatkins.Ast/Node-TNode/AncestorsOfType.md) filters ancestors to a specific node type, which is useful for finding the enclosing scope, function or block for a node.

```c#
var fifty = new ConstantNumber(50);
var sixty = new ConstantNumber(60);
var expression = new Addition(fifty, sixty);

var allNodes = expression.ThisAndDescendants;
var fiftyAndParent = fifty.ThisAndAncestors;
var fiftyAndSixty = fifty.ThisAndNextSiblings;
var justSixty = sixty.PreviousSibling;
var result = expression.Children.OfType<ConstantNumber>().Sum(n => n.Value);
```

## Restructuring the Tree

Nodes can be moved around from either end of the relationship:

```c#
sixty.ReplaceWith(new ConstantNumber(55));  // Swap this node for another.
sixty.MoveTo(otherExpression);              // Reparent this node.
sixty.RemoveFromParent();
sixty.AddNextSibling(seventy);              // Insert after this node in the parent.
sixty.AddPreviousSibling(forty);            // Insert before this node in the parent.
```

[`AddNextSibling`](api/MrKWatkins.Ast/Node-TNode/AddNextSibling.md) and [`AddPreviousSibling`](api/MrKWatkins.Ast/Node-TNode/AddPreviousSibling.md) throw if the node is the root, as a root node has no parent to insert into.

For bulk restructuring driven by node type, [replacers](processing.md#replacers) handle the traversal and the swapping for you.

## Traversal

[`Descendants`](api/MrKWatkins.Ast/Node-TNode/Descendants.md) walks the tree depth first, pre-order. Other orders are available from the static `Traverse` class on your node type:

```c#
var breadthFirst = Expression.Traverse.BreadthFirst(root);
var postOrder = Expression.Traverse.DepthFirstPostOrder(root);

// Skip the descendants of any node we don't care about.
var pruned = Expression.Traverse.DepthFirstPreOrder(root, shouldEnumerateDescendants: n => n is not Function);
```

Each method takes an `includeRoot` flag and an optional `shouldEnumerateDescendants` predicate to prune whole subtrees from the walk. All enumerations are lazy.

The same strategies are available as [`ITraversal<TNode>`](api/MrKWatkins.Ast.Traversal/ITraversal-TNode/index.md) singletons — [`DepthFirstPreOrderTraversal<TNode>`](api/MrKWatkins.Ast.Traversal/DepthFirstPreOrderTraversal-TNode/index.md), [`DepthFirstPostOrderTraversal<TNode>`](api/MrKWatkins.Ast.Traversal/DepthFirstPostOrderTraversal-TNode/index.md) and [`BreadthFirstTraversal<TNode>`](api/MrKWatkins.Ast.Traversal/BreadthFirstTraversal-TNode/index.md) — for passing to [processing](processing.md) pipelines, or for your own code that needs to be agnostic about the order it walks in.

## Creating Nodes by Type

[`INodeFactory<TNode>`](api/MrKWatkins.Ast/INodeFactory-TNode/index.md) creates nodes from a `Type` for code that works generically over node types, such as copying. [`DefaultNodeFactory<TNode>`](api/MrKWatkins.Ast/DefaultNodeFactory-TNode/index.md) is the built-in implementation; it calls a parameterless constructor, which may be non-public, and caches a compiled delegate for each type. Implement your own if your nodes need something other than a parameterless constructor to build.
