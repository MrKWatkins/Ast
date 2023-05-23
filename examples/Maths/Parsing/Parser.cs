using MrKWatkins.Ast.Examples.Maths.Lexing;
using MrKWatkins.Ast.Examples.Maths.Processing;
using MrKWatkins.Ast.Examples.Maths.Tree;

namespace MrKWatkins.Ast.Examples.Maths.Parsing;

/// <summary>
/// Parses expressions to a <see cref="MathsNode" />.
/// </summary>
/// <remarks>Based on https://matklad.github.io/2020/04/13/simple-but-powerful-pratt-parsing.html.</remarks>
public static class Parser
{
    /// <summary>
    /// Parses the expression contained in the specified string.
    /// </summary>
    /// <param name="expression">The expression.</param>
    /// <returns>A <see cref="Function" /> representing the expression as an AST.</returns>
    [Pure]
    public static Function Parse(string expression)
    {
        using var reader = new StringReader(expression);
        return Parse(reader);
    }
    
    /// <summary>
    /// Parses the expression contained in the specified <see cref="TextReader" />.
    /// </summary>
    /// <param name="expression">The expression.</param>
    /// <returns>A <see cref="Function" /> representing the expression as an AST.</returns>
    [MustUseReturnValue]
    public static Function Parse(TextReader expression)
    {
        var lexer = new Lexer(expression);
        return Parse(lexer);
    }

    /// <summary>
    /// Parses the expression contained in the specified <see cref="Lexer" />.
    /// </summary>
    /// <param name="lexer">The lexer.</param>
    /// <returns>A <see cref="Function" /> representing the expression as an AST.</returns>
    [MustUseReturnValue]
    public static Function Parse(Lexer lexer) => Processor.Process(ParseWithoutProcessing(lexer));

    [MustUseReturnValue]
    internal static Function ParseWithoutProcessing(Lexer lexer)
    {
        var expression = ParseExpression(lexer, 0);

        var parameters = new Dictionary<string, Parameter>();
        foreach (var variable in expression.ThisAndDescendents.OfType<Variable>())
        {
            if (!parameters.ContainsKey(variable.Name))
            {
                parameters.Add(variable.Name, new Parameter(variable.Name));
            }
        }

        return new Function(parameters.Values.OrderBy(p => p.Name), expression);
    }

    [MustUseReturnValue]
    private static Expression ParseExpression(Lexer lexer, int minimumBindingPower)
    {
        Expression left;
        switch (lexer.Next())
        {
            case Number number:
                left = new Constant(number.Value);
                break;
            
            case OpenBracket:
                left = ParseExpression(lexer, 0);
                var next = lexer.Next();
                if (next is not CloseBracket)
                {
                    throw CreateUnexpectedTokenException(next);
                }
                break;
            
            case Identifier identifier:
                left = new Variable(identifier.Name);
                break;
            
            case var token:
                throw CreateUnexpectedTokenException(token);
        }

        while (true)
        {
            var token = lexer.Peek();
            if (token is EndOfFile or CloseBracket)
            {
                break;
            }

            if (token is not Operator @operator)
            {
                throw CreateUnexpectedTokenException(token);
            }

            var (leftBindingPower, rightBindingPower) = GetBindingPower(@operator);
            if (leftBindingPower < minimumBindingPower)
            {
                break;
            }

            lexer.Next();

            var right = ParseExpression(lexer, rightBindingPower);
            left = new BinaryOperation(@operator.Symbol, left, right);
        }

        return left;
    }

    [Pure]
    private static (int Left, int Right) GetBindingPower(Operator @operator) => @operator.Symbol is '+' or '-' ? (1, 2) : (3, 4);

    [Pure]
    private static InvalidOperationException CreateUnexpectedTokenException(Token token) => new ($"Unexpected token {token.GetType().Name} {token} at index {token.StartIndex}.");
}