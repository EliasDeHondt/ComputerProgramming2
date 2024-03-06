/***************************************
 *                                     *
 *   Created by Elias De Hondt         *
 *   Visit https://eliasdh.com         *
 *                                     *
 ***************************************/
// Top level statements, i.e. entry point of the application (Start)

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PadelClubManagement.BL;
using PadelClubManagement.DAL;
using PadelClubManagement.DAL.EF;

// Composition Root
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PadelClubManagementDbContext>(options => options.UseSqlite(@"Data Source=..\PadelClubManagement.db"));
//builder.Services.AddDbContext<PadelClubManagementDbContext>(options => options.UseNpgsql("Host=35.187.177.58;Port=5432;Database=codeforge;Username=admin;Password=123"));

builder.Services.AddDbContext<PadelClubManagementDbContext>();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<PadelClubManagementDbContext>();

builder.Services.AddScoped<IRepository, DbContextRepository>();
builder.Services.AddScoped<IManager, Manager>();

// Add the controllers and the Razor Pages to the application for routing purposes
builder.Services.ConfigureApplicationCookie (cfg =>
{
    cfg.Events.OnRedirectToLogin += ctx =>
    {
        if (ctx.Request.Path.StartsWithSegments ("/api")) ctx.Response.StatusCode = 401;

        return Task.CompletedTask ;
    };
    
    cfg.Events.OnRedirectToAccessDenied += ctx =>
    {
        if (ctx.Request.Path.StartsWithSegments ("/api")) ctx.Response.StatusCode = 403;

        return Task.CompletedTask ;
    };
});

WebApplication app = builder.Build();

using (IServiceScope scope = app.Services.CreateScope())
{
    PadelClubManagementDbContext padelClubManagementDbContext = scope.ServiceProvider.GetRequiredService<PadelClubManagementDbContext>();
    bool databaseCreated = padelClubManagementDbContext.CreateDatabase(true); // Create the database (Code first flow)
    if (databaseCreated)
    {
        UserManager<IdentityUser> userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
        RoleManager<IdentityRole> roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        SeedUsers(userManager, roleManager); // Seed the database with some users
        DataSeeder.Seed(padelClubManagementDbContext); // Seed the database with some data
    }
}

// HTTP pipeline
if (!app.Environment.IsDevelopment()) // If the application is not in development mode
{
    app.UseExceptionHandler("/Player/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication(); // Use authentication is required e.g. 2FA
app.UseAuthorization();

app.MapControllerRoute( 
    name: "default", 
    pattern: "{controller=Player}/{action=Index}/{id?}");

app.MapRazorPages(); // Map Razor Pages

app.Run();

void SeedUsers(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager) // Seed the database with some users & roles
{
    // Create some users
    string[] roleNames = { "Admin", "User" };
    foreach (var roleName in roleNames) roleManager.CreateAsync(new IdentityRole(roleName)).Wait();
    
    // Create some users
    var user1 = new IdentityUser { UserName = "user1@eliasdh.com", Email = "user1@eliasdh.com", EmailConfirmed = true };
    var user2 = new IdentityUser { UserName = "user2@eliasdh.com", Email = "user2@eliasdh.com", EmailConfirmed = true };
    var user3 = new IdentityUser { UserName = "user3@eliasdh.com", Email = "user3@eliasdh.com", EmailConfirmed = true };
    var user4 = new IdentityUser { UserName = "user4@eliasdh.com", Email = "user4@eliasdh.com", EmailConfirmed = true };
    var user5 = new IdentityUser { UserName = "user5@eliasdh.com", Email = "user5@eliasdh.com", EmailConfirmed = true };
    
    // Add the password to the users and create them
    var a = userManager.CreateAsync(user1, "User1$").Result;
    var b = userManager.CreateAsync(user2, "User2$").Result;
    var c = userManager.CreateAsync(user3, "User3$").Result;
    var d = userManager.CreateAsync(user4, "User4$").Result;
    var e = userManager.CreateAsync(user5, "User5$").Result;

    if (!a.Succeeded || !b.Succeeded || !c.Succeeded || !d.Succeeded || !e.Succeeded) throw new Exception("Failed to create the users");
    
    // Add the users to the roles
    userManager.AddToRoleAsync(user1, roleNames[0]).Wait();
    userManager.AddToRoleAsync(user2, roleNames[0]).Wait();
    userManager.AddToRoleAsync(user3, roleNames[1]).Wait();
    userManager.AddToRoleAsync(user4, roleNames[1]).Wait();
    userManager.AddToRoleAsync(user5, roleNames[1]).Wait();
}