public class ProductServices
{
    private readonly AppDbContext _db;

    public ProductServices(AppDbContext db)
    {
        _db = db;
    }

    public void AddProduct(Product product)
    {
        _db.Products.Add(product);
        _db.SaveChanges();
        Console.WriteLine($"Product with id {product.Id} added successfully.");
    }

    public void RemoveProduct(int productId)
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

    public void UpdateProduct(int id, string newName, float newPrice, int newStockQuantity, Category newCategory)
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

}