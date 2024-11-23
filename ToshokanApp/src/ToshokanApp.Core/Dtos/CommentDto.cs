using System.ComponentModel.DataAnnotations;

namespace ToshokanApp.Core.Dtos;

public class CommentDto
{
    public Guid Id { get; set; }
    public Guid BookId { get; set; }
    public Guid SenderId { get; set; }
    public string? SenderName { get; set; }
    public string? SenderSurname { get; set; }
    public string? Text { get; set; }
}
