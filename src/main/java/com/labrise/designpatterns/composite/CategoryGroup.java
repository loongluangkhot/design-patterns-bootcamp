package com.labrise.designpatterns.composite;

import java.util.ArrayList;
import java.util.List;

import com.labrise.designpatterns.models.Product;

/**
 * COMPOSITE PATTERN - Composite Component
 * 
 * This represents a category group that can contain both products and subcategories.
 * It's the "composite" in our composite tree structure.
 * 
 * Your task: Implement all the methods from CategoryComponent interface.
 * Focus on:
 * - Managing both products and subcategories
 * - Delegating operations to subcategories when appropriate
 * - Aggregating results from all subcategories
 */
public class CategoryGroup implements CategoryComponent {
    
    private final String name;
    private final String description;
    private final List<Product> products;
    private final List<CategoryComponent> subcategories;
    
    public CategoryGroup(String name, String description) {
        this.name = name;
        this.description = description;
        this.products = new ArrayList<>();
        this.subcategories = new ArrayList<>();
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
        List<Product> allProducts = new ArrayList<>(this.products);
        for (CategoryComponent subcategory : this.subcategories) {
            allProducts.addAll(subcategory.getAllProducts());
        }
        return allProducts;
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
        return this.getAllProducts().size();
    }
    
    @Override
    public CategoryComponent findCategory(String categoryName) {
        if (this.name.equals(categoryName)) {
            return this;
        }
        for (CategoryComponent subcategory : this.subcategories) {
            CategoryComponent foundCategory = subcategory.findCategory(categoryName);
            if (foundCategory != null) {
                return foundCategory;
            }
        }
        return null;
    }
    
    @Override
    public void addSubcategory(CategoryComponent category) {
        if (category == null) {
            throw new IllegalArgumentException("category cannot be null!");
        }
        if (this.subcategories.stream().anyMatch(i -> i.getName().equals(category.getName()))) {
            throw new IllegalArgumentException("category already exists!");
        }

        this.subcategories.add(category);
    }
    
    @Override
    public List<CategoryComponent> getSubcategories() {
        return this.subcategories;
    }
    
    @Override
    public String displayHierarchy(int indent) {
        String result = String.format("%s%s (%d products) - %s", " ".repeat(indent), this.name, this.getTotalProductCount(), this.description);
        for (CategoryComponent subcategory : this.subcategories) {
            result += "\n" + subcategory.displayHierarchy(indent + 2);
        }
        return result;
    }
    
    @Override
    public boolean equals(Object obj) {
        if (obj instanceof CategoryGroup) {
            return this.name.equals(((CategoryGroup)obj).getName());
        }
        return false;
    }
    
    @Override
    public int hashCode() {
        return this.name.hashCode();
    }
}
