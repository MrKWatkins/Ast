# Maths Example

A simple example showing how to parse and evaluate maths expressions. Expressions can have constant values or variables. +, -, * and / operations
are supported, along with parentheses for things like (1 + 2) * 3. Expressions are converted into a Function AST node that has the expression and
a set of input parameters, determined by the variables used in the expression. The example contains several namespaces:

* Tree. Contains the nodes that define the AST.
* Lexing. Hand rolled lexer that splits an expression into tokens.
* Parser. Hand rolled parser that consumes a set of tokens and produces an AST.
* Processing. Processors that the parser runs after parsing. Constant expressions are reduced into smaller ones, e.g. 3 + 4 would just become 7. Validation is performed for dividing by a constant zero.
* Evaluation. Evaluates a Function on the fly for the specified set of arguments.
* Compilation. Compiles a Function into a .NET delegate using LINQ expression trees.