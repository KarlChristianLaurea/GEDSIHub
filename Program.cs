using DinkToPdf.Contracts;
using DinkToPdf;
using GADApplication.Data;
using GADApplication.Hubs;
using GADApplication.Identity;
using GADApplication.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using QuestPDF.Infrastructure; // Add this to use QuestPDF
using IronPdf; // Add this to use IronPdf
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

// Load the .env file
Env.Load(".env"); // If your .env file is named exactly .env and located in the root

// Add environment variables from .env file
builder.Configuration.AddEnvironmentVariables();


// Apply IronPdf license key from the .env file
IronPdf.Installation.LicenseKey = Environment.GetEnvironmentVariable("IRONPDF_LICENSE_KEY");

// Set QuestPDF license type to Community
QuestPDF.Settings.License = LicenseType.Community;

builder.Services.AddTransient<SomeService>();


// Configure Application DbContext using the connection string from the .env file
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(Environment.GetEnvironmentVariable("DEFAULT_CONNECTION")));

// Configure Identity DbContext using the connection string from the .env file
builder.Services.AddDbContext<ApplicationIdentityDbContext>(options =>
    options.UseSqlServer(Environment.GetEnvironmentVariable("IDENTITY_CONNECTION")));

// Configure Identity with Roles
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequireDigit = true; // Configure other Identity options as needed
})
.AddEntityFrameworkStores<ApplicationIdentityDbContext>()
.AddDefaultTokenProviders()
.AddDefaultUI();

// Register the BudgetPredictionService with HttpClient
builder.Services.AddHttpClient<IBudgetPredictionService, BudgetPredictionService>();

// Register MessageService in the dependency injection
builder.Services.AddScoped<MessageService>();

// Add DinkToPdf PDF generator service
builder.Services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

// Register other services
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.Configure<EmailSettings>(options =>
{
    options.SmtpServer = Environment.GetEnvironmentVariable("SMTP_SERVER");
    options.SmtpPort = int.Parse(Environment.GetEnvironmentVariable("SMTP_PORT"));
    options.SmtpUsername = Environment.GetEnvironmentVariable("SMTP_USERNAME");
    options.SmtpPassword = Environment.GetEnvironmentVariable("SMTP_PASSWORD");
    options.SenderName = Environment.GetEnvironmentVariable("SENDER_NAME");
    options.SenderEmail = Environment.GetEnvironmentVariable("SENDER_EMAIL");
    options.UseSSL = bool.Parse(Environment.GetEnvironmentVariable("USE_SSL"));
});
builder.Services.AddTransient<IEmailSender, EmailSender>();
builder.Services.AddScoped<NotificationService>();

// Register Authentication and configure default login path
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = IdentityConstants.ApplicationScheme;
}).AddCookie(options =>
{
    options.LoginPath = "/Identity/Account/Login"; // Redirect to the login page if not authenticated
});

// Register Razor Pages with global authorization filter
builder.Services.AddRazorPages(options =>
{
    // Apply authorization globally without a custom policy object
    options.Conventions.AuthorizeFolder("/"); // Requires authentication for all pages in root folder
});

// Register SignalR
builder.Services.AddSignalR();

var app = builder.Build();

// Seed roles after application is built
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
    SeedRolesAndAdminUserAsync(roleManager, userManager).Wait();
}

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Apply authentication and authorization middlewares
app.UseAuthentication();
app.UseAuthorization();

// Map Razor Pages and SignalR Hubs
app.MapRazorPages();
app.MapHub<MessageHub>("/messageHub");
app.MapHub<NotificationHub>("/notificationHub");

app.Run();

// Method to seed roles and create admin user
async Task SeedRolesAndAdminUserAsync(RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
{
    // Retrieve role names from environment variables
    string[] roleNames = { "Admin", "User" };

    foreach (var roleName in roleNames)
    {
        var roleExists = await roleManager.RoleExistsAsync(roleName);
        if (!roleExists)
        {
            await roleManager.CreateAsync(new IdentityRole(roleName));
        }
    }

    // Retrieve admin user credentials from environment variables
    string adminEmail = Environment.GetEnvironmentVariable("ADMIN_EMAIL");
    string adminPassword = Environment.GetEnvironmentVariable("ADMIN_PASSWORD");

    var adminUser = await userManager.FindByEmailAsync(adminEmail);
    if (adminUser == null)
    {
        var user = new ApplicationUser
        {
            UserName = adminEmail,
            Email = adminEmail
        };
        var result = await userManager.CreateAsync(user, adminPassword);
        if (result.Succeeded)
        {
            string adminRole = Environment.GetEnvironmentVariable("ADMIN_ROLE");
            await userManager.AddToRoleAsync(user, adminRole);
        }
    }
}
