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
                          Choice (0-6): 
                          """);
    }

    private void ShowAllPlayers() // Shows all the players
    {
        List<Player> players = _manager.GetAllPlayers(); // Get all the players from the manager
        foreach (Player player in players) Console.WriteLine(player.GetInfo()); // Print all the players in a foreach loop
    }

    private void ShowPlayersByPosition() // Shows all the players by position
    {
        Console.WriteLine("Which position would you like to see?");
        ShowPositions();
        string inputPosition = Console.ReadLine();
        
        if (Enum.TryParse(inputPosition, out PlayerPosition position)) // If the inputPosition is a valid PlayerPosition
        {
            List<Player> players = _manager.GetPlayersByPosition(position); // Get all the players by position from the manager
            foreach (Player player in players) Console.WriteLine(player.GetInfo()); // Print all the players in a foreach loop (if position == player.Position)
        }
    }
    
    private void ShowPositions() // Shows all the positions
    {
        foreach (PlayerPosition positionE in Enum.GetValues(typeof(PlayerPosition))) // Loop through all the values of the PlayerPosition enum
        {
            Console.WriteLine($"{(byte)positionE}) {positionE}");
        }
        Console.Write("Choice position (1-4): ");
    }

    private void ShowAllPadelCourts() // Shows all the PadelCourts
    {
        List<PadelCourt> padelCourts = _manager.GetAllPadelCourts(); // Get all the PadelCourts from the manager
        foreach (PadelCourt padelCourt in padelCourts) Console.WriteLine(padelCourt.GetInfo()); // Print all the PadelCourts in a foreach loop
    }

    private void ShowPadelCourtsByFilter() // Shows all the PadelCourts with a price and/or indoor filter
    {
        double? price = GetPriceFilter();
        bool? indoor = GetIndoorFilter();
        
        List<PadelCourt> padelCourts = _manager.GetPadelCourtsByFilter(price, indoor); // Get all the PadelCourts by filter from the manager
        foreach (PadelCourt padelCourt in padelCourts) Console.WriteLine(padelCourt.GetInfo()); // Print all the PadelCourts in a foreach loop (if price == padelCourt.Price && indoor == padelCourt.IsIndoor)
    }

    private double? GetPriceFilter() // Returns a double or null (double?)
    {
        while (true)
        {
            Console.Write("Enter the price of the Padel Court or leave blank: ");
            string inputPrice = Console.ReadLine();
            
            if (String.IsNullOrWhiteSpace(inputPrice)) return null; // If the inputPrice is null or whitespace, return null
            
            if (Double.TryParse(inputPrice, out double price)) return price; // If the inputPrice is a valid double, return the price
            
            ValidationException validationException = new ValidationException("\nAn error occurred, please try again:\n * Invalid input for price. Please enter a valid number.\n * end\n");
            CatchValidationException(validationException);
        }
    }

    private bool? GetIndoorFilter() // Returns a bool or null (bool?)
    {
        while (true) // While true, run the code below (until a return statement is reached)
        {
            Console.Write("Enter (I)ndoor or (O)utdoor or leave blank: ");
            string inputIndoor = Console.ReadLine()?.ToLower();
            
            if (String.IsNullOrWhiteSpace(inputIndoor)) return null;
            
            if (inputIndoor == "i" || inputIndoor == "o") return inputIndoor == "i";
            
            ValidationException validationException = new ValidationException("\nAn error occurred, please try again:\n * Invalid input for indoor/outdoor. Please enter 'I' for indoor or 'O' for outdoor.\n * end\n");
            CatchValidationException(validationException);
        }
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
                if (isParsedDate) // is it Parse? Yes/No
                {
                    birthDate = new DateOnly(parsedDate.Year, parsedDate.Month, parsedDate.Day);
                }
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
    
    private void AddPadelCourt() // Add a PadelCourt
    {
        try
        {
            Console.Write("Is the Padel Court indoor? (Y/n): ");
            string inputIndoor = Console.ReadLine()?.ToLower(); // ToLower() to make it case insensitive ? to make it nullable (because of the null check)
            bool isIndoor;
            if (inputIndoor == "y" || inputIndoor == "n") if (inputIndoor == "y") isIndoor = true; else isIndoor = false; // If inputIndoor is "y" or "n", set isIndoor to true or false
            else isIndoor = true; // If inputIndoor is not "y" or "n", set isIndoor to true (default)
        
            Console.Write("Enter the capacity of the Padel Court: ");
            string inputCapacity = Console.ReadLine();
            int capacity = Int32.TryParse(inputCapacity, out int capacityInt) ? capacityInt : 5; // If it's not convertible to an int set it to 5 to trigger the validation exception.
        
            Console.Write("Enter the price of the Padel Court: ");
            string inputPrice = Console.ReadLine();
            double price;
            bool isParsedPrice = Double.TryParse(inputPrice, out price); // is it Parse? Yes/No
            if (!isParsedPrice) price = 101.00; // If it's not convertible to a double set it to 101.00 to trigger the validation exception.
            
            Club club = new Club { Name = "Padel Club" }; // This is temporarily statically programmed!!!
            _manager.AddPadelCourt(isIndoor, capacity, price, club);
        }
        catch (ValidationException validationException)
        {
            CatchValidationException(validationException);
        }
    }
    
    private void CatchValidationException(ValidationException validationException) // Catch the ValidationException
    {
        Console.ForegroundColor = ConsoleColor.Red; // Set the console color to red
        Console.WriteLine(validationException.Message);
        Console.ResetColor(); // Reset the console color
    }
}