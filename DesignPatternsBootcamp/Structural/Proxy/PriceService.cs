namespace DesignPatternsBootcamp.Structural.Proxy;

/// <summary>
/// The <b>Subject</b>: the interface both the real service and its proxy implement. Because they
/// share it, a proxy can slot in anywhere the real thing is expected — the client can't tell.
/// </summary>
public interface IPriceService
{
    decimal GetPrice(string symbol);
}

/// <summary>
/// The <b>Real Subject</b> (provided). Each call stands in for an expensive vendor/network round-trip;
/// <see cref="CallCount"/> lets tests prove how often we actually hit it.
/// </summary>
public sealed class RealPriceService : IPriceService
{
    public int CallCount { get; private set; }

    public decimal GetPrice(string symbol)
    {
        CallCount++; // pretend this is a slow, costly lookup
        return symbol switch
        {
            "AAPL" => 150m,
            "MSFT" => 400m,
            _ => throw new KeyNotFoundException($"No price for {symbol}."),
        };
    }
}
