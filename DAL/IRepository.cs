/***************************************
 *                                     *
 *   Created by Elias De Hondt         *
 *   Visit https://eliasdh.com         *
 *                                     *
 ***************************************/
// Interface IRepository
using PadelClubManagement.BL.Domain;

namespace PadelClubManagement.DAL;

public interface IRepository
{
    public Player ReadPlayer(int playerNumber);
    public List<Player> ReadAllPlayers();
    public List<Player> ReadPlayersByPosition(PlayerPosition position);
    public void CreatePlayer(Player player);
    public PadelCourt ReadPadelCourt(int courtNumber);
    public List<PadelCourt> ReadAllPadelCourts();
    public List<PadelCourt> ReadPadelCourtsByFilter(double? price, bool? indoor);
    public void CreatePadelCourt(PadelCourt padelCourt);
}