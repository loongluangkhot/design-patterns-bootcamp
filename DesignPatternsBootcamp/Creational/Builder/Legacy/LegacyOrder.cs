namespace DesignPatternsBootcamp.Creational.Builder.Legacy;

/// <summary>
/// THE "BEFORE" CODE — one giant constructor with ten positional parameters.
///
/// It technically works, but every call site is a hazard:
///
/// <code>
/// // A simple market order — but you still must pass a wall of nulls:
/// var o = new LegacyOrder("AAPL", "Buy", 100, "Market",
///                         null, null, "Day", "ACC-1", null, null);
///
/// // Two adjacent decimal? parameters (limitPrice, stopPrice). Transpose them and it still
/// // COMPILES — you have silently swapped your limit and your stop:
/// var bug = new LegacyOrder("TSLA", "Buy", 10, "StopLimit",
///                           /*stop where limit should be*/ 250m, 255m, "Day", "ACC-1", null, null);
/// </code>
///
/// Smells: unreadable call sites, easy positional mistakes, "valid" objects that are logically
/// nonsense, and no single place that guarantees the invariants. The Builder pattern fixes all of
/// these by naming each step and validating once, at the end.
/// </summary>
public sealed class LegacyOrder
{
    public string Symbol { get; }
    public string Side { get; }
    public int Quantity { get; }
    public string OrderType { get; }
    public decimal? LimitPrice { get; }
    public decimal? StopPrice { get; }
    public string TimeInForce { get; }
    public string Account { get; }
    public IReadOnlyList<(string SubAccount, int Quantity)> Allocations { get; }
    public string? Note { get; }

    public LegacyOrder(
        string symbol,
        string side,
        int quantity,
        string orderType,
        decimal? limitPrice,
        decimal? stopPrice,
        string timeInForce,
        string account,
        List<(string SubAccount, int Quantity)>? allocations,
        string? note)
    {
        Symbol = symbol;
        Side = side;
        Quantity = quantity;
        OrderType = orderType;
        LimitPrice = limitPrice;
        StopPrice = stopPrice;
        TimeInForce = timeInForce;
        Account = account;
        Allocations = allocations ?? new List<(string, int)>();
        Note = note;
    }
}
