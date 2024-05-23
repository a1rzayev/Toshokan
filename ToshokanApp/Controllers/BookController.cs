using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using ToshokanApp.Models;

namespace LibraryApp.Controllers
{
    public class BookController : Controller
    {
        
        private readonly string jsonPath = "Resources/books.json";
        [HttpGet]
        public IActionResult Index(){
            return View();
        }

        // [HttpGet]
        // [ActionName("Get")]
        // [Route("[controller]/[action]")]
        // public async Task<IActionResult> Get()
        // {
        //     var booksJson = await System.IO.File.ReadAllTextAsync(jsonPath);
        //     var books = JsonSerializer.Deserialize<IEnumerable<Book>>(booksJson, new JsonSerializerOptions {
        //         PropertyNameCaseInsensitive = true
        //     });
        //     return Ok(books);
        // }
        
        [HttpGet]
        [ActionName("GetByName")]
        public async Task<IActionResult> GetByName(string? name)
        {
            var booksJson = await System.IO.File.ReadAllTextAsync(jsonPath);
            var books = JsonSerializer.Deserialize<IEnumerable<Book>>(booksJson, new JsonSerializerOptions {
                PropertyNameCaseInsensitive = true
            });
            List<Book> booksByName = new List<Book>();
            foreach (var book in books){
                if (book.Name?.ToLower().Trim() == name?.ToLower().Trim()){
                    booksByName.Add(book);
                }
            }
            ViewData["search"] = name;
            return View(model: booksByName);

        }
        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Add")]
        [Route("[controller]")]
        public async Task<IActionResult> Add([FromForm] Book newBook) {
            var booksJson = await System.IO.File.ReadAllTextAsync(jsonPath);

            var books = JsonSerializer.Deserialize<List<Book>>(booksJson, new JsonSerializerOptions {
                PropertyNameCaseInsensitive = true
            });

            books?.Add(newBook);

            var editedJson = JsonSerializer.Serialize<List<Book>>(books, new JsonSerializerOptions {
                PropertyNameCaseInsensitive = true
            });

            await System.IO.File.WriteAllTextAsync(jsonPath, editedJson);

            return base.RedirectToAction(actionName: "Index");
        }
    }
}
