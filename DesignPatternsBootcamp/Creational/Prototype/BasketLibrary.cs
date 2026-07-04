namespace DesignPatternsBootcamp.Creational.Prototype;

/// <summary>
/// A <b>prototype registry</b> (provided complete). This is the classic way Prototype is used:
/// keep a library of ready-made prototypes and hand out independent copies on demand — no need to
/// know or reconstruct how each template was originally assembled.
///
/// It leans entirely on <see cref="ModelBasket.DeepClone"/>, so it starts working the moment you
/// implement the clone.
/// </summary>
public sealed class BasketLibrary
{
    private readonly Dictionary<string, ModelBasket> _prototypes = new();

    public void Register(string key, ModelBasket prototype) => _prototypes[key] = prototype;

    /// <summary>Return a fresh, independent basket built from the named prototype.</summary>
    public ModelBasket CreateFrom(string key) => _prototypes[key].DeepClone();
}
