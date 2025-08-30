package com.labrise.designpatterns.payment.interfaces;

import com.labrise.designpatterns.payment.suites.PaymentSuite;

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