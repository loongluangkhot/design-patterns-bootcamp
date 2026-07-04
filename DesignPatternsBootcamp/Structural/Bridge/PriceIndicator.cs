namespace DesignPatternsBootcamp.Structural.Bridge;

/// <summary>
/// The <b>Abstraction</b> — the "what do we derive from the ticks" dimension. It does NOT inherit a
/// feed; it <b>holds</b> one (<see cref="Source"/>) and delegates the low-level fetch to it. That
/// composition is the "bridge": indicators and feeds now vary on their own axes and combine freely.
/// </summary>
public abstract class PriceIndicator
{
    protected readonly IPriceSource Source;

    protected PriceIndicator(IPriceSource source) => Source = source;

    public abstract decimal Value(string symbol);
}

// ---- Refined Abstractions: each derives a number from whatever feed it was handed. -----------

public sealed class LastTradeIndicator : PriceIndicator
{
    public LastTradeIndicator(IPriceSource source) : base(source) { }

    public override decimal Value(string symbol) =>
        throw new NotImplementedException(
            "TODO(student): return the most recent tick — the last element of Source.RecentTicks(symbol).");
}

public sealed class MovingAverageIndicator : PriceIndicator
{
    public MovingAverageIndicator(IPriceSource source) : base(source) { }

    public override decimal Value(string symbol) =>
        throw new NotImplementedException(
            "TODO(student): return the average of Source.RecentTicks(symbol).");
}
