using BLL.Services;
using BLL.Services.IServices;
using DAL.Context;
using DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Host.UseSerilog();

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<UstdbContext>(options => options.UseNpgsql(connectionString));

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.LogoutPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";

    options.Cookie.Name = "authCookie";

    options.ExpireTimeSpan = TimeSpan.FromDays(1);
});

builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequiredLength = 8;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequireNonAlphanumeric = false;

    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);

    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedEmail = true;
})
.AddEntityFrameworkStores<UstdbContext>()
.AddDefaultTokenProviders();

// Adding services
//builder.Services.AddScoped<ICourseService, CourseService>();
builder.Services.AddScoped<IAdminCreatorService, AdminCreatorService>();
builder.Services.AddScoped<IEmailService, EmailService>();
builder.Services.AddSingleton<IStartupService, StartupService>();

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

try
{
    Log.Information("Starting web application");
    var app = builder.Build();
    if (app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        app.UseHsts();
    }

    app.UseHttpsRedirection();

    app.UseStaticFiles();

    app.UseSerilogRequestLogging();

    app.UseRouting();

    app.UseAuthentication();
    app.UseAuthorization();

    var startupService = new StartupService();

    await startupService.CreateRoles(app.Services);
    await startupService.CreateAdmins(app.Services, new List<(User, string)>
    {
        new (
        new User
        {
        FirstName = "Admin",
        LastName = "Admin",
        MiddleName = "Admin",
        Email = "admin@gmail.com",
        UserName = "Admin_Admin_Admin",
        },
        "123456qQ"),
    });

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Account}/{action=Login}");

    Log.Information("Running app.");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}