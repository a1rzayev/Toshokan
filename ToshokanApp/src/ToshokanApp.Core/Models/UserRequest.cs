using System.ComponentModel.DataAnnotations;
using ToshokanApp.Core.Resources;

namespace ToshokanApp.Core.Models;

public class UserRequest(){
    [Key]
    public Guid UserId { get; set; }
    [Required]
    public string? Role { get; set; }
}