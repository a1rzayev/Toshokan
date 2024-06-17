using ToshokanApp.Core.Models;
using ToshokanApp.Core.Repositories;
using ToshokanApp.Infrastructure.Repositories.EfCore.DbContexts;

namespace ToshokanApp.Infrastructure.Repositories.EfCore;

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

    public async Task<Book?> GetByIdAsync(Guid id)
    {
        return dbContext.Books.FirstOrDefault(book => book.Id == id);
    }

    public async Task<IEnumerable<Book>?> GetByNameAsync(string name)
    {
        return dbContext.Books.Where(book => book.Name == name);
    }
    
    public async Task DeleteAsync(Guid id)
    {
        dbContext.Books.Remove((Book)dbContext.Books.Where(c => c.Id == id));
        await dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Comment>?> GetComments(Guid id){
        return dbContext.Comments;//.Where(bookComment => bookComment.BookId == id);
    }
}