using DesignPatternsBootcamp.Behavioral.Command;
using DesignPatternsBootcamp.Behavioral.Command.Legacy;

namespace DesignPatternsBootcamp.Tests.Behavioral;

public class CommandTests
{
    // -----------------------------------------------------------------------------------------
    //  Legacy baseline — already PASSING. It mutates directly and offers no way to undo.
    // -----------------------------------------------------------------------------------------

    [Fact]
    public void Legacy_blotter_mutates_directly_with_no_undo()
    {
        var blotter = new LegacyBlotter();
        blotter.AddOrder(new Order("O1", "AAPL", 10));
        blotter.AddOrder(new Order("O2", "MSFT", 5));

        blotter.CancelOrder("O1"); // gone for good — nothing remembers how to bring it back

        Assert.Equal(1, blotter.Count);
    }

    // -----------------------------------------------------------------------------------------
    //  Your refactor — RED until the three commands implement Execute/Undo.
    // -----------------------------------------------------------------------------------------

    [Fact]
    public void Add_command_adds_the_order_and_undo_removes_it()
    {
        var blotter = new Blotter();
        var history = new BlotterHistory();

        history.Do(new AddOrderCommand(blotter, new Order("O1", "AAPL", 10)));
        Assert.Equal(1, blotter.Count);

        history.Undo();
        Assert.Equal(0, blotter.Count);
    }

    [Fact]
    public void Cancel_command_removes_the_order_and_undo_restores_it_intact()
    {
        var blotter = new Blotter();
        blotter.Add(new Order("O1", "AAPL", 10));
        var history = new BlotterHistory();

        history.Do(new CancelOrderCommand(blotter, "O1"));
        Assert.Equal(0, blotter.Count);

        history.Undo();
        Assert.Equal(1, blotter.Count);
        Assert.Equal(10, blotter.Find("O1")!.Quantity); // restored with its details
    }

    [Fact]
    public void Amend_command_changes_quantity_and_undo_restores_the_old_value()
    {
        var blotter = new Blotter();
        blotter.Add(new Order("O1", "AAPL", 10));
        var history = new BlotterHistory();

        history.Do(new AmendQuantityCommand(blotter, "O1", 25));
        Assert.Equal(25, blotter.Find("O1")!.Quantity);

        history.Undo();
        Assert.Equal(10, blotter.Find("O1")!.Quantity);
    }

    [Fact]
    public void Redo_reapplies_an_undone_command()
    {
        var blotter = new Blotter();
        var history = new BlotterHistory();

        history.Do(new AddOrderCommand(blotter, new Order("O1", "AAPL", 10)));
        history.Undo();
        Assert.Equal(0, blotter.Count);

        history.Redo();
        Assert.Equal(1, blotter.Count);
    }

    [Fact]
    public void Undo_walks_multiple_actions_back_in_order()
    {
        var blotter = new Blotter();
        var history = new BlotterHistory();

        history.Do(new AddOrderCommand(blotter, new Order("O1", "AAPL", 10)));
        history.Do(new AddOrderCommand(blotter, new Order("O2", "MSFT", 5)));
        history.Do(new AmendQuantityCommand(blotter, "O1", 30));

        Assert.Equal(2, blotter.Count);
        Assert.Equal(30, blotter.Find("O1")!.Quantity);

        history.Undo(); // undo the amend
        Assert.Equal(10, blotter.Find("O1")!.Quantity);

        history.Undo(); // undo adding O2
        Assert.Equal(1, blotter.Count);

        history.Undo(); // undo adding O1
        Assert.Equal(0, blotter.Count);
    }
}
