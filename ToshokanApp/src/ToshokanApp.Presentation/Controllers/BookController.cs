using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using ToshokanApp.Core.Models;
using ToshokanApp.Core.Services;

namespace ToshokanApp.Presentation.Controllers;

public class BookController : Controller
{

    private readonly IDataProtector dataProtector;
    private readonly IBookService bookService;
    public BookController(IBookService bookService, IDataProtectionProvider dataProtectionProvider)
    {
        this.dataProtector = dataProtectionProvider.CreateProtector("identity");
        this.bookService = bookService;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var books = await this.bookService.GetAllAsync();
        return View(model: books);
    }

    [HttpGet]
    [ActionName("GetByName")]
    public async Task<IActionResult> GetByName(string? name)
    {
        var booksByName = await this.bookService.GetByNameAsync(name);
        return View(model: booksByName);

    }

    [HttpPost]
    [ActionName("Add")]
    [Route("[controller]")]
    public async Task<IActionResult> Add([FromForm] Book newBook)
    {
        await this.bookService.AddAsync(newBook);
        return base.RedirectToAction(actionName: "Index");
    }
}
