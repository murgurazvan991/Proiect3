public class SaleItem
{
    public int Id { get; set; }

    public int SaleId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public float UnitPrice { get; set; }

    public float LineTotal { get; set; }
}