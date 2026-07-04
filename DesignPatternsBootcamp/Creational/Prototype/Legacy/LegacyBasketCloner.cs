namespace DesignPatternsBootcamp.Creational.Prototype.Legacy;

/// <summary>
/// THE "BEFORE" CODE — the desk tried to "copy" a template so each client could get a tweaked
/// version. But this copy is <b>shallow</b>: it copies the scalar fields and then reuses the very
/// same <c>Lines</c> list (and the same <c>OrderLine</c> objects inside it).
///
/// The result is a landmine: editing a client's "copy" reaches back and mutates the master
/// template — and every other client cloned from it. The test
/// <c>Legacy_shallow_copy_leaks_mutations_back_to_the_template</c> documents the bug on purpose.
///
/// Prototype fixes this by giving each object a <c>DeepClone</c> that copies all the way down.
/// </summary>
public sealed class LegacyBasketCloner
{
    public ModelBasket Copy(ModelBasket basket) => new()
    {
        Name = basket.Name,
        Strategy = basket.Strategy,
        Lines = basket.Lines, // BUG: shares the list reference and every OrderLine in it.
    };
}
