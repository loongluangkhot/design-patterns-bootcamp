namespace DesignPatternsBootcamp.Creational.Prototype;

/// <summary>
/// The <b>Prototype</b> contract: an object that knows how to copy itself. We use a generic,
/// deep-copy-by-contract version rather than <see cref="System.ICloneable"/> because
/// <c>ICloneable.Clone()</c> returns <c>object</c> and famously never says whether the copy is
/// shallow or deep. Here the name makes the promise explicit: <see cref="DeepClone"/> returns a
/// fully independent copy.
/// </summary>
public interface IPrototype<out T>
{
    T DeepClone();
}
