/***************************************
 *                                     *
 *   Created by Elias De Hondt         *
 *   Visit https://eliasdh.com         *
 *                                     *
 ***************************************/
// Class ConsoleUi
using System.ComponentModel.DataAnnotations;
using PadelClubManagement.BL.Domain;
using PadelClubManagement.BL;
using PadelClubManagement.UI.CA.Extensions;

namespace PadelClubManagement.UI.CA;

public class ConsoleUi
{
    private IManager _manager; // IManager is an interface, so we can use dependency injection
    public ConsoleUi(IManager manager) // Constructor
    {
        _manager = manager;
    }

    public void Start() // Start the application
    {
        Console.WriteLine("Welcome to the Padel Club application!");
        bool programLoop = true;
        String line = new String('=', 26); // Create a new string with 26 "="
        while (programLoop) // While programLoop is true, run the code below
        {
            Console.Write("\n");
            PrintMenu();
            string input = Console.ReadLine();
            Console.Write("\n");
            
            switch (input)
            {
                case "0":
                    Console.WriteLine("Goodbye!");
                    programLoop = false;
                    break;
                case "1":
                    Console.WriteLine($"All Players:\n{line}");
                    ShowAllPlayers();
                    break;
                case "2":
                    Console.WriteLine($"Players by position:\n{line}");
                    ShowPlayersByPosition();
                    break;
                case "3":
                    Console.WriteLine($"All Padel Courts:\n{line}");
                    ShowAllPadelCourts();
                    break;
                case "4":
                    Console.WriteLine($"Padel Courts by filter:\n{line}");
                    ShowPadelCourtsByFilter();
                    break;
                case "5":
                    Console.WriteLine($"Add Player:\n{line}");
                    AddPlayer();
                    break;
                case "6":
                    Console.WriteLine($"Add Padel Court:\n{line}");
                    AddPadelCourt();
                    break;
                case "7":
                    Console.WriteLine($"Add Player to Booking:\n{line}");
                    AddPlayerToBooking();
                    break;
                case "8":
                    Console.WriteLine($"Remove Player from Booking:\n{line}");
                    RemovePlayerFromBooking();
                    break;
                default:
                    ValidationException validationException = new ValidationException("An error occurred, please try again:\n * Invalid input. Please enter a number from 0 to 6.\n * end");
                    CatchValidationException(validationException);
                    break;
            }
        }
    }

    private void PrintMenu() // Prints the menu
    {
        Console.Write("""
                          What would you like to do?
                          ==========================
                          0) Quit
                          1) Show all Players
                          2) Show players by position
                          3) Show all Padel Courts
                          4) Show Padel Courts with Price and/or (Indoor?)
                          5) Add a player
                          6) Add a Padel Court
                          7) Add Player to Booking
                          8) Remove Player from Booking
                          Choice (0-8): 
                          """);
    }
    
    private void ShowAllClubsBrief() // Shows all the clubs (brief)
    {
        IEnumerable<Club> clubs = _manager.GetAllClubs();
        foreach (Club club in clubs) Console.WriteLine(club.GetInfoBrief());
    }
    
    private void ShowAllPlayersBrief() // Shows all the players (brief)
    {
        IEnumerable<Player> players = _manager.GetAllPlayers();
        foreach (Player player in players) Console.WriteLine(player.GetInfoBrief());
    }
    
    private void ShowAllBookingsBrief() // Shows all the bookings (brief)
    {
        IEnumerable<Booking> bookings = _manager.GetAllBookings();
        foreach (Booking booking in bookings) Console.WriteLine(booking.GetInfoBrief());
    }
    
    private void ShowAllPlayers() // Shows all the players
    {
        IEnumerable<Player> players = _manager.GetAllPlayersWithBookingsAndPadelCourts();
        foreach (Player player in players) Console.WriteLine(player.GetInfo());
    }
    
    private void ShowPositions() // Shows all the positions
    {
        foreach (PlayerPosition positionE in Enum.GetValues(typeof(PlayerPosition))) // Loop through all the values of the PlayerPosition enum
        {
            Console.WriteLine($"{(byte)positionE}) {positionE}");
        }
        Console.Write("Choice position (1-4): ");
    }

    private void ShowPlayersByPosition() // Shows all the players by position
    {
        Console.WriteLine("Which position would you like to see?");
        ShowPositions();
        string inputPosition = Console.ReadLine();
        
        if (Enum.TryParse(inputPosition, out PlayerPosition position)) // If the inputPosition is a valid PlayerPosition
        {
            IEnumerable<Player> players = _manager.GetPlayersByPosition(position); // Get all the players by position from the manager
            foreach (Player player in players) Console.WriteLine(player.GetInfoBrief()); // Print all the players in a foreach loop (if position == player.Position)
        }
    }

