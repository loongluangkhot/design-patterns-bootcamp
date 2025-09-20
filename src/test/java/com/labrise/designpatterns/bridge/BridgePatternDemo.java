package com.labrise.designpatterns.bridge;

import com.labrise.designpatterns.models.Order;
import com.labrise.designpatterns.models.Product;
import com.labrise.designpatterns.facade.ShippingMethod;

import java.math.BigDecimal;
import java.time.LocalDateTime;
import java.util.List;

/**
 * BRIDGE PATTERN DEMO
 * 
 * This demo shows how the Bridge pattern separates abstraction from implementation.
 * We'll demonstrate database operations that can work with different database types.
 */
public class BridgePatternDemo {
    
    public static void main(String[] args) {
        System.out.println("🌉 Bridge Pattern Demo - Database Abstraction");
        System.out.println("=============================================\n");
        
        // Step 1: Create MySQL database implementation
        System.out.println("1. Creating MySQL database implementation...");
        MySQLDatabaseImplementation mysqlImpl = new MySQLDatabaseImplementation(
            "jdbc:mysql://localhost:3306/shopcraft",
            "shopcraft_user",
            "secure_password"
        );
        
        // Step 2: Create database service (abstraction) with MySQL implementation
        System.out.println("\n2. Creating database service with MySQL implementation...");
        DatabaseService databaseService = new DatabaseService(mysqlImpl);
        
        // Step 3: Connect to database
        System.out.println("\n3. Connecting to database...");
        boolean connected = databaseService.connect();
        System.out.println("Connected: " + connected);
        
        // Step 4: Test product operations
        System.out.println("\n4. Testing product operations...");
        testProductOperations(databaseService);
        
        // Step 5: Test order operations
        System.out.println("\n5. Testing order operations...");
        testOrderOperations(databaseService);
        
        // Step 6: Test transaction operations
        System.out.println("\n6. Testing transaction operations...");
        testTransactionOperations(databaseService);
        
        // Step 7: Disconnect from database
        System.out.println("\n7. Disconnecting from database...");
        databaseService.disconnect();
        System.out.println("Disconnected: " + !databaseService.isConnected());
        
        System.out.println("\n✅ Bridge Pattern Demo Complete!");
        System.out.println("The bridge successfully separates abstraction from implementation!");
    }
    
    private static void testProductOperations(DatabaseService databaseService) {
        System.out.println("Testing product operations...");
        
        // Create a sample product
        Product product = new Product(
            "PROD_001",
            "MacBook Pro",
            "Apple MacBook Pro 16-inch",
            "Laptops",
            "MBP16-001",
            "Apple",
            new BigDecimal("2499.99"),
            "USD",
            null, // options
            null, // addOns
            null, // optionPriceAdjustments
            new BigDecimal("2499.99"), // finalPrice
            true, // isActive
            10 // stockQuantity
        );
        
        // Add product to database
        String productId = databaseService.addProduct(product);
        if (productId != null) {
            System.out.println("✅ Product added successfully! ID: " + productId);
            
            // Get product from database
            var retrievedProduct = databaseService.getProduct(productId);
            if (retrievedProduct.isPresent()) {
                System.out.println("✅ Product retrieved successfully: " + retrievedProduct.get().getName());
            } else {
                System.out.println("❌ Failed to retrieve product");
            }
            
            // Search products by category
            List<Product> laptops = databaseService.searchProductsByCategory("Laptops");
            System.out.println("✅ Found " + laptops.size() + " products in Laptops category");
            
            // Update stock
            boolean stockUpdated = databaseService.updateStockAfterSale(productId, 2);
            System.out.println("Stock updated after sale: " + (stockUpdated ? "SUCCESS" : "FAILED"));
        } else {
            System.out.println("❌ Failed to add product");
        }
    }
    
    private static void testOrderOperations(DatabaseService databaseService) {
        System.out.println("Testing order operations...");
        
        // Create a sample order
        Order order = new Order(
            "ORDER_001",
            LocalDateTime.now(),
            Order.OrderStatus.PENDING,
            "CUST_001",
            "John Doe",
            "john.doe@example.com",
            List.of(), // items
            new BigDecimal("2499.99"), // subtotal
            "123 Main St, City, State 12345",
            ShippingMethod.STANDARD,
            new BigDecimal("9.99"), // shipping cost
            "123 Main St, City, State 12345", // billing address
            "Credit Card",
            null, // discount code
            BigDecimal.ZERO, // discount amount
            new BigDecimal("199.99"), // tax amount
            new BigDecimal("2709.97"), // total amount
            false, // gift wrapping
            null, // gift message
            null // special instructions
        );
        
        // Create order in database
        String orderId = databaseService.createOrder(order);
        if (orderId != null) {
            System.out.println("✅ Order created successfully! ID: " + orderId);
            
            // Get order from database
            var retrievedOrder = databaseService.getOrder(orderId);
            if (retrievedOrder.isPresent()) {
                System.out.println("✅ Order retrieved successfully: " + retrievedOrder.get().getOrderId());
            } else {
                System.out.println("❌ Failed to retrieve order");
            }
            
            // Get customer orders
            List<Order> customerOrders = databaseService.getCustomerOrders("CUST_001");
            System.out.println("✅ Found " + customerOrders.size() + " orders for customer");
            
            // Update order status
            boolean statusUpdated = databaseService.updateOrderStatus(orderId, Order.OrderStatus.CONFIRMED);
            System.out.println("Order status updated: " + (statusUpdated ? "SUCCESS" : "FAILED"));
        } else {
            System.out.println("❌ Failed to create order");
        }
    }
    
    private static void testTransactionOperations(DatabaseService databaseService) {
        System.out.println("Testing transaction operations...");
        
        // Create a sample order for transaction testing
        Order order = new Order(
            "ORDER_002",
            LocalDateTime.now(),
            Order.OrderStatus.PENDING,
            "CUST_002",
            "Jane Smith",
            "jane.smith@example.com",
            List.of(), // items
            new BigDecimal("199.99"), // subtotal
            "456 Oak Ave, City, State 54321",
            ShippingMethod.EXPRESS,
            new BigDecimal("19.99"), // shipping cost
            "456 Oak Ave, City, State 54321", // billing address
            "Credit Card",
            null, // discount code
            BigDecimal.ZERO, // discount amount
            new BigDecimal("16.00"), // tax amount
            new BigDecimal("235.98"), // total amount
            true, // gift wrapping
            "Happy Birthday!", // gift message
            "Please deliver after 5 PM" // special instructions
        );
        
        // Process order with transaction
        boolean transactionSuccess = databaseService.processOrderWithTransaction(order);
        System.out.println("Order processed with transaction: " + (transactionSuccess ? "SUCCESS" : "FAILED"));
    }
}
