using System.Text;

namespace DesignPatternsBootcamp.Behavioral.TemplateMethod.Legacy;

/// <summary>
/// THE "BEFORE" CODE — each report type re-implements the <b>whole</b> skeleton, copy-pasted with a
/// few tweaks. The "title, then a row per trade, then a summary" shape appears in every method.
///
/// Smells: the skeleton is duplicated per report; a change to it (say, add a generation timestamp
/// line, or blank-line separators) must be made in every copy, and the copies drift apart. Template
/// Method writes the skeleton once in a base class and lets subclasses fill in only the steps.
/// </summary>
public sealed class LegacyReportGenerator
{
    public string GenerateMifid(IReadOnlyList<Trade> trades)
    {
        var sb = new StringBuilder();
        sb.AppendLine("MiFID II Transaction Report");
        foreach (Trade t in trades)
            sb.AppendLine($"{t.Symbol},{t.Quantity},{t.Price:0.00},EUR");
        sb.Append($"Total trades: {trades.Count}");
        return sb.ToString();
    }

    public string GenerateFinra(IReadOnlyList<Trade> trades)
    {
        var sb = new StringBuilder();
        sb.AppendLine("FINRA OATS Report");                 // same skeleton, copied
        foreach (Trade t in trades)
            sb.AppendLine($"{t.Symbol}|{t.Quantity}|{t.Price:0.00}|USD");
        sb.Append($"{trades.Count} trades reported to FINRA");
        return sb.ToString();
    }
}
