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
using DesignPatternsBootcamp.Structural.Facade;
using DesignPatternsBootcamp.Structural.Facade.Legacy;

namespace DesignPatternsBootcamp.Tests.Structural;

public class FacadeTests
{
    private static (SettlementFacade facade, ClearingHouse clearing, Ledger ledger) BuildFacade(decimal balance = 10_000m)
    {
        var funding = new FundingService(new Dictionary<string, decimal> { ["ACC-1"] = balance });
        var clearing = new ClearingHouse();
        var ledger = new Ledger();
        var facade = new SettlementFacade(new TradeValidator(), funding, clearing, ledger);
        return (facade, clearing, ledger);
    }

    private static Trade ValidTrade() => new("T1", "ACC-1", "AAPL", 10, 150m); // notional 1,500

    // -----------------------------------------------------------------------------------------
    //  Legacy baseline — already PASSING. The caller wires all four subsystems by hand.
    // -----------------------------------------------------------------------------------------

    [Fact]
    public void Legacy_client_settles_by_wiring_the_subsystem_itself()
    {
        var funding = new FundingService(new Dictionary<string, decimal> { ["ACC-1"] = 10_000m });
        var ledger = new Ledger();

        string result = new LegacyDeskClient()
            .Settle(ValidTrade(), new TradeValidator(), funding, new ClearingHouse(), ledger);

        Assert.StartsWith("SETTLED: CLR-T1", result);
        Assert.Single(ledger.Entries);
    }

    // -----------------------------------------------------------------------------------------
    //  Your refactor — RED until Settle() choreographs the subsystem.
    // -----------------------------------------------------------------------------------------

    [Fact]
    public void One_call_settles_a_valid_funded_trade()
    {
        var (facade, clearing, ledger) = BuildFacade();

        SettlementResult result = facade.Settle(ValidTrade());

        Assert.True(result.Settled);
        Assert.Equal("CLR-T1", result.ClearingReference);
        Assert.Equal(1, clearing.SubmissionCount);
        Assert.Single(ledger.Entries);
    }

    [Fact]
    public void An_invalid_trade_is_rejected_before_clearing_or_ledger()
    {
        var (facade, clearing, ledger) = BuildFacade();

        SettlementResult result = facade.Settle(ValidTrade() with { Quantity = 0 });

        Assert.False(result.Settled);
        Assert.Null(result.ClearingReference);
        Assert.Equal(0, clearing.SubmissionCount); // short-circuited — clearing never called
        Assert.Empty(ledger.Entries);
    }

    [Fact]
    public void An_underfunded_trade_is_rejected_before_clearing_or_ledger()
    {
        var (facade, clearing, ledger) = BuildFacade(balance: 100m); // < 1,500 notional

        SettlementResult result = facade.Settle(ValidTrade());

        Assert.False(result.Settled);
        Assert.Equal("Insufficient funds.", result.Message);
        Assert.Equal(0, clearing.SubmissionCount);
        Assert.Empty(ledger.Entries);
    }
}
*/
