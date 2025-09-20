package com.labrise.designpatterns.flyweight;

/**
 * Flyweight class for product images
 * This class stores the intrinsic (shared) state of product images
 * to reduce memory usage when many products share the same image
 */
public class ProductImageFlyweight {
    
    // Intrinsic state - shared among many products
    private final String imagePath;
    private final int width;
    private final int height;
    private final String format;
    
    private final byte[] imageData;
    
    public ProductImageFlyweight(String imagePath, int width, int height, String format) {
        this.imagePath = imagePath;
        this.width = width;
        this.height = height;
        this.format = format;
        
        // For demonstration, create a small byte array
        this.imageData = new byte[1024]; // 1KB of sample data
    }
    
    // Getters for intrinsic state
    public String getImagePath() { return imagePath; }
    public int getWidth() { return width; }
    public int getHeight() { return height; }
    public String getFormat() { return format; }
    public byte[] getImageData() { return imageData; }
    
    /**
     * Render the image with specific product context (extrinsic state)
     * @param productId The ID of the product using this image
     * @param displaySize The size to display the image
     * @return A string representation of the rendered image
     */
    public String render(String productId, int displaySize) {
        return String.format("Rendering image %s for product %s at size %d", 
                           imagePath, productId, displaySize);
    }
    
    @Override
    public String toString() {
        return String.format("ProductImageFlyweight{path='%s', size=%dx%d, format='%s'}", 
                           imagePath, width, height, format);
    }
}
