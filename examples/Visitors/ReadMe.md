# Visitors Example

A simple example to show the use of visitors. The example AST has boolean expressions: constants, variables, and, or and not. Visitors are then
used to evaluate an expression to a bool. There is a visitor for each node type, each returning the value of its node, and the Evaluator class
builds a composite visitor from them. A visitor visits its own children, so the visitors for and and or can short-circuit and skip their right
operand, something a listener cannot do as it always walks the whole tree.