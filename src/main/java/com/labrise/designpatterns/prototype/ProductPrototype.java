package com.labrise.designpatterns.prototype;

import java.math.BigDecimal;
import java.util.Map;
import java.util.UUID;

import com.labrise.designpatterns.models.Product;

/**
 * ProductPrototype (scaffold)
 *
 * Purpose: Demonstrate Prototype pattern by cloning a configured Product.
 *
 * You will implement deep-copy semantics to ensure the clone cannot be
 * mutated via shared collections.
 */
public class ProductPrototype implements Prototype<ProductPrototype> {

    // Source properties to clone
    private final Product product;

    public ProductPrototype(Product product) {
        this.product = product;
    }

    public Product getProduct() {
        return product;
    }

    @Override
    public ProductPrototype copy() {
        String uuid = UUID.randomUUID().toString();
        Map<String,String> optionsCopy = Map.copyOf(product.getOptions());
        Map<String,BigDecimal> addOnsCopy = Map.copyOf(product.getAddOns());
        Map<String,BigDecimal> adjustmentsCopy = Map.copyOf(product.getOptionPriceAdjustments());
        Product cloned = new Product(
            uuid,
            product.getName(),
            product.getDescription(),
            product.getCategory(),
            product.getSku(),
            product.getBrand(),
            product.getBasePrice(),
            product.getCurrency(),
            optionsCopy,
            addOnsCopy,
            adjustmentsCopy,
            product.getFinalPrice(),
            product.isActive(),
            product.getStockQuantity()
        );
        return new ProductPrototype(cloned);
    }
}
