namespace DesignPatternsBootcamp.Creational.Builder;

/// <summary>
/// The <b>Builder</b>. It accumulates the pieces of an order through small, readable, fluent calls
/// and defers all validation and construction to <see cref="Build"/>.
///
/// Two ideas make this pattern work:
///   1. <b>Fluent accumulation</b> — every setter returns <c>this</c>, so calls chain and read like
///      a sentence. Notice they do NOT validate; they just record intent.
///   2. <b>Deferred construction</b> — nothing is checked until <see cref="Build"/>, which is the
///      single gate that enforces the invariants and produces the immutable product.
///
/// The setters are provided so you can see the idiom. <b>Your job is to implement Build().</b>
/// </summary>
public sealed class TradeOrderBuilder
{
    private Side _side;
    private int _quantity;
    private string? _symbol;
    private OrderType _orderType = OrderType.Market;
    private decimal? _limitPrice;
    private decimal? _stopPrice;
    private TimeInForce _timeInForce = TimeInForce.Day;
    private string? _account;
    private readonly List<Allocation> _allocations = new();
    private string? _note;

    public static TradeOrderBuilder Create() => new();

    public TradeOrderBuilder Buy(int quantity, string symbol)
    {
        _side = Side.Buy;
        _quantity = quantity;
        _symbol = symbol;
        return this;
    }

    public TradeOrderBuilder Sell(int quantity, string symbol)
    {
        _side = Side.Sell;
        _quantity = quantity;
        _symbol = symbol;
        return this;
    }

    public TradeOrderBuilder Market()
    {
        _orderType = OrderType.Market;
        _limitPrice = null;
        _stopPrice = null;
        return this;
    }

    public TradeOrderBuilder Limit(decimal price)
    {
        _orderType = OrderType.Limit;
        _limitPrice = price;
        return this;
    }

    public TradeOrderBuilder Stop(decimal price)
    {
        _orderType = OrderType.Stop;
        _stopPrice = price;
        return this;
    }

    public TradeOrderBuilder StopLimit(decimal stopPrice, decimal limitPrice)
    {
        _orderType = OrderType.StopLimit;
        _stopPrice = stopPrice;
        _limitPrice = limitPrice;
        return this;
    }

    public TradeOrderBuilder Day() { _timeInForce = TimeInForce.Day; return this; }

    public TradeOrderBuilder GoodTilCanceled() { _timeInForce = TimeInForce.GoodTilCanceled; return this; }

    public TradeOrderBuilder ImmediateOrCancel() { _timeInForce = TimeInForce.ImmediateOrCancel; return this; }

    public TradeOrderBuilder ForAccount(string account) { _account = account; return this; }

    public TradeOrderBuilder AllocateTo(string subAccount, int quantity)
    {
        _allocations.Add(new Allocation(subAccount, quantity));
        return this;
    }

    public TradeOrderBuilder WithNote(string note) { _note = note; return this; }

    /// <summary>
    /// Validate the accumulated state and produce an immutable <see cref="TradeOrder"/>.
    /// </summary>
    /// <remarks>
    /// TODO(student): implement this. Enforce, throwing <see cref="InvalidOperationException"/> on
    /// any violation:
    ///   • Quantity must be &gt; 0.
    ///   • Symbol must be non-empty.
    ///   • Account must be set (non-empty).
    ///   • If any allocations were added, their quantities must sum to Quantity.
    /// Then construct the TradeOrder. Copy the allocations into a NEW read-only list so that later
    /// calls on this builder cannot mutate an order you already handed out (defensive copy).
    /// </remarks>
    public TradeOrder Build() =>
        throw new NotImplementedException(
            "TODO(student): validate the accumulated state and build an immutable TradeOrder. See the <remarks> above.");
}
