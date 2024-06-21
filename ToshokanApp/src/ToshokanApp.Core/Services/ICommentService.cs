using ToshokanApp.Core.Models;

namespace ToshokanApp.Core.Services;
public interface ICommentService
{
    Task<IEnumerable<Comment>?> GetAllAsync();
    Task AddAsync(Comment comment);
    Task DeleteAsync(Guid id);
    Task<Comment?> GetByIdAsync(Guid id);
}
