namespace DesignPatternsBootcamp.Structural.Facade.Legacy;

/// <summary>
/// THE "BEFORE" CODE — the desk settles a trade by driving all four subsystems <b>itself</b>: it
/// knows they exist, knows the order, knows each API, and hand-codes the failure paths.
///
/// It works, but that knowledge is now smeared across the caller — and every other place that
/// settles a trade must repeat the exact same dance. Change the flow (add a compliance check, swap
/// the clearing house) and you hunt down every copy. A Facade collapses all of it into one method
/// behind one small interface.
/// </summary>
public sealed class LegacyDeskClient
{
    public string Settle(Trade trade, TradeValidator validator, FundingService funding, ClearingHouse clearing, Ledger ledger)
    {
        if (!validator.IsValid(trade, out string reason))
            return $"REJECTED: {reason}";

        if (!funding.HasSufficientFunds(trade.Account, trade.Notional))
            return "REJECTED: Insufficient funds.";

        string clearingRef = clearing.Submit(trade);
        ledger.Post(trade, clearingRef);
        return $"SETTLED: {clearingRef}";
    }
}
