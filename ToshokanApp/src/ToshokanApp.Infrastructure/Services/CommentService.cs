using Microsoft.Extensions.Options;
using ToshokanApp.Core.Models;
using ToshokanApp.Core.Repositories;
using ToshokanApp.Core.Services;

namespace ToshokanApp.Infrastructure.Services;

public class CommentService : ICommentService
{
    private readonly ICommentRepository bookCommentRepository; 
    private readonly IOptionsSnapshot<string> connectionString;
    public CommentService(ICommentRepository bookCommentRepository, IOptionsSnapshot<string> connectionString)
    {
        this.connectionString = connectionString;
        this.bookCommentRepository = bookCommentRepository;
    }
    public async Task AddAsync(Comment comment)
    {
        await this.bookCommentRepository.AddAsync(comment);
    }

    public async Task DeleteAsync(Guid commentId)
    {
        await this.bookCommentRepository.DeleteAsync(commentId);
    }

    public async Task<IEnumerable<Comment>?> GetAllAsync()
    {
        return await this.bookCommentRepository.GetAllAsync();
    }
    public async Task<Comment?> GetByIdAsync(Guid bookId){
        return await this.bookCommentRepository.GetByIdAsync(bookId);
    }

}