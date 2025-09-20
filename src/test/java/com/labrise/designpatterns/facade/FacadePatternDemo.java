package com.labrise.designpatterns.facade;

import com.labrise.designpatterns.cart.Cart;
import com.labrise.designpatterns.models.Order;
import com.labrise.designpatterns.models.OrderItem;
import com.labrise.designpatterns.models.PaymentSuite;
import com.labrise.designpatterns.payment.IPaymentProcessor;
import com.labrise.designpatterns.payment.IReceiptProcessor;
import com.labrise.designpatterns.payment.IRefundProcessor;

import java.math.BigDecimal;

/**
 * Demonstration of the Facade Pattern
 * This class shows how the CheckoutFacade simplifies the complex checkout process
 */
public class FacadePatternDemo {
    
    public static void main(String[] args) {
        System.out.println("=== Facade Pattern Demo ===\n");
        
        // Create a shopping cart with some products
        Cart cart = new Cart();
        
        cart.addItem(new OrderItem("123", "Laptop", 1, BigDecimal.valueOf(999.99), BigDecimal.valueOf(999.99)));
        cart.addItem(new OrderItem("456", "Mouse", 2, BigDecimal.valueOf(29.99), BigDecimal.valueOf(59.98)));
        
        System.out.println("Cart contents:");
        cart.getItems().forEach(item -> 
            System.out.println("- " + item.getProductName() + " x" + item.getQuantity() + " = $" + item.getTotalPrice())
        );
        
        // Create payment information
        IPaymentProcessor paymentProcessor = new MockPaymentProcessor();
        IRefundProcessor refundProcessor = new MockRefundProcessor();
        IReceiptProcessor receiptProcessor = new MockReceiptProcessor();
        PaymentSuite payment = new PaymentSuite(paymentProcessor, refundProcessor, receiptProcessor);
        
        // Use the CheckoutFacade to process the checkout
        CheckoutFacade checkoutFacade = new CheckoutFacade();
        
        System.out.println("\nProcessing checkout...");
        Order order = checkoutFacade.checkout(cart, payment, 
            ShippingMethod.STANDARD, "123 Main St, City, State 12345");
        
        if (order != null) {
            System.out.println("✅ Checkout successful!");
            System.out.println("Order ID: " + order.getOrderId());
            System.out.println("Total: $" + order.getTotalAmount());
            System.out.println("Shipping: " + order.getShippingMethod());
        } else {
            System.out.println("❌ Checkout failed!");
        }
        
        System.out.println("\nFacade Pattern Demo completed!");
    }
    
    // Mock implementations for demonstration
    static class MockPaymentProcessor implements IPaymentProcessor {
        @Override
        public void process(Order order) {
            System.out.println("Processing payment for order: " + order.getOrderId());
        }
    }
    
    static class MockRefundProcessor implements IRefundProcessor {
        @Override
        public void process(Order order) {
            System.out.println("Processing refund for order: " + order.getOrderId());
        }
    }
    
    static class MockReceiptProcessor implements IReceiptProcessor {
        @Override
        public void process(Order order) {
            System.out.println("Generating receipt for order: " + order.getOrderId());
        }
    }
}
