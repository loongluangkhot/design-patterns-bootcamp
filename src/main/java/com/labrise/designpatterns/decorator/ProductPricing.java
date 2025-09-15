package com.labrise.designpatterns.decorator;

import java.math.BigDecimal;

import com.labrise.designpatterns.models.Product;

/**
 * DECORATOR PATTERN - Component Interface
 * 
 * This is the base interface for product pricing calculations.
 * The Decorator pattern allows us to:
 * 1. Add pricing behaviors (discounts, taxes, fees) dynamically
 * 2. Combine multiple pricing rules in any order
 * 3. Keep pricing logic separate and composable
 * 4. Add new pricing rules without modifying existing code
 * 
 * Challenge: How do we apply complex pricing rules to products where:
 * - Different customers get different discounts
 * - Various taxes apply based on location
 * - Shipping fees depend on product weight and destination
 * - Promotional codes can stack with other discounts
 * - We need to calculate the final price step by step
 * - We want to be able to add new pricing rules easily
 */
public interface ProductPricing {
    
    /**
     * Calculate the final price for a product
     * @param product Product to calculate price for
     * @return Final price after applying all pricing rules
     */
    BigDecimal calculatePrice(Product product);
    
    /**
     * Get a description of the pricing calculation
     * @param product Product being priced
     * @return Human-readable description of the pricing
     */
    String getPricingDescription(Product product);
    
    /**
     * Get the base price before any modifications
     * @param product Product to get base price for
     * @return Base price of the product
     */
    BigDecimal getBasePrice(Product product);
}
