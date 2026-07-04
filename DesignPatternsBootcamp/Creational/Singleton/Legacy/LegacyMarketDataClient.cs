namespace DesignPatternsBootcamp.Creational.Singleton.Legacy;

/// <summary>
/// THE "BEFORE" CODE — a market-data client that anyone can <c>new</c> up. Because the constructor
/// is public, every module that needs data opens its <b>own</b> expensive connection.
///
/// The counter proves the waste: two callers, two sessions, two authentications, two subscription
/// sets — and now the app has two sources of "truth" that can drift apart. The test
/// <c>Legacy_client_opens_a_new_expensive_connection_every_time</c> documents this.
///
/// Singleton removes the public constructor so there can be only one.
/// </summary>
public sealed class LegacyMarketDataClient
{
    public static int InstancesCreated { get; private set; }
    public string SessionId { get; }

    public LegacyMarketDataClient()
    {
        InstancesCreated++;
        SessionId = $"legacy-session-{InstancesCreated}";
        // Imagine expensive work here, repeated needlessly for every caller.
    }
}
