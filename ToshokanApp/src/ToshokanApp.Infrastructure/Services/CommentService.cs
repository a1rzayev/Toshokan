using Microsoft.Extensions.Options;
using ToshokanApp.Core.Models;
using ToshokanApp.Core.Repositories;
using ToshokanApp.Core.Services;

namespace ToshokanApp.Infrastructure.Services;

public class CommentService : ICommentService
{
    private readonly ICommentRepository commentRepository; 
    private readonly IOptionsSnapshot<string> connectionString;
    public CommentService(ICommentRepository commentRepository, IOptionsSnapshot<string> connectionString)
    {
        this.connectionString = connectionString;
        this.commentRepository = commentRepository;
    }
    public async Task AddAsync(Comment comment)
    {
        await this.commentRepository.AddAsync(comment);
    }

    public async Task DeleteAsync(Guid commentId)
    {
        await this.commentRepository.DeleteAsync(commentId);
    }

    public async Task<IEnumerable<Comment>?> GetAllAsync()
    {
        return await this.commentRepository.GetAllAsync();
    }
    public async Task<Comment?> GetByIdAsync(Guid commentId){
        return await this.commentRepository.GetByIdAsync(commentId);
    }

}