namespace ToshokanApp.Models;

public class Book
{
    public string? Title { get; set; }
    public string? Author { get; set; }
    public int? PublicationYear { get; set; } 
    public string? Genre { get; set; }
    public string? Language { get; set; }
    public string? Description { get; set; }
    public int AddedBy { get; set; }
    public DateTime AddedDate { get; set; }
}