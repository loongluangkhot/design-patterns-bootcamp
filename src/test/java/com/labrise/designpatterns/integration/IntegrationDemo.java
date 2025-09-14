package com.labrise.designpatterns.integration;

/**
 * Integration Demo for Creational Patterns
 * 
 * This demo shows how all creational patterns work together
 * in a complete e-commerce workflow.
 */
public class IntegrationDemo {
    
    public static void main(String[] args) {
        System.out.println("🎓 Design Patterns Bootcamp - Day 5: Integration Demo");
        System.out.println("=====================================================\n");
        
        // Create and run the complete e-commerce workflow
        ECommerceWorkflow workflow = new ECommerceWorkflow();
        workflow.demonstrateCompleteWorkflow();
        
        System.out.println("\n🎉 Congratulations! You've seen all creational patterns in action!");
        System.out.println("Next week: Structural Patterns (Adapter, Bridge, Composite, etc.)");
    }
}
