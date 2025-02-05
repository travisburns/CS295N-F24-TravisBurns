using AnimeInfo.Data;
using AnimeInfo.Models;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllersWithViews();



var connectionString = builder.Configuration.GetConnectionString("MySqlConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
  options.UseMySql(connectionString,
    new MySqlServerVersion(new Version(9, 0, 1))));

builder.Services.AddTransient<IBlogRepository, BlogRepository>();


builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler(errorApp =>
    {
        errorApp.Run(async context =>
        {
            var exceptionHandler = context.Features.Get<IExceptionHandlerPathFeature>();
            var error = exceptionHandler?.Error;
            await context.Response.WriteAsync($"Error: {error?.Message}\n\nStack Trace: {error?.StackTrace}");
        });
    });
    app.UseHsts();
}



app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Get a DbContext object -- we will refactor this


using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider
        .GetRequiredService<ApplicationDbContext>();
  await SeedData.Seed(context, scope.ServiceProvider);
}
    app.Run();