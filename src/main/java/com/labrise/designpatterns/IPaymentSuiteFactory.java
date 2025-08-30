package com.labrise.designpatterns;

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