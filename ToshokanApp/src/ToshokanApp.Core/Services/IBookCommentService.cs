using ToshokanApp.Core.Models;

namespace ToshokanApp.Core.Services;
public interface IBookCommentService
{
    Task<IEnumerable<BookComment>?> GetAllAsync();
    Task AddAsync(BookComment comment);
    Task DeleteAsync(Guid commentId);
}
