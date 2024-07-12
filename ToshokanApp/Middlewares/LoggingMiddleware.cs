using System.Data.SqlClient;
using System.Text;
using Dapper;
using ToshokanApp.Models;

namespace ToshokanApp.Middlewares;

public class LoggingMiddleware
{
    private readonly RequestDelegate next;
    private readonly IConfiguration configuration;
    private readonly string connectionString;

    public LoggingMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        this.next = next;
        this.configuration = configuration;
        this.connectionString = this.configuration.GetConnectionString("MsSql");
    }

    public async Task InvokeAsync(HttpContext context)
    {
        bool isLoggingEnabled = this.configuration.GetValue<bool>("Logging:IsEnabled");
        if (!isLoggingEnabled)
        {
            await this.next(context);
            return;
        }

        var log = new Log
        {
            Url = context.Request.Path,
            HttpMethod = context.Request.Method,
            CreationDate = DateTime.UtcNow
        };

        context.Request.EnableBuffering();
        using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8, true, 1024, true))
        {
            log.RequestBody = await reader.ReadToEndAsync();
            context.Request.Body.Position = 0;
        }

        var originalResponseBodyStream = context.Response.Body;
        using (var responseBodyStream = new MemoryStream())
        {
            context.Response.Body = responseBodyStream;

            try
            {
                await this.next(context);
            }
            catch (Exception ex)
            {
                log.ResponseBody = $"Exception: {ex.Message}";
                log.StatusCode = 500; 
                log.EndDate = DateTime.UtcNow;

                await LogRequestAsync(log);
                throw; 
            }

            context.Response.Body = originalResponseBodyStream;
            responseBodyStream.Seek(0, SeekOrigin.Begin);

            using (var reader = new StreamReader(responseBodyStream))
            {
                log.ResponseBody = await reader.ReadToEndAsync();
                responseBodyStream.Seek(0, SeekOrigin.Begin);
                await responseBodyStream.CopyToAsync(originalResponseBodyStream);
            }
        }

        log.StatusCode = context.Response.StatusCode;
        log.EndDate = DateTime.UtcNow;

        await LogRequestAsync(log);
    }

    private async Task LogRequestAsync(Log log)
    {
        try
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                var sql = @"
                    INSERT INTO Logs (Url, RequestBody, ResponseBody, CreationDate, EndDate, StatusCode, HttpMethod)
                    VALUES (@Url, @RequestBody, @ResponseBody, @CreationDate, @EndDate, @StatusCode, @HttpMethod)";
                await connection.ExecuteAsync(sql, log);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to log: {ex.Message}");
        }
    }
}