    private void ShowAllPadelCourtsBrief() // Shows all the PadelCourts (brief)
    {
        IEnumerable<PadelCourt> padelCourts = _manager.GetAllPadelCourts();
        foreach (PadelCourt padelCourt in padelCourts) Console.WriteLine(padelCourt.GetInfoBrief());
    }
    
    private void ShowAllPadelCourts() // Shows all the PadelCourts
    {
        IEnumerable<PadelCourt> padelCourts = _manager.GetAllPadelCourtsWithClub();
        foreach (PadelCourt padelCourt in padelCourts) Console.WriteLine(padelCourt.GetInfo());
    }

    private void ShowPadelCourtsByFilter() // Shows all the PadelCourts with a price and/or indoor filter
    {
        double? price = GetPriceFilter();
        bool? indoor = GetIndoorFilter();
        
        IEnumerable<PadelCourt> padelCourts = _manager.GetPadelCourtsByFilter(price, indoor); // Get all the PadelCourts by filter from the manager
        foreach (PadelCourt padelCourt in padelCourts) Console.WriteLine(padelCourt.GetInfoBrief()); // Print all the PadelCourts in a foreach loop (if price == padelCourt.Price && indoor == padelCourt.IsIndoor)
    }

    private double? GetPriceFilter() // Returns a double or null (double?)
    {
        do
        {
            Console.Write("Enter the price of the Padel Court or leave blank: ");
            string inputPrice = Console.ReadLine();
            
            if (String.IsNullOrWhiteSpace(inputPrice)) return null; // If the inputPrice is null or whitespace, return null
            
            if (Double.TryParse(inputPrice, out double price)) return price; // If the inputPrice is a valid double, return the price
            
            ValidationException validationException = new ValidationException("\nAn error occurred, please try again:\n * Invalid input for price. Please enter a valid number.\n * end\n");
            CatchValidationException(validationException);
        } while (true);
    }

    private bool? GetIndoorFilter() // Returns a bool or null (bool?)
    {
        do
        {
            Console.Write("Enter (I)ndoor or (O)utdoor or leave blank: ");
            string inputIndoor = Console.ReadLine()?.ToLower();
            
            if (String.IsNullOrWhiteSpace(inputIndoor)) return null;
            
            if (inputIndoor == "i" || inputIndoor == "o") return inputIndoor == "i";
            
            ValidationException validationException = new ValidationException("\nAn error occurred, please try again:\n * Invalid input for indoor/outdoor. Please enter 'I' for indoor or 'O' for outdoor.\n * end\n");
            CatchValidationException(validationException);
        } while (true);
    }
    
    private void AddPlayer() // Add a player
    {
        try
        {
            Console.Write("Enter the first name of the player: ");
            string firstName = Console.ReadLine();
        
            Console.Write("Enter the last name of the player: ");
            string lastName = Console.ReadLine();
        
            Console.Write("Enter the birth date of the player (dd/MM/yyyy): ");
            string inputBirthDate = Console.ReadLine();
            DateOnly birthDate;
            if (!String.IsNullOrWhiteSpace(inputBirthDate)) // If inputBirthDate is not null or whitespace
            {
                bool isParsedDate = DateTime.TryParseExact(inputBirthDate, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime parsedDate);
                
                if (isParsedDate) birthDate = new DateOnly(parsedDate.Year, parsedDate.Month, parsedDate.Day); // is it Parse? Yes/No
 
                else birthDate = new DateOnly(01, 01, 0001); // If it's not convertible to a DateTime set it to 00/00/0000 to trigger the validation exception.
            }
            else birthDate = new DateOnly(01, 01, 0001); // If it's null or whitespace set it to 00/00/0000 to trigger the validation exception.
            
            Console.Write("Enter the level of the player: ");
            string inputLevel = Console.ReadLine();
            int level;
            bool isParsedLevel = Int32.TryParse(inputLevel, out level); // is it Parse? Yes/No
            if (!isParsedLevel) level = 11; // If it's not convertible to an int set it to 11 to trigger the validation exception.
        
            ShowPositions();
            string inputPosition = Console.ReadLine();
            PlayerPosition position;
            bool isParsedPosition = Enum.TryParse(inputPosition, out position); // is it Parse? Yes/No
            if (!isParsedPosition) position = PlayerPosition.Member; // If it's not convertible set it to Member default.
        
            _manager.AddPlayer(firstName, lastName, birthDate, level, position);
        }
        catch (ValidationException validationException)
        {
            CatchValidationException(validationException);
        }
    }

    private bool ConvertInputIndoor(string inputIndoor) // Convert the inputIndoor string to a bool
    {
        inputIndoor = inputIndoor?.ToLower(); // ToLower() to make it case insensitive ? to make it nullable (because of the null check)
        bool isIndoor;

        if (inputIndoor == "y") isIndoor = true;
        else if (inputIndoor == "n") isIndoor = false;
        else isIndoor = true;
        
        return isIndoor;
    }
    
