using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using ToshokanApp.Models;
using ToshokanApp.Services.Base;

namespace LibraryApp.Controllers
{
    public class BookController : Controller
    {

        private readonly IDataProtector dataProtector;
        private readonly IBookService bookService;
        public BookController(IBookService bookService, IDataProtectionProvider dataProtectionProvider)
        {
            this.dataProtector = dataProtectionProvider.CreateProtector("identity");
            this.bookService = bookService;
        }
        //private readonly string jsonPath = "Resources/books.json";
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // var authenticationHashedValue = base.HttpContext.Request.Cookies["Authentication"];

            // if(string.IsNullOrWhiteSpace(authenticationHashedValue) == false) {
            // var authenticationValue = this.dataProtector.Unprotect(authenticationHashedValue);
            // if(Guid.TryParse(authenticationValue, out Guid userId)) {

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
}
