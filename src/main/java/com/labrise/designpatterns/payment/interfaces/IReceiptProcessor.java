package com.labrise.designpatterns.payment.interfaces;

import com.labrise.designpatterns.payment.models.Order;

public interface IReceiptProcessor {
    void process(Order order);
}