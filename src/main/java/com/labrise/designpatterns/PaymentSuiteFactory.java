package com.labrise.designpatterns;

import java.util.ArrayList;
import java.util.Optional;

/**
 * Week 1, Day 1: Abstract Factory Pattern
 * 
 * LEARNING CONTEXT:
 * - Started with Factory Method (single product creation)
 * - Evolved to Abstract Factory (family of related products)
 * - Key insight: E-commerce operations are temporally separated
 * 
 * PATTERN BENEFITS:
 * - Ensures consistency: PayPal payment → PayPal refund → PayPal receipt
 * - Prevents incompatible combinations
 * - Easy to add new payment methods (Apple Pay, Crypto, etc.)
 * 
 * REAL-WORLD CONSIDERATIONS:
 * - Payment, refund, and receipt happen at different times
 * - Memory efficiency: only create what you need when you need it
 * - Error isolation: one operation failure doesn't affect others
 * 
 * ALTERNATIVES CONSIDERED:
 * - PaymentSuite approach: Would create all objects at once
 * - Rejected because e-commerce operations are temporally separated
 */
public class PaymentSuiteFactory {

    private ArrayList<IPaymentSuiteFactory> _paymentServiceFactories;

    public PaymentSuiteFactory(ArrayList<IPaymentSuiteFactory> paymentServiceFactories) {
        _paymentServiceFactories = paymentServiceFactories;
    }

    private IPaymentSuiteFactory getFactory(String method) {
        Optional<IPaymentSuiteFactory> factory = _paymentServiceFactories
                .stream()
                .filter(i -> i.getMethod().equals(method))
                .findFirst();

        if (factory.isPresent()) {
            return factory.get();
        }

        throw new IllegalArgumentException("Unknown payment method: " + method);
    }

    IPaymentProcessor createPaymentProcessor(String method) {
        return getFactory(method).createPaymentProcessor();
    }

    IRefundProcessor createRefundProcessor(String method) {
        return getFactory(method).createRefundProcessor();
    }

    IReceiptProcessor createReceiptProcessor(String method) {
        return getFactory(method).createReceiptProcessor();
    }

}
