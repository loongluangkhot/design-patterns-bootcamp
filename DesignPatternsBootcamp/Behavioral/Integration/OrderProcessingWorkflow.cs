using Cor = DesignPatternsBootcamp.Behavioral.ChainOfResponsibility;
using Cmd = DesignPatternsBootcamp.Behavioral.Command;

namespace DesignPatternsBootcamp.Behavioral.Integration;

/// <summary>The result of pushing an order through the workflow.</summary>
public record PlacementResult(bool Placed, string? Reason);

/// <summary>
/// WEEK 3 CAPSTONE — an order-processing workflow that composes two behavioral patterns you built:
///
///   • <b>Chain of Responsibility</b> validates the order (Day 1a).
///   • <b>Command</b> places it as an <i>undoable</i> action on the blotter (Day 1b).
///
/// Complete <b>Day 1a</b> and <b>Day 1b</b> first — this workflow calls into both. It builds each
/// subsystem's own <c>Order</c> type from primitives, so nothing awkward crosses between them.
/// </summary>
public sealed class OrderProcessingWorkflow
{
    private readonly Cor.OrderValidator _validationChain;
    private readonly Cmd.Blotter _blotter;
    private readonly Cmd.BlotterHistory _history;

    public OrderProcessingWorkflow(Cor.OrderValidator validationChain, Cmd.Blotter blotter, Cmd.BlotterHistory history)
    {
        _validationChain = validationChain;
        _blotter = blotter;
        _history = history;
    }

    /// <remarks>
    /// TODO(student): wire the two patterns together:
    ///   1. Validate: run <c>_validationChain.Validate(new Cor.Order(symbol, quantity, price))</c>.
    ///      If not approved, return <c>new PlacementResult(false, result.Reason)</c> — do NOT place it.
    ///   2. Place: run an <c>AddOrderCommand</c> through the history so it's undoable:
    ///      <c>_history.Do(new Cmd.AddOrderCommand(_blotter, new Cmd.Order(id, symbol, quantity)))</c>,
    ///      then return <c>new PlacementResult(true, null)</c>.
    /// </remarks>
    public PlacementResult Place(string id, string symbol, int quantity, decimal price) =>
        throw new NotImplementedException(
            "TODO(student): validate via the chain, then place via an undoable command. See <remarks>.");
}
