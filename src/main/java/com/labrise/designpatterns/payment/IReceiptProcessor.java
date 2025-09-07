package com.labrise.designpatterns.payment;

import com.labrise.designpatterns.models.Order;

public interface IReceiptProcessor {
    void process(Order order);
}