namespace DesignPatternsBootcamp.Creational.Builder;

public enum Side { Buy, Sell }

public enum OrderType { Market, Limit, Stop, StopLimit }

public enum TimeInForce { Day, GoodTilCanceled, ImmediateOrCancel }

/// <summary>A slice of an order routed to a specific sub-account.</summary>
public record Allocation(string SubAccount, int Quantity);

/// <summary>
/// The <b>Product</b>: a fully-formed, <b>immutable</b> trade order. Once built it cannot be
/// changed — every property is init-only and the allocation list is a read-only snapshot.
/// The only sanctioned way to create one is through <see cref="TradeOrderBuilder"/>.
/// </summary>
public sealed record TradeOrder(
    Side Side,
    int Quantity,
    string Symbol,
    OrderType OrderType,
    decimal? LimitPrice,
    decimal? StopPrice,
    TimeInForce TimeInForce,
    string Account,
    IReadOnlyList<Allocation> Allocations,
    string? Note);
