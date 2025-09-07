package com.labrise.designpatterns.models;

import lombok.Value;

import java.math.BigDecimal;

@Value
public class OrderItem {
    private String productId;
    private String productName;
    private int quantity;
    private BigDecimal unitPrice;
    private BigDecimal totalPrice;
}
