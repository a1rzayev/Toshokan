using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using ToshokanApp.Core.Models;
using ToshokanApp.Core.Services;

namespace ToshokanApp.Presentation.Controllers;

public class CommentController : Controller
{
    private readonly ICommentService commentService;
    public CommentController(ICommentService commentService)
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
        var currentUserId = HttpContext.Items["CurrentUserId"];
        var currentBookId = HttpContext.Items["CurrentBookId"];
        Guid senderId, bookId;
        Guid.TryParse(HttpContext.User.FindFirst("Id")?.Value, out senderId);
        //Guid.TryParse(currentBookId, out bookId);
        comment.SenderId = senderId;
        if(ModelState.IsValid){
            await commentService.AddAsync(comment);
            return Created();
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