package com.labrise.designpatterns.composite;

import java.util.ArrayList;
import java.util.List;

import com.labrise.designpatterns.models.Product;

/**
 * COMPOSITE PATTERN - Leaf Component
 * 
 * This represents an individual category that can contain products directly.
 * It's the "leaf" in our composite tree structure.
 * 
 * Your task: Implement all the methods from CategoryComponent interface.
 * Focus on:
 * - Managing the list of products in this category
 * - Implementing the required operations for a leaf node
 * - Proper null checks and validation
 */
public class ProductCategory implements CategoryComponent {
    
    private final String name;
    private final String description;
    private final List<Product> products;
    
    public ProductCategory(String name, String description) {
        if (name == null || name.isBlank()) {
            throw new IllegalArgumentException("name cannot be null or empty!");
        }

        this.name = name;
        this.description = description;
        this.products = new ArrayList<>();
    }
    
    @Override
    public String getName() {
        return this.name;
    }
    
    @Override
    public String getDescription() {
        return this.description;
    }
    
    @Override
    public List<Product> getAllProducts() {
        return this.getDirectProducts();
    }
    
    @Override
    public List<Product> getDirectProducts() {
        return this.products;
    }
    
    @Override
    public void addProduct(Product product) {
        if (product == null) {
            throw new IllegalArgumentException("product cannot be null!");
        }
        if (this.products.stream().anyMatch(i -> i.getProductId().equals(product.getProductId()))) {
            throw new IllegalArgumentException("product already exists!");
        }

        this.products.add(product);
    }
    
    @Override
    public boolean removeProduct(String productId) {
        return this.products.removeIf(i -> i.getProductId().equals(productId));
    }
    
    @Override
    public int getTotalProductCount() {
        return this.products.size();
    }
    
    @Override
    public CategoryComponent findCategory(String categoryName) {
        if (this.name.equals(categoryName)) {
            return this;
        }
        return null;
    }
    
    @Override
    public void addSubcategory(CategoryComponent category) {
        throw new UnsupportedOperationException("Implement this method");
    }
    
    @Override
    public List<CategoryComponent> getSubcategories() {
        return new ArrayList<>();
    }
    
    @Override
    public String displayHierarchy(int indent) {
        return String.format("%s%s (%d products) - %s", " ".repeat(indent), this.name, this.getTotalProductCount(), this.description);
    }
    
    @Override
    public boolean equals(Object obj) {
        if (obj instanceof ProductCategory) {
            return this.name.equals(((ProductCategory)obj).getName());
        }
        return false;
    }
    
    @Override
    public int hashCode() {
        return this.name.hashCode();
    }
}
