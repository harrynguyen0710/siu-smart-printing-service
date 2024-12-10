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
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IConfigurationRepository, ConfigurationRepository>();    

builder.Services.AddScoped<PrinterService>();
builder.Services.AddScoped<FileTypeService>();
builder.Services.AddScoped<PrintingLogService>();
builder.Services.AddScoped<UploadedFileService>();
builder.Services.AddScoped<RoomService>();  
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<ConfigurationService>();


builder.Services.AddIdentity<Users, IdentityRole>(options =>
{
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = true;

    options.SignIn.RequireConfirmedAccount = false;
    options.SignIn.RequireConfirmedEmail = false;
    options.SignIn.RequireConfirmedPhoneNumber = false;

    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 0;
})
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

using (var scope = app.Services.CreateScope())
{
    var roleManger = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var roles = new[] { "Admin", "Student" };

    foreach (var role in roles)
    {
        if (!await roleManger.RoleExistsAsync(role))
            await roleManger.CreateAsync(new IdentityRole(role));
    }
}

using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<Users>>();
    string email = "admin@gmail.com";
    string emailtStaff = "student@gmail.com";
    string pass = "1002";
    double balance = 5000;
    string fullName = "Test Account";
    if (await userManager.FindByEmailAsync(email) == null)
    {
        var user = new Users();
        user.UserName = email;
        user.Email = email;
        user.balance = balance;
        user.fullName = fullName;
        await userManager.CreateAsync(user, pass);
        await userManager.AddToRoleAsync(user, "Admin");
    }
    if (await userManager.FindByEmailAsync(emailtStaff) == null)
    {
        var user = new Users();
        user.fullName = fullName;
        user.UserName = emailtStaff;
        user.Email = emailtStaff;
        user.balance = balance;
        await userManager.CreateAsync(user, pass);
        await userManager.AddToRoleAsync(user, "Student");
    }
}

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
