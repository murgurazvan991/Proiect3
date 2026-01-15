public class Product
{
    [Key]
    public int Id { get; set; }

    [MaxLength(150)]
    public string Name { get; set; }

    public float Price { get; set; }

    public int StockQuantity { get; set; }

    public int CategoryId { get; set; }

    [ForeignKey("CategoryId")]
    public Category Category { get; set; }
}