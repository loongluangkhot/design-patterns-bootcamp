package com.labrise.designpatterns.prototype;

/**
 * Prototype pattern contract (scaffold)
 *
 * Intent: Provide a way to clone objects without coupling to their concrete classes.
 *
 * Implementations should define what "clone" means:
 * - shallow vs deep copy
 * - identity handling (IDs preserved or regenerated)
 * - defensive copies for collections
 */
public interface Prototype<T> {
    T copy();
}
