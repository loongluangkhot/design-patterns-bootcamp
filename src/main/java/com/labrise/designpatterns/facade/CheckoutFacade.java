package com.labrise.designpatterns.facade;

import com.labrise.designpatterns.models.Order;
import com.labrise.designpatterns.models.OrderItem;
import com.labrise.designpatterns.models.PaymentSuite;
import com.labrise.designpatterns.order.OrderBuilder;
import com.labrise.designpatterns.cart.Cart;

import java.math.BigDecimal;
import java.util.List;

/**
 * Facade that simplifies the complex checkout process
 * This is the main Facade class that provides a simple interface to the complex
 * subsystem
 */
public class CheckoutFacade {

    private final InventoryService inventoryService;
    private final PaymentService paymentService;
    private final ShippingService shippingService;

    public CheckoutFacade() {
        this.inventoryService = new InventoryService();
        this.paymentService = new PaymentService();
        this.shippingService = new ShippingService();
    }
    
    public CheckoutFacade(InventoryService inventoryService, PaymentService paymentService,
            ShippingService shippingService) {
        this.inventoryService = inventoryService;
        this.paymentService = paymentService;
        this.shippingService = shippingService;
    }

    /**
     * Simplified checkout method that handles the entire complex process
     * 
     * @param cart            The shopping cart containing items to checkout
     * @param paymentSuite    The payment information
     * @param shippingMethod  The preferred shipping method
     * @param shippingAddress The delivery address
     * @return The created order if successful, null if failed
     */
    public Order checkout(Cart cart, PaymentSuite paymentSuite,
            ShippingMethod shippingMethod,
            String shippingAddress) {

        List<OrderItem> items = cart.getItems();
        if (items.isEmpty()) {
            return null;
        }

        if (items.stream().anyMatch(i -> !inventoryService.isProductAvailable(i.getProductId(), i.getQuantity()))) {
            return null;
        }

        if (!paymentService.validatePayment(paymentSuite)) {
            return null;
        }

        BigDecimal shippingCost = shippingService.calculateShippingCost(shippingMethod, items.size());
        OrderBuilder orderBuilder = new OrderBuilder();
        Order order = orderBuilder.addOrderItems(items)
            .withShipping(shippingAddress, shippingMethod, shippingCost)
            .build();

        paymentSuite.getPaymentProcessor().process(order);

        for (OrderItem orderItem : items) {
            inventoryService.reserveProduct(orderItem.getProductId(), orderItem.getQuantity());
        }

        return order;
    }

    /**
     * Cancel an order and rollback all operations
     * 
     * @param order The order to cancel
     * @return true if cancellation successful, false otherwise
     */
    public boolean cancelOrder(Order order) {
        // For now, just release inventory since we don't have a proper PaymentSuiteFactory
        for (OrderItem orderItem : order.getItems()) {
            inventoryService.releaseProduct(orderItem.getProductId(), orderItem.getQuantity());
        }

        return true;
    }
}
