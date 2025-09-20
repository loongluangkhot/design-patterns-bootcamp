package com.labrise.designpatterns.facade;

import java.math.BigDecimal;

/**
 * Enum representing different shipping methods
 */
public enum ShippingMethod {
    STANDARD("Standard Shipping", BigDecimal.valueOf(5.00), 5),
    EXPRESS("Express Shipping", BigDecimal.valueOf(10.00), 2),
    OVERNIGHT("Overnight Shipping", BigDecimal.valueOf(20.00), 1);
    
    private final String description;
    private final BigDecimal baseCost;
    private final int daysToDeliver;
    
    ShippingMethod(String description, BigDecimal baseCost, int daysToDeliver) {
        this.description = description;
        this.baseCost = baseCost;
        this.daysToDeliver = daysToDeliver;
    }
    
    public String getDescription() {
        return description;
    }
    
    public BigDecimal getCost() {
        return baseCost;
    }
    
    public int getDaysToDeliver() {
        return daysToDeliver;
    }
}