/***************************************
 *                                     *
 *   Created by Elias De Hondt         *
 *   Visit https://eliasdh.com         *
 *                                     *
 ***************************************/
// Class Manager
using System.ComponentModel.DataAnnotations;
using PadelClubManagement.BL.Domain;
using PadelClubManagement.DAL;

namespace PadelClubManagement.BL;

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
        Validate(player);
        
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
        Validate(padelCourt);
        
        _repository.CreatePadelCourt(padelCourt);
    }
    
    private void Validate(Player player) // Validate the Player object (overload)
    {
        List<ValidationResult> errors = new List<ValidationResult>();
        bool valid = Validator.TryValidateObject(player, new ValidationContext(player), errors, true);
        
        if (!valid) // If the object is not valid
        {
            string errorString = "\nAn error occurred, please try again:\n * ";
            
            foreach (ValidationResult error in errors) errorString += error.ErrorMessage + "\n * "; // Add each error to the errorString
            
            throw new ValidationException(errorString + "end"); // Throw a ValidationException with the errorString to the caller
        }
    }
    
    private void Validate(PadelCourt padelCourt) // Validate the PadelCourt object (overload)
    {
        List<ValidationResult> errors = new List<ValidationResult>();
        bool valid = Validator.TryValidateObject(padelCourt, new ValidationContext(padelCourt), errors, true);
        
        if (!valid) // If the object is not valid
        {
            string errorString = "\nAn error occurred, please try again:\n * ";
            
            foreach (ValidationResult error in errors) errorString += error.ErrorMessage + "\n * "; // Add each error to the errorString
            
            throw new ValidationException(errorString + "end"); // Throw a ValidationException with the errorString to the caller
        }
    }
}