package com.labrise.designpatterns.payment.suites;

import lombok.AllArgsConstructor;
import lombok.Data;
import com.labrise.designpatterns.payment.interfaces.IPaymentProcessor;
import com.labrise.designpatterns.payment.interfaces.IRefundProcessor;
import com.labrise.designpatterns.payment.interfaces.IReceiptProcessor;

@Data
@AllArgsConstructor
public class PaymentSuite {
    public IPaymentProcessor paymentProcessor;
    public IRefundProcessor refundProcessor;
    public IReceiptProcessor receiptProcessor;
}
