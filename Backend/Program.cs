using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("--- Starting Backend Test Runner ---");

        // 1. Initialize Database
        using var db = new AppDbContext();
        
        // This creates the 'store.db' file if it doesn't exist
        Console.WriteLine("Checking database...");
        db.Database.EnsureCreated();
        Console.WriteLine("Database ready.");

        // 2. Initialize Services
        var categoryServices = new CategoryServices(db);
        var productServices = new ProductServices(db);
        var userServices = new UserServices(db);
        var saleServices = new SaleServices(db);

        // ==========================================
        // TEST 1: User Management (Required for Sales)
        // ==========================================
        Console.WriteLine("\n--- Testing User Services ---");
        
        // Check if our Admin user exists, if not, create one
        var adminUser = db.Users.FirstOrDefault(u => u.FullName == "Admin User");
        if (adminUser == null)
        {
            adminUser = new User 
            { 
                FullName = "Admin User", 
                Role = "Admin", 
                IsActive = true,
                PasswordHash = "hashed_secret", // In a real app, hash this properly!
                PasswordSalt = "random_salt"
            };
            userServices.AddUser(adminUser);
        }
        else 
        {
            Console.WriteLine($"Found existing user: {adminUser.FullName}");
        }

        // ==========================================
        // TEST 2: Category Management (Required for Products)
        // ==========================================
        Console.WriteLine("\n--- Testing Category Services ---");
        
        var techCategory = db.Categories.FirstOrDefault(c => c.Name == "Technology");
        if (techCategory == null)
        {
            techCategory = new Category { Name = "Technology" };
            categoryServices.AddCategory(techCategory);
        }
        else
        {
            Console.WriteLine($"Found existing category: {techCategory.Name}");
        }

        // ==========================================
        // TEST 3: Product Management (Async)
        // ==========================================
        Console.WriteLine("\n--- Testing Product Services ---");

        // Create a new product
        var newProduct = new Product
        {
            Name = "Laptop Pro X",
            Price = 1299.99f,
            StockQuantity = 10,
            Category = techCategory,
            CategoryId = techCategory.Id
        };

        // Note: We use 'await' because your ProductServices are async
        await productServices.AddProductAsync(newProduct);

        // Fetch and display products
        Console.WriteLine("Fetching all products...");
        var allProducts = await productServices.GetAllAsync(null, null);
        foreach (var p in allProducts)
        {
            Console.WriteLine($" -> Product: {p.Name} | Price: ${p.Price} | Stock: {p.StockQuantity}");
        }

        // ==========================================
        // TEST 4: Sales Management
        // ==========================================
        Console.WriteLine("\n--- Testing Sale Services ---");

        // Create a Sale record
        var sale = new Sale
        {
            Date = DateOnly.FromDateTime(DateTime.Now),
            UserId = adminUser.Id,
            TotalAmount = newProduct.Price * 2
        };
        saleServices.AddSale(sale);

        // Manually add SaleItems (since SaleServices doesn't have a specific method for items yet)
        var saleItem = new SaleItem
        {
            SaleId = sale.Id,
            ProductId = newProduct.Id,
            Quantity = 2,
            UnitPrice = newProduct.Price,
            LineTotal = newProduct.Price * 2
        };
        db.SaleItems.Add(saleItem);
        db.SaveChanges(); // Commit the items
        Console.WriteLine("Sale Items added manually.");

        // Display Sales Report
        Console.WriteLine("\n--- Sales Report ---");
        var report = saleServices.GetSales();
        Console.WriteLine(report);

        // ==========================================
        // Cleanup (Optional - for demo purposes)
        // ==========================================
        Console.WriteLine("\n--- Demo Completed ---");
        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}