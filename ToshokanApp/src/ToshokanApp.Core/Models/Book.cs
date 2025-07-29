using System.ComponentModel.DataAnnotations;

namespace ToshokanApp.Core.Models;

public class Book
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    [StringLength(100)]
    public string? Name { get; set; }
    [Required]
    [StringLength(100)]
    public string? Author { get; set; }
    [Required]
    public int? PublicationYear { get; set; } 
    [Required]
    [StringLength(50)]
    public string? Genre { get; set; }
    [Required]
    [StringLength(50)]
    public string? Language { get; set; }
    [Required]
    [StringLength(1000)]
    public string? Description { get; set; }
    [Required]
    public DateTime AddedDate { get; set; }
    public string? LayoutUrl { get; set; }
    public string? FileUrl { get; set; }
}