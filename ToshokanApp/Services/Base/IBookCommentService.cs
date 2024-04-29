namespace ToshokanApp.Services.Base;
using ToshokanApp.Models;
public interface IBookCommentService
{
    Task<IEnumerable<BookComment>?> GetAllAsync(int bookId);
    Task AddAsync(BookComment comment);
    Task DeleteAsync(int commentId);
}
