/***************************************
 *                                     *
 *   Created by Elias De Hondt         *
 *   Visit https://eliasdh.com         *
 *                                     *
 ***************************************/
// Interface IRepository

using Microsoft.AspNetCore.Identity;
using PadelClubManagement.BL.Domain;

namespace PadelClubManagement.DAL;

public interface IRepository
{
    public Player ReadPlayer(int playerNumber);
    public IEnumerable<Player> ReadAllPlayers();
    public IEnumerable<Player> ReadPlayersByPosition(PlayerPosition position);
    public void CreatePlayer(Player player);
    public PadelCourt ReadPadelCourt(int courtNumber);
    public IEnumerable<PadelCourt> ReadPadelCourtsByFilter(double? price, bool? indoor);
    public void CreatePadelCourt(PadelCourt padelCourt);
    public IEnumerable<Player> ReadAllPlayersWithBookingsAndPadelCourts();
    public IEnumerable<PadelCourt> ReadAllPadelCourtsWithClub();
    public IEnumerable<Club> ReadAllClubs();
    public void CreateClub(Club club);
    public Booking ReadBooking(int bookingNumber);
    public IEnumerable<Booking> ReadAllBookings();
    public void CreatePlayerToBooking(Player player, Booking booking);
    public void CreatePadelCourtToBooking(PadelCourt padelCourt, Booking booking);
    public void DeletePlayerFromBooking(Player player, Booking booking);
    public IEnumerable<Booking> ReadBookingsOfPlayer(Player player);
    public IEnumerable<Player> ReadPlayersOfPadelCourt(int courtNumber);
    public int CreateBooking(Booking booking, bool returnBookingNumber);
    public IEnumerable<PadelCourt> ReadAllPadelCourts();
    public IEnumerable<Player> ReadAllPlayersWithManager();
    public Player ReadPlayerWithBookingsAndPadelCourtsAndManager(int playerNumber);
    public IdentityUser ReadManagerByEmail(string email);
}