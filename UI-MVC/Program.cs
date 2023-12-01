/***************************************
 *                                     *
 *   Created by Elias De Hondt         *
 *   Visit https://eliasdh.com         *
 *                                     *
 ***************************************/
// Top level statements, i.e. entry point of the application (Start)
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

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

WebApplication app = builder.Build();

using (IServiceScope scope = app.Services.CreateScope())
{
    PadelClubManagementDbContext padelClubManagementDbContext = scope.ServiceProvider.GetRequiredService<PadelClubManagementDbContext>();
    bool databaseCreated = padelClubManagementDbContext.CreateDatabase(true); // Create the database (Code first flow)
    if (databaseCreated) DataSeeder.Seed(padelClubManagementDbContext); // Seed the database with some data
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Player/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute( 
    name: "default", 
    pattern: "{controller=Player}/{action=Index}/{id?}");
app.Run();