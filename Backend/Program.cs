using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SharedData; 

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("--- Starting Backend Test Runner ---");

        using var db = new AppDbContext();
        
        Console.WriteLine("Checking database...");
        db.Database.EnsureCreated();
        Console.WriteLine("Database ready.");

        var productServices = new ProductServices(db);

        Console.WriteLine("\n--- Testing Product Services ---");

        var category = db.Categories.FirstOrDefault(c => c.Name == "Test Category");
        if (category == null)
        {
            category = new Category { Name = "Test Category" };
            db.Categories.Add(category);
            await db.SaveChangesAsync();
        }

        await productServices.AddProductAsync(new Product
        {
            Name = "Console Test Product",
            Price = 99.99f,
            StockQuantity = 5,
            Category = category,
            CategoryId = category.Id
        });

        Console.WriteLine("Fetching products...");
        var allProducts = await productServices.GetAllAsync(null, null);
        foreach (var p in allProducts)
        {
            Console.WriteLine($" -> Product: {p.Name} | Price: ${p.Price}");
        }

        Console.WriteLine("\n--- Demo Completed ---");
    }
}