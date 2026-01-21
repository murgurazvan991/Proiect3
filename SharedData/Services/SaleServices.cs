namespace SharedData;
public class SaleServices
{
    private readonly AppDbContext _db;

    public SaleServices(AppDbContext db)
    {
        _db = db;
    }

    public void AddSale(Sale sale)
    {
        _db.Sales.Add(sale);
        _db.SaveChanges();
        Console.WriteLine($"Sale with id {sale.Id} added successfully.");
    }


    public string GetSales()
    {
        var sales = _db.Sales.ToList();
        var saleItems = _db.SaleItems.ToList();
        
        string result = "";
        
        foreach (var sale in sales)
        {
            result += $"Sale ID: {sale.Id}, Date: {sale.Date}, User ID: {sale.UserId}, Total Amount: {sale.TotalAmount}\n";
            
            var itemsFromSale = saleItems.Where(si => si.SaleId == sale.Id).ToList();
            
            foreach (var item in itemsFromSale)
            {
                result += $"  - Item ID: {item.Id}, Product ID: {item.ProductId}, Quantity: {item.Quantity}, Unit Price: {item.UnitPrice}, Line Total: {item.LineTotal}\n";
            }
        }
        
        return result;
    }
}