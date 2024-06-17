using ToshokanApp.Core.Models;
using ToshokanApp.Core.Repositories;
using ToshokanApp.Infrastructure.Repositories.EfCore.DbContexts;

namespace ToshokanApp.Infrastructure.Repositories.EfCore;

public class CommentEfCoreRepository : ICommentRepository
{
    private readonly ToshokanDbContext dbContext;
    public CommentEfCoreRepository(ToshokanDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task AddAsync(Comment newComment)
    {
        await dbContext.Comments.AddAsync(newComment);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        dbContext.Comments.Remove((Comment)dbContext.Comments.Where(c => c.Id == id));
        await dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Comment>?> GetAllAsync()
    {
        return dbContext.Comments;
    }
    

    public async Task<Comment?> GetByIdAsync(Guid id)
    {
        return dbContext.Comments.FirstOrDefault(bookComment => bookComment.Id == id);
    }
}