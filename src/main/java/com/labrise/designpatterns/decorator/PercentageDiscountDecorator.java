package com.labrise.designpatterns.decorator;

import java.math.BigDecimal;
import java.math.RoundingMode;

import com.labrise.designpatterns.models.Product;

/**
 * DECORATOR PATTERN - Concrete Decorator
 * 
 * This decorator applies a percentage discount to the product price.
 * It wraps another ProductPricing object and modifies its behavior.
 * 
 * Your task: Implement the percentage discount logic that:
 * 1. Gets the price from the wrapped pricing object
 * 2. Applies the percentage discount
 * 3. Returns the discounted price
 * 4. Provides a clear description of the discount applied
 */
public class PercentageDiscountDecorator extends PricingDecorator {
    
    private final BigDecimal discountPercentage;
    private final String discountReason;
    
    public PercentageDiscountDecorator(ProductPricing wrappedPricing, 
                                     BigDecimal discountPercentage, 
                                     String discountReason) {
        super(wrappedPricing);
        this.discountPercentage = discountPercentage;
        this.discountReason = discountReason;
    }
    
    @Override
    public BigDecimal calculatePrice(Product product) {
        BigDecimal price = super.calculatePrice(product);
        return price.multiply(BigDecimal.valueOf(100 - discountPercentage.doubleValue())).divide(BigDecimal.valueOf(100), 2, RoundingMode.HALF_UP);
    }

    @Override
    public String getPricingDescription(Product product) {
        String description = super.getPricingDescription(product);
        return String.format("%s, %s off (%s): %s", description, discountPercentage, discountReason, calculatePrice(product));
    }
}
