namespace MrKWatkins.Ast.Tests;

public sealed class TypeExtensionsTests
{
    [TestCase(typeof(string), "String")]
    [TestCase(typeof(int), "Int32")]
    [TestCase(typeof(List<string>), "List<String>")]
    [TestCase(typeof(Dictionary<string, int>), "Dictionary<String, Int32>")]
    public void SimpleName(Type type, string expected) => type.SimpleName().Should().Be(expected);
}