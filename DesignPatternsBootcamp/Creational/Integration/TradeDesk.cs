using DesignPatternsBootcamp.Creational.AbstractFactory;
using DesignPatternsBootcamp.Creational.Builder;

namespace DesignPatternsBootcamp.Creational.Integration;

/// <summary>
/// The completed ticket: the immutable order plus how it will be priced and settled in its market.
/// It stitches together a <see cref="TradeOrder"/> (from the Builder kata) and <see cref="Money"/>
/// (from the Abstract Factory kata).
/// </summary>
public record TradeTicket(TradeOrder Order, Money Commission, int SettlementDays, string SettlementCurrency);

/// <summary>
/// WEEK 1 CAPSTONE — the desk books a trade end-to-end by <b>composing</b> the patterns you built:
///
///   • <b>Abstract Factory</b> (<see cref="IMarketFactory"/>) supplies a consistent fee + settlement
///     family for the market this desk trades.
///   • <b>Builder</b> (<see cref="TradeOrderBuilder"/>) assembles the validated, immutable order.
///
/// Complete <b>Day 1b (Abstract Factory)</b> and <b>Day 2 (Builder)</b> first — this capstone calls
/// into both. See the README for how Factory Method, Prototype, and Singleton also slot in.
/// </summary>
public sealed class TradeDesk
{
    private readonly IMarketFactory _market;

    // The desk is configured with ONE market family. Everything it books is priced and settled
    // consistently, because both come from this single factory (the Abstract Factory guarantee).
    public TradeDesk(IMarketFactory market) => _market = market;

    /// <summary>
    /// Book a limit buy: build the order, price the commission on its notional, and attach the
    /// market's settlement terms.
    /// </summary>
    /// <remarks>
    /// TODO(student): implement using the two patterns:
    ///   1. Build the order with <see cref="TradeOrderBuilder"/>: Buy(quantity, symbol),
    ///      Limit(limitPrice), ForAccount(account), then Build().
    ///   2. Pull the fee schedule and settlement policy from <c>_market</c>.
    ///   3. Commission is charged on the notional = quantity × limitPrice.
    ///   4. Return a <see cref="TradeTicket"/> with the order, commission, and settlement terms.
    /// </remarks>
    public TradeTicket BookLimitBuy(string symbol, int quantity, decimal limitPrice, string account) =>
        throw new NotImplementedException(
            "TODO(student): compose TradeOrderBuilder + IMarketFactory into a TradeTicket. See <remarks>.");
}
