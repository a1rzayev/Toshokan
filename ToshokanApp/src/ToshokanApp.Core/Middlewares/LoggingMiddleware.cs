using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using ToshokanApp.Core.Models;

namespace ToshokanApp.Core.Middlewares;

public class LoggingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IConfiguration _configuration;
    private readonly string _connectionString;

    public LoggingMiddleware(RequestDelegate next, IConfiguration configuration)
    {
        _next = next;
        _configuration = configuration;
        _connectionString = _configuration.GetConnectionString("MsSql");
    }

    public async Task InvokeAsync(HttpContext context)
    {
        bool isLoggingEnabled;
        bool.TryParse(_configuration.GetSection("Logging:IsEnabled").Value, out isLoggingEnabled);
        if (!isLoggingEnabled)
        {
            await _next(context);
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
                await _next(context);
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
            using (var connection = new SqlConnection(_connectionString))
            {
                var sql = @"
                        INSERT INTO Logs (Id, Url, RequestBody, ResponseBody, CreationDate, EndDate, StatusCode, HttpMethod)
                        VALUES (@Id, @Url, @RequestBody, @ResponseBody, @CreationDate, @EndDate, @StatusCode, @HttpMethod)";
                await connection.ExecuteAsync(sql, log);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Failed to log: {ex.Message}");
        }
    }
}