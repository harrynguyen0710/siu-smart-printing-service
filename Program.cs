using Microsoft.EntityFrameworkCore;
using siu_smart_printing_service.Data;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

// Get environment variables
var serverName = Environment.GetEnvironmentVariable("SERVER_NAME");
var databaseName = Environment.GetEnvironmentVariable("DATABASE");
var password = Environment.GetEnvironmentVariable("PASSWORD");

// Build the connection string
//var connectionString = $"Server={serverName}; Database={databaseName}; ; MultipleActiveResultSets=True; TrustServerCertificate=True;";


//builder.Services.AddDbContext<ApplicationDbContext>(options =>
//    options.UseSqlServer(connectionString)
//);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PrinterApplication"))
);

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
