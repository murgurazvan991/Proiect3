using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharedData;
public class Product
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(150)]
    public string Name { get; set; }

    public float Price { get; set; }

    public int StockQuantity { get; set; }

    public int CategoryId { get; set; }

    [Required]
    [ForeignKey("CategoryId")]
    public Category Category { get; set; }
}