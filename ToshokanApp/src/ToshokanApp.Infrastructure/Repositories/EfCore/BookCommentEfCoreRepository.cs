using ToshokanApp.Core.Models;
using ToshokanApp.Core.Repositories;
using ToshokanApp.Infrastructure.Repositories.EfCore.DbContexts;

namespace ToshokanApp.Infrastructure.Repositories.EfCore;

public class BookCommentEfCoreRepository : IBookCommentRepository
{
    private readonly ToshokanDbContext dbContext;
    public BookCommentEfCoreRepository(ToshokanDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task AddAsync(BookComment newComment)
    {
        await dbContext.BookComments.AddAsync(newComment);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid Id)
    {
        dbContext.BookComments.Remove((BookComment)dbContext.BookComments.Where(c => c.Id == Id));
        await dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<BookComment>?> GetAllAsync()
    {
        return dbContext.BookComments;
    }
    

    public async Task<IEnumerable<BookComment>?> GetByIdAsync(Guid id)
    {
        return dbContext.BookComments.Where(bookComment => bookComment.Id == id);
    }
}