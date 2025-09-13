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
            System.out.println("[PayPal] Processing payment for order " + order.getOrderId() +
                ": total=" + order.getTotalAmount() + ", customer=" + order.getCustomerEmail());
        }
        
    }

    public class PaypalRefundProcessor implements IRefundProcessor {

        @Override
        public void process(Order order) {
            System.out.println("[PayPal] Processing refund for order " + order.getOrderId() +
                ": amount=" + order.getTotalAmount());
        }
        
    }

    public class PaypalReceiptProcessor implements IReceiptProcessor {

        @Override
        public void process(Order order) {
            System.out.println("[PayPal] Sending receipt for order " + order.getOrderId() +
                " to " + order.getCustomerEmail());
        }
        
    }

}