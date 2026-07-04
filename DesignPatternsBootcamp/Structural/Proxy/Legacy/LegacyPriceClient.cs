namespace DesignPatternsBootcamp.Structural.Proxy.Legacy;

/// <summary>
/// THE "BEFORE" CODE — the client calls the expensive price service <b>directly</b>, every time.
///
/// Ask for the same symbol three times and you pay for three round-trips. There is nowhere to put
/// caching (or access checks, or lazy loading) without either editing the client everywhere or
/// editing the real service itself. A Proxy — same interface, inserted transparently — gives you
/// that seam: the client keeps calling <see cref="IPriceService"/>, unaware anything changed.
/// </summary>
public sealed class LegacyPriceClient
{
    public decimal[] Quotes(IPriceService service, params string[] symbols) =>
        symbols.Select(service.GetPrice).ToArray();
}
