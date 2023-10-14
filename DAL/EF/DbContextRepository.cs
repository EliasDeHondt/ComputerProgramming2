/***************************************
 *                                     *
 *   Created by Elias De Hondt         *
 *   Visit https://eliasdh.com         *
 *                                     *
 ***************************************/
// Class DbContextRepository
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
        foreach (Player player in DbContext.Players)
        {
            if (player.PlayerNumber == playerNumber) return player;
        }
        return null;
    }
    
    public List<Player> ReadAllPlayers()
    {
        return DbContext.Players.ToList();
    }

    public List<Player> ReadPlayersByPosition(PlayerPosition position)
    {
        List<Player> players = new List<Player>();
        foreach (Player player in DbContext.Players)
        {
            if (player.Position == position) players.Add(player);
        }
        return players;
    }

    public void CreatePlayer(Player player)
    {
        player.PlayerNumber = DbContext.Players.Count() + 1;
        DbContext.Players.Add(player);
        DbContext.SaveChanges(); // Save changes to the database
    }

    public PadelCourt ReadPadelCourt(int courtNumber)
    {
        foreach (PadelCourt padelCourt in DbContext.PadelCourts)
        {
            if (padelCourt.CourtNumber == courtNumber) return padelCourt;
        }
        return null;
    }

    public List<PadelCourt> ReadAllPadelCourts()
    {
        return DbContext.PadelCourts.ToList();
    }

    public List<PadelCourt> ReadPadelCourtsByFilter(double? price, bool? indoor)
    {
        List<PadelCourt> padelCourts = new List<PadelCourt>();
        IQueryable<PadelCourt> query = DbContext.PadelCourts; // Get all padelCourts from the database in 1 query
        
        foreach (PadelCourt padelCourt in query)
        {
            // If price is null or the price of the padelCourt is equal to the price filter and indoor is null or the indoor of the padelCourt is equal to the indoor filter
            bool meetsPriceCriteria = !price.HasValue || padelCourt.Price.Equals(price.Value);
            bool meetsIndoorCriteria = !indoor.HasValue || padelCourt.IsIndoor == indoor.Value;
            
            if (meetsPriceCriteria && meetsIndoorCriteria) padelCourts.Add(padelCourt);
        }
        return padelCourts;
    }

    public void CreatePadelCourt(PadelCourt padelCourt)
    {
        padelCourt.CourtNumber = DbContext.PadelCourts.Count() + 1;
        DbContext.PadelCourts.Add(padelCourt);
        DbContext.SaveChanges(); // Save changes to the database
    }
}