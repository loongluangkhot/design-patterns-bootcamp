package com.labrise.designpatterns.facade;

import java.math.BigDecimal;
import java.time.LocalDateTime;
import java.util.UUID;
/**
 * Service responsible for shipping calculations and processing
 * This is part of the complex subsystem that the Facade will simplify
 */
public class ShippingService {
    
    /**
     * Calculate shipping cost for a given method and weight
     * @param method The shipping method
     * @param weight The total weight of items
     * @return The shipping cost
     */
    public BigDecimal calculateShippingCost(ShippingMethod method, double weight) {
        return method.getCost().add(BigDecimal.valueOf(weight * 0.50));
    }
    
    /**
     * Get estimated delivery time in days
     * @param method The shipping method
     * @return Number of days for delivery
     */
    public int getEstimatedDeliveryDays(ShippingMethod method) {
        return method.getDaysToDeliver();
    }
    
    /**
     * Create shipping label
     * @param method The shipping method
     * @param address The delivery address
     * @return The shipping label ID
     */
    public String createShippingLabel(ShippingMethod method, String address) {
        return String.format("%s-%s-%s-%s", UUID.randomUUID(), method.getDescription(), address, LocalDateTime.now());
    }
}
