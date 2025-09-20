package com.labrise.designpatterns.flyweight;

import java.util.HashMap;
import java.util.Map;

/**
 * Factory for creating and managing ProductImageFlyweight instances
 * This ensures that flyweight objects are shared and reused efficiently
 */
public class ImageFlyweightFactory {
    
    // Cache to store existing flyweight instances
    private static final Map<String, ProductImageFlyweight> flyweightCache = new HashMap<>();
    
    /**
     * Get or create a ProductImageFlyweight for the given image properties
     * @param imagePath The path to the image file
     * @param width The image width
     * @param height The image height
     * @param format The image format (jpg, png, etc.)
     * @return The ProductImageFlyweight instance
     */
    public static ProductImageFlyweight getFlyweight(String imagePath, int width, int height, String format) {
        // Create a unique key from the image properties
        String key = String.format("%s_%dx%d_%s", imagePath, width, height, format);
        
        return flyweightCache.computeIfAbsent(key, k -> 
            new ProductImageFlyweight(imagePath, width, height, format)
        );
    }
    
    /**
     * Get the number of flyweight instances currently cached
     * @return The cache size
     */
    public static int getCacheSize() {
        return flyweightCache.size();
    }
    
    /**
     * Clear the flyweight cache (useful for testing)
     */
    public static void clearCache() {
        flyweightCache.clear();
    }
    
    /**
     * Get cache statistics
     * @return A string with cache information
     */
    public static String getCacheStats() {
        return String.format("Flyweight Cache: %d instances cached", getCacheSize());
    }
}
