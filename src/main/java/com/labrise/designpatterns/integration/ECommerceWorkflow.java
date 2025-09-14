package com.labrise.designpatterns.integration;

import com.labrise.designpatterns.models.*;
import com.labrise.designpatterns.order.OrderBuilder;
import com.labrise.designpatterns.product.ProductConfigurationBuilder;
import com.labrise.designpatterns.prototype.ProductPrototype;
import com.labrise.designpatterns.payment.CreditCardPaymentSuiteFactory;
import com.labrise.designpatterns.payment.PaymentSuiteFactory;
import com.labrise.designpatterns.payment.PaypalPaymentSuiteFactory;
import com.labrise.designpatterns.singleton.AppConfig;
import com.labrise.designpatterns.singleton.DatabaseConnectionManager;
import com.labrise.designpatterns.cart.Cart;

import java.math.BigDecimal;
import java.util.Arrays;
import java.util.List;
import java.util.Map;

/**
 * E-Commerce Workflow Integration Demo
 * 
 * This class demonstrates how all creational patterns work together
 * in a real e-commerce scenario.
 * 
 * PATTERNS USED:
 * 1. Singleton - AppConfig, DatabaseConnectionManager
 * 2. Factory Method - PaymentSuiteFactory
 * 3. Abstract Factory - PaymentSuiteFactory implementations
 * 4. Builder - OrderBuilder, ProductConfigurationBuilder
 * 5. Prototype - ProductPrototype for template cloning
 */
public class ECommerceWorkflow {
    
    private final AppConfig config;
    private final DatabaseConnectionManager dbManager;
    private final PaymentSuiteFactory paymentFactory;
    
    public ECommerceWorkflow() {
        // Singleton pattern - single instance of configuration and DB manager
        this.config = AppConfig.getInstance();
        this.dbManager = DatabaseConnectionManager.getInstance(
            config.get("db.url"),
            config.get("db.username"), 
            config.get("db.password")
        );
        
        // Factory pattern - create payment suite factory
        // Note: In a real app, this would be injected or configured
        this.paymentFactory = new PaymentSuiteFactory(Arrays.asList(
            new PaypalPaymentSuiteFactory(),
            new CreditCardPaymentSuiteFactory()
        ));
    }
    
    /**
     * Complete e-commerce workflow demonstrating all creational patterns
     */
    public void demonstrateCompleteWorkflow() {
        System.out.println("=== E-COMMERCE WORKFLOW DEMO ===\n");
        
        // Step 1: Create product templates using Prototype pattern
        System.out.println("1. Creating product templates using Prototype pattern...");
        createProductTemplates();
        
        // Step 2: Configure products using Builder pattern
        System.out.println("\n2. Configuring products using Builder pattern...");
        List<Product> configuredProducts = configureProducts();
        
        // Step 3: Build shopping cart
        System.out.println("\n3. Building shopping cart...");
        Cart cart = buildShoppingCart(configuredProducts);
        
        // Step 4: Process order using Builder pattern
        System.out.println("\n4. Processing order using Builder pattern...");
        Order order = processOrder(cart, "customer123", "john.doe@email.com");
        
        // Step 5: Process payment using Factory pattern
        System.out.println("\n5. Processing payment using Factory pattern...");
        processPayment(order);
        
        // Step 6: Database operations using Singleton pattern
        System.out.println("\n6. Saving to database using Singleton pattern...");
        saveToDatabase(order);
        
        System.out.println("\n=== WORKFLOW COMPLETED ===");
    }
    
    /**
     * TODO: Implement product template creation using Prototype pattern
     * 
     * Create base templates for:
     * - Laptop template
     * - Smartphone template  
     * - Headphones template
     * 
     * Use ProductPrototype to clone and customize each template
     */
    private void createProductTemplates() {
        // TODO: Implement using ProductPrototype
        // 1. Create base laptop template
        // 2. Create base smartphone template
        // 3. Create base headphones template
        // 4. Store templates for later use
    }
    
    /**
     * TODO: Implement product configuration using Builder pattern
     * 
     * Configure specific products from templates:
     * - MacBook Pro with specific CPU, RAM, storage
     * - iPhone with specific color, storage
     * - AirPods with specific model
     * 
     * Use ProductConfigurationBuilder for each product
     */
    private List<Product> configureProducts() {
        // TODO: Implement using ProductConfigurationBuilder
        // 1. Configure MacBook Pro from laptop template
        // 2. Configure iPhone from smartphone template  
        // 3. Configure AirPods from headphones template
        // 4. Return list of configured products
        return null;
    }
    
    /**
     * TODO: Implement shopping cart building
     * 
     * Add configured products to cart with quantities:
     * - MacBook Pro x1
     * - iPhone x2
     * - AirPods x1
     */
    private Cart buildShoppingCart(List<Product> products) {
        // TODO: Implement cart building
        // 1. Create new Cart instance
        // 2. Add products with appropriate quantities
        // 3. Return populated cart
        return null;
    }
    
    /**
     * TODO: Implement order processing using Builder pattern
     * 
     * Build complete order with:
     * - Customer information
     * - Cart items
     * - Shipping details
     * - Payment information
     * - Calculate totals
     */
    private Order processOrder(Cart cart, String customerId, String customerEmail) {
        // TODO: Implement using OrderBuilder
        // 1. Start building order with customer info
        // 2. Add cart items
        // 3. Set shipping details
        // 4. Set payment method
        // 5. Calculate subtotal, tax, total
        // 6. Build and return order
        return null;
    }
    
    /**
     * TODO: Implement payment processing using Factory pattern
     * 
     * Process payment using the configured payment suite:
     * - Create payment processor from factory
     * - Process payment
     * - Generate receipt
     * - Handle any errors
     */
    private void processPayment(Order order) {
        // TODO: Implement using PaymentSuiteFactory
        // 1. Get payment processor from factory
        // 2. Process payment for order total
        // 3. Generate receipt
        // 4. Handle success/failure scenarios
    }
    
    /**
     * TODO: Implement database operations using Singleton pattern
     * 
     * Save order to database:
     * - Connect to database using singleton manager
     * - Save order details
     * - Update inventory
     * - Disconnect from database
     */
    private void saveToDatabase(Order order) {
        // TODO: Implement using DatabaseConnectionManager singleton
        // 1. Connect to database
        // 2. Save order to database
        // 3. Update product inventory
        // 4. Disconnect from database
    }
    
    /**
     * Helper method to calculate order totals
     */
    private BigDecimal calculateSubtotal(List<OrderItem> items) {
        return items.stream()
            .map(item -> item.getUnitPrice().multiply(BigDecimal.valueOf(item.getQuantity())))
            .reduce(BigDecimal.ZERO, BigDecimal::add);
    }
    
    /**
     * Helper method to calculate tax
     */
    private BigDecimal calculateTax(BigDecimal subtotal, String state) {
        // Simple tax calculation - in real app, this would be more complex
        return subtotal.multiply(new BigDecimal("0.08"));
    }
}
