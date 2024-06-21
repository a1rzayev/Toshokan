using System.ComponentModel.DataAnnotations;
using ToshokanApp.Core.Resources;

namespace ToshokanApp.Core.Models;

public class UserRole(){
    [Key]
    public Guid UserId { get; set; }
    [Required]
    public string Role { get; set; }
}