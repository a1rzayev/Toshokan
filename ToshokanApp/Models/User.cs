using System.ComponentModel.DataAnnotations;

namespace ToshokanApp.Models;

public class User
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    [StringLength(50)]
    public string Name { get; set; }
    [Required]
    [StringLength(50)]
    public string Surname { get; set; }
    [Required]
    [StringLength(100)]
    public string Email { get; set; }
    [Required]
    [StringLength(100)]
    public string Password { get; set; }
    public string? AvatarPath { get; set; }
    public List<Guid>? PurchasedBooks { get; set; }
    public List<Guid>? WishList { get; set; }


}