using ToshokanApp.Models;

namespace ToshokanApp.Services.Base;
public interface IBookService
{
    Task<IEnumerable<Book>?> GetAllAsync();
    Task<IEnumerable<Book>?> GetByNameAsync(string name);
    Task AddAsync(Book newBook);
}
