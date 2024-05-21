using System.Data;
using ToshokanApp.Repositories;
using ToshokanApp.Services;
using ToshokanApp.Services.Base;
using System.Data.SqlClient;
using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IDbConnection>(sp => new System.Data.SqlClient.SqlConnection(builder.Configuration.GetConnectionString("MsSql")));
    
builder.Services.AddTransient<IBookCommentRepository, BookCommentRepository>();
builder.Services.AddTransient<IBookCommentService, BookCommentService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();


app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
