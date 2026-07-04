using System.Collections.Concurrent;
using DesignPatternsBootcamp.Creational.Singleton;
using DesignPatternsBootcamp.Creational.Singleton.Legacy;

namespace DesignPatternsBootcamp.Tests.Creational;

public class SingletonTests
{
    // -----------------------------------------------------------------------------------------
    //  Legacy baseline — already PASSING. It documents the "one expensive connection per caller"
    //  waste we want to eliminate.
    // -----------------------------------------------------------------------------------------

    [Fact]
    public void Legacy_client_opens_a_new_expensive_connection_every_time()
    {
        int before = LegacyMarketDataClient.InstancesCreated;

        var a = new LegacyMarketDataClient();
        var b = new LegacyMarketDataClient();

        Assert.NotEqual(a.SessionId, b.SessionId);                       // two different sessions
        Assert.Equal(before + 2, LegacyMarketDataClient.InstancesCreated); // both were constructed
    }

    // -----------------------------------------------------------------------------------------
    //  Your refactor — RED until Instance returns a single, lazily-created, thread-safe instance.
    // -----------------------------------------------------------------------------------------

    [Fact]
    public void Instance_is_the_same_object_every_time()
    {
        Assert.Same(MarketDataConnection.Instance, MarketDataConnection.Instance);
    }

    [Fact]
    public void The_expensive_constructor_runs_only_once()
    {
        _ = MarketDataConnection.Instance;
        _ = MarketDataConnection.Instance;
        _ = MarketDataConnection.Instance;

        Assert.Equal(1, MarketDataConnection.ConstructionCount);
    }

    [Fact]
    public void Concurrent_first_access_still_constructs_exactly_one()
    {
        var seen = new ConcurrentBag<MarketDataConnection>();

        Parallel.For(0, 256, _ => seen.Add(MarketDataConnection.Instance));

        Assert.Single(seen.Distinct());                       // everyone got the same object
        Assert.Equal(1, MarketDataConnection.ConstructionCount); // even under a thundering herd
    }
}
