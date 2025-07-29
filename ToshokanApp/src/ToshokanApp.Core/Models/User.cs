using System.ComponentModel.DataAnnotations;
using ToshokanApp.Core.Resources;

namespace ToshokanApp.Core.Models;

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
    public List<Guid>? PurchasedBooks { get; set; }
    public List<Guid>? WishList { get; set; }
    [Required]
    public bool EmailVerified { get; set; }
    public string? AvatarUrl { get; set; }
}