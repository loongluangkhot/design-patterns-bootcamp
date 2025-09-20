package com.labrise.designpatterns.bridge;

import com.labrise.designpatterns.models.Order;
import com.labrise.designpatterns.models.Product;

import java.math.BigDecimal;
import java.util.List;
import java.util.Optional;

/**
 * BRIDGE PATTERN - Database Service (Abstraction)
 * 
 * This is the "abstraction" side of the Bridge pattern.
 * It defines what database operations are available for our business logic,
 * but delegates the actual implementation to DatabaseImplementation.
 * 
 * The Bridge pattern allows us to:
 * 1. Switch between different database implementations without changing business logic
 * 2. Add new database types without modifying existing code
 * 3. Keep business logic separate from database-specific details
 */
public class DatabaseService {
    
    private final DatabaseImplementation databaseImplementation;
    
    public DatabaseService(DatabaseImplementation databaseImplementation) {
        this.databaseImplementation = databaseImplementation;
    }
    
    // ===== Product Management =====
    
    /**
     * Add a new product to the catalog
     * 
     * @param product Product to add
     * @return Product ID if successful, null if failed
     */
    public String addProduct(Product product) {
        System.out.println("DatabaseService: Adding new product to catalog...");
        System.out.println("Product: " + product.getName());
        
        try {
            if (product.getName() == null || product.getName().trim().isEmpty()) {
                System.out.println("❌ Product name is required");
                return null;
            }
            
            if (product.getBasePrice() == null || product.getBasePrice().compareTo(BigDecimal.ZERO) < 0) {
                System.out.println("❌ Invalid product price");
                return null;
            }
            
            // Check if product already exists (by SKU)
            if (product.getSku() != null && !product.getSku().trim().isEmpty()) {
                var existingProduct = databaseImplementation.findProductById(product.getSku());
                if (existingProduct.isPresent()) {
                    System.out.println("❌ Product with SKU already exists: " + product.getSku());
                    return null;
                }
            }
            
            // Delegate to database implementation
            String productId = databaseImplementation.saveProduct(product);
            
            if (productId != null) {
                System.out.println("✅ Product added successfully with ID: " + productId);
            } else {
                System.out.println("❌ Failed to add product to database");
            }
            
            return productId;
            
        } catch (Exception e) {
            System.out.println("❌ Error adding product: " + e.getMessage());
            return null;
        }
    }
    
    /**
     * Get a product by ID
     * 
     * @param productId Product ID to find
     * @return Product if found, empty if not found
     */
    public Optional<Product> getProduct(String productId) {
        System.out.println("DatabaseService: Getting product by ID: " + productId);
        
        try {
            // Validate product ID
            if (productId == null || productId.trim().isEmpty()) {
                System.out.println("❌ Product ID cannot be null or empty");
                return Optional.empty();
            }
            
            // Delegate to database implementation
            Optional<Product> product = databaseImplementation.findProductById(productId);
            
            if (product.isPresent()) {
                System.out.println("✅ Product found: " + product.get().getName());
            } else {
                System.out.println("❌ Product not found with ID: " + productId);
            }
            
            return product;
            
        } catch (Exception e) {
            System.out.println("❌ Error getting product: " + e.getMessage());
            return Optional.empty();
        }
    }
    
    /**
     * Search products by category
     * 
     * @param category Category to search
     * @return List of products in category
     */
    public List<Product> searchProductsByCategory(String category) {
        System.out.println("DatabaseService: Searching products by category: " + category);
        
        try {
            // Validate category
            if (category == null || category.trim().isEmpty()) {
                System.out.println("❌ Category cannot be null or empty");
                return List.of();
            }
            
            // Delegate to database implementation
            List<Product> products = databaseImplementation.findProductsByCategory(category);
            
            System.out.println("✅ Found " + products.size() + " products in category: " + category);
            return products;
            
        } catch (Exception e) {
            System.out.println("❌ Error searching products: " + e.getMessage());
            return List.of();
        }
    }
    
