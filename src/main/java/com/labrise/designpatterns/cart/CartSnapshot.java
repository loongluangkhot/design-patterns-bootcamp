package com.labrise.designpatterns.cart;

import java.util.Collections;
import java.util.List;

import com.labrise.designpatterns.models.OrderItem;

/**
 * CartSnapshot (scaffold)
 *
 * Immutable memento of a Cart's state.
 *
 * TODOs for learner:
 * - Decide on immutability strategy (unmodifiable list + immutable items)
 * - Provide accessors
 */
public class CartSnapshot {

    private final List<OrderItem> items;

    public CartSnapshot(List<OrderItem> items) {
        // TODO: defensive copy of items
        this.items = items;
    }

    public List<OrderItem> getItems() {
        // TODO: return unmodifiable list
        return Collections.unmodifiableList(items);
    }
}
