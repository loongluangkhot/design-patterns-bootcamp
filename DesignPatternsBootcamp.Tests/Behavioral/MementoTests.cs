using DesignPatternsBootcamp.Behavioral.Memento;
using DesignPatternsBootcamp.Behavioral.Memento.Legacy;

namespace DesignPatternsBootcamp.Tests.Behavioral;

public class MementoTests
{
    // -----------------------------------------------------------------------------------------
    //  Legacy baseline — already PASSING. Snapshots are done by copying every field externally.
    // -----------------------------------------------------------------------------------------

    [Fact]
    public void Legacy_snapshots_are_taken_by_copying_fields_externally()
    {
        var ticket = new LegacyTicket("AAPL", 10, 150m);

        // The caller must know and copy every field to snapshot...
        string savedSymbol = ticket.Symbol;
        int savedQuantity = ticket.Quantity;
        decimal savedPrice = ticket.Price;

        ticket.Quantity = 99;
        ticket.Price = 1m;

        // ...and copy each one back to restore.
        ticket.Symbol = savedSymbol;
        ticket.Quantity = savedQuantity;
        ticket.Price = savedPrice;

        Assert.Equal(10, ticket.Quantity);
        Assert.Equal(150m, ticket.Price);
    }

    // -----------------------------------------------------------------------------------------
    //  Your refactor — RED until Save/Restore are implemented.
    // -----------------------------------------------------------------------------------------

    [Fact]
    public void Save_then_restore_rolls_the_ticket_back()
    {
        var ticket = new OrderTicket("AAPL", 10, 150m);

        OrderTicket.Memento snapshot = ticket.Save();
        ticket.Symbol = "MSFT";
        ticket.Quantity = 99;
        ticket.Price = 1m;

        ticket.Restore(snapshot);

        Assert.Equal("AAPL", ticket.Symbol);
        Assert.Equal(10, ticket.Quantity);
        Assert.Equal(150m, ticket.Price);
    }

    [Fact]
    public void The_caretaker_supports_multi_level_undo()
    {
        var ticket = new OrderTicket("AAPL", 10, 150m);
        var history = new TicketHistory();

        history.Save(ticket);   // checkpoint: quantity 10
        ticket.Quantity = 20;
        history.Save(ticket);   // checkpoint: quantity 20
        ticket.Quantity = 30;

        Assert.Equal(2, history.Count);

        history.Undo(ticket);   // back to 20
        Assert.Equal(20, ticket.Quantity);

        history.Undo(ticket);   // back to 10
        Assert.Equal(10, ticket.Quantity);
    }

    [Fact]
    public void Restore_works_through_the_caretaker_which_never_reads_the_memento()
    {
        var ticket = new OrderTicket("AAPL", 10, 150m);
        var history = new TicketHistory();

        history.Save(ticket);
        ticket.Symbol = "TSLA";
        history.Undo(ticket);

        Assert.Equal("AAPL", ticket.Symbol);
        // Note: this test can't read snapshot.Symbol either — the Memento's state is internal to the
        // originator's assembly. That opacity is exactly the guarantee Memento provides.
    }
}
