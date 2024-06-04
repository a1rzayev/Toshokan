using System.ComponentModel.DataAnnotations;

namespace ToshokanApp.Models;

public class BookComment
{
    [Key]
    public int Id { get; set; }
    public int BookId { get; set; }
    public int SenderId { get; set; }
    [Required]
    [StringLength(1000)]
    public string? Comment { get; set; }
}
