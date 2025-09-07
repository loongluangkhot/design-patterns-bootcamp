package com.labrise.designpatterns.payment;

import com.labrise.designpatterns.models.Order;

public interface IRefundProcessor {
    void process(Order order);
}