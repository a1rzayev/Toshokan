
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ToshokanApp.Core.Models;
using ToshokanApp.Infrastructure.Repositories.EfCore;
using ToshokanApp.Infrastructure.Repositories.EfCore.DbContexts;
using Xunit;

namespace ToshokanApp.test.ToshokanApp.Infrastructure.UnitTests.Repositories;
public class CommentEfCoreRepositoryUnitTests
{
    private readonly DbContextOptions<ToshokanDbContext> _options;

    public CommentEfCoreRepositoryUnitTests()
    {
        _options = new DbContextOptionsBuilder<ToshokanDbContext>()
            .UseInMemoryDatabase(databaseName: "ToshokanDb")
            .Options;
    }

    private async Task ClearDatabase(ToshokanDbContext context)
    {
        context.Comments.RemoveRange(context.Comments);
        await context.SaveChangesAsync();
    }

    [Fact]
    public async Task AddAsync_ShouldAddNewComment()
    {
        using (var context = new ToshokanDbContext(_options))
        {
            await ClearDatabase(context);
            var repository = new CommentEfCoreRepository(context);
            var newComment = new Comment
            {
                Id = Guid.NewGuid(),
                BookId = Guid.NewGuid(),
                SenderId = Guid.NewGuid(),
                Text = "This is a comment."
            };

            await repository.AddAsync(newComment);
        }

        using (var context = new ToshokanDbContext(_options))
        {
            Assert.Equal(1, await context.Comments.CountAsync());
            var comment = await context.Comments.FirstAsync();
            Assert.Equal("This is a comment.", comment.Text);
        }
    }
    
    [Fact]
    public async Task DeleteAsync_ShouldRemoveCommentWithGivenId()
    {
        var commentId = Guid.NewGuid();

        using (var context = new ToshokanDbContext(_options))
        {
            await ClearDatabase(context);
            context.Comments.Add(new Comment { Id = commentId, BookId = Guid.NewGuid(), SenderId = Guid.NewGuid(), Text = "This is a comment." });
            await context.SaveChangesAsync();
        }

        using (var context = new ToshokanDbContext(_options))
        {
            var repository = new CommentEfCoreRepository(context);
            repository.DeleteAsync(commentId);
        }

        using (var context = new ToshokanDbContext(_options))
        {
            Assert.Equal(0, await context.Comments.CountAsync());
        }
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllComments()
    {
        using (var context = new ToshokanDbContext(_options))
        {
            await ClearDatabase(context);
            context.Comments.AddRange(
                new Comment { Id = Guid.NewGuid(), BookId = Guid.NewGuid(), SenderId = Guid.NewGuid(), Text = "Comment 1" },
                new Comment { Id = Guid.NewGuid(), BookId = Guid.NewGuid(), SenderId = Guid.NewGuid(), Text = "Comment 2" }
            );
            await context.SaveChangesAsync();
        }

        using (var context = new ToshokanDbContext(_options))
        {
            var repository = new CommentEfCoreRepository(context);
            var comments = await repository.GetAllAsync();
            Assert.Equal(2, comments.Count());
        }
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnCommentWithGivenId()
    {
        var commentId = Guid.NewGuid();

        using (var context = new ToshokanDbContext(_options))
        {
            await ClearDatabase(context);
            context.Comments.Add(new Comment { Id = commentId, BookId = Guid.NewGuid(), SenderId = Guid.NewGuid(), Text = "This is a comment." });
            await context.SaveChangesAsync();
        }

        using (var context = new ToshokanDbContext(_options))
        {
            var repository = new CommentEfCoreRepository(context);
            var comment = await repository.GetByIdAsync(commentId);
            Assert.NotNull(comment);
            Assert.Equal(commentId, comment.Id);
        }
    }
}
