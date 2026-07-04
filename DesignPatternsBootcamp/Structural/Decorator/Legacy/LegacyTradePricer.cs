namespace DesignPatternsBootcamp.Structural.Decorator.Legacy;

/// <summary>
/// THE "BEFORE" CODE — one pricer that takes a boolean flag (and a value) for every possible charge.
///
/// <code>
/// pricer.Price(10_000m, commission: true,  commissionRate: 0.001m,
///                        exchangeFee: true, exchangeAmount: 5m,
///                        tax: true,         taxRate: 0.10m);
/// </code>
///
/// Smells: the "flag argument" anti-pattern — a wall of booleans no caller can read; the order of
/// charges is hard-coded here, not chosen by the caller; and adding a new charge (a stamp duty, a
/// clearing fee) changes this method's signature and body, breaking every call site. Decorator makes
/// each charge a small object you can stack in any order and combination.
/// </summary>
public sealed class LegacyTradePricer
{
    public decimal Price(
        decimal notional,
        bool commission, decimal commissionRate,
        bool exchangeFee, decimal exchangeAmount,
        bool tax, decimal taxRate)
    {
        decimal total = notional;

        if (commission) total += total * commissionRate;
        if (exchangeFee) total += exchangeAmount;
        if (tax) total *= 1 + taxRate;

        return total;
    }
}
