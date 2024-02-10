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


builder.Services.AddDbContext<PadelClubManagementDbContext>();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<PadelClubManagementDbContext>();
builder.Services.AddScoped<IRepository, DbContextRepository>();
builder.Services.AddScoped<IManager, Manager>();

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

app.MapControllerRoute( 
    name: "default", 
    pattern: "{controller=Player}/{action=Index}/{id?}");

app.MapRazorPages(); // Map Razor Pages

app.Run();

void SeedUsers(UserManager<IdentityUser> usermanager)
{
    var user1 = new IdentityUser { UserName = "user1@eliasdh.com", Email = "user1@eliasdh.com", EmailConfirmed = true };
    var user2 = new IdentityUser { UserName = "user2@eliasdh.com", Email = "user2@eliasdh.com", EmailConfirmed = true };
    var user3 = new IdentityUser { UserName = "user3@eliasdh.com", Email = "user3@eliasdh.com", EmailConfirmed = true };
    var user4 = new IdentityUser { UserName = "user4@eliasdh.com", Email = "user4@eliasdh.com", EmailConfirmed = true };
    var user5 = new IdentityUser { UserName = "user5@eliasdh.com", Email = "user5@eliasdh.com", EmailConfirmed = true };
    
    usermanager.CreateAsync(user1, "User1$");
    usermanager.CreateAsync(user2, "User2$");
    usermanager.CreateAsync(user3, "User3$");
    usermanager.CreateAsync(user4, "User4$");
    usermanager.CreateAsync(user5, "User5$");
}