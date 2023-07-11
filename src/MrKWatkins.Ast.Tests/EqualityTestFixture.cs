using System.Reflection;

namespace MrKWatkins.Ast.Tests;

public abstract class EqualityTestFixture
{
    protected static void AssertEqual<T>(T x, object? y, bool expected)
        where T : IEquatable<T>
    {
        x.Equals(y).Should().Be(expected);
        y?.Equals(x).Should().Be(expected);

        if (y is T ty)
        {
            AssertEqual(x, ty, expected);
        }
    }

    protected static void AssertEqual<T>(T x, T? y, bool expected)
        where T : IEquatable<T>
    {
        x.Equals(y).Should().Be(expected);
        y?.Equals(x).Should().Be(expected);

        ((object)x).Equals(y).Should().Be(expected);
        (y as object)?.Equals(x).Should().Be(expected);

        AssertOperator("Equality", x, y, expected);
        AssertOperator("Inequality", x, y, !expected);

        if (y != null && expected)
        {
            x.GetHashCode().Should().Be(y.GetHashCode());
            y.GetHashCode().Should().Be(x.GetHashCode());
        }
    }

    private static void AssertOperator<T>(string name, T x, T? y, bool expected)
    {
        var method = typeof(T).GetMethod($"op_{name}", BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy);
        if (method != null)
        {
            method.Invoke(null, new object?[] { x, y }).Should().Be(expected);
            method.Invoke(null, new object?[] { y, x }).Should().Be(expected);
        }
    }
}