package com.labrise.designpatterns.decorator;

import java.math.BigDecimal;

import com.labrise.designpatterns.models.Product;

/**
 * DECORATOR PATTERN - Concrete Decorator
 * 
 * This decorator applies a fixed amount discount to the product price.
 * It wraps another ProductPricing object and modifies its behavior.
 * 
 * Your task: Implement the fixed amount discount logic that:
 * 1. Gets the price from the wrapped pricing object
 * 2. Subtracts the fixed discount amount
 * 3. Returns the discounted price
 * 4. Provides a clear description of the discount applied
 */
public class FixedAmountDiscountDecorator extends PricingDecorator {
    
    private final BigDecimal discountAmount;
    private final String discountReason;
    
    public FixedAmountDiscountDecorator(ProductPricing wrappedPricing, 
                                      BigDecimal discountAmount, 
                                      String discountReason) {
        super(wrappedPricing);
        this.discountAmount = discountAmount;
        this.discountReason = discountReason;
    }
    
    @Override
    public BigDecimal calculatePrice(Product product) {
        BigDecimal price = super.calculatePrice(product);
        return price.subtract(discountAmount).max(BigDecimal.ZERO);
    }
    
    @Override
    public String getPricingDescription(Product product) {
        String description = super.getPricingDescription(product);
        return String.format("%s, %s off (%s): %s", description, discountAmount, discountReason, calculatePrice(product));
    }
}
