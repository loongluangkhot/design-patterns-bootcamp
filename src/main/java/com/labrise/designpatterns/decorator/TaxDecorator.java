package com.labrise.designpatterns.decorator;

import java.math.BigDecimal;
import com.labrise.designpatterns.models.Product;

/**
 * DECORATOR PATTERN - Concrete Decorator
 * 
 * This decorator applies tax to the product price.
 * It wraps another ProductPricing object and modifies its behavior.
 * 
 * Your task: Implement the tax calculation logic that:
 * 1. Gets the price from the wrapped pricing object
 * 2. Calculates the tax amount
 * 3. Adds the tax to the price
 * 4. Provides a clear description of the tax applied
 */
public class TaxDecorator extends PricingDecorator {
    
    private final BigDecimal taxRate;
    private final String taxType;
    
    public TaxDecorator(ProductPricing wrappedPricing, 
                       BigDecimal taxRate, 
                       String taxType) {
        super(wrappedPricing);
        this.taxRate = taxRate;
        this.taxType = taxType;
    }
    
    @Override
    public BigDecimal calculatePrice(Product product) {
        BigDecimal price = super.calculatePrice(product);
        return price.multiply(BigDecimal.valueOf(1 + taxRate.doubleValue()));
    }
    
    @Override
    public String getPricingDescription(Product product) {
        String description = super.getPricingDescription(product);
        return String.format("%s, %s %s added: %s", description, taxRate, taxType, calculatePrice(product));
    }
}
