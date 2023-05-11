#!/bin/bash

echo "Installing DocFX..."
dotnet tool update -g docfx

echo "Building documentation..."
docfx

echo "Post-processing documentation..."
rm _site/logo.svg
sed _site/api/*.html -i -e 's|<a class="xref" href="MrKWatkins.html">MrKWatkins</a>.<a class="xref" href="MrKWatkins.Ast.html">Ast</a>|<a class="xref" href="MrKWatkins.Ast.html">MrKWatkins.Ast</a>|g' 
