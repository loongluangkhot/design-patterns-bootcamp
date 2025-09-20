package com.labrise.designpatterns.facade;

import java.util.HashMap;
import java.util.Map;

/**
 * Service responsible for inventory management
 * This is part of the complex subsystem that the Facade will simplify
 */
public class InventoryService {

    private Map<String, Integer> inventory;
    
    public InventoryService() {
        inventory = new HashMap<>();
        inventory.put("123", 100);
        inventory.put("456", 200);
        inventory.put("789", 300);
    }
    
    /**
     * Check if a product is available in the required quantity
     * @param productId The ID of the product to check
     * @param quantity The required quantity
     * @return true if available, false otherwise
     */
    public boolean isProductAvailable(String productId, int quantity) {
        return inventory.get(productId) >= quantity;
    }
    
    /**
     * Reserve products in inventory
     * @param productId The ID of the product to reserve
     * @param quantity The quantity to reserve
     * @return true if reservation successful, false otherwise
     */
    public boolean reserveProduct(String productId, int quantity) {
        int currQty = inventory.get(productId);
        if (currQty < quantity) {
            return false;
        }
        inventory.put(productId, currQty - quantity);
        return true;
    }
    
    /**
     * Release reserved products back to inventory
     * @param productId The ID of the product to release
     * @param quantity The quantity to release
     */
    public void releaseProduct(String productId, int quantity) {
        inventory.put(productId, inventory.get(productId) + quantity);
    }
}
