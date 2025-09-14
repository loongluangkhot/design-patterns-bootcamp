package com.labrise.designpatterns.adapter;

import com.labrise.designpatterns.payment.IPaymentProcessor;
import com.labrise.designpatterns.payment.IRefundProcessor;
import com.labrise.designpatterns.payment.IReceiptProcessor;
import com.labrise.designpatterns.payment.PaymentMethod;
import com.labrise.designpatterns.models.Order;

import java.math.BigDecimal;

/**
 * ADAPTER PATTERN - Payment Gateway Adapter
 * 
 * PROBLEM: Third-party payment gateway has different interface than our internal system
 * SOLUTION: Create an adapter that makes the external service compatible with our interface
 * 
 * This adapter implements our internal payment interfaces and translates calls
 * to the external payment gateway's interface.
 * 
 * Note: This adapter provides additional methods beyond the basic interfaces
 * to demonstrate the adapter pattern more comprehensively.
 */
public class PaymentGatewayAdapter implements IPaymentProcessor, IRefundProcessor, IReceiptProcessor {
    
    private final ThirdPartyPaymentGateway externalGateway;
    private final String merchantId;
    
    public PaymentGatewayAdapter(ThirdPartyPaymentGateway externalGateway, String merchantId) {
        this.externalGateway = externalGateway;
        this.merchantId = merchantId;
    }
    
    // ===== Interface Implementations =====
    
    @Override
    public void process(Order order) {
        // This method implements all three interfaces
        // The actual behavior depends on the order status or context
        if (order.getStatus() == Order.OrderStatus.CANCELLED) {
            processRefund(order);
        } else if (order.getStatus() == Order.OrderStatus.CONFIRMED) {
            processReceipt(order);
        } else {
            processPayment(order);
        }
    }
    
    public void processPayment(Order order) {
        System.out.println("Adapter: Processing payment for order: " + order.getOrderId());
        
        try {
            // Extract payment information from order
            String paymentMethod = order.getPaymentMethod();
            BigDecimal totalAmount = order.getTotalAmount();
            
            // Create PaymentMethod object (simplified for demo)
            PaymentMethod pm = new PaymentMethod(
                PaymentMethod.CREDIT_CARD,
                "4111111111111111", // Default test card
                "12/25",
                "123"
            );
            
            // Process payment using our enhanced method
            String transactionId = processPaymentEnhanced(totalAmount, pm);
            
            if (transactionId != null) {
                System.out.println("✅ Payment processed successfully for order: " + order.getOrderId());
            } else {
                System.out.println("❌ Payment failed for order: " + order.getOrderId());
            }
            
        } catch (Exception e) {
            System.out.println("❌ Error processing payment for order: " + e.getMessage());
        }
    }
    
    // ===== Enhanced Payment Methods (Adapter-specific) =====
    
    public String processPaymentEnhanced(BigDecimal amount, PaymentMethod paymentMethod) {
        System.out.println("Adapter: Converting internal payment request to external format...");
        
        try {
            // Validate payment method
            if (!paymentMethod.isValid()) {
                System.out.println("❌ Invalid payment method");
                return null;
            }
            
            // Convert BigDecimal amount to cents (int)
            int amountInCents = convertToCents(amount);
            
            // Extract card details from PaymentMethod
            String cardNumber = extractCardNumber(paymentMethod);
            String expiryDate = extractExpiryDate(paymentMethod);
            String cvv = extractCVV(paymentMethod);
            
            // Call external gateway with correct parameters
            String transactionId = externalGateway.processPayment(
                amountInCents, 
                "USD",
                cardNumber, 
                expiryDate, 
                cvv
            );
            
            if (transactionId != null) {
                System.out.println("✅ Payment processed successfully via external gateway");
                return transactionId;
            } else {
                System.out.println("❌ Payment failed via external gateway");
                return null;
            }
            
        } catch (Exception e) {
            System.out.println("❌ Error processing payment: " + e.getMessage());
            return null;
        }
    }
    
