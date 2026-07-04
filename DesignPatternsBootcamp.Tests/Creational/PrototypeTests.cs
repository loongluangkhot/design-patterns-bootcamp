using DesignPatternsBootcamp.Creational.Prototype;
using DesignPatternsBootcamp.Creational.Prototype.Legacy;

namespace DesignPatternsBootcamp.Tests.Creational;

public class PrototypeTests
{
    private static ModelBasket SampleTemplate() => new()
    {
        Name = "Balanced Model",
        Strategy = "60/40",
        Lines =
        [
            new OrderLine { Symbol = "VTI", Quantity = 60 },
            new OrderLine { Symbol = "BND", Quantity = 40 },
        ],
    };

    // -----------------------------------------------------------------------------------------
    //  Legacy baseline — already PASSING. It pins the shallow-copy BUG we are here to remove.
    // -----------------------------------------------------------------------------------------

    [Fact]
    public void Legacy_shallow_copy_leaks_mutations_back_to_the_template()
    {
        ModelBasket template = SampleTemplate();
        ModelBasket copy = new LegacyBasketCloner().Copy(template);

        copy.Lines[0].Quantity = 999; // tweak only the "copy"...

        Assert.Equal(999, template.Lines[0].Quantity); // ...and the master template is corrupted.
    }

    // -----------------------------------------------------------------------------------------
    //  Your refactor — RED until DeepClone is implemented for OrderLine and ModelBasket.
    // -----------------------------------------------------------------------------------------

    [Fact]
    public void Cloning_copies_every_field_value()
    {
        ModelBasket clone = SampleTemplate().DeepClone();

        Assert.Equal("Balanced Model", clone.Name);
        Assert.Equal("60/40", clone.Strategy);
        Assert.Equal(2, clone.Lines.Count);
        Assert.Equal("VTI", clone.Lines[0].Symbol);
        Assert.Equal(60, clone.Lines[0].Quantity);
    }

    [Fact]
    public void Cloning_produces_distinct_instances_all_the_way_down()
    {
        ModelBasket template = SampleTemplate();
        ModelBasket clone = template.DeepClone();

        Assert.NotSame(template, clone);
        Assert.NotSame(template.Lines, clone.Lines);       // a new list...
        Assert.NotSame(template.Lines[0], clone.Lines[0]); // ...of new order lines.
    }

    [Fact]
    public void Mutating_the_clone_never_touches_the_original()
    {
        ModelBasket template = SampleTemplate();
        ModelBasket clone = template.DeepClone();

        clone.Name = "Client A tweak";
        clone.Lines[0].Quantity = 999;
        clone.Lines.Add(new OrderLine { Symbol = "GLD", Quantity = 10 });

        Assert.Equal("Balanced Model", template.Name);
        Assert.Equal(60, template.Lines[0].Quantity);
        Assert.Equal(2, template.Lines.Count);
    }

    [Fact]
    public void Order_line_deep_clone_is_independent()
    {
        var line = new OrderLine { Symbol = "VTI", Quantity = 60 };
        OrderLine copy = line.DeepClone();

        copy.Quantity = 5;

        Assert.NotSame(line, copy);
        Assert.Equal(60, line.Quantity);
    }

    [Fact]
    public void Library_hands_out_independent_baskets_from_a_single_prototype()
    {
        var library = new BasketLibrary();
        library.Register("balanced", SampleTemplate());

        ModelBasket a = library.CreateFrom("balanced");
        ModelBasket b = library.CreateFrom("balanced");
        a.Lines[0].Quantity = 1;

        Assert.NotSame(a, b);
        Assert.Equal(60, b.Lines[0].Quantity); // b is untouched by edits to a
    }
}
