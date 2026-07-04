using DesignPatternsBootcamp.Behavioral.TemplateMethod;
using DesignPatternsBootcamp.Behavioral.TemplateMethod.Legacy;

namespace DesignPatternsBootcamp.Tests.Behavioral;

public class TemplateMethodTests
{
    // -----------------------------------------------------------------------------------------
    //  Legacy baseline — already PASSING. Each report copy-pastes the whole skeleton.
    // -----------------------------------------------------------------------------------------

    [Fact]
    public void Legacy_generator_duplicates_the_skeleton_per_report()
    {
        var output = new LegacyReportGenerator().GenerateMifid(new[] { new Trade("AAPL", 10, 150m) });

        Assert.StartsWith("MiFID II Transaction Report", output);
        Assert.Contains("AAPL,10,150.00,EUR", output);
    }

    // -----------------------------------------------------------------------------------------
    //  Your refactor — RED until the report subclasses fill in their steps.
    // -----------------------------------------------------------------------------------------

    [Fact]
    public void Mifid_report_supplies_its_title_and_row_format()
    {
        var output = new MifidReport(new[] { new Trade("AAPL", 10, 150m) }).Generate();

        Assert.StartsWith("MiFID II Transaction Report", output);
        Assert.Contains("AAPL,10,150.00,EUR", output);
        Assert.Contains("Total trades: 1", output); // the default Summary hook
    }

    [Fact]
    public void Finra_report_overrides_the_row_format_and_the_summary_hook()
    {
        var output = new FinraReport(new[] { new Trade("MSFT", 5, 400m) }).Generate();

        Assert.StartsWith("FINRA OATS Report", output);
        Assert.Contains("MSFT|5|400.00|USD", output);
        Assert.Contains("1 trades reported to FINRA", output);
    }

    [Fact]
    public void Both_reports_follow_the_same_skeleton_order()
    {
        // title, then rows, then summary — enforced by the (shared) template method for every report.
        string output = new MifidReport(new[] { new Trade("AAPL", 10, 150m) }).Generate();

        int titleIndex = output.IndexOf("MiFID", StringComparison.Ordinal);
        int rowIndex = output.IndexOf("AAPL", StringComparison.Ordinal);
        int summaryIndex = output.IndexOf("Total trades", StringComparison.Ordinal);

        Assert.True(titleIndex >= 0 && titleIndex < rowIndex && rowIndex < summaryIndex);
    }
}
