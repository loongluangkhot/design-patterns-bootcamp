namespace DesignPatternsBootcamp.Creational.Singleton;

/// <summary>
/// A market-data feed connection. Opening one is <b>expensive</b> (socket, authentication,
/// subscriptions) and the whole app should share <b>exactly one</b>. That is the Singleton's job:
/// guarantee a single instance and provide one global access point to it.
///
/// The private constructor and the construction counter are provided. <b>Your task is to expose
/// the single instance</b> via <see cref="Instance"/>, created lazily and thread-safely.
/// </summary>
public sealed class MarketDataConnection
{
    /// <summary>How many times the (expensive) constructor has actually run. Must stay at 1.</summary>
    public static int ConstructionCount { get; private set; }

    /// <summary>Identifies this physical session — proves two references are the same connection.</summary>
    public string SessionId { get; }

    // Private: nobody outside this class may call `new MarketDataConnection()`. The only way to get
    // one is through Instance. This is what makes "exactly one" enforceable.
    private MarketDataConnection()
    {
        ConstructionCount++;
        SessionId = $"session-{ConstructionCount}";
        // Imagine expensive work here: open socket, authenticate, subscribe to symbols…
    }

    /// <summary>
    /// The single, shared instance.
    /// </summary>
    /// <remarks>
    /// TODO(student): replace this stub so that:
    ///   • the instance is created only on first access (lazy), and
    ///   • concurrent first-access from many threads still constructs it only ONCE (thread-safe).
    /// The idiomatic C# way is a <c>static readonly Lazy&lt;MarketDataConnection&gt;</c> field whose
    /// value you return here. (A static-field initializer or double-checked locking also work — see
    /// the README.)
    /// </remarks>
    public static MarketDataConnection Instance =>
        throw new NotImplementedException(
            "TODO(student): return the single shared instance, created lazily and thread-safely.");
}
