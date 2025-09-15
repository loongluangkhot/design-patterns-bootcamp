package com.labrise.designpatterns.decorator;

import java.math.BigDecimal;

import com.labrise.designpatterns.models.Product;

/**
 * DECORATOR PATTERN - Abstract Decorator
 * 
 * This is the base class for all pricing decorators. It maintains a reference
 * to the wrapped ProductPricing object and delegates calls to it.
 * 
 * Your task: Implement the basic decorator structure that:
 * 1. Wraps another ProductPricing object
 * 2. Delegates calls to the wrapped object
 * 3. Provides a foundation for concrete decorators to extend
 */
public abstract class PricingDecorator implements ProductPricing {
    
    protected final ProductPricing wrappedPricing;
    
    public PricingDecorator(ProductPricing wrappedPricing) {
        this.wrappedPricing = wrappedPricing;
    }
    
    @Override
    public BigDecimal calculatePrice(Product product) {
        return wrappedPricing.calculatePrice(product);
    }
    
    @Override
    public String getPricingDescription(Product product) {
        return wrappedPricing.getPricingDescription(product);
    }
    
    @Override
    public BigDecimal getBasePrice(Product product) {
        return wrappedPricing.getBasePrice(product);
    }
}
