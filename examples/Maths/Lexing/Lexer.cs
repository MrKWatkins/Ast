using System.Collections;
using System.Text;

namespace MrKWatkins.Ast.Examples.Maths.Lexing;

/// <summary>
/// Hand rolled lexer to take an input stream and split it into tokens.
/// </summary>
public sealed class Lexer : IEnumerable<Token>
{
    private readonly TextReader input;
    private int currentIndex;
    private bool isFinished;
    private Token? peeked;

    public Lexer(TextReader input)
    {
        this.input = input;
    }

    public Token Next()
    {
        if (isFinished)
        {
            throw new InvalidOperationException("Input has already been consumed.");
        }

        // If we've already peeked reuse that token, otherwise read one in.
        Token token;
        if (peeked != null)
        {
            token = peeked;
            peeked = null;
        }
        else
        {
            token = ReadToken(currentIndex);
        }

        // Update the state from the token.
        currentIndex = token.StartIndex + token.Length;
        if (token is EndOfFile)
        {
            isFinished = true;
        }
        return token;
    }

    public Token Peek()
    {
        if (isFinished)
        {
            throw new InvalidOperationException("Input has already been consumed.");
        }

        if (peeked != null)
        {
            return peeked;
        }

        peeked = ReadToken(currentIndex);
        return peeked;
    }

    private Token ReadToken(int startIndex)
    {
        while (true)
        {
            var value = input.Read();
            if (value == -1)
            {
                return new EndOfFile(currentIndex);
            }

            var character = (char)value;
            if (char.IsWhiteSpace(character))
            {
                startIndex += 1;
                continue;
            }

            if (char.IsAsciiDigit(character))
            {
                return ReadNumber(startIndex, character);
            }

            if (char.IsAsciiLetter(character))
            {
                return ReadVariable(startIndex, character);
            }

            return character switch
            {
                '+' or '-' or '*' or '/' => new Operator(startIndex, character),
                '(' => new OpenBracket(startIndex),
                ')' => new CloseBracket(startIndex),
                _ => throw new InvalidOperationException($"Unexpected character '{character}'.")
            };
        }
    }

    private Number ReadNumber(int startIndex, char firstDigit)
    {
        var length = 1;
        var number = firstDigit - '0';
        while (char.IsAsciiDigit((char)input.Peek()))
        {
            length++;
            number = number * 10 + (input.Read() - '0');
        }

        return new Number(startIndex, length, number);
    }

    private Identifier ReadVariable(int startIndex, char firstCharacter)
    {
        var variable = new StringBuilder();
        variable.Append(firstCharacter);
        while (char.IsAsciiLetter((char)input.Peek()))
        {
            variable.Append((char)input.Read());
        }

        return new Identifier(startIndex, variable.ToString());
    }

    public IEnumerator<Token> GetEnumerator()
    {
        Token token;
        do
        {
            token = Next();
            yield return token;
        } while (token is not EndOfFile);
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}