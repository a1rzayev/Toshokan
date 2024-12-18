using ToshokanApp.Infrastructure.Services;
using ToshokanApp.Core.Services;
using ToshokanApp.Core.Middlewares;
using ToshokanApp.Infrastructure.Repositories.EfCore.DbContexts;
using Microsoft.EntityFrameworkCore;
using ToshokanApp.Core.Repositories;
using ToshokanApp.Infrastructure.Repositories.EfCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ToshokanDbContext>(dbContextOptionsBuilder =>
{
    var connectionString = builder.Configuration.GetConnectionString("MsSql");
    dbContextOptionsBuilder.UseSqlServer(connectionString, options =>
    {
        options.MigrationsAssembly("ToshokanApp.Infrastructure");
    });
});



builder.Services.AddDataProtection();


var assetsDirPath = builder.Configuration["StaticFileRoutes:Assets"];
var avatarsDirPath = builder.Configuration["StaticFileRoutes:Avatars"];

if (!Directory.Exists(assetsDirPath))
{
    try
    {
        Directory.CreateDirectory(assetsDirPath);
        Directory.CreateDirectory($"{assetsDirPath}/Avatars");
        Directory.CreateDirectory($"{assetsDirPath}/Books");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error while initializing folder: {ex.Message}");
    }
}

if (!Directory.Exists(avatarsDirPath))
{
    try
    {
        Directory.CreateDirectory(avatarsDirPath);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error while initializing folder: {ex.Message}");
    }
}
if (!Directory.Exists($"{assetsDirPath}/Books"))
{
    try
    {
        Directory.CreateDirectory($"{assetsDirPath}/Books");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error while initializing folder: {ex.Message}");
    }
}


builder.Services.AddTransient<IEmailRepository, EmailEfCoreRepository>();
builder.Services.AddTransient<IEmailService, EmailService>();

builder.Services.AddTransient<ICurrentStateService, CurrentStateService>();

builder.Services.AddTransient<ICommentRepository, CommentEfCoreRepository>();
builder.Services.AddTransient<ICommentService, CommentService>();

builder.Services.AddTransient<IBookRepository, BookEfCoreRepository>();
builder.Services.AddTransient<IBookService, BookService>();

builder.Services.AddTransient<ILogRepository, LogEfCoreRepository>();

builder.Services.AddTransient<ILogService, LogService>();

builder.Services.AddTransient<IIdentityRepository, IdentityEfCoreRepository>();
builder.Services.AddTransient<IIdentityService, IdentityService>();

builder.Services.AddTransient<IAdminRepository, AdminEfCoreRepository>();
builder.Services.AddTransient<IAdminService, AdminService>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Identity/Login";
        options.AccessDeniedPath = "/Book/Index";
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdminAccess", policyBuilder =>
    {
        policyBuilder.RequireRole("Admin");
    });
    options.AddPolicy("UserAccess", policyBuilder =>
    {
        policyBuilder.RequireRole("User");
    });
});



var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}



app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<LoggingMiddleware>();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Book}/{action=Index}");


app.Run();
