/***************************************
 *                                     *
 *   Created by Elias De Hondt         *
 *   Visit https://eliasdh.com         *
 *                                     *
 ***************************************/
// Class DbContextRepository

using Microsoft.EntityFrameworkCore;
using PadelClubManagement.BL.Domain;

namespace PadelClubManagement.DAL.EF;

public class DbContextRepository : IRepository
{
    public PadelClubManagementDbContext DbContext { get; set; }
    
    public DbContextRepository(PadelClubManagementDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public Player ReadPlayer(int playerNumber)
    {
        Player player = DbContext.Players.Find(playerNumber);
        return player; // player or null
    }
    
    public IEnumerable<Player> ReadAllPlayers()
    {
        return DbContext.Players;
    }
    
    public IEnumerable<Player> ReadPlayersByPosition(PlayerPosition position)
    {
        IEnumerable<Player> matchingPlayers = DbContext.Players.Where(player => player.Position == position);
        return matchingPlayers; // IEnumerable<Player> or empty IEnumerable<Player>
    }

    public void CreatePlayer(Player player)
    {
        player.PlayerNumber = DbContext.Players.Count() + 1;
        DbContext.Players.Add(player);
        DbContext.SaveChanges(); // Save changes to the database
    }

    public PadelCourt ReadPadelCourt(int courtNumber)
    {
        PadelCourt padelCourt = DbContext.PadelCourts.Find(courtNumber);
        return padelCourt;
    }

    public IEnumerable<PadelCourt> ReadAllPadelCourts()
    {
        return DbContext.PadelCourts;
    }
    
    public IEnumerable<PadelCourt> ReadPadelCourtsByFilter(double? price, bool? indoor)
    {
        IQueryable<PadelCourt> filteredPadelCourts = DbContext.PadelCourts.AsQueryable(); // Get all PadelCourts as a queryable source

        if (price.HasValue)
        {
            filteredPadelCourts = filteredPadelCourts.Where(padelCourt => padelCourt.Price.Equals(price.Value));
        }

        if (indoor.HasValue)
        {
            filteredPadelCourts = filteredPadelCourts.Where(padelCourt => padelCourt.IsIndoor == indoor.Value);
        }

        return filteredPadelCourts;
    }


    public void CreatePadelCourt(PadelCourt padelCourt)
    {
        padelCourt.CourtNumber = DbContext.PadelCourts.Count() + 1;
        DbContext.PadelCourts.Add(padelCourt);
        DbContext.SaveChanges(); // Save changes to the database
    }

    public IEnumerable<Player> ReadAllPlayersWithBookingsAndPadelCourts()
    {
        return DbContext.Players
            .Include(player => player.Bookings)
            .ThenInclude(booking => booking.PadelCourt)
            .ThenInclude(padelCourt => padelCourt.Club);
    }

    public IEnumerable<PadelCourt> ReadAllPadelCourtsWithClub()
    {
        return DbContext.PadelCourts.Include(padelCourt => padelCourt.Club);
    }
    
    public IEnumerable<Club> ReadAllClubs()
    {
        return DbContext.Clubs;
    }
    
    public Booking ReadBooking(int bookingNumber)
    {
        Booking booking = DbContext.Bookings.Find(bookingNumber);
        return booking;
    }

    public IEnumerable<Booking> ReadAllBookings()
    {
        return DbContext.Bookings;
    }
    
    public void CreatePlayerToBooking(Player player, Booking booking)
    {
        booking.Player = player;
        player.Bookings.Add(booking);
        DbContext.SaveChanges(); // Save changes to the database
    }

    public void DeletePlayerFromBooking(Player player, Booking booking)
    {
        booking.Player = null;
        player.Bookings.Remove(booking);
        DbContext.SaveChanges(); // Save changes to the database
    }
    
    public IEnumerable<Booking> ReadBookingsOfPlayer(Player player)
    {
        IEnumerable<Booking> bookings = DbContext.Bookings.Where(booking => booking.Player == player);
        return bookings;
    }
}