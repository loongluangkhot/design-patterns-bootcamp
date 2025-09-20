package com.labrise.designpatterns.flyweight;

import java.util.ArrayList;
import java.util.List;

/**
 * Manager class that demonstrates the Flyweight pattern
 * This class manages product images using flyweights to optimize memory usage
 */
public class ProductImageManager {
    
    private final List<ProductImageReference> productImages;
    
    public ProductImageManager() {
        this.productImages = new ArrayList<>();
    }
    
    /**
     * Add a product image reference
     * @param productId The ID of the product
     * @param imagePath The path to the image file
     * @param width The image width
     * @param height The image height
     * @param format The image format
     */
    public void addProductImage(String productId, String imagePath, int width, int height, String format) {
        ProductImageFlyweight flyweight = ImageFlyweightFactory.getFlyweight(imagePath, width, height, format);
        ProductImageReference reference = new ProductImageReference(productId, flyweight);
        productImages.add(reference);
    }
    
    /**
     * Render all product images
     * @param displaySize The size to display images
     * @return List of rendered image strings
     */
    public List<String> renderAllImages(int displaySize) {
        List<String> renderedImages = new ArrayList<>();
        for (ProductImageReference reference : productImages) {
            String rendered = reference.render(displaySize);
            renderedImages.add(rendered);
        }
        return renderedImages;
    }
    
    /**
     * Get memory usage statistics
     * @return A string with memory usage information
     */
    public String getMemoryStats() {
        int totalProducts = productImages.size();
        int uniqueImages = ImageFlyweightFactory.getCacheSize();
        int memorySaved = (totalProducts - uniqueImages) * 1024; // Assuming 1KB per image
        
        return String.format("Memory Stats: %d products, %d unique images, %d bytes saved", 
                           totalProducts, uniqueImages, memorySaved);
    }
    
    /**
     * Inner class representing a product's reference to an image flyweight
     * This contains the extrinsic state (product-specific information)
     */
    public static class ProductImageReference {
        private final String productId;
        private final ProductImageFlyweight flyweight;
        
        public ProductImageReference(String productId, ProductImageFlyweight flyweight) {
            this.productId = productId;
            this.flyweight = flyweight;
        }
        
        public String render(int displaySize) {
            return flyweight.render(productId, displaySize);
        }
        
        public String getProductId() { return productId; }
        public ProductImageFlyweight getFlyweight() { return flyweight; }
    }
}
