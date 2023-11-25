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
    
    public DbSet<Club> Clubs { get; set; } // Table Clubs
    public DbSet<PadelCourt> PadelCourts { get; set; } // Table PadelCourts
    public DbSet<Booking> Bookings { get; set; } // Table Bookings
    public DbSet<Player> Players { get; set; } // Table Players
    
    public PadelClubManagementDbContext(DbContextOptions options) : base(options)
    {
        
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder); // Call base method to configure the optionsBuilder
        
        if (!optionsBuilder.IsConfigured) // If not configured, configure it
        {
            optionsBuilder.UseLazyLoadingProxies(false); // Disable lazy loading
            optionsBuilder.LogTo(message => Debug.WriteLine(message), LogLevel.Information); // Log to Debug
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Club has many PadelCourts (PadelCourt has one Club)
        modelBuilder.Entity<Club>()
            .HasMany(club => club.PadelCourts)
            .WithOne(padelcourt => padelcourt.Club)
            .HasForeignKey("FK_PadelCourt_Club")
            .IsRequired();
        
        // PadelCourt has many Bookings (Booking has one PadelCourt)
        modelBuilder.Entity<PadelCourt>()
            .HasMany(padelcourt => padelcourt.Bookings)
            .WithOne(booking => booking.PadelCourt)
            .HasForeignKey("FK_Booking_PadelCourt");
        
        // Player has many Bookings (Booking has one Player)
        modelBuilder.Entity<Player>()
            .HasMany(player => player.Bookings)
            .WithOne(booking => booking.Player)
            .HasForeignKey("FK_Booking_Player");
        
        // Primary key of Club, PadleCourt, Booking and Player
        modelBuilder.Entity<Club>().HasKey(club => club.ClubNumber);
        modelBuilder.Entity<PadelCourt>().HasKey(padelcourt => padelcourt.CourtNumber);
        modelBuilder.Entity<Booking>().HasKey(booking => booking.BookingNumber);
        modelBuilder.Entity<Player>().HasKey(player => player.PlayerNumber);
    }
    
    public bool CreateDatabase(bool delete)
    {
        if (delete) Database.EnsureDeleted(); // Delete the database
        bool databaseCreated = Database.EnsureCreated(); // Create the database 
        return databaseCreated; // Return true if the database was created
    }
}