    public Club SelectClubForPadelCourt()
    {
        Club selectedClub = null;

        do
        {
            ShowAllClubsBrief();
            Console.Write("Enter the club number where the Padel Court is located: ");
            string inputClubNumber = Console.ReadLine();

            if (int.TryParse(inputClubNumber, out int clubNumber))
            {
                selectedClub = _manager.GetAllClubs().FirstOrDefault(c => c.ClubNumber == clubNumber);

                if (selectedClub == null)
                {
                    ValidationException validationException = new ValidationException("\nAn error occurred, please try again:\n * The entered club number does not exist. Please try again.\n * end\n");
                    CatchValidationException(validationException);
                }
            }
            else
            {
                ValidationException validationException = new ValidationException("\nAn error occurred, please try again:\n * Invalid input. Please enter a valid club number.\n * end\n");
                CatchValidationException(validationException);
            }

        } while (selectedClub == null);

        return selectedClub;
    }

    
    private void AddPadelCourt() // Add a PadelCourt
    {
        try
        {
            Console.Write("Is the Padel Court indoor? (Y/n): ");
            string inputIndoor = Console.ReadLine();
            bool isIndoor = ConvertInputIndoor(inputIndoor);
            
            Console.Write("Enter the capacity of the Padel Court: ");
            string inputCapacity = Console.ReadLine();
            int capacity = Int32.TryParse(inputCapacity, out int capacityInt) ? capacityInt : 5; // If it's not convertible to an int set it to 5 to trigger the validation exception.
        
            Console.Write("Enter the price of the Padel Court: ");
            string inputPrice = Console.ReadLine();
            double price;
            bool isParsedPrice = Double.TryParse(inputPrice, out price); // is it Parse? Yes/No
            if (!isParsedPrice) price = 101.00; // If it's not convertible to a double set it to 101.00 to trigger the validation exception.
            
            
            Club club = SelectClubForPadelCourt();
            
            _manager.AddPadelCourt(isIndoor, capacity, price, club);

        }
        catch (ValidationException validationException)
        {
            CatchValidationException(validationException);
        }
    }

    private int ChoosePlayer() // Choose a player
    {
        ShowAllPlayersBrief();
        Console.Write("Enter the player number: ");
        string inputPlayerNumber = Console.ReadLine();
        int playerNumber;
        
        bool isParsedPlayerNumber = Int32.TryParse(inputPlayerNumber, out playerNumber); // is it Parse? Yes/No
        if (!isParsedPlayerNumber)
        {
            ValidationException validationException = new ValidationException("\nAn error occurred, please try again:\n * Invalid input for player number. Please enter a valid number.\n * end\n");
            CatchValidationException(validationException);
        }
        return playerNumber;
    }
    
    private int ChooseBooking(bool showInfoBookingsBrief) // Choose a booking
    {
        if (showInfoBookingsBrief) ShowAllBookingsBrief();
        Console.Write("Enter the booking number: ");
        string inputBookingNumber = Console.ReadLine();
        int bookingNumber;
        
        bool isParsedBookingNumber = Int32.TryParse(inputBookingNumber, out bookingNumber); // is it Parse? Yes/No
        if (!isParsedBookingNumber)
        {
            ValidationException validationException = new ValidationException("\nAn error occurred, please try again:\n * Invalid input for booking number. Please enter a valid number.\n * end\n");
            CatchValidationException(validationException);
        }
        
        return bookingNumber;
    }
    
    private void AddPlayerToBooking() // Add a player to a booking
    {
        Console.WriteLine("Which player would you like to add to a booking?");
        int playerNumber = ChoosePlayer();
        
        Console.WriteLine("Which booking would you like to add the player to?");
        int bookingNumber = ChooseBooking(true);
        
        _manager.AddPlayerToBooking(playerNumber, bookingNumber);
    }
    
    private void RemovePlayerFromBooking() // Remove a player from a booking
    {
        Console.WriteLine("Which player would you like to remove from a booking?");
        int playerNumber = ChoosePlayer();
        
        Console.WriteLine("Which booking would you like to remove the player from?");
        
        IEnumerable<Booking> bookings = _manager.GetBookingsOfPlayer(playerNumber);
        foreach (Booking booking in bookings) Console.WriteLine(booking.GetInfoBrief());
        
        int bookingNumber = ChooseBooking(false);
        
        _manager.RemovePlayerFromBooking(playerNumber, bookingNumber);
    }
    
    private void CatchValidationException(ValidationException validationException) // Catch the ValidationException
    {
        Console.ForegroundColor = ConsoleColor.Red; // Set the console color to red
        Console.WriteLine(validationException.Message);
        Console.ResetColor(); // Reset the console color
    }
}