# Listeners Example

A simple example to show the use of listeners. The example AST has expressions that are either arrays or constants. An array can hold both constants and other arrays.
Listeners are then used to format an AST to a string. There are listeners for constants and arrays, and shared code in a base class. The Formatter class then builds
a composite listener which can then be used to build up the formatted string. Using the BeforeListenToNode and AfterListenToNode makes it easy to wrap arrays in square
brackets and insert separators when needed.