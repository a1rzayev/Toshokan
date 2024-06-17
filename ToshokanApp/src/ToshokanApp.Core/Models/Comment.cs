using System.ComponentModel.DataAnnotations;

namespace ToshokanApp.Core.Models;

public class Comment
{
    [Key]
    public Guid Id { get; set; }
    //[Required]
    public Guid BookId { get; set; }
    //[Required]
    public Guid SenderId { get; set; }
    //[Required]
    [StringLength(1000)]
    public string? Text { get; set; }
}
