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

    public IActionResult Add()
    {
        return View();
    }

    [HttpPost]
    [Route("[controller]")]
    public async Task<IActionResult> Add(BookComment bookComment)
    {   
        if(ModelState.IsValid){
            await bookCommentService.AddAsync(bookComment);
            return RedirectToAction(nameof(Index));
        }
        return View(bookComment);
    }

    [HttpPost]
    [Route("[controller]/{bookId}")]
    public async Task<IActionResult> Delete(int bookId)
    {
        await this.bookCommentService.DeleteAsync(bookId);

        return base.RedirectToAction(nameof(Index));
    }
}