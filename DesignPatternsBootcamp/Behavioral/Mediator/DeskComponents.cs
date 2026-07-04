namespace DesignPatternsBootcamp.Behavioral.Mediator;

/// <summary>An order to process on the desk.</summary>
public record Order(string Id, string Symbol, int Quantity, decimal Price)
{
    public decimal Notional => Quantity * Price;
}

/// <summary>The result of processing an order through the desk.</summary>
public record OrderOutcome(bool Executed, string? ExecutionRef, string Message);

// =============================================================================================
//  THE COLLEAGUES (provided). Each does ONE job and — crucially — knows nothing about the others.
//  They talk only to the mediator. Add settlement, compliance, P&L later and they stay just as
//  isolated; only the mediator learns about them.
// =============================================================================================

public sealed class RiskComponent
{
    private readonly decimal _limit;

    public RiskComponent(decimal limit) => _limit = limit;

    public bool Approve(Order order, out string reason)
    {
        if (order.Notional > _limit)
        {
            reason = "Exceeds risk limit.";
            return false;
        }
        reason = "";
        return true;
    }
}

public sealed class ExecutionComponent
{
    public int Executions { get; private set; }

    public string Execute(Order order)
    {
        Executions++;
        return $"EXE-{order.Id}";
    }
}

public sealed class NotificationComponent
{
    private readonly List<string> _sent = new();

    public IReadOnlyList<string> Sent => _sent;

    public void Notify(string message) => _sent.Add(message);
}
