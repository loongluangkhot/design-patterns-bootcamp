package com.labrise.designpatterns.adapter;

import java.math.BigDecimal;

/**
 * Third-party Payment Gateway (External Service)
 * 
 * This represents a real third-party payment service like Stripe, PayPal, or Square.
 * It has its own interface that's different from our internal payment system.
 * 
 * CHALLENGE: This external service has a different interface than our internal
 * payment processors. How do we make them work together?
 * 
 * This is where the ADAPTER PATTERN comes in!
 */
public class ThirdPartyPaymentGateway {
    
    private final String apiKey;
    private final String merchantId;
    
    public ThirdPartyPaymentGateway(String apiKey, String merchantId) {
        this.apiKey = apiKey;
        this.merchantId = merchantId;
    }
    
    /**
     * External service method - different signature than our internal system
     * 
     * @param amount Amount in cents (not BigDecimal)
     * @param currency 3-letter currency code
     * @param cardNumber Credit card number
     * @param expiryDate MM/YY format
     * @param cvv Security code
     * @return Transaction ID if successful, null if failed
     */
    public String processPayment(int amount, String currency, String cardNumber, String expiryDate, String cvv) {
        // Simulate external API call
        System.out.println("Calling external payment gateway...");
        System.out.println("API Key: " + apiKey);
        System.out.println("Merchant ID: " + merchantId);
        System.out.println("Amount: " + amount + " " + currency);
        System.out.println("Card: ****" + cardNumber.substring(cardNumber.length() - 4));
        
        // Simulate processing
        try {
            Thread.sleep(1000); // Simulate network delay
        } catch (InterruptedException e) {
            Thread.currentThread().interrupt();
        }
        
        // Simulate success/failure
        if (amount > 0 && cardNumber.length() >= 16) {
            return "TXN_" + System.currentTimeMillis();
        } else {
            return null;
        }
    }
    
    /**
     * External service method for refunds
     * 
     * @param transactionId Original transaction ID
     * @param amount Amount to refund in cents
     * @return Refund ID if successful, null if failed
     */
    public String processRefund(String transactionId, int amount) {
        System.out.println("Processing refund via external gateway...");
        System.out.println("Transaction ID: " + transactionId);
        System.out.println("Refund Amount: " + amount);
        
        // Simulate processing
        try {
            Thread.sleep(500);
        } catch (InterruptedException e) {
            Thread.currentThread().interrupt();
        }
        
        return "REF_" + System.currentTimeMillis();
    }
    
    /**
     * External service method for checking transaction status
     * 
     * @param transactionId Transaction to check
     * @return Status string
     */
    public String getTransactionStatus(String transactionId) {
        System.out.println("Checking transaction status...");
        System.out.println("Transaction ID: " + transactionId);
        
        // Simulate API call
        try {
            Thread.sleep(200);
        } catch (InterruptedException e) {
            Thread.currentThread().interrupt();
        }
        
        return "COMPLETED";
    }
}
