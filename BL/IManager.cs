/***************************************
 *                                     *
 *   Created by Elias De Hondt         *
 *   Visit https://eliasdh.com         *
 *                                     *
 ***************************************/
// Interface IManager
using PadelClubManagement.BL.Domain;

namespace PadelClubManagement.BL;

public interface IManager
{
    public Player GetPlayer(int playerNumber);
    public Player GetPlayerWithBookingsAndPadelCourts(int playerNumber);
    public IEnumerable<Player> GetAllPlayers();
    public IEnumerable<Player> GetPlayersByPosition(PlayerPosition position);
    public void AddPlayer(string firstName, string lastName, DateOnly? birthDate, double level, PlayerPosition position);
    public PadelCourt GetPadelCourt(int courtNumber);
    public IEnumerable<PadelCourt> GetAllPadelCourts();
    public IEnumerable<PadelCourt> GetPadelCourtsByFilter(double? price, bool? indoor);
    public void AddPadelCourt(bool isIndoor, int capacity, double price, Club club);
    public IEnumerable<Player> GetAllPlayersWithBookingsAndPadelCourts();
    public IEnumerable<PadelCourt> GetAllPadelCourtsWithClub();
    public IEnumerable<Club> GetAllClubs();
    public IEnumerable<Booking> GetAllBookings();
    public void AddPlayerToBooking(int playerNumber, int bookingNumber);
    public void RemovePlayerFromBooking(int playerNumber, int bookingNumber);
    public IEnumerable<Booking> GetBookingsOfPlayer(int playerNumber);
}