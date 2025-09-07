package com.labrise.designpatterns.payment;

import com.labrise.designpatterns.models.Order;

public interface IPaymentProcessor {
    void process(Order order);
}