namespace DesignPatternsBootcamp.Creational.Prototype;

/// <summary>
/// A single line in a model basket. It is <b>mutable</b> on purpose — the whole point of the desk's
/// workflow is to clone a template and then tweak quantities per client.
/// </summary>
public sealed class OrderLine : IPrototype<OrderLine>
{
    public required string Symbol { get; set; }
    public required int Quantity { get; set; }

    public OrderLine DeepClone() =>
        throw new NotImplementedException(
            "TODO(student): return a NEW OrderLine with the same Symbol and Quantity.");
}

/// <summary>
/// A reusable template of orders (a "model basket"). The desk keeps a library of these and clones
/// one for each client, adjusting quantities to the client's capital — without ever corrupting the
/// master template.
/// </summary>
public sealed class ModelBasket : IPrototype<ModelBasket>
{
    public required string Name { get; set; }
    public required string Strategy { get; set; }
    public required List<OrderLine> Lines { get; set; }

    /// <remarks>
    /// TODO(student): return a NEW ModelBasket that is fully independent of this one:
    ///   • a NEW Lines list (not the same reference), and
    ///   • each element DEEP-cloned (not the same OrderLine objects).
    /// After cloning, editing the copy's lines must not touch the original.
    /// </remarks>
    public ModelBasket DeepClone() =>
        throw new NotImplementedException(
            "TODO(student): deep-clone the basket — new list, cloned lines. See the <remarks>.");
}
