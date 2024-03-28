using System.Net;
using System.Text.Json;
using ToshokanApp.Extensions;
using ToshokanApp.Models;

async Task LayoutAsync(HttpListenerResponse response, string bodyHtml, string layoutName = "Home") {
    response.ContentType = "text/html";
    using var streamWriter = new StreamWriter(response.OutputStream);

    var html = (await File.ReadAllTextAsync($"Views/{layoutName}.html")).Replace("{{books}}", bodyHtml);

    await streamWriter.WriteLineAsync(html);
    response.StatusCode = (int)HttpStatusCode.OK;
}

async Task NotFoundAsync(HttpListenerResponse response, string resourceName) {
    Dictionary<string, object>? viewValues = new() {
        {"resource", resourceName}
    };

    await WriteViewAsync(response, "NotFound", viewValues);
}

async Task WriteViewAsync(HttpListenerResponse response, string viewName, Dictionary<string, object>? viewValues = null, string? layoutName = null)
{
    var html = await File.ReadAllTextAsync($"Views/{viewName}.html");

    if (viewValues is not null)
    {
        foreach (var viewValue in viewValues)
            html = html.Replace("{{" + viewValue.Key + "}}", viewValue.Value.ToString());
    }
    
    await LayoutAsync(response, html, layoutName ?? "Home");
}

HttpListener httpListener = new HttpListener();

const int port = 8080;
httpListener.Prefixes.Add($"http://*:{port}/");

Console.WriteLine($"server started on port {port}");

httpListener.Start();

while(true){
    var context = await httpListener.GetContextAsync();

    string? endpoint = context.Request.RawUrl;

    Console.WriteLine($"endpoint: {endpoint}");
switch (endpoint)
    {
        case "/":
        case "/Home.html":
            {
                var booksJson = await File.ReadAllTextAsync("Json/books.json");
                var books = JsonSerializer.Deserialize<IEnumerable<Book>>(booksJson);
                
                if(books is not null && books.Any()) {
                    var html = books.AsHtml();
                    await LayoutAsync(context.Response, html);
                }
                else {
                    await NotFoundAsync(context.Response, nameof(books));
                }
                break;
            }
        case "/Users":
        case "/Users.html":
            {
                var usersJson = await File.ReadAllTextAsync("Json/users.json");
                var users = JsonSerializer.Deserialize<IEnumerable<User>>(usersJson);
                
                if(users is not null && users.Any()) {
                    var html = users.AsHtml();
                    await LayoutAsync(context.Response, html);
                }
                else {
                    await NotFoundAsync(context.Response, nameof(users));
                }
                break;
            }
        case "/Add":
        case "/Add.html":
            {
                context.Response.ContentType = "text/html";
                using var streamWriter = new StreamWriter(context.Response.OutputStream);
                await streamWriter.WriteLineAsync(await File.ReadAllTextAsync($"Views/Add.html"));
                context.Response.StatusCode = (int)HttpStatusCode.OK;
                break;
            }
        case "/BookInfo":
        case "/BookInfo.html":
            {
                context.Response.ContentType = "text/html";
                using var streamWriter = new StreamWriter(context.Response.OutputStream);
                await streamWriter.WriteLineAsync(await File.ReadAllTextAsync($"Views/BookInfo.html"));
                context.Response.StatusCode = (int)HttpStatusCode.OK;
                break;
            }
        case "/UserInfo":
        case "/UserInfo.html":
            {
                context.Response.ContentType = "text/html";
                using var streamWriter = new StreamWriter(context.Response.OutputStream);
                await streamWriter.WriteLineAsync(await File.ReadAllTextAsync($"Views/UserInfo.html"));
                context.Response.StatusCode = (int)HttpStatusCode.OK;
                break;
            }
        default:
            {
                await NotFoundAsync(context.Response, endpoint!);

                break;
            }
    }
    context.Response.Close();
}