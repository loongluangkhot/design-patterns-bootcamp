package com.labrise.designpatterns.decorator;

import java.math.BigDecimal;

import com.labrise.designpatterns.models.Product;
import com.labrise.designpatterns.product.ProductConfigurationBuilder;

/**
 * DECORATOR PATTERN DEMO
 * 
 * This class demonstrates how to use the Decorator pattern for product pricing.
 * It shows how to chain different pricing decorators to apply multiple pricing rules.
 */
public class DecoratorPatternDemo {
    
    public static void main(String[] args) {
        System.out.println("=== DECORATOR PATTERN DEMO ===");
        System.out.println("Building pricing decorator chains...");
        
        Product product = createSampleProduct();

        ProductPricing basePrice = new BaseProductPricing();
        ProductPricing percentageDiscount = new PercentageDiscountDecorator(basePrice, BigDecimal.valueOf(10), "10% discount");
        ProductPricing fixedDiscount = new FixedAmountDiscountDecorator(basePrice, BigDecimal.valueOf(10), "10% discount");
        ProductPricing percentageDiscountPostTax = new TaxDecorator(percentageDiscount, BigDecimal.valueOf(0.085), "Sales Tax");
        ProductPricing fixedDiscountPostTax = new TaxDecorator(fixedDiscount, BigDecimal.valueOf(0.085), "Sales Tax");
        
        demonstratePricingScenario("Base price only", basePrice, product);
        demonstratePricingScenario("Base price + percentage discount", percentageDiscount, product);
        demonstratePricingScenario("Base price + fixed discount", fixedDiscount, product);
        demonstratePricingScenario("Base price + percentage discount + tax", percentageDiscountPostTax, product);
        demonstratePricingScenario("Base price + fixed discount + tax", fixedDiscountPostTax, product);
    }
    
    private static Product createSampleProduct() {
        return ProductConfigurationBuilder.laptopPreset().build();
    }
    
    private static void demonstratePricingScenario(String scenarioName, ProductPricing pricing, Product product) {
        System.out.println(scenarioName);
        System.out.println(pricing.getPricingDescription(product));
        System.out.println(pricing.calculatePrice(product));
        System.out.println();
    }
}
