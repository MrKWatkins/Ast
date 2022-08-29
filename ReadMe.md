# MrKWatkins.Ast

[![Build Status](https://github.com/MrKWatkins/Ast/actions/workflows/build.yml/badge.svg)](https://github.com/MrKWatkins/Ast/actions/workflows/build.yml)
[![NuGet Version and Download Count](https://buildstats.info/nuget/MrKWatkins.Ast)](https://www.nuget.org/packages/MrKWatkins.Ast)

> C# library to build and manipulate abstract syntax trees when writing compilers.

## Background

As part of my [Oakley](https://www.mrkwatkins.co.uk/tag/oakley/) project to create a compiler and
it's associated OakAsm project to create an assembler (details coming soon) I needed to represent
[abstract syntax trees](https://en.wikipedia.org/wiki/Abstract_syntax_tree) in C#. This library
was created so I could share the code between those two projects.

## Usage

Create a base node type for your abstract syntax tree:

```csharp
public abstract class Expression : Node<Expression>
{
}
```

Create more specific nodes:

```csharp
public sealed class ConstantNumber : Expression
{
    public ConstantNumber(int value)
    {
        Value = value;
    }

    public int Value
    {
        get => Properties.GetOrThrow<int>(nameof(Value));
        init => Properties.Set(nameof(Value), value);
    }
}

public sealed class Addition : Expression
{
    public Addition(ConstantNumber x, ConstantNumber y)
    {
        Children.Add(x);
        Children.Add(y);
    }
}
```

Walk the tree:

```csharp
var fifty = new ConstantNumber(50);
var sixty = new ConstantNumber(60);
var expression = new Addition(fifty, sixty);

var allNodes = expression.ThisAndDescendents;
var fiftyAndParent = fifty.ThisAndAncestors;
var fiftyAndSixty = fifty.ThisAndNextSiblings;
var justSixty = sixty.PreviousSibling;
var result = expression.Children.OfType<ConstantNumber>().Sum(n => n.Value);
```

Mark nodes with errors, warnings and info messages:

```csharp
sixty.AddError("Value must be less than 55.");
var expressionHasErrors = expression.HasErrors; // true.
```

Associate nodes with their position in source code during parsing:

```csharp
var source = new TextFile(new FileInfo("MySource.code"));   // Contains "50 + 60".
expression.SourcePosition = source.CreatePosition(0, 7, 0, 0); // startIndex, length, startLineIndex, startColumnIndex.
fifty.SourcePosition = source.CreatePosition(0, 2, 0, 0);
sixty.SourcePosition = source.CreatePosition(5, 2, 0, 5);
```

Output errors with highlighted source information:

```csharp
var errors = MessageFormatter.FormatErrors(expression);

// MySource.code (1, 6): Error: Parent Value must be less than 55.
// 50 + 60
//      --
```

Manipulate and copy the tree:
```csharp
sixty.ReplaceWith(new ConstantNumber(55));

var copy = expression.Copy();
```

Full documentation will be available with version 1.0.x.

## Install

```
dotnet add package MrKWatkins.Ast
```

## License

MIT
