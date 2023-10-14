/***************************************
 *                                     *
 *   Created by Elias De Hondt         *
 *   Visit https://eliasdh.com         *
 *                                     *
 ***************************************/
// Class PadelClubManagementDbContext
using System.Diagnostics;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using PadelClubManagement.BL.Domain;

namespace PadelClubManagement.DAL.EF;

public class PadelClubManagementDbContext : DbContext
{
    
    public DbSet<Player> Players { get; set; } // Table Players
    public DbSet<PadelCourt> PadelCourts { get; set; } // Table PadelCourts
    public DbSet<Club> Clubs { get; set; } // Table Clubs
    public PadelClubManagementDbContext(DbContextOptions options) : base(options)
    {
        
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured) // If not configured, configure it
        {
            optionsBuilder.UseSqlite("Data Source=PadelClubManagement.db"); // Use SQLite
            optionsBuilder.LogTo(message => Debug.WriteLine(message), LogLevel.Information); // Log to Debug
        }
    }
    
    public bool CreateDatabase(bool delete)
    {
        if (delete) Database.EnsureDeleted(); // Delete the database
        bool databaseCreated = Database.EnsureCreated(); // Create the database 
        return databaseCreated; // Return true if the database was created
    }
}