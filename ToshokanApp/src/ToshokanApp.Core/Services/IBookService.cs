using ToshokanApp.Core.Models;

namespace ToshokanApp.Core.Services;
public interface IBookService
{
    Task<IEnumerable<Book>?> GetAllAsync();
    Task<IEnumerable<Book>?> GetByNameAsync(string name);
    Task<IEnumerable<Book>?> GetByIdAsync(Guid id);
    Task AddAsync(Book newBook);
}
