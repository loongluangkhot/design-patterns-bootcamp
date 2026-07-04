namespace DesignPatternsBootcamp.Structural.Bridge;

/// <summary>
/// The <b>Implementor</b> — the low-level "where do the ticks come from" dimension. Vendors vary
/// here (primary vs. backup vs. some future feed) independently of how we derive numbers from them.
/// </summary>
public interface IPriceSource
{
    IReadOnlyList<decimal> RecentTicks(string symbol);
}

// ---- Concrete Implementors (provided). Feed data lives here, in ONE place per feed. ----------

public sealed class PrimaryFeed : IPriceSource
{
    public IReadOnlyList<decimal> RecentTicks(string symbol) => symbol switch
    {
        "AAPL" => new[] { 100m, 101m, 102m },
        _ => throw new KeyNotFoundException($"Primary feed has no data for {symbol}."),
    };
}

public sealed class BackupFeed : IPriceSource
{
    public IReadOnlyList<decimal> RecentTicks(string symbol) => symbol switch
    {
        "AAPL" => new[] { 100m, 104m },
        _ => throw new KeyNotFoundException($"Backup feed has no data for {symbol}."),
    };
}
