package com.labrise.designpatterns.models;

import lombok.Value;

import java.math.BigDecimal;
import java.time.LocalDateTime;
import java.util.List;

@Value
public class Order {
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
    
    public enum OrderStatus {
        PENDING, CONFIRMED, PROCESSING, SHIPPED, DELIVERED, CANCELLED
    }
}
