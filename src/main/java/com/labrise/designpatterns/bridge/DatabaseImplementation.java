package com.labrise.designpatterns.bridge;

import com.labrise.designpatterns.models.Order;
import com.labrise.designpatterns.models.Product;

import java.util.List;
import java.util.Optional;

/**
 * BRIDGE PATTERN - Database Implementation Interface
 * 
 * This is the "implementation" side of the Bridge pattern.
 * It defines how database operations are performed, but not what operations
 * are available (that's the abstraction side).
 * 
 * The Bridge pattern allows us to:
 * 1. Switch between different database implementations (MySQL, PostgreSQL, MongoDB)
 * 2. Add new database types without changing existing code
 * 3. Keep database-specific logic separate from business logic
 */
public interface DatabaseImplementation {
    
    // ===== Connection Management =====
    
    /**
     * Connect to the database
     * @return true if connection successful, false otherwise
     */
    boolean connect();
    
    /**
     * Disconnect from the database
     */
    void disconnect();
    
    /**
     * Check if currently connected
     * @return true if connected, false otherwise
     */
    boolean isConnected();
    
    // ===== Product Operations =====
    
    /**
     * Save a product to the database
     * @param product Product to save
     * @return Product ID if successful, null if failed
     */
    String saveProduct(Product product);
    
    /**
     * Find a product by ID
     * @param productId Product ID to find
     * @return Product if found, empty if not found
     */
    Optional<Product> findProductById(String productId);
    
    /**
     * Find all products in a category
     * @param category Category to search
     * @return List of products in category
     */
    List<Product> findProductsByCategory(String category);
    
    /**
     * Update product stock quantity
     * @param productId Product ID to update
     * @param newQuantity New stock quantity
     * @return true if successful, false otherwise
     */
    boolean updateProductStock(String productId, int newQuantity);
    
    // ===== Order Operations =====
    
    /**
     * Save an order to the database
     * @param order Order to save
     * @return Order ID if successful, null if failed
     */
    String saveOrder(Order order);
    
    /**
     * Find an order by ID
     * @param orderId Order ID to find
     * @return Order if found, empty if not found
     */
    Optional<Order> findOrderById(String orderId);
    
    /**
     * Find orders by customer ID
     * @param customerId Customer ID to search
     * @return List of orders for customer
     */
    List<Order> findOrdersByCustomer(String customerId);
    
    /**
     * Update order status
     * @param orderId Order ID to update
     * @param newStatus New order status
     * @return true if successful, false otherwise
     */
    boolean updateOrderStatus(String orderId, Order.OrderStatus newStatus);
    
    // ===== Transaction Management =====
    
    /**
     * Start a database transaction
     * @return Transaction ID if successful, null if failed
     */
    String beginTransaction();
    
    /**
     * Commit a database transaction
     * @param transactionId Transaction to commit
     * @return true if successful, false otherwise
     */
    boolean commitTransaction(String transactionId);
    
    /**
     * Rollback a database transaction
     * @param transactionId Transaction to rollback
     * @return true if successful, false otherwise
     */
    boolean rollbackTransaction(String transactionId);
}
