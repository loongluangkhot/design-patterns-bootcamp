namespace DesignPatternsBootcamp.Behavioral.Mediator.Legacy;

/// <summary>
/// THE "BEFORE" CODE — order entry knows about, and directly drives, every other part of the desk in
/// sequence. Risk, execution, and (imagine) notification, settlement, compliance, and P&amp;L all get
/// wired together by hand, and often straight into each other.
///
/// With a handful of parts this is fine; as they multiply it becomes an N×N web where everything
/// references everything, and one flow change ripples everywhere. The Mediator pattern replaces that
/// web with a hub: each colleague talks only to the mediator, and the mediator owns the choreography.
/// </summary>
public sealed class LegacyOrderEntry
{
    public string Submit(Order order, decimal riskLimit)
    {
        if (order.Notional > riskLimit)
            return "REJECTED: Exceeds risk limit.";

        string executionRef = $"EXE-{order.Id}";
        // imagine executionRef then hand-wired directly into notification, settlement, ledger, ...
        return $"EXECUTED: {executionRef}";
    }
}
