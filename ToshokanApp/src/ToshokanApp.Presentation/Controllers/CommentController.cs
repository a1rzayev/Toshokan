using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using ToshokanApp.Core.Models;
using ToshokanApp.Core.Services;

namespace ToshokanApp.Presentation.Controllers;

public class CommentController : Controller
{
    private readonly ICommentService commentService;
    public CommentController(ICommentService commentService, IDataProtectionProvider dataProtectionProvider)
    {
        this.commentService = commentService;
    }

    [HttpGet]
    [Route("[controller]")]
    public async Task<IActionResult> Index()
    {
        var comments = await this.commentService.GetAllAsync();

        return base.View(comments);
    }

    [HttpPost]
    [Route("api/[controller]")]
    public async Task<IActionResult> Add(Comment comment)
    {
        Guid senderId;
        var hashedSenderId = base.HttpContext.Request.Cookies["CurrentUserId"];

        if (string.IsNullOrWhiteSpace(hashedSenderId) == false)
        {

            if (Guid.TryParse(hashedSenderId, out senderId))
            {

                comment.SenderId = senderId;
            }
        }

        Guid bookId;
        var hashedBookId = base.HttpContext.Request.Cookies["CurrentBookId"];

        if (string.IsNullOrWhiteSpace(hashedBookId) == false)
        {

            if (Guid.TryParse(hashedBookId, out bookId))
            {
                comment.BookId = bookId;

            }
        }
        if (ModelState.IsValid)
        {
            await commentService.AddAsync(comment);
            return base.RedirectToAction("GetById", "Book", new
            {
                id = comment.BookId
            });
        }
        return Forbid();
    }

    [HttpGet]
    [ActionName("Delete")]
    [Route("api/[controller]")]
    public async Task<IActionResult> Delete(Guid commentId)
    {
        if (ModelState.IsValid)
        {
            var comment = await this.commentService.GetByIdAsync(commentId);

            if (comment != null)
            {
                var bookId = comment?.BookId;
                await this.commentService.DeleteAsync(commentId);
            }
            return base.RedirectToAction("GetById", "Book", new
            {
                id = comment.BookId
            });

        }

        return Forbid();
    }
}