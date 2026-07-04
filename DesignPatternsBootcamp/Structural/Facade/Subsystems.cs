namespace DesignPatternsBootcamp.Structural.Facade;

/// <summary>A trade to be settled.</summary>
public record Trade(string TradeId, string Account, string Symbol, int Quantity, decimal Price)
{
    public decimal Notional => Quantity * Price;
}

/// <summary>What the caller gets back — one simple result, whatever happened inside.</summary>
public record SettlementResult(bool Settled, string? ClearingReference, string Message);

// =============================================================================================
//  THE SUBSYSTEM — four independent services with their own APIs. Individually fine; the pain is
//  making a caller learn all four, call them in the right order, and handle the failure paths.
//  (All provided complete — you orchestrate them, you don't change them.)
// =============================================================================================

public sealed class TradeValidator
{
    public bool IsValid(Trade trade, out string reason)
    {
        if (trade.Quantity <= 0) { reason = "Quantity must be positive."; return false; }
        if (trade.Price <= 0) { reason = "Price must be positive."; return false; }
        reason = "";
        return true;
    }
}

public sealed class FundingService
{
    private readonly IReadOnlyDictionary<string, decimal> _balances;

    public FundingService(IReadOnlyDictionary<string, decimal> balances) => _balances = balances;

    public bool HasSufficientFunds(string account, decimal amount) =>
        _balances.TryGetValue(account, out decimal balance) && balance >= amount;
}

public sealed class ClearingHouse
{
    /// <summary>Counts submissions so tests can prove the facade short-circuits before clearing.</summary>
    public int SubmissionCount { get; private set; }

    public string Submit(Trade trade)
    {
        SubmissionCount++;
        return $"CLR-{trade.TradeId}";
    }
}

public sealed class Ledger
{
    private readonly List<string> _entries = new();

    public IReadOnlyList<string> Entries => _entries;

    public void Post(Trade trade, string clearingRef) =>
        _entries.Add($"{trade.TradeId}:{clearingRef}:{trade.Notional}");
}
