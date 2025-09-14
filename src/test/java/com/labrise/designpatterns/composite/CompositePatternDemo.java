package com.labrise.designpatterns.composite;

import java.math.BigDecimal;
import java.util.List;

import com.labrise.designpatterns.models.Product;
import com.labrise.designpatterns.product.ProductConfigurationBuilder;

/**
 * COMPOSITE PATTERN DEMO
 * 
 * This class demonstrates how to use the Composite pattern for category hierarchy.
 * It shows how to build a tree structure of categories and perform operations on them.
 */
public class CompositePatternDemo {
    
    public static void main(String[] args) {
        System.out.println("=== COMPOSITE PATTERN DEMO ===");
        System.out.println("Building category hierarchy...");
        
        // Create the category hierarchy
        CategoryGroup electronics = new CategoryGroup("Electronics", "All electronic devices and gadgets");
        CategoryGroup accessories = new CategoryGroup("Accessories", "Electronic accessories and peripherals");
        
        ProductCategory smartphones = new ProductCategory("Smartphones", "Mobile phones and smartphones");
        ProductCategory laptops = new ProductCategory("Laptops", "Portable computers and laptops");
        ProductCategory cases = new ProductCategory("Cases", "Phone and laptop cases");
        ProductCategory chargers = new ProductCategory("Chargers", "Charging cables and adapters");
        
        // Build the hierarchy
        electronics.addSubcategory(smartphones);
        electronics.addSubcategory(laptops);
        electronics.addSubcategory(accessories);
        
        accessories.addSubcategory(cases);
        accessories.addSubcategory(chargers);
        
        // Create sample products for each category
        List<Product> smartphoneProducts = createSmartphoneProducts();
        List<Product> laptopProducts = createLaptopProducts();
        List<Product> caseProducts = createCaseProducts();
        List<Product> chargerProducts = createChargerProducts();
        
        // Add products to appropriate categories
        for (Product product : smartphoneProducts) {
            smartphones.addProduct(product);
        }
        
        for (Product product : laptopProducts) {
            laptops.addProduct(product);
        }
        
        for (Product product : caseProducts) {
            cases.addProduct(product);
        }
        
        for (Product product : chargerProducts) {
            chargers.addProduct(product);
        }
        
        // Demonstrate Composite pattern benefits
        System.out.println("\n1. DISPLAYING THE ENTIRE HIERARCHY:");
        System.out.println(electronics.displayHierarchy(0));
        
        System.out.println("\n2. COUNTING PRODUCTS:");
        System.out.println("Total products in Electronics: " + electronics.getTotalProductCount());
        System.out.println("Total products in Accessories: " + accessories.getTotalProductCount());
        System.out.println("Total products in Smartphones: " + smartphones.getTotalProductCount());
        
        System.out.println("\n3. FINDING CATEGORIES:");
        CategoryComponent foundCategory = electronics.findCategory("Smartphones");
        if (foundCategory != null) {
            System.out.println("Found category: " + foundCategory.getName() + " - " + foundCategory.getDescription());
        }
        
        foundCategory = electronics.findCategory("Cases");
        if (foundCategory != null) {
            System.out.println("Found category: " + foundCategory.getName() + " - " + foundCategory.getDescription());
        }
        
        System.out.println("\n4. UNIFORM OPERATIONS ON BOTH LEAF AND COMPOSITE:");
        System.out.println("Smartphones (leaf) - Direct products: " + smartphones.getDirectProducts().size());
        System.out.println("Smartphones (leaf) - All products: " + smartphones.getAllProducts().size());
        System.out.println("Electronics (composite) - Direct products: " + electronics.getDirectProducts().size());
        System.out.println("Electronics (composite) - All products: " + electronics.getAllProducts().size());
        
        System.out.println("\n5. LISTING ALL PRODUCTS IN ELECTRONICS:");
        List<Product> allElectronicsProducts = electronics.getAllProducts();
        for (Product product : allElectronicsProducts) {
            System.out.println("  - " + product.getName() + " ($" + product.getBasePrice() + ")");
        }
        
        System.out.println("\n6. DEMONSTRATING RECURSIVE SEARCH:");
        CategoryComponent deepSearch = electronics.findCategory("Chargers");
        if (deepSearch != null) {
            System.out.println("Found deep category: " + deepSearch.getName());
            System.out.println("Products in Chargers: " + deepSearch.getTotalProductCount());
        }
        
        System.out.println("\n=== COMPOSITE PATTERN DEMO COMPLETE ===");
    }
    
    private static List<Product> createSmartphoneProducts() {
        // iPhone 15 Pro
        Product iphone = new ProductConfigurationBuilder()
            .withBasicInfo("iPhone 15 Pro", "Latest iPhone with titanium design", "Smartphones", 
                          new BigDecimal("999.00"), "USD")
            .withBranding("Apple", "IPH15PRO")
            .withInventory(50, true)
            .build();
        
        // Samsung Galaxy S24
        Product samsung = new ProductConfigurationBuilder()
            .withBasicInfo("Samsung Galaxy S24", "Flagship Android smartphone", "Smartphones", 
                          new BigDecimal("799.99"), "USD")
            .withBranding("Samsung", "SGS24")
            .withInventory(30, true)
            .build();
        
        return List.of(iphone, samsung);
    }
    
    private static List<Product> createLaptopProducts() {
        // MacBook Pro
        Product macbook = new ProductConfigurationBuilder()
            .withBasicInfo("MacBook Pro 14\"", "Professional laptop with M3 chip", "Laptops", 
                          new BigDecimal("1999.00"), "USD")
            .withBranding("Apple", "MBP14M3")
            .withInventory(25, true)
            .build();
        
        // Dell XPS 13
        Product dell = new ProductConfigurationBuilder()
            .withBasicInfo("Dell XPS 13", "Ultrabook with 13-inch display", "Laptops", 
                          new BigDecimal("1299.99"), "USD")
            .withBranding("Dell", "XPS13")
            .withInventory(40, true)
            .build();
        
        return List.of(macbook, dell);
    }
    
    private static List<Product> createCaseProducts() {
        // iPhone Case
        Product iphoneCase = new ProductConfigurationBuilder()
            .withBasicInfo("iPhone 15 Pro Case", "Protective case for iPhone 15 Pro", "Cases", 
                          new BigDecimal("49.99"), "USD")
            .withBranding("Apple", "CASE15PRO")
            .withInventory(100, true)
            .build();
        
        // Laptop Sleeve
        Product laptopSleeve = new ProductConfigurationBuilder()
            .withBasicInfo("Laptop Sleeve", "Protective sleeve for 13-14 inch laptops", "Cases", 
                          new BigDecimal("29.99"), "USD")
            .withBranding("Generic", "SLEEVE13")
            .withInventory(75, true)
            .build();
        
        return List.of(iphoneCase, laptopSleeve);
    }
    
    private static List<Product> createChargerProducts() {
        // USB-C Charger
        Product usbCharger = new ProductConfigurationBuilder()
            .withBasicInfo("USB-C Charger 65W", "Fast charging adapter with USB-C", "Chargers", 
                          new BigDecimal("39.99"), "USD")
            .withBranding("Anker", "USBC65W")
            .withInventory(60, true)
            .build();
        
        // Wireless Charger
        Product wirelessCharger = new ProductConfigurationBuilder()
            .withBasicInfo("Wireless Charging Pad", "Qi-compatible wireless charger", "Chargers", 
                          new BigDecimal("24.99"), "USD")
            .withBranding("Belkin", "WIRELESS15W")
            .withInventory(45, true)
            .build();
        
        return List.of(usbCharger, wirelessCharger);
    }
}
