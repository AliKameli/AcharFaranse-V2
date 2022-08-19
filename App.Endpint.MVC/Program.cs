using System.Net.Mime;
using App.Endpoint.MVC;
using App.Infrastructures.SQLServer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDistributedMemoryCache();

builder.Logging.ClearProviders();
builder.Logging.AddConsole();
// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(
    options => { options.UseSqlServer(connectionString); });

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Logout";
    options.AccessDeniedPath = "/Account/AccessDenied";
});

builder.Services.AddServicesCollection();
builder.Services.AddAppServicesCollection();
builder.Services.AddMyIdentity();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler(exceptionHandlerApp =>
    {
        exceptionHandlerApp.Run(async context =>
        {
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            // using static System.Net.Mime.MediaTypeNames;
            context.Response.ContentType = MediaTypeNames.Text.Plain;

            await context.Response.WriteAsync("An exception was thrown.");

            var exceptionHandlerPathFeature =
                context.Features.Get<IExceptionHandlerPathFeature>();

            if (exceptionHandlerPathFeature?.Error is FileNotFoundException)
                await context.Response.WriteAsync(" The file was not found.");

            if (exceptionHandlerPathFeature?.Path == "/") await context.Response.WriteAsync(" Page: Home.");
        });
    });
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;

    var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser<int>>>();

    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();

    await MyIdentityDataInitializer.SeedData
        (userManager, roleManager);
}

app.MapAreaControllerRoute(
    "areas",
    "Admin",
    "Admin/{controller=Home}/{action=Index}/{id?}");
app.MapAreaControllerRoute(
    "areas",
    "Costumer",
    "Costumer/{controller=Home}/{action=Index}/{id?}");
app.MapAreaControllerRoute(
    "areas",
    "Worker",
    "Worker/{controller=Home}/{action=Index}/{id?}");
app.MapControllerRoute(
    "default",
    "{controller=Home}/{action=Index}/{id?}");

app.Run();