using System.Data;
using ToshokanApp.Repositories;
using ToshokanApp.Services;
using ToshokanApp.Services.Base;
using ToshokanApp.Middlewares;
using ToshokanApp.Repositories.EfCore.DbContexts;
using Microsoft.EntityFrameworkCore;
using ToshokanApp.Configuration;
using Microsoft.Extensions.Options;
using ToshokanApp.Repositories.Base;
using ToshokanApp.Repositories.EfCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
//builder.Services.AddScoped<IDbConnection>(sp => new System.Data.SqlClient.SqlConnection(builder.Configuration.GetConnectionString("MsSql")));
builder.Services.AddDbContext<ToshokanDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MsSql")));
builder.Services.AddTransient<IBookCommentRepository, BookCommentEfCoreRepository>();
builder.Services.AddTransient<IBookCommentService, BookCommentService>();

builder.Services.AddTransient<IBookRepository, BookEfCoreRepository>();
builder.Services.AddTransient<IBookService, BookService>();

builder.Services.AddTransient<ILogRepository, LogEfCoreRepository>();
builder.Services.AddTransient<ILogService, LogService>();

var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStaticFiles();

app.UseRouting();


app.UseAuthorization();

app.UseMiddleware<LoggingMiddleware>();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