    /**
     * Update product stock after order
     * 
     * @param productId Product ID to update
     * @param quantitySold Quantity sold (will be subtracted from stock)
     * @return true if successful, false otherwise
     */
    public boolean updateStockAfterSale(String productId, int quantitySold) {
        System.out.println("DatabaseService: Updating stock after sale...");
        System.out.println("Product ID: " + productId);
        System.out.println("Quantity Sold: " + quantitySold);
        
        try {
            // Validate parameters
            if (productId == null || productId.trim().isEmpty()) {
                System.out.println("❌ Product ID cannot be null or empty");
                return false;
            }
            
            if (quantitySold <= 0) {
                System.out.println("❌ Quantity sold must be positive");
                return false;
            }
            
            // Get current product from database
            Optional<Product> productOpt = databaseImplementation.findProductById(productId);
            if (productOpt.isEmpty()) {
                System.out.println("❌ Product not found: " + productId);
                return false;
            }
            
            Product product = productOpt.get();
            int currentStock = product.getStockQuantity();
            
            // Calculate new stock quantity
            int newStock = currentStock - quantitySold;
            if (newStock < 0) {
                System.out.println("❌ Insufficient stock. Current: " + currentStock + ", Requested: " + quantitySold);
                return false;
            }
            
            // Update stock in database
            boolean success = databaseImplementation.updateProductStock(productId, newStock);
            
            if (success) {
                System.out.println("✅ Stock updated successfully. New quantity: " + newStock);
            } else {
                System.out.println("❌ Failed to update stock in database");
            }
            
            return success;
            
        } catch (Exception e) {
            System.out.println("❌ Error updating stock: " + e.getMessage());
            return false;
        }
    }
    
    // ===== Order Management =====
    
    /**
     * Create a new order
     * 
     * @param order Order to create
     * @return Order ID if successful, null if failed
     */
    public String createOrder(Order order) {
        System.out.println("DatabaseService: Creating new order...");
        System.out.println("Order: " + order.getOrderId());
        
        try {
            if (order.getCustomerId() == null || order.getCustomerId().trim().isEmpty()) {
                System.out.println("❌ Customer ID is required");
                return null;
            }
            
            if (order.getItems() == null || order.getItems().isEmpty()) {
                System.out.println("❌ Order must have at least one item");
                return null;
            }
            
            // Check product availability for each item
            for (var item : order.getItems()) {
                Optional<Product> productOpt = databaseImplementation.findProductById(item.getProductId());
                if (productOpt.isEmpty()) {
                    System.out.println("❌ Product not found: " + item.getProductId());
                    return null;
                }
                
                Product product = productOpt.get();
                if (product.getStockQuantity() < item.getQuantity()) {
                    System.out.println("❌ Insufficient stock for product: " + product.getName() + 
                                     " (Available: " + product.getStockQuantity() + ", Required: " + item.getQuantity() + ")");
                    return null;
                }
            }
            
            // Delegate to database implementation
            String orderId = databaseImplementation.saveOrder(order);
            
            if (orderId != null) {
                System.out.println("✅ Order created successfully with ID: " + orderId);
            } else {
                System.out.println("❌ Failed to create order in database");
            }
            
            return orderId;
            
        } catch (Exception e) {
            System.out.println("❌ Error creating order: " + e.getMessage());
            return null;
        }
    }
    
    /**
     * Get an order by ID
     * 
     * @param orderId Order ID to find
     * @return Order if found, empty if not found
     */
    public Optional<Order> getOrder(String orderId) {
        System.out.println("DatabaseService: Getting order by ID: " + orderId);
        
        try {
            // Validate order ID
            if (orderId == null || orderId.trim().isEmpty()) {
                System.out.println("❌ Order ID cannot be null or empty");
                return Optional.empty();
            }
            
            // Delegate to database implementation
            Optional<Order> order = databaseImplementation.findOrderById(orderId);
            
            if (order.isPresent()) {
                System.out.println("✅ Order found: " + order.get().getOrderId());
            } else {
                System.out.println("❌ Order not found with ID: " + orderId);
            }
            
            return order;
            
        } catch (Exception e) {
            System.out.println("❌ Error getting order: " + e.getMessage());
            return Optional.empty();
        }
    }
    
    /**
     * Get all orders for a customer
     * 
     * @param customerId Customer ID to search
     * @return List of orders for customer
     */
    public List<Order> getCustomerOrders(String customerId) {
        System.out.println("DatabaseService: Getting orders for customer: " + customerId);
        
        try {
            // Validate customer ID
            if (customerId == null || customerId.trim().isEmpty()) {
                System.out.println("❌ Customer ID cannot be null or empty");
                return List.of();
            }
            
            // Delegate to database implementation
            List<Order> orders = databaseImplementation.findOrdersByCustomer(customerId);
            
            System.out.println("✅ Found " + orders.size() + " orders for customer: " + customerId);
            return orders;
            
        } catch (Exception e) {
            System.out.println("❌ Error getting customer orders: " + e.getMessage());
            return List.of();
        }
    }
    
