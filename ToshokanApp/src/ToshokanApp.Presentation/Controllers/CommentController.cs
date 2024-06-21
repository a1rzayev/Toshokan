using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using ToshokanApp.Core.Models;
using ToshokanApp.Core.Services;

namespace ToshokanApp.Presentation.Controllers;

public class CommentController : Controller
{
    private readonly IDataProtector dataProtector;
    private readonly ICommentService commentService;
    public CommentController(ICommentService commentService, IDataProtectionProvider dataProtectionProvider)
    {
        this.commentService = commentService;
        this.dataProtector = dataProtectionProvider.CreateProtector("comment");
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
            //var senderIdValue = this.dataProtector.Unprotect(hashedSenderId);

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

        // var currentUserId = HttpContext.Items["CurrentUserId"];
        // var currentBookId = HttpContext.Items["CurrentBookId"];
        //var hashedSenderId = this.dataProtector.Unprotect(base.HttpContext.Request.Cookies["CurrentUserId"]);
        //Guid senderId, bookId;
        // Guid.TryParse(HttpContext.User.FindFirst("Id")?.Value, out senderId);
        // Guid.TryParse(, out bookId);
        // base.HttpContext.Request.Cookies.Get
        if (ModelState.IsValid)
        {
            await commentService.AddAsync(comment);
            return Created();
            //return base.RedirectToRoute($"Book/GetById?id={comment.BookId}");
        }
        return Forbid();
    }

    [HttpPost]
    [Route("api/[controller]/{bookId}")]
    public async Task<IActionResult> Delete(Guid bookId)
    {
        if (ModelState.IsValid)
        {
            await this.commentService.DeleteAsync(bookId);
            return base.RedirectToAction("Book", "Index");
        }

        return Forbid();
    }
}