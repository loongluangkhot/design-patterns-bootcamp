namespace DesignPatternsBootcamp.Behavioral.State.Legacy;

/// <summary>
/// THE "BEFORE" CODE — the order tracks a status string and every operation re-checks it with
/// <c>if</c>s to decide what's allowed and where to go next.
///
/// Smells: the transition rules are duplicated across <see cref="Fill"/> and <see cref="Cancel"/>;
/// the state machine exists only implicitly, smeared across those guards; and adding a state (say,
/// "PendingCancel") means editing every method's checks. The State pattern makes each state a class
/// that owns its own transitions, so the machine is explicit and each rule lives in exactly one place.
/// </summary>
public sealed class LegacyOrder
{
    public int Quantity { get; }
    public int FilledQuantity { get; private set; }
    public string Status { get; private set; } = "New";

    public LegacyOrder(int quantity) => Quantity = quantity;

    public void Fill(int quantity)
    {
        if (Status is "Filled" or "Cancelled")
            throw new InvalidOperationException($"Cannot fill a {Status} order.");

        FilledQuantity += quantity;
        Status = FilledQuantity >= Quantity ? "Filled" : "PartiallyFilled";
    }

    public void Cancel()
    {
        if (Status is "Filled" or "Cancelled")
            throw new InvalidOperationException($"Cannot cancel a {Status} order.");

        Status = "Cancelled";
    }
}
