/***************************************
 *                                     *
 *   Created by Elias De Hondt         *
 *   Visit https://eliasdh.com         *
 *                                     *
 ***************************************/
// Class Manager
using SC.BL.Domain;
using SC.DAL;

namespace SC.BL;

public class Manager : IManager
{
    private IRepository _repository; // Dependency injection
    
    public Manager(IRepository repository)
    {
        _repository = repository;
    }
    
    public Player GetPlayer(int playerNumber)
    {
        return _repository.ReadPlayer(playerNumber);
    }
    
    public List<Player> GetAllPlayers()
    {
        return _repository.ReadAllPlayers();
    }
    
    public List<Player> GetPlayersByPosition(PlayerPosition position)
    {
        return _repository.ReadPlayersByPosition(position);
    }
    
    public void AddPlayer(string firstName, string lastName, DateOnly? birthDate, double level, PlayerPosition position)
    {
        Player player = new Player { FirstName = firstName, LastName = lastName, BirthDate = birthDate, Level = level, Position = position };
        
        _repository.CreatePlayer(player);
    }
    
    public PadelCourt GetPadelCourt(int courtNumber)
    {
        return _repository.ReadPadelCourt(courtNumber);
    }
    
    public List<PadelCourt> GetAllPadelCourts()
    {
        return _repository.ReadAllPadelCourts();
    }
    
    public List<PadelCourt> GetPadelCourtsByFilter(double? price, bool? indoor)
    {
        return _repository.ReadPadelCourtsByFilter(price, indoor);
    }
    
    public void AddPadelCourt(bool isIndoor, int capacity, double price, Club club)
    {
        PadelCourt padelCourt = new PadelCourt { IsIndoor = isIndoor, Capacity = capacity, Price = price, Club = club };
        
        _repository.CreatePadelCourt(padelCourt);
    }
}