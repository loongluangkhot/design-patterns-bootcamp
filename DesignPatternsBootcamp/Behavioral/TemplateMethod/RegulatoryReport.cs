using System.Text;

namespace DesignPatternsBootcamp.Behavioral.TemplateMethod;

/// <summary>A trade to appear on a report.</summary>
public record Trade(string Symbol, int Quantity, decimal Price);

/// <summary>
/// The <b>Abstract Class</b>. <see cref="Generate"/> is the <b>template method</b>: it fixes the
/// skeleton every report follows (title, then a formatted row per trade, then a summary) and calls
/// out to steps that subclasses supply. It is deliberately not overridable — the shape is the same
/// for everyone; only the steps differ.
/// </summary>
public abstract class RegulatoryReport
{
    protected IReadOnlyList<Trade> Trades { get; }

    protected RegulatoryReport(IReadOnlyList<Trade> trades) => Trades = trades;

    /// <summary>The template method — the invariant skeleton. Don't override this.</summary>
    public string Generate()
    {
        var sb = new StringBuilder();
        sb.AppendLine(Title());                       // step 1
        foreach (Trade trade in Trades)
            sb.AppendLine(FormatTrade(trade));        // step 2 (per row)
        sb.Append(Summary());                         // step 3 (hook — has a default)
        return sb.ToString();
    }

    /// <summary>Primitive operation: the report's title line.</summary>
    protected abstract string Title();

    /// <summary>Primitive operation: how one trade is rendered.</summary>
    protected abstract string FormatTrade(Trade trade);

    /// <summary>Hook: a default footer subclasses may override.</summary>
    protected virtual string Summary() => $"Total trades: {Trades.Count}";
}

// ---- Concrete Classes. Fill in the steps; the skeleton comes from the base. ------------------

public sealed class MifidReport : RegulatoryReport
{
    public MifidReport(IReadOnlyList<Trade> trades) : base(trades) { }

    protected override string Title() =>
        throw new NotImplementedException("TODO(student): return \"MiFID II Transaction Report\".");

    protected override string FormatTrade(Trade trade) =>
        throw new NotImplementedException("TODO(student): CSV row: $\"{Symbol},{Quantity},{Price:0.00},EUR\".");

    // Uses the default Summary hook.
}

public sealed class FinraReport : RegulatoryReport
{
    public FinraReport(IReadOnlyList<Trade> trades) : base(trades) { }

    protected override string Title() =>
        throw new NotImplementedException("TODO(student): return \"FINRA OATS Report\".");

    protected override string FormatTrade(Trade trade) =>
        throw new NotImplementedException("TODO(student): pipe row: $\"{Symbol}|{Quantity}|{Price:0.00}|USD\".");

    protected override string Summary() =>
        throw new NotImplementedException("TODO(student): override the footer: $\"{Trades.Count} trades reported to FINRA\".");
}
