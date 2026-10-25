# MrKWatkins.Ast

[![Build Status](https://github.com/MrKWatkins/Ast/actions/workflows/build.yml/badge.svg)](https://github.com/MrKWatkins/Ast/actions/workflows/build.yml)
[![NuGet Version](https://img.shields.io/nuget/v/MrKWatkins.Ast)](https://www.nuget.org/packages/MrKWatkins.Ast)
[![NuGet Downloads](https://img.shields.io/nuget/dt/MrKWatkins.Ast)](https://www.nuget.org/packages/MrKWatkins.Ast)

> C# library to build and manipulate abstract syntax trees when writing compilers.

## Background

As part of my [Oakley](https://www.mrkwatkins.co.uk/tag/oakley/) project to create a compiler and its associated OakAsm project to create an assembler (details coming soon) I needed to represent
[abstract syntax trees](https://en.wikipedia.org/wiki/Abstract_syntax_tree) in C#. This library was created so I could share the code between those two projects.

Full documentation is available at [mrkwatkins.github.io/Ast](https://mrkwatkins.github.io/Ast/). Source code and issue tracking are on [GitHub](https://github.com/MrKWatkins/Ast).

## Usage

Create a base node type for your abstract syntax tree:

```csharp
public abstract class Expression : PropertyNode<Expression>
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

var allNodes = expression.ThisAndDescendants;
var fiftyAndParent = fifty.ThisAndAncestors;
var fiftyAndSixty = fifty.ThisAndNextSiblings;
var justSixty = sixty.PreviousSibling;
var result = expression.Children.OfType<ConstantNumber>().Sum(n => n.Value);
```

Mark nodes with errors, warnings and info messages:

```csharp
sixty.AddError("Value must be less than 55.");
var expressionHasErrors = expression.ThisAndDescendantsHaveErrors; // true.
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
var errors = MessageFormatter.FormatErrors(expression, MessageFormatterOptions.PrefixAndHighlight);

// MySource.code (1, 6): Error: Value must be less than 55.
// 50 + 60
//      --
```

Manipulate and copy the tree:

```csharp
sixty.ReplaceWith(new ConstantNumber(55));

var copy = expression.Copy();
```

## Install

```
dotnet add package MrKWatkins.Ast
```

## Pull Requests

I'm not accepting pull requests at the current time; this project is tailored for some other projects of mine and I want to get them in a suitable state first.

Feel free to raise issues for bugs or suggestions, but I make no guarantees they will get looked at I'm afraid!

## License

MIT

## Building and documentation

The library and examples remain separate solutions: `src/Ast.slnx` and `examples/Examples.slnx`. Tests use NUnit on Microsoft Testing Platform; run `dotnet test`
from the corresponding directory to use its test configuration. **Build and Test** runs the two solutions in parallel jobs.

Handwritten documentation lives in `docs/content`; `docs/site.json` defines navigation and API inputs. Run `python3 docs/preview.py` with Docker and the .NET 10 SDK
installed, or select **Preview documentation** in a JetBrains IDE. Open `http://localhost:5173/Ast/`.

**Build and Test** runs on pushes and pull requests. **Release** requires the examples to pass before building, testing and optionally publishing the library
packages and documentation. Both publication options default to true. Leave the version blank for the next minor version, or enter an explicit stable version. To update documentation without
publishing a package, turn **publish-packages** off and leave **publish-docs** on. This uses the latest released API and version; `api-ref` can override the API
source for XML comment corrections. For the first docs-only publication of 0.9.139, set `api-ref` to `main` to include the prose corrections required by the new documentation checks.

Configure NuGet trusted publishing for owner `MrKWatkins`, repository `Ast`, workflow `release.yml`, environment `github-pages` and package `MrKWatkins.Ast`.
Set GitHub Pages to **GitHub Actions**, and allow `main` to deploy to the `github-pages` environment. No stored NuGet API key is required.
