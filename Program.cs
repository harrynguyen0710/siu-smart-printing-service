using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using siu_smart_printing_service.Areas.Admin.Services;
using siu_smart_printing_service.Data;
using siu_smart_printing_service.IRepositories;
using siu_smart_printing_service.Models;
using siu_smart_printing_service.Repositories;
using siu_smart_printing_service.Services;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

// Get environment variables
var serverName = Environment.GetEnvironmentVariable("SERVER_NAME");
var databaseName = Environment.GetEnvironmentVariable("DATABASE");
var password = Environment.GetEnvironmentVariable("PASSWORD");


builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IFileTypesRepository, FileTypesRepository>();
builder.Services.AddScoped<IPrinterRepository, PrinterRepository>();
builder.Services.AddScoped<IPrintingLogsRepository, PrintingLogsRepository>();
builder.Services.AddScoped<IUploadedFileRepository, UploadedFileRepository>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();  

builder.Services.AddScoped<PrinterService>();
builder.Services.AddScoped<FileTypeService>();
builder.Services.AddScoped<PrintingLogService>();
builder.Services.AddScoped<UploadedFileService>();
builder.Services.AddScoped<RoomService>();  


builder.Services.AddIdentity<Users, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();


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
app.UseAuthentication();
app.UseAuthorization();



app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
