package com.labrise.designpatterns.models;

import java.math.BigDecimal;
import java.util.Map;

import lombok.Value;

/**
 * Product domain model (scaffold)
 *
 * Implement decisions:
 * - Immutability (recommended) vs mutability
 * - Constructors/builders/factory methods
 * - Getter strategy (Lombok or manual)
 * - Currency handling (string vs Money type)
 */
@Value
public class Product {
    // === Core identity ===
    private String productId;     // decide: provided vs generated in builder
    private String name;
    private String description;
    private String category;
    private String sku;
    private String brand;

    // === Pricing ===
    private BigDecimal basePrice;
    private String currency;      // keep simple for now; extend later if needed

    // === Configuration ===
    // Example: "CPU" -> "i9", "Color" -> "Space Gray"
    private Map<String, String> options;
    // Example: "Extended Warranty" -> 99.99
    private Map<String, BigDecimal> addOns;
    // Example: "RAM" -> 200.00 (price delta)
    private Map<String, BigDecimal> optionPriceAdjustments;

    // Derived/aggregated
    private BigDecimal finalPrice; // builder should compute and set

    // === State & inventory ===
    private boolean isActive;
    private int stockQuantity;
}
