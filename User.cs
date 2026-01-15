public class User
{
    [Key]
    public int Id { get; set; }

    [MaxLength(50)]    
    public string Username { get; set; }
    
    public string PasswordHash { get; set; }
    
    public string PasswordSalt { get; set; }
    
    [MaxLength(100)]
    public string FullName { get; set; }
    
    public string Role { get; set; }
    
    public bool IsActive { get; set; }
}