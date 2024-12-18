using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using ToshokanApp.Core.Dtos;
using ToshokanApp.Core.Models;
using ToshokanApp.Core.Services;

namespace ToshokanApp.Presentation.Controllers;

public class BookController : Controller
{

    private readonly IConfiguration bookDirConfiguration;
    private readonly IBookService bookService;
    private readonly IIdentityService identityService;
    public BookController(IBookService bookService, IIdentityService identityService, IConfiguration bookDirConfiguration)
    {
        this.bookService = bookService;
        this.identityService = identityService;
        this.bookDirConfiguration = bookDirConfiguration;
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
    public async Task<IActionResult> GetByName(string? name)
    {
        var booksByName = await this.bookService.GetByNameAsync(name);
        return View("Index", booksByName);
    }

    [HttpGet]
    [ActionName("GetById")]
    [AllowAnonymous]
    public async Task<IActionResult> GetById(Guid id)
    {
        var bookById = await this.bookService.GetByIdAsync(id);
        var commentDtos = this.bookService.GetComments(bookById.Id);

        var bookComments = new BookComment
        {
            book = bookById,
            comments = commentDtos
        };

        Guid userId;
        var hashedSenderId = base.HttpContext.Request.Cookies["CurrentUserId"];

        if (string.IsNullOrWhiteSpace(hashedSenderId) == false)
        {
            Guid.TryParse(hashedSenderId, out userId);
            var user = await identityService.GetByIdAsync(userId);
            ViewBag.InPurchased = user.PurchasedBooks.Contains(id);
            ViewBag.InWishlist = user.WishList.Contains(id);
        }       
        else{
            ViewBag.InPurchased = false;
            ViewBag.InWishlist = false;
        }

        base.HttpContext.Response.Cookies.Append("CurrentBookId", bookById.Id.ToString());
        ViewBag.avatarDirPath = bookDirConfiguration["StaticFileRoutes:Avatars"];
        return View("Description", bookComments);
    }

    [HttpPost]
    [ActionName("Add")]
    [Route("api/[controller]/[action]/")]
    [Authorize("RequireAdminAccess")]
    public async Task<IActionResult> Add([FromForm] Book newBook, IFormFile layout, IFormFile bookFile)
    {
        try
        {
            newBook.Id = new Guid();
            newBook.AddedDate = DateTime.Now;


            await this.bookService.AddAsync(newBook);
            if (bookFile != null)
            {
                var extension = Path.GetExtension(bookFile.FileName);
                using var newFileStream = System.IO.File.Create($"{bookDirConfiguration["StaticFileRoutes:Books"]}{newBook.Id}{extension}");
                await bookFile.CopyToAsync(newFileStream);
            }
            var layoutFilePath = $"{bookDirConfiguration["StaticFileRoutes:Layouts"]}{newBook.Id}";

            if (layout == null)
            {
                var defaultLayoutUrl = "https://static.vecteezy.com/system/resources/previews/000/357/095/non_2x/vector-book-icon.jpg";

                var uri = new Uri(defaultLayoutUrl);
                var extension = Path.GetExtension(uri.AbsolutePath);

                using var httpClient = new HttpClient();
                HttpResponseMessage response = null;

                try
                {
                    response = await httpClient.GetAsync(defaultLayoutUrl);
                    response.EnsureSuccessStatusCode();
                }
                catch (HttpRequestException e)
                {
                    throw new Exception("Error fetching default layout from the internet: " + e.Message);
                }

                using var newFileStream = System.IO.File.Create(layoutFilePath + extension);
                await response.Content.CopyToAsync(newFileStream);
            }
            else
            {
                var extension = Path.GetExtension(layout.FileName);

                using var newFileStream = System.IO.File.Create(layoutFilePath + extension);
                await layout.CopyToAsync(newFileStream);
            }
        }
        catch (Exception ex)
        {
            TempData["error"] = ex.Message;
        }

        return base.RedirectToAction("Index");
    }



    [HttpGet]
    [ActionName("DownloadBook")]
    public async Task<IActionResult> DownloadBook(Guid id)
    {
        var book = await this.bookService.GetByIdAsync(id);
        var filePath = $"{bookDirConfiguration["StaticFileRoutes:Books"]}{id}.pdf";

        if (!System.IO.File.Exists(filePath))
        {
            return NotFound();
        }

        var mimeType = "application/pdf";
        var newFileName = $"{book.Name}({book.Author}).pdf";
        var cd = new System.Net.Mime.ContentDisposition
        {
            FileName = newFileName,
            Inline = false,
        };

        Response.Headers.Add("Content-Disposition", cd.ToString());
        return PhysicalFile(filePath, mimeType);
    }


    [HttpGet]
    [ActionName("DeleteBook")]
    [Authorize("RequireAdminAccess")]
    public async Task<IActionResult> Delete(Guid id)
    {
        if (ModelState.IsValid)
        {
            await this.bookService.DeleteAsync(id);
            return base.RedirectToAction("Index");
        }

        return Forbid();
    }
}
