namespace DesignPatternsBootcamp.Creational.AbstractFactory;

/// <summary>
/// The <b>Client</b> (provided complete, so you can see the pay-off). It depends ONLY on the
/// abstractions. It is handed one <see cref="IMarketFactory"/> and pulls a whole consistent
/// family out of it — it never mentions "US" or "EU", and cannot mix markets even if it tried.
/// </summary>
public sealed class TradeBooking
{
    private readonly IFeeSchedule _fees;
    private readonly ISettlementPolicy _settlement;

    public TradeBooking(IMarketFactory market)
    {
        _fees = market.CreateFeeSchedule();
        _settlement = market.CreateSettlementPolicy();
    }

    public BookingSummary Book(decimal notional)
    {
        Money commission = _fees.Commission(notional);
        return new BookingSummary(commission, _settlement.SettlementDays, _settlement.Currency);
    }
}
