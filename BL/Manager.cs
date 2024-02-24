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
    private readonly IRepository _repository; // Dependency injection
    
    public Manager(IRepository repository)
    {
        _repository = repository;
    }
    
    public Player GetPlayer(int playerNumber)
    {
        return _repository.ReadPlayer(playerNumber);
    }
    
    public IEnumerable<Player> GetAllPlayers()
    {
        return _repository.ReadAllPlayers();
    }
    
    public IEnumerable<Player> GetPlayersByPosition(PlayerPosition position)
    {
        return _repository.ReadPlayersByPosition(position);
    }
    
    public void AddPlayer(string firstName, string lastName, DateOnly? birthDate, double level, PlayerPosition position)
    {
        Player player = new Player { FirstName = firstName, LastName = lastName, BirthDate = birthDate, Level = level, Position = position };
        Validate(player);
        
        _repository.CreatePlayer(player);
    }
    
    public void AddPlayerAsObject(Player player, string email)
    {
        player.PlayerManager = _repository.ReadManagerByEmail(email);
        Validate(player);
        _repository.CreatePlayer(player);
    }
    
    public PadelCourt GetPadelCourt(int courtNumber)
    {
        return _repository.ReadPadelCourt(courtNumber);
    }
    
    public IEnumerable<PadelCourt> GetPadelCourtsByFilter(double? price, bool? indoor)
    {
        return _repository.ReadPadelCourtsByFilter(price, indoor);
    }
    
    public void AddPadelCourt(bool isIndoor, int capacity, double price, Club club)
    {
        PadelCourt padelCourt = new PadelCourt { IsIndoor = isIndoor, Capacity = capacity, Price = price, Club = club};
        Validate(padelCourt);
        
        _repository.CreatePadelCourt(padelCourt);
    }
    
    public IEnumerable<Player> GetAllPlayersWithBookingsAndPadelCourts()
    {
        return _repository.ReadAllPlayersWithBookingsAndPadelCourts();
    }

    public IEnumerable<PadelCourt> GetAllPadelCourtsWithClub()
    {
        return _repository.ReadAllPadelCourtsWithClub();
    }
    
    public IEnumerable<Club> GetAllClubs()
    {
        return _repository.ReadAllClubs();
    }
    
    public void AddClub(string name, int numberOfCourts, string streetName, int houseNumber, int zipCode)
    {
        Club club = new Club { Name = name, NumberOfCourts = numberOfCourts, StreetName = streetName, HouseNumber = houseNumber, ZipCode = zipCode };
        Validate(club);
        
        _repository.CreateClub(club);
    }

    public IEnumerable<Booking> GetAllBookings()
    {
        return _repository.ReadAllBookings();
    }
    
    public void AddPlayerToBooking(int playerNumber, int bookingNumber)
    {
        Player player = _repository.ReadPlayer(playerNumber);
        Booking booking = _repository.ReadBooking(bookingNumber);
        
        Validate(player);
        Validate(booking);
        
        _repository.CreatePlayerToBooking(player, booking);
    }
    
    public void AddPadelCourtToBooking(int courtNumber, int bookingNumber)
    {
        PadelCourt padelCourt = _repository.ReadPadelCourt(courtNumber);
        Booking booking = _repository.ReadBooking(bookingNumber);
        
        Validate(padelCourt);
        Validate(booking);
        
        _repository.CreatePadelCourtToBooking(padelCourt, booking);
    }

    public void RemovePlayerFromBooking(int playerNumber, int bookingNumber)
    {
        Player player = _repository.ReadPlayer(playerNumber);
        Booking booking = _repository.ReadBooking(bookingNumber);
        
        Validate(player);
        Validate(booking);
        
        _repository.DeletePlayerFromBooking(player, booking);
    }

    public IEnumerable<Booking> GetBookingsOfPlayer(int playerNumber)
    {
        Player player = _repository.ReadPlayer(playerNumber);
        
        Validate(player);
        
        return _repository.ReadBookingsOfPlayer(player);
    }
    
    public IEnumerable<Player> GetPlayersOfPadelCourt(int courtNumber)
    {
        IEnumerable<Player> players = _repository.ReadPlayersOfPadelCourt(courtNumber);
        
        return players;
    }
    
    public int AddBooking(int playerNumber, int courtNumber, Booking booking, bool returnBookingNumber)
    {
        Player player = _repository.ReadPlayer(playerNumber);
        PadelCourt padelCourt = _repository.ReadPadelCourt(courtNumber);
        
        Validate(player);
        Validate(padelCourt);
        
        booking.Player = player;
        booking.PadelCourt = padelCourt;
        Validate(booking);
        
        int bookingNumber = _repository.CreateBooking(booking, returnBookingNumber);
        return bookingNumber;
    }
    
    public IEnumerable<PadelCourt> GetAllPadelCourts()
    {
        return _repository.ReadAllPadelCourts();
    }
    
    public IEnumerable<Player> GetAllPlayersWithManager()
    {
        return _repository.ReadAllPlayersWithManager();
    }
    
    public Player GetPlayerWithBookingsAndPadelCourtsAndManager(int playerNumber)
    {
        return _repository.ReadPlayerWithBookingsAndPadelCourtsAndManager(playerNumber);
    }
    
    public void UpdatePlayer(Player player)
    {
        Validate(player);
        _repository.WritePlayer(player);
    }
    
    public Player GetPlayerWithUser(int playerNumber)
    {
        return _repository.ReadPlayerWithUser(playerNumber);
    }
    
    private void Validate(Player player) // Validate the Player object (overload)
    {
        if (player == null) throw new ValidationException("\nAn error occurred, please try again:\n * Player does not exist\nend");
        
        List<ValidationResult> errors = new List<ValidationResult>();
        bool valid = Validator.TryValidateObject(player, new ValidationContext(player), errors, true);
        
        if (!valid) // If the object is not valid
        {
            string errorString = "\nAn error occurred, please try again:\n * ";
            
            foreach (ValidationResult error in errors) errorString += error.ErrorMessage + "\n * "; // Add each error to the errorString
            
            throw new ValidationException(errorString + "end"); // Throw a ValidationException with the errorString to the caller
        }
    }

    private void Validate(Booking booking) // Validate the Booking object (overload)
    {
        if (booking == null) throw new ValidationException("\nAn error occurred, please try again:\n * Booking does not exist\nend");
        
        List<ValidationResult> errors = new List<ValidationResult>();
        bool valid = Validator.TryValidateObject(booking, new ValidationContext(booking), errors, true);
        
        if (!valid) // If the object is not valid
        {
            string errorString = "\nAn error occurred, please try again:\n * ";
            
            foreach (ValidationResult error in errors) errorString += error.ErrorMessage + "\n * "; // Add each error to the errorString
            
            throw new ValidationException(errorString + "end"); // Throw a ValidationException with the errorString to the caller
        }
    }
    
    private void Validate(PadelCourt padelCourt) // Validate the PadelCourt object (overload)
    {
        if (padelCourt == null) throw new ValidationException("\nAn error occurred, please try again:\n * PadelCourt does not exist\nend");
        
        List<ValidationResult> errors = new List<ValidationResult>();
        bool valid = Validator.TryValidateObject(padelCourt, new ValidationContext(padelCourt), errors, true);
        
        if (!valid) // If the object is not valid
        {
            string errorString = "\nAn error occurred, please try again:\n * ";
            
            foreach (ValidationResult error in errors) errorString += error.ErrorMessage + "\n * "; // Add each error to the errorString
            
            throw new ValidationException(errorString + "end"); // Throw a ValidationException with the errorString to the caller
        }
    }
    
    private void Validate(Club club) // Validate the Club object (overload)
    {
        if (club == null) throw new ValidationException("\nAn error occurred, please try again:\n * Club does not exist\nend");
        
        List<ValidationResult> errors = new List<ValidationResult>();
        bool valid = Validator.TryValidateObject(club, new ValidationContext(club), errors, true);
        
        if (!valid) // If the object is not valid
        {
            string errorString = "\nAn error occurred, please try again:\n * ";
            
            foreach (ValidationResult error in errors) errorString += error.ErrorMessage + "\n * "; // Add each error to the errorString
            
            throw new ValidationException(errorString + "end"); // Throw a ValidationException with the errorString to the caller
        }
    }
}