    /**
     * Update order status
     * 
     * @param orderId Order ID to update
     * @param newStatus New order status
     * @return true if successful, false otherwise
     */
    public boolean updateOrderStatus(String orderId, Order.OrderStatus newStatus) {
        System.out.println("DatabaseService: Updating order status...");
        System.out.println("Order ID: " + orderId);
        System.out.println("New Status: " + newStatus);
        
        try {
            // Validate order ID and status
            if (orderId == null || orderId.trim().isEmpty()) {
                System.out.println("❌ Order ID cannot be null or empty");
                return false;
            }
            
            if (newStatus == null) {
                System.out.println("❌ New status cannot be null");
                return false;
            }
            
            // Check if order exists
            Optional<Order> orderOpt = databaseImplementation.findOrderById(orderId);
            if (orderOpt.isEmpty()) {
                System.out.println("❌ Order not found: " + orderId);
                return false;
            }
            
            Order order = orderOpt.get();
            Order.OrderStatus currentStatus = order.getStatus();
            
            // Check if status transition is valid (basic validation)
            if (currentStatus == Order.OrderStatus.CANCELLED) {
                System.out.println("❌ Cannot update status of cancelled order");
                return false;
            }
            
            if (currentStatus == Order.OrderStatus.DELIVERED && newStatus != Order.OrderStatus.DELIVERED) {
                System.out.println("❌ Cannot change status of delivered order");
                return false;
            }
            
            // Delegate to database implementation
            boolean success = databaseImplementation.updateOrderStatus(orderId, newStatus);
            
            if (success) {
                System.out.println("✅ Order status updated successfully: " + currentStatus + " -> " + newStatus);
            } else {
                System.out.println("❌ Failed to update order status in database");
            }
            
            return success;
            
        } catch (Exception e) {
            System.out.println("❌ Error updating order status: " + e.getMessage());
            return false;
        }
    }
    
    // ===== Transaction Management =====
    
    /**
     * Process order with transaction safety
     * 
     * @param order Order to process
     * @return true if successful, false otherwise
     */
    public boolean processOrderWithTransaction(Order order) {
        System.out.println("DatabaseService: Processing order with transaction...");
        System.out.println("Order: " + order.getOrderId());
        
        String transactionId = null;
        
        try {
            // Begin database transaction
            transactionId = databaseImplementation.beginTransaction();
            if (transactionId == null) {
                System.out.println("❌ Failed to begin transaction");
                return false;
            }
            
            System.out.println("Transaction started: " + transactionId);
            
            // Create order in database
            String orderId = databaseImplementation.saveOrder(order);
            if (orderId == null) {
                System.out.println("❌ Failed to save order in transaction");
                databaseImplementation.rollbackTransaction(transactionId);
                return false;
            }
            
            // Update product stock for each item
            for (var item : order.getItems()) {
                Optional<Product> productOpt = databaseImplementation.findProductById(item.getProductId());
                if (productOpt.isEmpty()) {
                    System.out.println("❌ Product not found in transaction: " + item.getProductId());
                    databaseImplementation.rollbackTransaction(transactionId);
                    return false;
                }
                
                Product product = productOpt.get();
                int newStock = product.getStockQuantity() - item.getQuantity();
                
                boolean stockUpdated = databaseImplementation.updateProductStock(item.getProductId(), newStock);
                if (!stockUpdated) {
                    System.out.println("❌ Failed to update stock in transaction for product: " + item.getProductId());
                    databaseImplementation.rollbackTransaction(transactionId);
                    return false;
                }
            }
            
            // Commit transaction if all successful
            boolean committed = databaseImplementation.commitTransaction(transactionId);
            if (committed) {
                System.out.println("✅ Order processed successfully with transaction: " + transactionId);
                return true;
            } else {
                System.out.println("❌ Failed to commit transaction: " + transactionId);
                return false;
            }
            
        } catch (Exception e) {
            System.out.println("❌ Error processing order with transaction: " + e.getMessage());
            
            // Rollback transaction if any step fails
            if (transactionId != null) {
                databaseImplementation.rollbackTransaction(transactionId);
                System.out.println("Transaction rolled back: " + transactionId);
            }
            
            return false;
        }
    }
    
    // ===== Helper Methods =====
    
    /**
     * Check if database is connected
     * 
     * @return true if connected, false otherwise
     */
    public boolean isConnected() {
        return databaseImplementation.isConnected();
    }
    
    /**
     * Connect to database
     * 
     * @return true if successful, false otherwise
     */
    public boolean connect() {
        return databaseImplementation.connect();
    }
    
    /**
     * Disconnect from database
     */
    public void disconnect() {
        databaseImplementation.disconnect();
    }
}
