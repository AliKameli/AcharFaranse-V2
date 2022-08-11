using App.Endpoint.MVC;
using App.Infrastructures.SQLServer;
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
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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