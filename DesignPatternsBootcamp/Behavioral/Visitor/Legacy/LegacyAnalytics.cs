namespace DesignPatternsBootcamp.Behavioral.Visitor.Legacy;

/// <summary>
/// THE "BEFORE" CODE — every analytic re-switches on the instrument type.
///
/// Smells: each operation (market value, tax, exposure, …) hand-rolls the same
/// <c>Equity/Bond/Option</c> type switch, complete with a fall-through <c>default</c> the compiler
/// can't check for completeness; and each new operation is yet another such switch scattered across
/// the codebase. Visitor makes an operation a first-class object: the per-type logic is grouped by
/// operation, and dispatch is type-safe (no cast, no default) via each element's <c>Accept</c>.
/// </summary>
public sealed class LegacyAnalytics
{
    public decimal MarketValue(IInstrument instrument) => instrument switch
    {
        Equity e => e.Shares * e.Price,
        Bond b => b.FaceValue,
        Option o => o.Contracts * o.Premium * 100,
        _ => throw new ArgumentOutOfRangeException(nameof(instrument)),
    };

    // Tax(...) would repeat the exact same switch shape — and so would every other analytic.
}
