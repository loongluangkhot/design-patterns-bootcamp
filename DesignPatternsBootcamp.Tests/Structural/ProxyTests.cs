using DesignPatternsBootcamp.Structural.Proxy;
using DesignPatternsBootcamp.Structural.Proxy.Legacy;

namespace DesignPatternsBootcamp.Tests.Structural;

public class ProxyTests
{
    // -----------------------------------------------------------------------------------------
    //  Legacy baseline — already PASSING. Every lookup goes straight to the expensive backend.
    // -----------------------------------------------------------------------------------------

    [Fact]
    public void Legacy_client_hits_the_backend_on_every_lookup()
    {
        var real = new RealPriceService();

        decimal[] quotes = new LegacyPriceClient().Quotes(real, "AAPL", "AAPL", "AAPL");

        Assert.Equal(new[] { 150m, 150m, 150m }, quotes);
        Assert.Equal(3, real.CallCount); // three lookups → three expensive calls
    }

    // -----------------------------------------------------------------------------------------
    //  Your refactor — RED until CachingPriceProxy.GetPrice caches.
    // -----------------------------------------------------------------------------------------

    [Fact]
    public void Proxy_returns_the_same_price_as_the_real_service()
    {
        var proxy = new CachingPriceProxy(new RealPriceService());

        Assert.Equal(150m, proxy.GetPrice("AAPL"));
    }

    [Fact]
    public void Repeated_lookups_of_one_symbol_hit_the_backend_only_once()
    {
        var real = new RealPriceService();
        var proxy = new CachingPriceProxy(real);

        proxy.GetPrice("AAPL");
        proxy.GetPrice("AAPL");
        proxy.GetPrice("AAPL");

        Assert.Equal(1, real.CallCount);
    }

    [Fact]
    public void Each_distinct_symbol_is_fetched_exactly_once()
    {
        var real = new RealPriceService();
        var proxy = new CachingPriceProxy(real);

        proxy.GetPrice("AAPL");
        proxy.GetPrice("MSFT");
        proxy.GetPrice("AAPL");
        proxy.GetPrice("MSFT");

        Assert.Equal(2, real.CallCount);
    }

    [Fact]
    public void Proxy_is_a_drop_in_replacement_for_the_real_service()
    {
        // The unchanged legacy client works when handed a proxy instead of the real service —
        // and gets caching for free.
        var real = new RealPriceService();
        IPriceService service = new CachingPriceProxy(real);

        decimal[] quotes = new LegacyPriceClient().Quotes(service, "AAPL", "AAPL", "MSFT");

        Assert.Equal(new[] { 150m, 150m, 400m }, quotes);
        Assert.Equal(2, real.CallCount); // the duplicate AAPL lookup was served from cache
    }
}
