using ToshokanApp.Models;
using ToshokanApp.Repositories.Base;
using System.Text.Json;
using Microsoft.Extensions.Options;
using System.Data;
using Dapper;
using ToshokanApp.Repositories.EfCore.DbContexts;

namespace ToshokanApp.Repositories;

public class BookEfCoreRepository : IBookRepository
{
    private readonly ToshokanDbContext dbContext;
    public BookEfCoreRepository(ToshokanDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task AddAsync(Book newBook)
    {
        await dbContext.Books.AddAsync(newBook);
        await dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Book>?> GetAllAsync()
    {
        return dbContext.Books;
    }

    public Task<IEnumerable<Book>?> GetByNameAsync(string name)
    {
        throw new NotImplementedException();
    }
}