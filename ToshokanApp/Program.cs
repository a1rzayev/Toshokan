using System.Net;

HttpListener httpListener = new HttpListener();

const int port = 8080;
httpListener.Prefixes.Add($"http://*:{port}/");

Console.WriteLine($"server started on port {port}");

httpListener.Start();

while(true){
    var context = await httpListener.GetContextAsync();

    string? endpoint = context.Request.RawUrl;

    Console.WriteLine($"endpoint: {endpoint}");

    context.Response.Close();
}