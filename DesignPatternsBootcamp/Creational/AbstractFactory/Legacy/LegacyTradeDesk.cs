namespace DesignPatternsBootcamp.Creational.AbstractFactory.Legacy;

/// <summary>
/// THE "BEFORE" CODE — it books trades in multiple markets, but the market's rules are spread
/// across THREE independent <c>switch</c> statements, all keyed by the same loose "region" string.
///
/// The danger is <b>inconsistency</b>:
///   • Nothing forces the three switches to agree. A maintainer can update fees for a new market
///     and forget settlement, shipping a half-configured region.
///   • A caller can pass "US" to <see cref="Commission"/> and "EU" to <see cref="SettlementCurrency"/>
///     and the compiler is perfectly happy — you have just booked a trade with US fees settling in
///     euros.
///   • Adding an APAC market means hunting down and editing every one of these switches.
///
/// Abstract Factory fixes this by making "a market" a single object that hands out a matched set.
/// </summary>
public class LegacyTradeDesk
{
    public (decimal Amount, string Currency) Commission(string region, decimal notional)
    {
        switch (region)
        {
            case "US":
                return (Math.Round(Math.Max(1.00m, notional * 0.001m), 2), "USD");
            case "EU":
                return (Math.Round(1.20m + notional * 0.002m, 2), "EUR");
            default:
                throw new ArgumentOutOfRangeException(nameof(region), region, "Unsupported market.");
        }
    }

    public int SettlementDays(string region) => region switch
    {
        "US" => 1,
        "EU" => 2,
        _ => throw new ArgumentOutOfRangeException(nameof(region), region, "Unsupported market."),
    };

    public string SettlementCurrency(string region) => region switch
    {
        "US" => "USD",
        "EU" => "EUR",
        _ => throw new ArgumentOutOfRangeException(nameof(region), region, "Unsupported market."),
    };
}
