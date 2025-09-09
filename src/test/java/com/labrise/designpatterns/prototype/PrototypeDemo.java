package com.labrise.designpatterns.prototype;

import static org.junit.jupiter.api.Assertions.assertEquals;

import java.math.BigDecimal;

import org.junit.jupiter.api.Test;
import org.springframework.boot.test.context.SpringBootTest;

import com.labrise.designpatterns.models.Product;
import com.labrise.designpatterns.product.ProductConfigurationBuilder;

@SpringBootTest
class PrototypeDemo {

    @Test
    void testPrototype() {
        Product base = ProductConfigurationBuilder
            .laptopPreset()
            .withOption("CPU", "i9", BigDecimal.valueOf(100))
            .withOption("RAM", "32GB", BigDecimal.valueOf(200))
            .withOption("Storage", "1TB SSD", BigDecimal.valueOf(300))
            .build();

        ProductPrototype proto = new ProductPrototype(base);
        Product clone = proto.copy().getProduct();

        assertEquals(base.getName(), clone.getName());
        assertEquals(base.getFinalPrice(), clone.getFinalPrice());
    }
}
