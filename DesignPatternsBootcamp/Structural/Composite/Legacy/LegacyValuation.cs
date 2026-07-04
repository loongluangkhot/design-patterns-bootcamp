namespace DesignPatternsBootcamp.Structural.Composite.Legacy;

/// <summary>A holding, in the legacy world.</summary>
public sealed class LegacyHolding
{
    public required string Symbol { get; init; }
    public required int Quantity { get; init; }
    public required decimal Price { get; init; }
}

/// <summary>
/// A legacy portfolio keeps leaves and branches in SEPARATE lists of DIFFERENT types.
/// </summary>
public sealed class LegacyPortfolio
{
    public List<LegacyHolding> Holdings { get; } = new();
    public List<LegacyPortfolio> SubPortfolios { get; } = new();
}

/// <summary>
/// THE "BEFORE" CODE — because holdings and sub-portfolios are different types kept in different
/// lists, every operation over the tree must know the structure and hand-roll the recursion,
/// special-casing each kind of node.
///
/// Smells: the client is coupled to the tree's shape; the same two-loop traversal is copy-pasted
/// into every operation (valuation, counting, reporting…); adding a new node kind (say, a cash
/// balance) forces edits to all of them. Composite lets one interface stand for "leaf or branch"
/// so traversal becomes uniform and lives in the nodes themselves.
/// </summary>
public static class LegacyValuation
{
    public static decimal TotalValue(LegacyPortfolio portfolio)
    {
        decimal total = 0m;

        foreach (LegacyHolding holding in portfolio.Holdings)
            total += holding.Quantity * holding.Price;

        foreach (LegacyPortfolio sub in portfolio.SubPortfolios)
            total += TotalValue(sub); // caller must know branches exist and recurse by hand

        return total;
    }
}
