using Harmic.Models;
using Harmic.Models.Momo;
using Harmic.Services.Momo;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;



var builder = WebApplication.CreateBuilder(args);

//connect to momo 
builder.Services.Configure<MomoOptionModel>(builder.Configuration.GetSection("MomoAPI"));
builder.Services.AddScoped<IMomoService, MomoService>();


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("The connection string 'DefaultConnection' is not configured.");
}

builder.Services.AddDbContext<HarmicContext>(options =>
{
    // MariaDB 10.4.32 - sử dụng AutoDetect hoặc chỉ định version string
    options.UseMySql(connectionString, new MySqlServerVersion(new Version(10, 4, 32)));
});



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

// Middleware kiểm tra quyền truy cập admin
app.UseMiddleware<Harmic.Middleware.AdminAccessMiddleware>();

app.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

