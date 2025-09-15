package com.labrise.designpatterns.decorator;

import java.math.BigDecimal;

import com.labrise.designpatterns.models.Product;

/**
 * DECORATOR PATTERN - Concrete Component
 * 
 * This is the base implementation that simply returns the product's base price
 * without any modifications. It serves as the starting point for our decorator chain.
 * 
 * Your task: Implement the basic pricing logic that returns the product's
 * original price without any modifications.
 */
public class BaseProductPricing implements ProductPricing {
    
    @Override
    public BigDecimal calculatePrice(Product product) {
        return this.getBasePrice(product);
        
    }
    
    @Override
    public String getPricingDescription(Product product) {
        return String.format("Base price: %s %s", product.getBasePrice(), product.getCurrency());
    }
    
    @Override
    public BigDecimal getBasePrice(Product product) {
        return product.getBasePrice();
    }
}
