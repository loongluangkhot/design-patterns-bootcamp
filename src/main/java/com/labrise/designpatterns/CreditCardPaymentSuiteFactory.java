package com.labrise.designpatterns;

public class CreditCardPaymentSuiteFactory implements IPaymentSuiteFactory {

    @Override
    public String getMethod() {
        return PaymentMethod.CREDIT_CARD;
    }
    
    @Override
    public IPaymentProcessor createPaymentProcessor() {
        return new CreditCardPaymentProcessor();
    }

    @Override
    public IRefundProcessor createRefundProcessor() {
        return new CreditCardRefundProcessor();
    }

    @Override
    public IReceiptProcessor createReceiptProcessor() {
        return new CreditCardReceiptProcessor();
    }


    public class CreditCardPaymentProcessor implements IPaymentProcessor {

        @Override
        public void process(Order order) {
            // TODO Auto-generated method stub
            throw new UnsupportedOperationException("Unimplemented method 'process'");
        }
        
    }

    public class CreditCardRefundProcessor implements IRefundProcessor {

        @Override
        public void process(Order order) {
            // TODO Auto-generated method stub
            throw new UnsupportedOperationException("Unimplemented method 'process'");
        }
        
    }

    public class CreditCardReceiptProcessor implements IReceiptProcessor {

        @Override
        public void process(Order order) {
            // TODO Auto-generated method stub
            throw new UnsupportedOperationException("Unimplemented method 'process'");
        }
        
    }

}