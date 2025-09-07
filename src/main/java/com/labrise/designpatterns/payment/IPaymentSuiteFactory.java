package com.labrise.designpatterns.payment;

import com.labrise.designpatterns.models.PaymentSuite;

public interface IPaymentSuiteFactory {
    String getMethod();

    default PaymentSuite create() {
        return new PaymentSuite(
            createPaymentProcessor(),
            createRefundProcessor(),
            createReceiptProcessor()
        );
    }
    
    IPaymentProcessor createPaymentProcessor();
    IRefundProcessor createRefundProcessor();
    IReceiptProcessor createReceiptProcessor();
}