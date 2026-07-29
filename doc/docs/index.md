# Home

[![Build Status](https://github.com/MrKWatkins/Ast/actions/workflows/build.yml/badge.svg)](https://github.com/MrKWatkins/Ast/actions/workflows/build.yml)
[![NuGet Version](https://img.shields.io/nuget/v/MrKWatkins.Ast)](https://www.nuget.org/packages/MrKWatkins.Ast)
[![NuGet Downloads](https://img.shields.io/nuget/dt/MrKWatkins.Ast)](https://www.nuget.org/packages/MrKWatkins.Ast)

> C# library to build and manipulate abstract syntax trees when writing compilers.

As part of my [Oakley](https://www.mrkwatkins.co.uk/tag/oakley/) project to create a compiler, and its associated OakAsm project to create an assembler, I needed to represent [abstract syntax trees](https://en.wikipedia.org/wiki/Abstract_syntax_tree) in C#. This library was created so that I could share the code between those two projects.

## Installation

```bash
dotnet add package MrKWatkins.Ast
```

## Nodes and Trees

Trees are built from a self-generic base node type of your own. Nodes expose their children as a rich collection type and can be navigated by parent, sibling, ancestor and descendent, with several strategies for walking the whole tree.

[Read more](nodes.md)

## Properties

[`PropertyNode<TNode>`](API/MrKWatkins.Ast/PropertyNode-TNode/index.md) stores node state in a keyed [`Properties`](API/MrKWatkins.Ast/Properties/index.md) collection rather than in fields. That gives copyable nodes, structural equality by value, and single or multiple valued properties, all behind normal C# properties on your node types.

[Read more](properties.md)

## Messages

Errors, warnings and informational messages are attached to the nodes they apply to, then formatted for output — optionally with the offending source line highlighted.

[Read more](messages.md)

## Source Positions

Nodes can record where they came from with a [`SourcePosition`](API/MrKWatkins.Ast.Position/SourcePosition/index.md). Text and binary files are both supported, and positions can be combined to cover the source of an entire subtree.

[Read more](source-positions.md)

## Lexing

[`SourceReader`](API/MrKWatkins.Ast.Lexing/SourceReader/index.md) reads a text file character by character while tracking lines and columns, producing [`Token<TKind>`](API/MrKWatkins.Ast.Lexing/Token-TKind/index.md)s. [`TokenReader<TKind>`](API/MrKWatkins.Ast.Lexing/TokenReader-TKind/index.md) then feeds those tokens to a parser, with lookahead, rewinding and error recovery.

[Read more](lexing.md)

## Listeners

Listeners walk a tree and are notified as nodes are reached, with access to a context object to accumulate results. They are the lightweight option, best suited to building something new from a tree.

[Read more](listeners.md)

## Processing

Processing runs a pipeline of stages over a tree, each stage containing one or more processors running serially or in parallel. It is best suited to mutating a tree, with replacers and validators for the two most common cases.

[Read more](processing.md)

## API Documentation

Reference documentation is generated from the release assembly:

- [`Node<TNode>`](API/MrKWatkins.Ast/Node-TNode/index.md)
- [`PropertyNode<TNode>`](API/MrKWatkins.Ast/PropertyNode-TNode/index.md)
- [`Children<TNode>`](API/MrKWatkins.Ast/Children-TNode/index.md)
- [`Message`](API/MrKWatkins.Ast/Message/index.md)
- [`SourcePosition`](API/MrKWatkins.Ast.Position/SourcePosition/index.md)
- [`SourceReader`](API/MrKWatkins.Ast.Lexing/SourceReader/index.md)
- [`Listener<TContext, TNode>`](API/MrKWatkins.Ast.Listening/Listener-TContext-TNode/index.md)
- [`Pipeline<TBaseNode>`](API/MrKWatkins.Ast.Processing/Pipeline-TBaseNode/index.md)

## Examples

Two worked examples live alongside the source:

- [Listeners](https://github.com/MrKWatkins/Ast/tree/main/examples/Listeners) builds a string representation of a tree using composite listeners.
- [Maths](https://github.com/MrKWatkins/Ast/tree/main/examples/Maths) lexes, parses, reduces, validates, evaluates and compiles mathematical expressions.

## Pull Requests

I'm not accepting pull requests at the current time; this project is tailored for some other projects of mine and I want to get them in a suitable state first.

Feel free to raise issues for bugs or suggestions, but I make no guarantees they will get looked at I'm afraid!

## Use of AI

My general rule is I'll write the interesting bits and use AI for the boring bits. The main use of AI has been to generate the documentation and help with test coverage.

## Licencing

Licensed under MIT.