    public boolean isPaymentSuccessful(String transactionId) {
        System.out.println("Adapter: Checking payment status with external gateway...");
        
        try {
            if (transactionId == null || transactionId.trim().isEmpty()) {
                System.out.println("❌ Invalid transaction ID");
                return false;
            }
            
            // Call external gateway to check transaction status
            String status = externalGateway.getTransactionStatus(transactionId);
            
            // Convert external status to boolean
            boolean isSuccessful = "COMPLETED".equals(status);
            
            System.out.println("Payment status: " + status + " -> " + (isSuccessful ? "SUCCESS" : "FAILED"));
            return isSuccessful;
            
        } catch (Exception e) {
            System.out.println("❌ Error checking payment status: " + e.getMessage());
            return false;
        }
    }
    
    // ===== Refund Processing =====
    
    public void processRefund(Order order) {
        System.out.println("Adapter: Processing refund for order: " + order.getOrderId());
        
        try {
            // For refunds, we need the original transaction ID
            // In a real system, this would be stored with the order
            String transactionId = "TXN_" + order.getOrderId();
            BigDecimal refundAmount = order.getTotalAmount();
            
            // Process refund using our enhanced method
            String refundId = processRefundEnhanced(transactionId, refundAmount);
            
            if (refundId != null) {
                System.out.println("✅ Refund processed successfully for order: " + order.getOrderId());
            } else {
                System.out.println("❌ Refund failed for order: " + order.getOrderId());
            }
            
        } catch (Exception e) {
            System.out.println("❌ Error processing refund for order: " + e.getMessage());
        }
    }
    
    public String processRefundEnhanced(String transactionId, BigDecimal amount) {
        System.out.println("Adapter: Processing refund through external gateway...");
        
        try {
            if (transactionId == null || transactionId.trim().isEmpty()) {
                System.out.println("❌ Invalid transaction ID");
                return null;
            }
            
            if (amount == null || amount.compareTo(BigDecimal.ZERO) <= 0) {
                System.out.println("❌ Invalid refund amount");
                return null;
            }
            
            // Convert BigDecimal amount to cents (int)
            int amountInCents = convertToCents(amount);
            
            // Call external gateway refund method
            String refundId = externalGateway.processRefund(transactionId, amountInCents);
            
            if (refundId != null) {
                System.out.println("✅ Refund processed successfully via external gateway");
                return refundId;
            } else {
                System.out.println("❌ Refund failed via external gateway");
                return null;
            }
            
        } catch (Exception e) {
            System.out.println("❌ Error processing refund: " + e.getMessage());
            return null;
        }
    }
    
    public boolean isRefundSuccessful(String refundId) {
        System.out.println("Adapter: Checking refund status with external gateway...");
        
        try {
            if (refundId == null || refundId.trim().isEmpty()) {
                System.out.println("❌ Invalid refund ID");
                return false;
            }
            
            // Check refund status with external gateway
            // Note: In a real implementation, we'd need a specific refund status method
            // For now, we'll simulate by checking if it starts with "REF_"
            boolean isSuccessful = refundId.startsWith("REF_");
            
            System.out.println("Refund status: " + (isSuccessful ? "SUCCESS" : "FAILED"));
            return isSuccessful;
            
        } catch (Exception e) {
            System.out.println("❌ Error checking refund status: " + e.getMessage());
            return false;
        }
    }
    
    // ===== Receipt Processing =====
    
    public void processReceipt(Order order) {
        System.out.println("Adapter: Generating receipt for order: " + order.getOrderId());
        
        try {
            // Generate receipt for the order
            String transactionId = "TXN_" + order.getOrderId();
            String receipt = generateReceiptEnhanced(transactionId);
            
            if (receipt != null) {
                System.out.println("✅ Receipt generated successfully for order: " + order.getOrderId());
                System.out.println("Receipt:\n" + receipt);
                
                // Send receipt to customer
                boolean emailSent = sendReceiptEnhanced(transactionId, order.getCustomerEmail());
                if (emailSent) {
                    System.out.println("✅ Receipt sent to customer: " + order.getCustomerEmail());
                } else {
                    System.out.println("❌ Failed to send receipt to customer");
                }
            } else {
                System.out.println("❌ Failed to generate receipt for order: " + order.getOrderId());
            }
            
        } catch (Exception e) {
            System.out.println("❌ Error processing receipt for order: " + e.getMessage());
        }
    }
    
