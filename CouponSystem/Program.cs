using CouponSystem.DataAccess.Data;
using CouponSystem.Models.Entities;
using CouponSystem.Services.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Get connection string from configuration
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// Configure DbContext with SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

// Enable detailed exception page for EF Core
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Configure Identity services
builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;

})
.AddRoles<IdentityRole>()  
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

// Register application services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICouponService, CouponService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IDashboardService, DashboardService>();


builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>{
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
        options.JsonSerializerOptions.MaxDepth = 64;  // Adjust depth limit as needed
    });

var app = builder.Build();

// Seed data (roles and admin user)
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var configuration = services.GetRequiredService<IConfiguration>();

    // Ensure roles and admin are seeded
    await DataSeeder.SeedRolesAndAdminAsync(services, configuration);
}

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); // Use HSTS for production
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();  // Ensure authentication middleware is added
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
