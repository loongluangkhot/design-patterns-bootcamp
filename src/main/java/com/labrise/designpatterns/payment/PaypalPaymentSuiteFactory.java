package com.labrise.designpatterns.payment;

import com.labrise.designpatterns.models.Order;

public class PaypalPaymentSuiteFactory implements IPaymentSuiteFactory {

    @Override
    public String getMethod() {
        return PaymentMethod.PAYPAL;
    }

    @Override
    public IPaymentProcessor createPaymentProcessor() {
        return new PaypalPaymentProcessor();
    }

    @Override
    public IRefundProcessor createRefundProcessor() {
        return new PaypalRefundProcessor();
    }

    @Override
    public IReceiptProcessor createReceiptProcessor() {
        return new PaypalReceiptProcessor();
    }


    public class PaypalPaymentProcessor implements IPaymentProcessor {

        @Override
        public void process(Order order) {
            throw new UnsupportedOperationException("Unimplemented method 'process'");
        }
        
    }

    public class PaypalRefundProcessor implements IRefundProcessor {

        @Override
        public void process(Order order) {
            throw new UnsupportedOperationException("Unimplemented method 'process'");
        }
        
    }

    public class PaypalReceiptProcessor implements IReceiptProcessor {

        @Override
        public void process(Order order) {
            throw new UnsupportedOperationException("Unimplemented method 'process'");
        }
        
    }

}