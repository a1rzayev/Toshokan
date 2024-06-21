using ToshokanApp.Core.Dtos;
using ToshokanApp.Core.Models;

namespace ToshokanApp.Core.Services;
public interface IBookService
{
    Task<IEnumerable<Book>?> GetAllAsync();
    Task<IEnumerable<Book>?> GetByNameAsync(string name);
    Task<Book?> GetByIdAsync(Guid id);
    IEnumerable<CommentDto>? GetComments(Guid id);
    Task DeleteAsync(Guid id);
    Task AddAsync(Book newBook);
}
