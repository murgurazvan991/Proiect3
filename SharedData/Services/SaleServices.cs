using Microsoft.EntityFrameworkCore;

namespace SharedData;

public class SaleServices
{
    private readonly AppDbContext _db;

    public SaleServices(AppDbContext db)
    {
        _db = db;
    }

    public async Task AddSaleAsync(Sale sale)
    {
        _db.Sales.Add(sale);
        await _db.SaveChangesAsync();
    }

    public async Task<string> GetSalesReportAsync()
    {
        var sales = await _db.Sales.ToListAsync();
        var saleItems = await _db.SaleItems.ToListAsync();
        
        string result = "--- Sales Report ---\n";
        
        foreach (var sale in sales)
        {
            result += $"Sale ID: {sale.Id} | Date: {sale.Date} | Total: ${sale.TotalAmount}\n";
            
            var items = saleItems.Where(si => si.SaleId == sale.Id);
            foreach (var item in items)
            {
                result += $"   * Product ID: {item.ProductId} - Qty: {item.Quantity}\n";
            }
            result += "\n";
        }
        
        return result;
    }
}