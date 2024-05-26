using ToshokanApp.Models;

namespace ToshokanApp.Services.Base;
public interface IBookCommentService
{
    Task<IEnumerable<BookComment>?> GetAllAsync();
    Task AddAsync(BookComment comment);
    Task DeleteAsync(Guid commentId);
}
