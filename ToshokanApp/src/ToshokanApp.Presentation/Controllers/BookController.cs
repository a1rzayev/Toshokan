using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using ToshokanApp.Core.Dtos;
using ToshokanApp.Core.Models;
using ToshokanApp.Core.Services;

namespace ToshokanApp.Presentation.Controllers;

public class BookController : Controller
{

    private readonly IDataProtector dataProtector;
    private readonly IBookService bookService;
    public BookController(IBookService bookService, IDataProtectionProvider dataProtectionProvider)
    {
        this.bookService = bookService;
        this.dataProtector = dataProtectionProvider.CreateProtector("book");
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Index()
    {
        base.HttpContext.Response.Cookies.Delete("CurrentBookId");
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
        return View("Index", booksByName);
    }

    [HttpGet]
    [ActionName("GetById")]
    [AllowAnonymous]
    //[Route("[controller]/[action]/{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var bookById = await this.bookService.GetByIdAsync(id);
        var commentDtos = this.bookService.GetComments(bookById.Id);
       
        var bookComments = new BookComment{
            book = bookById,
            comments = commentDtos
        };
        base.HttpContext.Response.Cookies.Append("CurrentBookId", bookById.Id.ToString());
        return View("Description", bookComments);
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

    
    [HttpDelete]
    [Authorize("RequireAdminAccess")]
    [Route("/api/[controller]/[action]")]
    public async Task<IActionResult> Delete(Guid id){
        if (ModelState.IsValid)
        {
            await this.bookService.DeleteAsync(id);
            return base.RedirectToAction("Index");
        }

        return Forbid();
    }
}
