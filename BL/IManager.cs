/***************************************
 *                                     *
 *   Created by Elias De Hondt         *
 *   Visit https://eliasdh.com         *
 *                                     *
 ***************************************/
// Interface IManager
using SC.BL.Domain;

namespace SC.BL;

public interface IManager
{
    public Player GetPlayer(int playerNumber);
    public List<Player> GetAllPlayers();
    public List<Player> GetPlayersByPosition(PlayerPosition position);
    public void AddPlayer(string firstName, string lastName, DateOnly? birthDate, double level, PlayerPosition position);
    public PadelCourt GetPadelCourt(int courtNumber);
    public List<PadelCourt> GetAllPadelCourts();
    public List<PadelCourt> GetPadelCourtsByFilter(double? price, bool? indoor);
    public void AddPadelCourt(bool isIndoor, int capacity, double price);
}