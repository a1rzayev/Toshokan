using ToshokanApp.Models;
using ToshokanApp.Repositories.Base;
using System.Text.Json;
using Microsoft.Extensions.Options;
using System.Data;
using Dapper;
using ToshokanApp.Repositories.EfCore.DbContexts;
using ToshokanApp.Repositories.Base.Functions;

namespace ToshokanApp.Repositories.EfCore;



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
}