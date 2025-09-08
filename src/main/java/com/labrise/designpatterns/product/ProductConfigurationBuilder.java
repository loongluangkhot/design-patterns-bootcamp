package com.labrise.designpatterns.product;

import java.math.BigDecimal;
import java.util.Arrays;
import java.util.HashMap;
import java.util.Map;
import java.util.Objects;
import java.util.UUID;

import com.labrise.designpatterns.models.Product;

/**
 * ProductConfigurationBuilder (scaffold)
 *
 * Goal: Provide a fluent API for configuring a Product (options, add-ons, pricing)
 * while keeping validation and price computation centralized.
 *
 * Implement the methods to:
 * - Track selections (options/add-ons) and price adjustments
 * - Validate combinations (light rules here; complex rules OK too)
 * - Compute final price from base + adjustments
 * - Build an immutable Product (works with Product annotated with @Value)
 */
public class ProductConfigurationBuilder {

    // === Internal state to accumulate selections ===
    private Map<String, String> selectedOptions = new HashMap<>();
    private Map<String, BigDecimal> optionPriceAdjustments = new HashMap<>();
    private Map<String, BigDecimal> addOns = new HashMap<>();

    // === Core product info ===
    private String productId = UUID.randomUUID().toString();
    private String name;
    private String description;
    private String category;
    private String sku;
    private String brand;
    private BigDecimal basePrice;
    private String currency;
    private boolean isActive;
    private int stockQuantity;

    // === Fluent configuration API ===

    /**
     * Set required basic info.
     */
    public ProductConfigurationBuilder withBasicInfo(String name, String description, String category, BigDecimal basePrice, String currency) {
        this.name = name;
        this.description = description;
        this.category = category;
        this.basePrice = basePrice;
        this.currency = currency;
        return this;
    }

    /**
     * Optional branding details.
     */
    public ProductConfigurationBuilder withBranding(String brand, String sku) {
        this.brand = brand;
        this.sku = sku;
        return this;
    }

    /**
     * Inventory status.
     */
    public ProductConfigurationBuilder withInventory(int stockQuantity, boolean isActive) {
        this.stockQuantity = stockQuantity;
        this.isActive = isActive;
        return this;
    }

    /**
     * Add or change a configurable option (e.g., "CPU" -> "i9").
     */
    public ProductConfigurationBuilder withOption(String optionName, String optionValue, BigDecimal priceAdjustment) {
        this.selectedOptions.put(optionName, optionValue);
        this.optionPriceAdjustments.put(optionName, priceAdjustment);
        return this;
    }

    /**
     * Add an add-on (e.g., warranty) with its price.
     */
    public ProductConfigurationBuilder withAddOn(String addOnName, BigDecimal addOnPrice) {
        this.addOns.put(addOnName, addOnPrice);
        return this;
    }

    // === Validation and pricing ===

    /**
     * Validate current selections for basic invariants and compatibility rules.
     */
    private void validateConfiguration() {
        if (this.name == null || this.name.isBlank() || this.category == null || this.category.isBlank() || this.basePrice == null) {
            throw new IllegalArgumentException(String.format("name, category, and basePrice cannot be empty!"));
        }
        if (this.basePrice.signum() < 0) {
            throw new IllegalArgumentException("basePrice cannot be negative!");
        }
        if (this.stockQuantity < 0) {
            throw new IllegalArgumentException("stockQuantity cannot be negative!");
        }

        if ("Clothing".equalsIgnoreCase(category)) {
            String size = selectedOptions.get("size");
            if (size == null || Arrays.stream(new String[]{"XS","S","M","L","XL","XXL"})
                .noneMatch(size::equalsIgnoreCase)) {
              throw new IllegalArgumentException("Clothing size must be one of XS,S,M,L,XL,XXL");
            }
          }
    }

    /**
     * Compute final price from base + option adjustments + add-ons.
     */
    private BigDecimal calculateFinalPrice() {
        BigDecimal optionsPrice = this.optionPriceAdjustments.values().stream().filter(Objects::nonNull).reduce(BigDecimal.ZERO, BigDecimal::add);
        BigDecimal addOnsPrice = this.addOns.values().stream().filter(Objects::nonNull).reduce(BigDecimal.ZERO, BigDecimal::add);
        return basePrice.add(optionsPrice).add(addOnsPrice) ;
    }

    // === Build ===

    /**
     * Construct an immutable Product using accumulated state.
     */
    public Product build() {
        validateConfiguration();
        BigDecimal finalPrice = calculateFinalPrice();
        return new Product(
            this.productId,
            this.name,
            this.description,
            this.category,
            this.sku,
            this.brand,
            this.basePrice,
            this.currency,
            Map.copyOf(this.selectedOptions),
            Map.copyOf(this.addOns),
            Map.copyOf(this.optionPriceAdjustments),
            finalPrice,
            this.isActive,
            this.stockQuantity
        );
    }

    // === Director-style presets (optional helpers) ===

    /**
     * Seed a typical laptop baseline that consumers can customize further.
     */
    public static ProductConfigurationBuilder laptopPreset() {
        return new ProductConfigurationBuilder()
            .withBasicInfo("Laptop", "Customizable laptop", "Electronics", new BigDecimal("999.99"), "USD")
            .withBranding("TechBrand", "LAP-BASE")
            .withInventory(50, true)
            // minimal defaults (consumer can override)
            .withOption("CPU", "i5", new BigDecimal("0.00"))
            .withOption("RAM", "16GB", new BigDecimal("100.00"))
            .withOption("Storage", "512GB SSD", new BigDecimal("50.00"));
    }

    /**
     * Seed a typical t-shirt baseline that consumers can customize further.
     */
    public static ProductConfigurationBuilder tshirtPreset() {
        return new ProductConfigurationBuilder()
            .withBasicInfo("T-Shirt", "Cotton t-shirt", "Clothing", new BigDecimal("19.99"), "USD")
            .withBranding("FashionCo", "TSH-BASE")
            .withInventory(200, true)
            .withOption("size", "M", BigDecimal.ZERO)
            .withOption("color", "Black", BigDecimal.ZERO);
    }

    /**
     * Seed a typical smartphone baseline that consumers can customize further.
     */
    public static ProductConfigurationBuilder smartphonePreset() {
        return new ProductConfigurationBuilder()
            .withBasicInfo("Smartphone", "High-end smartphone", "Electronics", new BigDecimal("699.99"), "USD")
            .withBranding("PhoneCorp", "PHN-BASE")
            .withInventory(120, true)
            .withOption("Storage", "128GB", BigDecimal.ZERO)
            .withOption("Color", "Midnight", BigDecimal.ZERO);
    }
}
