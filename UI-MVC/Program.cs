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

builder.Services.AddDbContext<PadelClubManagementDbContext>(options
    => options.UseSqlite(@"Data Source=..\PadelClubManagement.db"));

builder.Services.AddScoped<IRepository, DbContextRepository>();
builder.Services.AddScoped<IManager, Manager>();

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation(); // Add MVC

//builder.Services.AddAuthentication("AppAuthCookie").AddCookie("AppAuthCookie"); // Add authentication (Cookie)
//builder.Services.AddDefaultIdentity<IdentityUser>().AddEntityFrameworkStores<PadelClubManagementDbContext>(); // Add identity

// Configure Identity services
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
    {
        options.User.RequireUniqueEmail = false;
    }).AddEntityFrameworkStores<PadelClubManagementDbContext>().AddDefaultTokenProviders().AddDefaultUI();

WebApplication app = builder.Build();

using (IServiceScope scope = app.Services.CreateScope())
{
    PadelClubManagementDbContext padelClubManagementDbContext = scope.ServiceProvider.GetRequiredService<PadelClubManagementDbContext>();
    bool databaseCreated = padelClubManagementDbContext.CreateDatabase(true); // Create the database (Code first flow)
    if (databaseCreated)
    {
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
        SeedUsers(userManager); // Seed the database with some users
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

app.MapRazorPages(); // Map Razor Pages

app.MapControllerRoute( 
    name: "default", 
    pattern: "{controller=Player}/{action=Index}/{id?}");
app.Run();

void SeedUsers(UserManager<IdentityUser> usermanager)
{
    var user1 = new IdentityUser { UserName = "User1", Email = "user1@eliasdh.com" };
    var user2 = new IdentityUser { UserName = "User2", Email = "user2@eliasdh.com" };
    var user3 = new IdentityUser { UserName = "User3", Email = "user3@eliasdh.com" };
    var user4 = new IdentityUser { UserName = "User4", Email = "user4@eliasdh.com" };
    var user5 = new IdentityUser { UserName = "User5", Email = "user5@eliasdh.com" };
    
    usermanager.CreateAsync(user1, "User1$");
    usermanager.CreateAsync(user2, "User2$");
    usermanager.CreateAsync(user3, "User3$");
    usermanager.CreateAsync(user4, "User4$");
    usermanager.CreateAsync(user5, "User5$");
}