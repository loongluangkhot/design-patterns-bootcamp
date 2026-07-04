namespace DesignPatternsBootcamp.Structural.Proxy;

/// <summary>
/// The <b>Proxy</b>: it implements <see cref="IPriceService"/> just like the real service, holds a
/// reference to the real one, and controls access to it — here by <b>caching</b> results so a
/// repeated lookup is served from memory instead of hitting the expensive backend again.
///
/// The client sees an <see cref="IPriceService"/> and never knows a proxy is in the way. Your task
/// is to implement <see cref="GetPrice"/>.
/// </summary>
public sealed class CachingPriceProxy : IPriceService
{
    private readonly IPriceService _real;
    private readonly Dictionary<string, decimal> _cache = new();

    public CachingPriceProxy(IPriceService real) => _real = real;

    /// <remarks>
    /// TODO(student):
    ///   • if <paramref name="symbol"/> is already in <c>_cache</c>, return the cached price
    ///     WITHOUT calling the real service;
    ///   • otherwise call <c>_real.GetPrice(symbol)</c> once, store the result in <c>_cache</c>, and
    ///     return it.
    /// </remarks>
    public decimal GetPrice(string symbol) =>
        throw new NotImplementedException(
            "TODO(student): serve from _cache on a hit; on a miss call _real once and cache it.");
}
