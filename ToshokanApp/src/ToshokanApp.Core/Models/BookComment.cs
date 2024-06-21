using System.ComponentModel.DataAnnotations;

namespace ToshokanApp.Core.Models;

public class BookComment
{
    public Book book { get; set; }   
    public IEnumerable<Comment>? comments { get ; set; }
}