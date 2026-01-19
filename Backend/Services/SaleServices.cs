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


    //Mi-o fost lene sa o mai gandesc momentan
    //public string GetSales()
    
}