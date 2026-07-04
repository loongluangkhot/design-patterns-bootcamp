namespace DesignPatternsBootcamp.Behavioral.Strategy;

/// <summary>
/// The <b>Strategy</b>: one way to compute a fee. Each concrete strategy is an interchangeable
/// algorithm behind this single interface, so the caller can pick or swap one without any branching.
/// </summary>
public interface IFeeStrategy
{
    decimal Calculate(decimal notional);
}

// ---- Concrete Strategies. Implement Calculate for all three. --------------------------------

/// <summary>A fixed fee, regardless of notional.</summary>
public sealed class FlatFee : IFeeStrategy
{
    private readonly decimal _amount;

    public FlatFee(decimal amount) => _amount = amount;

    public decimal Calculate(decimal notional) =>
        throw new NotImplementedException("TODO(student): return the flat _amount.");
}

/// <summary>A percentage of notional (e.g. 0.001 = 10 bps), rounded to cents.</summary>
public sealed class PercentageFee : IFeeStrategy
{
    private readonly decimal _rate;

    public PercentageFee(decimal rate) => _rate = rate;

    public decimal Calculate(decimal notional) =>
        throw new NotImplementedException("TODO(student): return Math.Round(notional * _rate, 2).");
}

/// <summary>Tiered: 0.1% below $100k, 0.05% at or above $100k. Rounded to cents.</summary>
public sealed class TieredFee : IFeeStrategy
{
    public decimal Calculate(decimal notional) =>
        throw new NotImplementedException(
            "TODO(student): notional < 100_000 → 0.1% (0.001), otherwise 0.05% (0.0005); round to 2 dp.");
}
