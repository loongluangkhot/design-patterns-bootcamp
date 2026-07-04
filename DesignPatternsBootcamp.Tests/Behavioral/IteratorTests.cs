using DesignPatternsBootcamp.Behavioral.Iterator;
using DesignPatternsBootcamp.Behavioral.Iterator.Legacy;

namespace DesignPatternsBootcamp.Tests.Behavioral;

public class IteratorTests
{
    // -----------------------------------------------------------------------------------------
    //  Legacy baseline — already PASSING. The internal list is public; callers rummage through it.
    // -----------------------------------------------------------------------------------------

    [Fact]
    public void Legacy_book_leaks_its_internal_list_and_makes_callers_filter()
    {
        var book = new LegacyBook();
        book.Positions.Add(new Position("AAPL", 10));
        book.Positions.Add(new Position("MSFT", -5));

        var longs = book.Positions.Where(p => p.Quantity > 0).ToList(); // filter re-written by caller

        Assert.Single(longs);
    }

    // -----------------------------------------------------------------------------------------
    //  Your refactor — RED until GetEnumerator and LongPositions yield.
    // -----------------------------------------------------------------------------------------

    [Fact]
    public void Foreach_walks_every_position_without_touching_storage()
    {
        var book = new Book();
        book.Add(new Position("AAPL", 10));
        book.Add(new Position("MSFT", -5));

        var symbols = new List<string>();
        foreach (Position p in book)
            symbols.Add(p.Symbol);

        Assert.Equal(new[] { "AAPL", "MSFT" }, symbols);
    }

    [Fact]
    public void The_book_is_enumerable_so_linq_just_works()
    {
        var book = new Book();
        book.Add(new Position("AAPL", 10));
        book.Add(new Position("MSFT", -5));

        Assert.Equal(2, book.Count()); // LINQ over IEnumerable<Position>
    }

    [Fact]
    public void The_long_only_iterator_yields_just_the_longs()
    {
        var book = new Book();
        book.Add(new Position("AAPL", 10));
        book.Add(new Position("MSFT", -5));
        book.Add(new Position("GOOG", 3));

        var longs = book.LongPositions().Select(p => p.Symbol).ToList();

        Assert.Equal(new[] { "AAPL", "GOOG" }, longs);
    }

    [Fact]
    public void Callers_can_only_traverse_the_book_not_index_into_it()
    {
        var book = new Book();
        book.Add(new Position("AAPL", 10));

        Assert.Single(book); // via IEnumerable — there is no public list to reach into
    }
}
