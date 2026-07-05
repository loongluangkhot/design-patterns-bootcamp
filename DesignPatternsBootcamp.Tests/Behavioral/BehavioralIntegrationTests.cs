// ============================================================================
//  STEP 1 OF THIS KATA — UNCOMMENT THESE TESTS.
//  They ship commented out so the rest of the test project still builds before
//  you start this kata. To begin: delete the "/*" on the line below AND the
//  "*/" on the very last line, then run this kata's tests (its README has the
//  filter + the target API). It will NOT compile at first — that is expected:
//  each "type or namespace ... could not be found" error is a type you must
//  create yourself. Build until it compiles, then turns green.
// ============================================================================
/*
using DesignPatternsBootcamp.Behavioral.Integration;
using Cor = DesignPatternsBootcamp.Behavioral.ChainOfResponsibility;
using Cmd = DesignPatternsBootcamp.Behavioral.Command;

namespace DesignPatternsBootcamp.Tests.Behavioral;

/// <summary>
/// Week 3 capstone. Requires Day 1a (Chain of Responsibility) AND Day 1b (Command) implemented,
/// plus OrderProcessingWorkflow.Place.
/// </summary>
public class BehavioralIntegrationTests
{
    private static OrderProcessingWorkflow BuildWorkflow(out Cmd.Blotter blotter, out Cmd.BlotterHistory history)
    {
        var chain = new Cor.PositiveQuantityValidator();
        chain.SetNext(new Cor.NotionalLimitValidator(1_000_000m))
             .SetNext(new Cor.RestrictedSymbolValidator("XYZ"));

        blotter = new Cmd.Blotter();
        history = new Cmd.BlotterHistory();
        return new OrderProcessingWorkflow(chain, blotter, history);
    }

    [Fact]
    public void A_valid_order_is_validated_then_placed_on_the_blotter()
    {
        OrderProcessingWorkflow workflow = BuildWorkflow(out Cmd.Blotter blotter, out _);

        PlacementResult result = workflow.Place("O1", "AAPL", 10, 150m);

        Assert.True(result.Placed);
        Assert.Equal(1, blotter.Count);
        Assert.Equal("AAPL", blotter.Find("O1")!.Symbol);
    }

    [Fact]
    public void A_rejected_order_never_reaches_the_blotter()
    {
        OrderProcessingWorkflow workflow = BuildWorkflow(out Cmd.Blotter blotter, out _);

        PlacementResult result = workflow.Place("O2", "XYZ", 10, 150m); // restricted symbol

        Assert.False(result.Placed);
        Assert.Equal("Symbol XYZ is restricted.", result.Reason);
        Assert.Equal(0, blotter.Count); // the chain short-circuited before any command ran
    }

    [Fact]
    public void A_placement_is_undoable_via_the_command_history()
    {
        OrderProcessingWorkflow workflow = BuildWorkflow(out Cmd.Blotter blotter, out Cmd.BlotterHistory history);

        workflow.Place("O1", "AAPL", 10, 150m);
        Assert.Equal(1, blotter.Count);

        history.Undo(); // Chain approved it; Command lets us take it back
        Assert.Equal(0, blotter.Count);
    }
}
*/
