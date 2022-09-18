namespace MrKWatkins.Ast.Tests;

public abstract class TreeTestFixture
{
    protected TestNode N1 { get; private set; } = null!;
    protected TestNode N11 { get; private set; } = null!;
    protected TestNode N12 { get; private set; } = null!;
    protected TestNode N13 { get; private set; } = null!;
    protected TestNode N111 { get; private set; } = null!;
    protected TestNode N121 { get; private set; } = null!;
    protected TestNode N122 { get; private set; } = null!;
    protected TestNode N123 { get; private set; } = null!;

    [SetUp]
    public void SetUp()
    {
        // Reset the nodes each test so we can mutate the tree to our heart's content.
        N1 = new ANode { Name = nameof(N1) };
        N11 = new ANode { Name = nameof(N11) };
        N12 = new BNode { Name = nameof(N12) };
        N13 = new CNode { Name = nameof(N13) };
        N111 = new ANode { Name = nameof(N111) };
        N121 = new ANode { Name = nameof(N121) };
        N122 = new BNode { Name = nameof(N122) };
        N123 = new CNode { Name = nameof(N123) };
        N1.Children.Add(N11, N12, N13);
        N11.Children.Add(N111);
        N12.Children.Add(N121, N122, N123);
    }
}