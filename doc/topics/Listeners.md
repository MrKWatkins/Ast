# Listeners

Listeners are an alternative lightweight method to [processing](Processing.md) for visiting nodes in a tree and performing some functionality. Listeners walk
all the nodes and a tree and have events that can be hooked into when nodes are reached. They have access to a context object that can be used to
store the result of the walk along with any configuration information. Processing is best for mutating a tree, listeners are more suitable for
building something new from a tree.

## Creating a Listener

There are two base listener classes that can be inherited from to create a listener:

* [Listener\<TContext, TNode\>](MrKWatkins.Ast.Listening.Listener-2.md) - Listens to all nodes in a tree with access to a context object.
* [Listener\<TContext, TBaseNode, TNode\>](MrKWatkins.Ast.Listening.Listener-3.md) - Listens to all nodes of a specific type in a tree with access to a context object.

The listeners have three methods that can be overridden to get access to the nodes:

* [BeforeListenToNode](MrKWatkins.Ast.Listening.Listener-2.BeforeListenToNode.md) - Called immediately before a node and its children are visited.
* [ListenToNode](MrKWatkins.Ast.Listening.Listener-2.ListenToNode.md) - Called when node is visited.
* [AfterListenToNode](MrKWatkins.Ast.Listening.Listener-2.AfterListenToNode.md) - Called immediately after a node and its children have been visited.

To start the listening process call the [Listen](MrKWatkins.Ast.Listening.Listener-2.Listen.md) method.

## Composite Listeners

Often you will want to visit a tree performing different actions for specific types of node in the tree. Rather than have all the code in one class with
switch statements on the node type you can instead build a composite listener from multiple other listeners. A composite listener can be built using a fluent
interface from the [Build](MrKWatkins.Ast.Listening.CompositeListener-2.Build.md) method.

Only one listener for a given type can be registered. However, listeners can be registered for base types too, and the listener with the most specific type
will be chosen to listen to a node. This is useful for fallback behaviour. Listeners can also have base classes to share implementation between different node
types.

## Example

Find an example of using listeners to produce a string representation of a tree at <https://github.com/MrKWatkins/Ast/tree/main/examples/Listeners>. The
maths example at <https://github.com/MrKWatkins/Ast/tree/main/examples/Maths> also uses listeners to evaluate and compile mathematical expressions.