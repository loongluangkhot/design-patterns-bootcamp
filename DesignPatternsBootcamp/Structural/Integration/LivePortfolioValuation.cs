using DesignPatternsBootcamp.Structural.Composite;
using DesignPatternsBootcamp.Structural.Proxy;

namespace DesignPatternsBootcamp.Structural.Integration;

/// <summary>
/// WEEK 2 CAPSTONE — value a whole book at <b>live</b> prices, efficiently, by composing two
/// structural patterns you built:
///
///   • <b>Composite</b> gives us the portfolio tree to walk (positions and sub-portfolios).
///   • <b>Proxy</b> (a caching <see cref="IPriceService"/>) means each distinct symbol is fetched
///     from the expensive backend at most once, even if it appears in many sub-portfolios.
///
/// A position's stored price is its <i>book</i> price; here we ignore it and reprice at the current
/// market price from the service. Complete <b>Day 4 (Proxy)</b> first — this capstone relies on the
/// caching proxy to get the "priced once" behaviour.
/// </summary>
public sealed class LivePortfolioValuation
{
    private readonly IPriceService _prices;

    public LivePortfolioValuation(IPriceService prices) => _prices = prices;

    /// <remarks>
    /// TODO(student): return the live market value of <paramref name="component"/> by walking the
    /// Composite tree. Pattern-match on the concrete node type:
    ///   • <c>Position p</c>  → <c>p.Quantity * _prices.GetPrice(p.Name)</c>
    ///   • <c>Portfolio pf</c> → the sum of <c>MarketValue(child)</c> over <c>pf.Children</c>
    ///
    /// (Composite's own interface prices a position at its <i>book</i> value; this is a NEW operation
    /// that needs an external service, so we reach for the concrete types here. Week 4's <b>Visitor</b>
    /// is the pattern for adding such operations to a hierarchy without pattern-matching.)
    /// </remarks>
    public decimal MarketValue(IPortfolioComponent component) =>
        throw new NotImplementedException(
            "TODO(student): walk the Composite tree, pricing each Position via _prices. See <remarks>.");
}