    public String generateReceiptEnhanced(String transactionId) {
        System.out.println("Adapter: Generating receipt from external gateway data...");
        
        try {
            if (transactionId == null || transactionId.trim().isEmpty()) {
                System.out.println("❌ Invalid transaction ID");
                return null;
            }
            
            // Get transaction details from external gateway
            String status = externalGateway.getTransactionStatus(transactionId);
            
            if (status == null) {
                System.out.println("❌ Transaction not found");
                return null;
            }
            
            // Format receipt according to our internal format
            StringBuilder receipt = new StringBuilder();
            receipt.append("=== PAYMENT RECEIPT ===\n");
            receipt.append("Transaction ID: ").append(transactionId).append("\n");
            receipt.append("Status: ").append(status).append("\n");
            receipt.append("Merchant ID: ").append(merchantId).append("\n");
            receipt.append("Date: ").append(java.time.LocalDateTime.now()).append("\n");
            receipt.append("========================\n");
            
            System.out.println("✅ Receipt generated successfully");
            return receipt.toString();
            
        } catch (Exception e) {
            System.out.println("❌ Error generating receipt: " + e.getMessage());
            return null;
        }
    }
    
    public boolean sendReceiptEnhanced(String transactionId, String email) {
        System.out.println("Adapter: Sending receipt via email...");
        
        try {
            if (transactionId == null || transactionId.trim().isEmpty()) {
                System.out.println("❌ Invalid transaction ID");
                return false;
            }
            
            if (email == null || !email.contains("@")) {
                System.out.println("❌ Invalid email address");
                return false;
            }
            
            // Generate receipt using generateReceiptEnhanced()
            String receipt = generateReceiptEnhanced(transactionId);
            
            if (receipt == null) {
                System.out.println("❌ Failed to generate receipt");
                return false;
            }
            
            // Send email with receipt (simulate)
            System.out.println("Sending email to: " + email);
            System.out.println("Email content:\n" + receipt);
            
            // Simulate email sending delay
            Thread.sleep(500);
            
            System.out.println("✅ Receipt sent successfully via email");
            return true;
            
        } catch (Exception e) {
            System.out.println("❌ Error sending receipt: " + e.getMessage());
            return false;
        }
    }
    
    // ===== Helper Methods =====
    
    /**
     * Convert BigDecimal amount to cents (int)
     * Example: $12.34 -> 1234 cents
     */
    private int convertToCents(BigDecimal amount) {
        // TODO: Implement conversion
        // Hint: multiply by 100 and convert to int
        return amount.multiply(BigDecimal.valueOf(100)).toBigInteger().intValue();
    }
    
    /**
     * Extract card number from PaymentMethod
     */
    private String extractCardNumber(PaymentMethod paymentMethod) {
        if (paymentMethod == null) {
            throw new IllegalArgumentException("PaymentMethod cannot be null");
        }
        
        String cardNumber = paymentMethod.getCardNumber();
        if (cardNumber == null || cardNumber.trim().isEmpty()) {
            throw new IllegalArgumentException("Card number is required");
        }
        
        return cardNumber;
    }
    
    /**
     * Extract expiry date from PaymentMethod
     */
    private String extractExpiryDate(PaymentMethod paymentMethod) {
        if (paymentMethod == null) {
            throw new IllegalArgumentException("PaymentMethod cannot be null");
        }
        
        String expiryDate = paymentMethod.getExpiryDate();
        if (expiryDate == null || expiryDate.trim().isEmpty()) {
            throw new IllegalArgumentException("Expiry date is required");
        }
        
        return expiryDate;
    }
    
    /**
     * Extract CVV from PaymentMethod
     */
    private String extractCVV(PaymentMethod paymentMethod) {
        if (paymentMethod == null) {
            throw new IllegalArgumentException("PaymentMethod cannot be null");
        }
        
        String cvv = paymentMethod.getCvv();
        if (cvv == null || cvv.trim().isEmpty()) {
            throw new IllegalArgumentException("CVV is required");
        }
        
        return cvv;
    }
}
