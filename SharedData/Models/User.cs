using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SharedData;
public class User
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public string PasswordHash { get; set; }
    
    [Required]
    public string PasswordSalt { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string FullName { get; set; }
    
    public string Role { get; set; }
    
    public bool IsActive { get; set; }
}