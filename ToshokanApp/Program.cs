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
        // case "/Add":
        //     {
        //         await LayoutAsync(context.Response, layoutName: "Add");

        //         break;
        //     }
        default:
            {
                await NotFoundAsync(context.Response, endpoint!);

                break;
            }
    }
    context.Response.Close();
}