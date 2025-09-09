package com.labrise.designpatterns.order;

import java.math.BigDecimal;
import java.time.LocalDateTime;
import java.util.ArrayList;
import java.util.List;
import java.util.UUID;

import com.labrise.designpatterns.models.Order;
import com.labrise.designpatterns.models.Order.OrderStatus;
import com.labrise.designpatterns.models.OrderItem;

/**
 * Week 1, Day 2: Builder Pattern
 * 
 * LEARNING CONTEXT:
 * - Orders have many optional fields and complex construction
 * - Checkout process is naturally step-by-step
 * - Validation happens at the end, not during construction
 * 
 * PATTERN BENEFITS:
 * - Fluent, readable API that mirrors checkout flow
 * - Handles optional fields elegantly
 * - Centralized validation logic
 * - Immutable final object
 */
public class OrderBuilder {
    // Core order info
    private String orderId;
    private LocalDateTime orderDate;
    private OrderStatus status;
    
    // Customer info
    private String customerId;
    private String customerName;
    private String customerEmail;
    
    // Items
    private List<OrderItem> items;
    private BigDecimal subtotal;
    
    // Shipping
    private String shippingAddress;
    private String shippingMethod;
    private BigDecimal shippingCost;
    
    // Billing
    private String billingAddress;
    private String paymentMethod;
    
    // Discounts
    private String discountCode;
    private BigDecimal discountAmount;
    
    // Final totals
    private BigDecimal taxAmount;
    private BigDecimal totalAmount;
    
    // Optional features
    private boolean giftWrapping;
    private String giftMessage;
    private String specialInstructions;
    
    public OrderBuilder() {
        this.orderId = UUID.randomUUID().toString();
        this.orderDate = LocalDateTime.now();
        this.status = OrderStatus.PENDING;
        this.items = new ArrayList<>();
        this.subtotal = BigDecimal.ZERO;
        this.shippingCost = BigDecimal.ZERO;
        this.discountAmount = BigDecimal.ZERO;
        this.taxAmount = BigDecimal.ZERO;
        this.totalAmount = BigDecimal.ZERO;
    }
    
    // Step 1: Add items from cart
    public OrderBuilder addOrderItems(List<OrderItem> orderItems) {
        this.items.addAll(orderItems);
        this.subtotal = calculateSubtotal();
        return this;
    }
    
    public OrderBuilder addOrderItem(OrderItem item) {
        this.items.add(item);
        this.subtotal = calculateSubtotal();
        return this;
    }
    
    // Step 2: Customer information
    public OrderBuilder withCustomer(String customerId, String customerName, String customerEmail) {
        this.customerId = customerId;
        this.customerName = customerName;
        this.customerEmail = customerEmail;
        return this;
    }
    
    // Step 3: Shipping information
    public OrderBuilder withShipping(String shippingAddress, String shippingMethod, BigDecimal shippingCost) {
        this.shippingAddress = shippingAddress;
        this.shippingMethod = shippingMethod;
        this.shippingCost = shippingCost;
        return this;
    }
    
    // Step 4: Billing information
    public OrderBuilder withBilling(String billingAddress, String paymentMethod) {
        this.billingAddress = billingAddress;
        this.paymentMethod = paymentMethod;
        return this;
    }
    
    // Step 5: Optional discount
    public OrderBuilder withDiscount(String discountCode, BigDecimal discountAmount) {
        this.discountCode = discountCode;
        this.discountAmount = discountAmount;
        return this;
    }
    
    // Step 6: Optional gift wrapping
    public OrderBuilder withGiftWrapping(String giftMessage) {
        this.giftWrapping = true;
        this.giftMessage = giftMessage;
        return this;
    }
    
    // Step 7: Optional special instructions
    public OrderBuilder withSpecialInstructions(String specialInstructions) {
        this.specialInstructions = specialInstructions;
        return this;
    }
    
    // Build method with validation
    public Order build() {
        validateOrder();
        calculateTotals();
        
        Order order = new Order(
            orderId,
            orderDate,
            status,
            customerId,
            customerName,
            customerEmail,
            items,
            subtotal,
            shippingAddress,
            shippingMethod,
            shippingCost,
            billingAddress,
            paymentMethod,
            discountCode,
            discountAmount,
            taxAmount,
            totalAmount,
            giftWrapping,
            giftMessage,
            specialInstructions
        );
        
        return order;
    }
    
    private void validateOrder() {
        if (customerId == null || customerId.trim().isEmpty()) {
            throw new IllegalStateException("Customer ID is required");
        }
        if (items == null || items.isEmpty()) {
            throw new IllegalStateException("Order must contain at least one item");
        }
        if (shippingAddress == null || shippingAddress.trim().isEmpty()) {
            throw new IllegalStateException("Shipping address is required");
        }
        if (paymentMethod == null || paymentMethod.trim().isEmpty()) {
            throw new IllegalStateException("Payment method is required");
        }
    }
    
    private BigDecimal calculateSubtotal() {
        return items.stream()
                .map(OrderItem::getTotalPrice)
                .reduce(BigDecimal.ZERO, BigDecimal::add);
    }
    
    private void calculateTotals() {
        // Calculate tax (simplified - 10% tax rate)
        this.taxAmount = subtotal.multiply(new BigDecimal("0.10"));
        
        // Calculate total
        this.totalAmount = subtotal
                .add(shippingCost)
                .add(taxAmount)
                .subtract(discountAmount);
    }
}