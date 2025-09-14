package com.labrise.designpatterns.payment;

/**
 * Payment Method - Contains actual payment information
 * 
 * This class represents a payment method with all necessary details
 * for processing payments through different gateways.
 */
public class PaymentMethod {
    
    // Payment method types
    public static final String PAYPAL = "PAYPAL";
    public static final String CREDIT_CARD = "CREDIT_CARD";
    public static final String DEBIT_CARD = "DEBIT_CARD";
    
    private final String methodType;
    private final String cardNumber;
    private final String expiryDate;
    private final String cvv;
    private final String cardholderName;
    private final String billingAddress;
    private final String email; // For PayPal
    
    public PaymentMethod(String methodType, String cardNumber, String expiryDate, String cvv) {
        this(methodType, cardNumber, expiryDate, cvv, null, null, null);
    }
    
    public PaymentMethod(String methodType, String cardNumber, String expiryDate, String cvv, 
                        String cardholderName, String billingAddress, String email) {
        this.methodType = methodType;
        this.cardNumber = cardNumber;
        this.expiryDate = expiryDate;
        this.cvv = cvv;
        this.cardholderName = cardholderName;
        this.billingAddress = billingAddress;
        this.email = email;
    }
    
    // Getters
    public String getMethodType() { return methodType; }
    public String getCardNumber() { return cardNumber; }
    public String getExpiryDate() { return expiryDate; }
    public String getCvv() { return cvv; }
    public String getCardholderName() { return cardholderName; }
    public String getBillingAddress() { return billingAddress; }
    public String getEmail() { return email; }
    
    // Validation methods
    public boolean isValid() {
        if (methodType == null || methodType.trim().isEmpty()) {
            return false;
        }
        
        if (CREDIT_CARD.equals(methodType) || DEBIT_CARD.equals(methodType)) {
            return cardNumber != null && cardNumber.length() >= 16 &&
                   expiryDate != null && expiryDate.matches("\\d{2}/\\d{2}") &&
                   cvv != null && cvv.length() >= 3;
        }
        
        if (PAYPAL.equals(methodType)) {
            return email != null && email.contains("@");
        }
        
        return false;
    }
    
    @Override
    public String toString() {
        return "PaymentMethod{" +
                "methodType='" + methodType + '\'' +
                ", cardNumber='" + maskCardNumber() + '\'' +
                ", expiryDate='" + expiryDate + '\'' +
                ", email='" + email + '\'' +
                '}';
    }
    
    private String maskCardNumber() {
        if (cardNumber == null || cardNumber.length() < 4) {
            return "****";
        }
        return "****" + cardNumber.substring(cardNumber.length() - 4);
    }
}
