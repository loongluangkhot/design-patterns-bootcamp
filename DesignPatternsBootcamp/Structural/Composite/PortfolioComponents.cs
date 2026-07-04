namespace DesignPatternsBootcamp.Structural.Composite;

/// <summary>
/// The <b>Component</b>: the single type the client programs against. Whether it is one holding or a
/// whole book of holdings, it can report its <see cref="MarketValue"/> — so the client never has to
/// ask "is this a leaf or a branch?".
/// </summary>
public interface IPortfolioComponent
{
    string Name { get; }
    decimal MarketValue();
}

/// <summary>The <b>Leaf</b>: a single position (a holding of one instrument).</summary>
public sealed class Position : IPortfolioComponent
{
    public string Name { get; }
    public int Quantity { get; }
    public decimal Price { get; }

    public Position(string symbol, int quantity, decimal price)
    {
        Name = symbol;
        Quantity = quantity;
        Price = price;
    }

    public decimal MarketValue() =>
        throw new NotImplementedException("TODO(student): a position is worth Quantity × Price.");
}

/// <summary>
/// The <b>Composite</b>: a portfolio that contains other components — positions AND sub-portfolios,
/// side by side, because both are just <see cref="IPortfolioComponent"/>.
/// </summary>
public sealed class Portfolio : IPortfolioComponent
{
    private readonly List<IPortfolioComponent> _children = new();

    public string Name { get; }

    public Portfolio(string name) => Name = name;

    public Portfolio Add(IPortfolioComponent component)
    {
        _children.Add(component);
        return this;
    }

    public IReadOnlyList<IPortfolioComponent> Children => _children;

    /// <remarks>
    /// TODO(student): a portfolio is worth the sum of its children's <see cref="MarketValue"/>.
    /// Because a child might itself be a Portfolio, this one line recurses through the whole tree —
    /// that uniform recursion is the entire point of Composite.
    /// </remarks>
    public decimal MarketValue() =>
        throw new NotImplementedException(
            "TODO(student): sum MarketValue() over Children (works for leaves and sub-portfolios alike).");
}
