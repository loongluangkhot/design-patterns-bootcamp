package com.labrise.designpatterns;

import lombok.AllArgsConstructor;
import lombok.Data;

@Data
@AllArgsConstructor
public class PaymentSuite {
    public IPaymentProcessor paymentProcessor;
    public IRefundProcessor refundProcessor;
    public IReceiptProcessor receiptProcessor;
}
