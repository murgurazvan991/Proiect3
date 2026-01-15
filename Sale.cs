public class Sale
{
    public int Id { get; set; }
    
    public DateOnly Date { get; set; }

    public int UserId { get; set; }

    public float TotalAmount { get; set; }

}