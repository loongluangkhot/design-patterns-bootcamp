package com.labrise.designpatterns.cart;

import java.util.ArrayList;
import java.util.List;

import com.labrise.designpatterns.models.OrderItem;

/**
 * Cart (scaffold)
 *
 * Intent: Manage cart items and support snapshot/restore (Memento pattern).
 *
 * TODOs for learner:
 * - Decide mutability and thread-safety requirements
 * - Implement add/remove/clear operations
 * - Implement snapshot() to return an immutable CartSnapshot
 * - Implement restore(CartSnapshot) to replace state from a snapshot
 */
public class Cart {

    // Internal state
    private final List<OrderItem> items = new ArrayList<>();

    // === Item operations ===

    public void addItem(OrderItem item) {
        // TODO: implement (aggregate quantities if same product?)
    }

    public void removeItem(String productId) {
        // TODO: implement (by productId)
    }

    public void clear() {
        // TODO: implement
    }

    public List<OrderItem> getItems() {
        // TODO: return safe view or defensive copy
        return items;
    }

    // === Memento operations ===

    public CartSnapshot snapshot() {
        // TODO: return an immutable snapshot of current items
        return null;
    }

    public void restore(CartSnapshot snapshot) {
        // TODO: replace internal state with snapshot state (defensive copy)
    }
}
