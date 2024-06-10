using System.ComponentModel.DataAnnotations;

namespace ToshokanApp.Models;

public class BookComment
{
    [Key]
    public Guid Id { get; set; }
    public Guid BookId { get; set; }
    public Guid SenderId { get; set; }
    [Required]
    [StringLength(1000)]
    public string? Comment { get; set; }
}
