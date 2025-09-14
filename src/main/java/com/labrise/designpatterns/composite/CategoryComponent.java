package com.labrise.designpatterns.composite;

import java.util.List;

import com.labrise.designpatterns.models.Product;

/**
 * COMPOSITE PATTERN - Component Interface
 * 
 * This is the common interface for both leaf nodes (individual categories)
 * and composite nodes (category groups) in our category hierarchy.
 * 
 * The Composite pattern allows us to:
 * 1. Treat individual categories and category groups uniformly
 * 2. Build tree-like structures for organizing products
 * 3. Perform operations on the entire hierarchy recursively
 * 
 * Challenge: How do we organize products into a flexible category system
 * where categories can contain subcategories, and we need to:
 * - Count total products in a category and all its subcategories
 * - Find all products matching certain criteria across the hierarchy
 * - Display the category tree structure
 * - Apply category-specific operations (like bulk discounts)
 */
public interface CategoryComponent {
    
    /**
     * Get the name of this category
     * @return Category name
     */
    String getName();
    
    /**
     * Get the description of this category
     * @return Category description
     */
    String getDescription();
    
    /**
     * Get all products in this category (and subcategories for composite)
     * @return List of all products
     */
    List<Product> getAllProducts();
    
    /**
     * Get direct products in this category only (not subcategories)
     * @return List of direct products
     */
    List<Product> getDirectProducts();
    
    /**
     * Add a product to this category
     * @param product Product to add
     */
    void addProduct(Product product);
    
    /**
     * Remove a product from this category
     * @param productId Product ID to remove
     * @return true if removed, false if not found
     */
    boolean removeProduct(String productId);
    
    /**
     * Get total count of products in this category and all subcategories
     * @return Total product count
     */
    int getTotalProductCount();
    
    /**
     * Find a subcategory by name
     * @param categoryName Name to search for
     * @return CategoryComponent if found, null otherwise
     */
    CategoryComponent findCategory(String categoryName);
    
    /**
     * Add a subcategory
     * @param category Category to add as subcategory
     */
    void addSubcategory(CategoryComponent category);
    
    /**
     * Get all subcategories
     * @return List of subcategories
     */
    List<CategoryComponent> getSubcategories();
    
    /**
     * Display the category hierarchy as a tree structure
     * @param indent Current indentation level
     * @return String representation of the tree
     */
    String displayHierarchy(int indent);
}
