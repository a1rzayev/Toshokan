namespace ToshokanApp.Models;

public class BookComment
{
    public int Id { get; set; }
    public int BookId { get; set; }
    public int SenderId { get; set; }
    public string? Comment { get; set; }
}
