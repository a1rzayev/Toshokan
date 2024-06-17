using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using ToshokanApp.Core.Models;
using ToshokanApp.Core.Services;

namespace ToshokanApp.Presentation.Controllers;

public class BookCommentController : Controller
{
    private readonly IBookCommentService bookCommentService;
    public BookCommentController(IBookCommentService bookCommentService)
    {
        this.bookCommentService = bookCommentService;
    }

    [HttpGet]
    [Route("[controller]")]
    public async Task<IActionResult> Index()
    {
        var bookComments = await this.bookCommentService.GetAllAsync();

        return base.View(bookComments);
    }

    [HttpPost]
    [Route("api/[controller]")]
    public async Task<IActionResult> Add(BookComment bookComment)
    {   
        if(ModelState.IsValid){
            await bookCommentService.AddAsync(bookComment);
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
            await this.bookCommentService.DeleteAsync(bookId);
            return base.Ok();
        }

        return Forbid();
    }
}