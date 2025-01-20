using System.Reflection;

namespace MrKWatkins.Ast.Tests;

public abstract class EqualityTestFixture
{
    [PublicAPI]
    protected static void AssertEqual<T>(T x, object? y, bool expected)
        where T : IEquatable<T>
    {
        x.Equals(y).Should().Equal(expected);
        y?.Equals(x).Should().Equal(expected);

        if (y is T ty)
        {
            AssertEqual(x, ty, expected);
        }
    }

    [PublicAPI]
    protected static void AssertEqual<T>(T x, T? y, bool expected)
        where T : IEquatable<T>
    {
        x.Equals(y).Should().Equal(expected);
        y?.Equals(x).Should().Equal(expected);

        ((object) x).Equals(y).Should().Equal(expected);
        (y as object)?.Equals(x).Should().Equal(expected);

        AssertOperator("Equality", x, y, expected);
        AssertOperator("Inequality", x, y, !expected);

        if (y != null && expected)
        {
            x.GetHashCode().Should().Equal(y.GetHashCode());
            y.GetHashCode().Should().Equal(x.GetHashCode());
        }
    }

    private static void AssertOperator<T>(string name, T x, T? y, bool expected)
    {
        var method = typeof(T).GetMethod($"op_{name}", BindingFlags.Static | BindingFlags.Public | BindingFlags.FlattenHierarchy);
        if (method != null)
        {
            method.Invoke(null, [x, y]).Should().Equal(expected);
            method.Invoke(null, [y, x]).Should().Equal(expected);
        }
    }
}