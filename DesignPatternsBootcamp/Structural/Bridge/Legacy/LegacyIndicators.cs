namespace DesignPatternsBootcamp.Structural.Bridge.Legacy;

/// <summary>
/// THE "BEFORE" CODE — someone baked the feed INTO each indicator by subclassing. With 2 indicators
/// (last-trade, moving-average) and 2 feeds (primary, backup) you already need <b>2 × 2 = 4</b>
/// classes, and the feed-access code is copy-pasted between every pair that shares a feed.
///
/// Add a <c>VwapIndicator</c> → 6 classes. Add a <c>TertiaryFeed</c> → 9. This is the combinatorial
/// explosion Bridge exists to prevent: it splits the two axes so you keep just
/// (indicators + feeds), not (indicators × feeds).
/// </summary>
public sealed class PrimaryLastTrade
{
    public decimal Value(string symbol) => PrimaryTicks(symbol)[^1];
    private static decimal[] PrimaryTicks(string symbol) =>
        symbol == "AAPL" ? new[] { 100m, 101m, 102m } : throw new KeyNotFoundException(symbol);
}

public sealed class PrimaryMovingAverage
{
    public decimal Value(string symbol) => PrimaryTicks(symbol).Average();
    private static decimal[] PrimaryTicks(string symbol) => // ← duplicated from PrimaryLastTrade
        symbol == "AAPL" ? new[] { 100m, 101m, 102m } : throw new KeyNotFoundException(symbol);
}

public sealed class BackupLastTrade
{
    public decimal Value(string symbol) => BackupTicks(symbol)[^1];
    private static decimal[] BackupTicks(string symbol) =>
        symbol == "AAPL" ? new[] { 100m, 104m } : throw new KeyNotFoundException(symbol);
}

public sealed class BackupMovingAverage
{
    public decimal Value(string symbol) => BackupTicks(symbol).Average();
    private static decimal[] BackupTicks(string symbol) => // ← duplicated from BackupLastTrade
        symbol == "AAPL" ? new[] { 100m, 104m } : throw new KeyNotFoundException(symbol);
}
