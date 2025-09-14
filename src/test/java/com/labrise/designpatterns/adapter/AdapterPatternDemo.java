package com.labrise.designpatterns.adapter;

import com.labrise.designpatterns.payment.PaymentMethod;
import com.labrise.designpatterns.models.Order;

import java.math.BigDecimal;
import java.util.List;

/**
 * ADAPTER PATTERN DEMO
 * 
 * This demo shows how the Adapter pattern makes incompatible interfaces work together.
 * We'll integrate a third-party payment gateway with our internal payment system.
 */
public class AdapterPatternDemo {
    
    public static void main(String[] args) {
        System.out.println("🔌 Adapter Pattern Demo - Payment Gateway Integration");
        System.out.println("====================================================\n");
        
        // Step 1: Create third-party payment gateway
        System.out.println("1. Creating third-party payment gateway...");
        ThirdPartyPaymentGateway externalGateway = new ThirdPartyPaymentGateway(
            "sk_test_1234567890abcdef",
            "merchant_12345"
        );
        
        // Step 2: Create adapter to make external gateway compatible with our system
        System.out.println("\n2. Creating payment gateway adapter...");
        PaymentGatewayAdapter adapter = new PaymentGatewayAdapter(externalGateway, "merchant_12345");
        
        // Step 3: Test payment processing through adapter
        System.out.println("\n3. Testing payment processing...");
        testPaymentProcessing(adapter);
        
        // Step 4: Test refund processing through adapter
        System.out.println("\n4. Testing refund processing...");
        testRefundProcessing(adapter);
        
        // Step 5: Test receipt generation through adapter
        System.out.println("\n5. Testing receipt generation...");
        testReceiptGeneration(adapter);
        
        System.out.println("\n✅ Adapter Pattern Demo Complete!");
        System.out.println("The adapter successfully bridges the interface differences!");
    }
    
    private static void testPaymentProcessing(PaymentGatewayAdapter adapter) {
        System.out.println("Testing payment processing...");
        
        // Create payment method (our internal format)
        PaymentMethod paymentMethod = new PaymentMethod(
            PaymentMethod.CREDIT_CARD, 
            "4111111111111111", 
            "12/25", 
            "123",
            "John Doe",
            "123 Main St, City, State 12345",
            null
        );
        
        // Create a sample order for payment processing
        Order order = new Order(
            "ORDER_001",
            java.time.LocalDateTime.now(),
            Order.OrderStatus.PENDING,
            "CUST_001",
            "John Doe",
            "john.doe@example.com",
            List.of(), // items
            new BigDecimal("99.99"), // subtotal
            "123 Main St, City, State 12345",
            "Standard Shipping",
            new BigDecimal("9.99"), // shipping cost
            "123 Main St, City, State 12345", // billing address
            "Credit Card",
            null, // discount code
            BigDecimal.ZERO, // discount amount
            new BigDecimal("8.00"), // tax amount
            new BigDecimal("117.98"), // total amount
            false, // gift wrapping
            null, // gift message
            null // special instructions
        );
        
        // Process payment using the interface method
        adapter.process(order);
        
        // Test enhanced methods
        String transactionId = adapter.processPaymentEnhanced(new BigDecimal("99.99"), paymentMethod);
        
        if (transactionId != null) {
            System.out.println("✅ Payment successful! Transaction ID: " + transactionId);
            
            // Check payment status
            boolean isSuccessful = adapter.isPaymentSuccessful(transactionId);
            System.out.println("Payment status check: " + (isSuccessful ? "SUCCESS" : "FAILED"));
        } else {
            System.out.println("❌ Payment failed!");
        }
    }
    
    private static void testRefundProcessing(PaymentGatewayAdapter adapter) {
        System.out.println("Testing refund processing...");
        
        // Create a sample order for refund processing
        Order order = new Order(
            "ORDER_002",
            java.time.LocalDateTime.now(),
            Order.OrderStatus.CANCELLED,
            "CUST_002",
            "Jane Smith",
            "jane.smith@example.com",
            List.of(), // items
            new BigDecimal("50.00"), // subtotal
            "456 Oak Ave, City, State 54321",
            "Standard Shipping",
            new BigDecimal("5.00"), // shipping cost
            "456 Oak Ave, City, State 54321", // billing address
            "Credit Card",
            null, // discount code
            BigDecimal.ZERO, // discount amount
            new BigDecimal("4.00"), // tax amount
            new BigDecimal("59.00"), // total amount
            false, // gift wrapping
            null, // gift message
            null // special instructions
        );
        
        // Process refund using the interface method
        adapter.process(order);
        
        // Test enhanced methods
        String transactionId = "TXN_1234567890";
        String refundId = adapter.processRefundEnhanced(transactionId, new BigDecimal("50.00"));
        
        if (refundId != null) {
            System.out.println("✅ Refund successful! Refund ID: " + refundId);
            
            // Check refund status
            boolean isSuccessful = adapter.isRefundSuccessful(refundId);
            System.out.println("Refund status check: " + (isSuccessful ? "SUCCESS" : "FAILED"));
        } else {
            System.out.println("❌ Refund failed!");
        }
    }
    
    private static void testReceiptGeneration(PaymentGatewayAdapter adapter) {
        System.out.println("Testing receipt generation...");
        
        // Create a sample order for receipt generation
        Order order = new Order(
            "ORDER_003",
            java.time.LocalDateTime.now(),
            Order.OrderStatus.CONFIRMED,
            "CUST_003",
            "Bob Johnson",
            "bob.johnson@example.com",
            List.of(), // items
            new BigDecimal("199.99"), // subtotal
            "789 Pine St, City, State 67890",
            "Express Shipping",
            new BigDecimal("15.00"), // shipping cost
            "789 Pine St, City, State 67890", // billing address
            "Credit Card",
            null, // discount code
            BigDecimal.ZERO, // discount amount
            new BigDecimal("16.00"), // tax amount
            new BigDecimal("230.99"), // total amount
            true, // gift wrapping
            "Happy Birthday!", // gift message
            "Please deliver after 5 PM" // special instructions
        );
        
        // Process receipt using the interface method
        adapter.process(order);
        
        // Test enhanced methods
        String transactionId = "TXN_1234567890";
        String receipt = adapter.generateReceiptEnhanced(transactionId);
        
        if (receipt != null) {
            System.out.println("✅ Receipt generated successfully!");
            System.out.println("Receipt: " + receipt);
            
            // Test email sending
            boolean emailSent = adapter.sendReceiptEnhanced(transactionId, "customer@example.com");
            System.out.println("Email sent: " + (emailSent ? "SUCCESS" : "FAILED"));
        } else {
            System.out.println("❌ Receipt generation failed!");
        }
    }
}
