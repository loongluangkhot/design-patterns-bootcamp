namespace DesignPatternsBootcamp.Structural.Facade;

/// <summary>
/// The <b>Facade</b>: one simple entry point — <see cref="Settle"/> — over the four-service
/// settlement subsystem. Callers say "settle this trade" and stop caring about validation order,
/// funding checks, clearing, and ledger posting. The facade owns that choreography once.
///
/// Your task is to implement <see cref="Settle"/>.
/// </summary>
public sealed class SettlementFacade
{
    private readonly TradeValidator _validator;
    private readonly FundingService _funding;
    private readonly ClearingHouse _clearing;
    private readonly Ledger _ledger;

    public SettlementFacade(TradeValidator validator, FundingService funding, ClearingHouse clearing, Ledger ledger)
    {
        _validator = validator;
        _funding = funding;
        _clearing = clearing;
        _ledger = ledger;
    }

    /// <remarks>
    /// TODO(student): choreograph the subsystem:
    ///   1. If <c>_validator.IsValid(trade, out var reason)</c> fails → return a failed result with
    ///      that reason. Do NOT touch clearing or the ledger.
    ///   2. If <c>_funding.HasSufficientFunds(trade.Account, trade.Notional)</c> is false → return a
    ///      failed result ("Insufficient funds."). Again, no clearing, no ledger.
    ///   3. Otherwise: submit to clearing to get a reference, post it to the ledger, and return a
    ///      successful <see cref="SettlementResult"/> carrying that reference.
    /// </remarks>
    public SettlementResult Settle(Trade trade) =>
        throw new NotImplementedException(
            "TODO(student): validate → check funding → clear → post to ledger. See <remarks>.");
}
