using ToshokanApp.Infrastructure.Services;
using ToshokanApp.Core.Services;
using ToshokanApp.Core.Middlewares;
using ToshokanApp.Infrastructure.Repositories.EfCore.DbContexts;
using Microsoft.EntityFrameworkCore;
using ToshokanApp.Core.Repositories;
using ToshokanApp.Infrastructure.Repositories.EfCore;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ToshokanDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("MsSql")));
builder.Services.AddTransient<IBookCommentRepository, BookCommentEfCoreRepository>();
builder.Services.AddTransient<IBookCommentService, BookCommentService>();

builder.Services.AddTransient<IBookRepository, BookEfCoreRepository>();
builder.Services.AddTransient<IBookService, BookService>();

builder.Services.AddTransient<ILogRepository, LogEfCoreRepository>();
builder.Services.AddTransient<ILogService, LogService>();

builder.Services.AddTransient<IIdentityRepository, IdentityEfCoreRepository>();
builder.Services.AddTransient<IIdentityService, IdentityService>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddDataProtection();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options => {
        
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


app.UseAuthorization();

app.UseMiddleware<LoggingMiddleware>();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
