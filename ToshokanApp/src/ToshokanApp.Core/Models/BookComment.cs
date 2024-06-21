using System.ComponentModel.DataAnnotations;
using ToshokanApp.Core.Dtos;

namespace ToshokanApp.Core.Models;

public class BookComment
{
    public Book book { get; set; }   
    public IEnumerable<CommentDto>? comments { get ; set; }
}