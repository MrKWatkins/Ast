using MrKWatkins.Ast.Visiting;

namespace MrKWatkins.Ast.Examples.Visitors;

/// <summary>
/// Visitor for variables. Looks up the value of the variable in the context.
/// </summary>
internal sealed class VariableVisitor : NodeVisitor<EvaluationContext, Expression, Variable, bool>
{
    protected override bool VisitNode(EvaluationContext context, Variable variable) => context.GetVariable(variable.Name);
}