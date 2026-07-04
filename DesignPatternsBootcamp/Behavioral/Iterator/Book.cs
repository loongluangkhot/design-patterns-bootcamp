using System.Collections;

namespace DesignPatternsBootcamp.Behavioral.Iterator;

/// <summary>A single position. Positive quantity = long, negative = short.</summary>
public record Position(string Symbol, int Quantity);

/// <summary>
/// The <b>Aggregate</b>: a book of positions that lets callers <b>traverse</b> it without ever
/// seeing how it stores things. C# bakes the Iterator pattern into the language: implement
/// <see cref="IEnumerable{T}"/> and the <c>yield</c> keyword builds the iterator (the state machine)
/// for you. The internal list stays private.
/// </summary>
public sealed class Book : IEnumerable<Position>
{
    private readonly List<Position> _positions = new();

    public void Add(Position position) => _positions.Add(position);

    /// <remarks>
    /// TODO(student): yield each position in order. The idiomatic way is a <c>foreach</c> over
    /// <c>_positions</c> with <c>yield return</c> — the compiler turns that into the iterator object.
    /// </remarks>
    public IEnumerator<Position> GetEnumerator() =>
        throw new NotImplementedException("TODO(student): yield return each position in _positions.");

    // Non-generic version required by IEnumerable — just defer to the generic one. (Provided.)
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    /// <summary>A second, custom traversal: only the long positions.</summary>
    /// <remarks>
    /// TODO(student): yield only positions whose Quantity &gt; 0 (again with <c>yield return</c>).
    /// </remarks>
    public IEnumerable<Position> LongPositions() =>
        throw new NotImplementedException("TODO(student): yield return each position with Quantity > 0.");
}
