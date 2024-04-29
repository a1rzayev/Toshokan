using Microsoft.AspNetCore.Mvc;
using ToshokanApp.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using ToshokanApp.Services.Base;

namespace ToshokanApp.Controllers;

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

        return base.View(model: bookComments);
    }

    [HttpPost]
    [Route("[controller]")]
    public IActionResult Add(BookComment bookComment)
    {
        this.bookCommentService.AddAsync(bookComment);

        return base.RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [Route("[controller]/{bookId}")]
    public IActionResult Delete(int bookId)
    {
        this.bookCommentService.DeleteAsync(bookId);

        return base.RedirectToAction(nameof(Index));
    }
}