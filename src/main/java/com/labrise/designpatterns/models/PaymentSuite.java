package com.labrise.designpatterns.models;

import com.labrise.designpatterns.payment.IPaymentProcessor;
import com.labrise.designpatterns.payment.IReceiptProcessor;
import com.labrise.designpatterns.payment.IRefundProcessor;

import lombok.AllArgsConstructor;
import lombok.Data;

@Data
@AllArgsConstructor
public class PaymentSuite {
    private IPaymentProcessor paymentProcessor;
    private IRefundProcessor refundProcessor;
    private IReceiptProcessor receiptProcessor;
}
