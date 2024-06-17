using Microsoft.AspNetCore.Authorization;
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
    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        var books = await this.bookService.GetAllAsync();
        return View(books);
    }

    [HttpGet]
    [ActionName("GetByName")]
    [AllowAnonymous]
    //[Route("[controller]/[action]/{name}")]
    public async Task<IActionResult> GetByName(string? name)
    {
        var booksByName = await this.bookService.GetByNameAsync(name);
        return View("Description", booksByName);
    }

    [HttpGet]
    [ActionName("GetById")]
    [AllowAnonymous]
    //[Route("[controller]/[action]/{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var bookById = await this.bookService.GetByIdAsync(id);
        return View("Description", bookById);
    }

    // [ActionName("Add")]
    // [Route("[controller]/[action]/")]
    // [Authorize("RequireAdminAccess")]
    // public IActionResult Add()
    // {
    //     return base.View();
    // }

    [HttpPost]
    [ActionName("Add")]
    [Route("api/[controller]/[action]/")]
    [Authorize("RequireAdminAccess")]
    public async Task<IActionResult> Add([FromForm] Book newBook)
    {
        await this.bookService.AddAsync(newBook);
        return base.RedirectToAction("Index");
    }
}
