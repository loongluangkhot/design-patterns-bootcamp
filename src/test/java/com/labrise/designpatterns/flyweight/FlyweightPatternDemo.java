package com.labrise.designpatterns.flyweight;

import java.util.List;

/**
 * Demonstration of the Flyweight Pattern
 * This class shows how the Flyweight pattern optimizes memory usage
 * when many objects share the same intrinsic state
 */
public class FlyweightPatternDemo {
    
    public static void main(String[] args) {
        System.out.println("=== Flyweight Pattern Demo ===\n");
        
        ProductImageManager imageManager = new ProductImageManager();
        
        // Add multiple products with shared images
        // Product 1: "laptop.jpg" (1920x1080, jpg)
        imageManager.addProductImage("LAPTOP-001", "laptop.jpg", 1920, 1080, "jpg");
        
        // Product 2: "laptop.jpg" (1920x1080, jpg) - same image!
        imageManager.addProductImage("LAPTOP-002", "laptop.jpg", 1920, 1080, "jpg");
        
        // Product 3: "phone.jpg" (800x600, jpg)
        imageManager.addProductImage("PHONE-001", "phone.jpg", 800, 600, "jpg");
        
        // Product 4: "laptop.jpg" (1920x1080, jpg) - same as products 1&2!
        imageManager.addProductImage("LAPTOP-003", "laptop.jpg", 1920, 1080, "jpg");
        
        // Product 5: "phone.jpg" (800x600, jpg) - same as product 3!
        imageManager.addProductImage("PHONE-002", "phone.jpg", 800, 600, "jpg");
        
        // Product 6: "tablet.jpg" (1024x768, png) - unique image
        imageManager.addProductImage("TABLET-001", "tablet.jpg", 1024, 768, "png");
        
        // Render all images and show the results
        System.out.println("\nRendering all product images:");
        List<String> renderedImages = imageManager.renderAllImages(200);
        for (String rendered : renderedImages) {
            System.out.println(rendered);
        }
        
        // Show memory optimization statistics
        System.out.println("\n" + imageManager.getMemoryStats());
        System.out.println(ImageFlyweightFactory.getCacheStats());
        
        System.out.println("\nFlyweight Pattern Demo completed!");
    }
}
