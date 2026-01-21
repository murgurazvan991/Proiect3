public class ProductServices
{
    private readonly AppDbContext _db;

    public ProductServices(AppDbContext db)
    {
        _db = db;
    }

    public async Task AddProductAsync(Product product)
    {
        _db.Products.Add(product);
        _db.SaveChanges();
        Console.WriteLine($"Product with id {product.Id} added successfully.");
    }

    public async Task RemoveProductAsync(int productId)
    {
        var product = _db.Products.Find(productId);
        if (product != null)
        {
            _db.Products.Remove(product);
            _db.SaveChanges();
            Console.WriteLine($"Product with id {productId} removed successfully.");
        }
        else
        {
            Console.WriteLine($"Product with id {productId} not found.");
        }
    }

    public async Task UpdateProductAsync(int id, string newName, float newPrice, int newStockQuantity, Category newCategory)
    {
        var product = _db.Products.Find(id);
        if (product != null)
        {
            product.Name = newName;
            product.Price = newPrice;
            product.StockQuantity = newStockQuantity;
            product.CategoryId = newCategory.Id;
            product.Category = newCategory;

            _db.SaveChanges();
            Console.WriteLine($"Product with id {id} updated successfully.");
        }
        else
        {
            Console.WriteLine($"Product with id {id} not found.");
        }
    }

    public List<Product> GetProductsByCategory(string categoryName)
    {
        return _db.Products
                  .Where(p => p.Category.Name.Contains(categoryName))
                  .ToList();
    }

    public async Task<List<Product>> GetAllAsync(string? searchText, int? categoryId)
    {
        var query = _db.Products.AsQueryable();

        if (!string.IsNullOrEmpty(searchText))
        {
            query = query.Where(p => p.Name.Contains(searchText));
        }

        if (categoryId.HasValue)
        {
            query = query.Where(p => p.CategoryId == categoryId.Value);
        }

        return query.ToList();
    }
}