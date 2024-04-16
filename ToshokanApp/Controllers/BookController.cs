using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using ToshokanApp.Models;

namespace LibraryApp.Controllers
{
    public class BookController : Controller
    {
        private readonly string jsonPath = "Resources/books.json";

        [HttpGet]
        [Route("[controller]")]
        public IActionResult Index(){
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetBooks()
        {
            var booksJson = await System.IO.File.ReadAllTextAsync(jsonPath);
            var books = JsonSerializer.Deserialize<IEnumerable<Book>>(booksJson, new JsonSerializerOptions {
                PropertyNameCaseInsensitive = true
            });
            return Ok();
        }
        [HttpPost]
        public async Task<IActionResult> AddBook(Book newBook) {
            var booksJson = await System.IO.File.ReadAllTextAsync(jsonPath);

            var books = JsonSerializer.Deserialize<List<Book>>(booksJson, new JsonSerializerOptions {
                PropertyNameCaseInsensitive = true
            });

            books?.Add(newBook);

            var resultUsersJson = JsonSerializer.Serialize<List<Book>>(books, new JsonSerializerOptions {
                PropertyNameCaseInsensitive = true
            });

            await System.IO.File.WriteAllTextAsync("Assets/users.json", resultUsersJson);

            return base.RedirectToAction(actionName: "Index");
        }
    }
}
