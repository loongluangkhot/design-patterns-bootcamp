namespace DesignPatternsBootcamp.Behavioral.Iterator.Legacy;

/// <summary>
/// THE "BEFORE" CODE — the book just exposes its internal <see cref="List{T}"/> and lets callers
/// rummage through it.
///
/// Smells: the storage type has leaked into every caller (change it to an array, a database cursor,
/// or a live feed and they all break); and any "special" traversal — long-only, sorted, top-N — is
/// re-written at each call site. The Iterator pattern hands out a way to <i>walk</i> the collection
/// so callers never touch, or depend on, the internal representation.
/// </summary>
public sealed class LegacyBook
{
    public List<Position> Positions { get; } = new();
}
