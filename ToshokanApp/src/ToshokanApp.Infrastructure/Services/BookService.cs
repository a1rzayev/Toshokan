using Microsoft.Extensions.Options;
using ToshokanApp.Core.Models;
using ToshokanApp.Core.Repositories;
using ToshokanApp.Core.Services;

namespace ToshokanApp.Infrastructure.Services;

public class BookService : IBookService
{
    private readonly IBookRepository bookRepository; 
    private readonly IOptionsSnapshot<string> connectionString;
    public BookService(IBookRepository bookRepository, IOptionsSnapshot<string> connectionString)
    {
        this.connectionString = connectionString;
        this.bookRepository = bookRepository;
    }
    public async Task AddAsync(Book newBook)
    {
        await this.bookRepository.AddAsync(newBook);
    }

    public async Task DeleteAsync(Guid id)
    {
        await this.bookRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<Book>?> GetAllAsync()
    {
        return await this.bookRepository.GetAllAsync();
    }

    public async Task<Book?> GetByIdAsync(Guid id)
    {
        return await this.bookRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Book>?> GetByNameAsync(string name)
    {
        return await this.bookRepository.GetByNameAsync(name);
    }

    public async Task<IEnumerable<Comment>?> GetComments(Guid id)
    {
        return await this.bookRepository.GetComments(id);
    }
}