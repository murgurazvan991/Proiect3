using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SharedData; // Import your shared library

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("--- Starting Backend Test Runner ---");

        // 1. Initialize Database
        using var db = new AppDbContext();
        
        // This creates the 'store.db' file if it doesn't exist (replaces migrations)
        Console.WriteLine("Checking database...");
        db.Database.EnsureCreated();
        Console.WriteLine("Database ready.");

        // 2. Initialize Services
        var productServices = new ProductServices(db);

        // ==========================================
        // TEST: Add a Product
        // ==========================================
        Console.WriteLine("\n--- Testing Product Services ---");

        // Ensure we have a category
        var category = db.Categories.FirstOrDefault(c => c.Name == "Test Category");
        if (category == null)
        {
            category = new Category { Name = "Test Category" };
            db.Categories.Add(category);
            await db.SaveChangesAsync();
        }

        // Add Product
        await productServices.AddProductAsync(new Product
        {
            Name = "Console Test Product",
            Price = 99.99f,
            StockQuantity = 5,
            Category = category,
            CategoryId = category.Id
        });

        // ==========================================
        // TEST: Read Products
        // ==========================================
        Console.WriteLine("Fetching products...");
        var allProducts = await productServices.GetAllAsync(null, null);
        foreach (var p in allProducts)
        {
            Console.WriteLine($" -> Product: {p.Name} | Price: ${p.Price}");
        }

        Console.WriteLine("\n--- Demo Completed ---");
    }
}