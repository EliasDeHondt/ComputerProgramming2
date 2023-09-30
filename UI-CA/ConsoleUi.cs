/***************************************
 *                                     *
 *   Created by Elias De Hondt         *
 *   Visit https://eliasdh.com         *
 *                                     *
 ***************************************/
// Class ConsoleUi
using System.ComponentModel.DataAnnotations;
using SC.BL.Domain;
using SC.BL;

namespace SC.UI.CA;

public class ConsoleUi
{
    private IManager _manager; // IManager is an interface, so we can use dependency injection
    public ConsoleUi(IManager manager) // Constructor
    {
        _manager = manager;
        Console.WriteLine("Welcome to the Padel Club application!");
        Start();
    }

    private void Start() // Start the application
    {
        bool programLoop = true;
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
                    Console.WriteLine("All Players:\n=============");
                    ShowAllPlayers();
                    break;
                case "2":
                    Console.WriteLine("Players by position:\n====================");
                    ShowPlayersByPosition();
                    break;
                case "3":
                    Console.WriteLine("All Padel Courts:\n=================");
                    ShowAllPadelCourts();
                    break;
                case "4":
                    Console.WriteLine("Padel Courts by filter:\n======================");
                    ShowPadelCourtsByFilter();
                    break;
                case "5":
                    AddPlayer();
                    break;
                case "6":
                    AddPadelCourt();
                    break;
                default:
                    Console.WriteLine("Invalid input. Please try again.");
                    break;
            }
        }
    }

    private void PrintMenu() // Prints the menu
    {
        Console.Write("""
                          What would you like to do?
                          ===============================
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
        foreach (Player player in players) Console.WriteLine(player.ToString()); // Print all the players in a foreach loop
    }

    private void ShowPlayersByPosition() // Shows all the players by position
    {
        Console.WriteLine("Which position would you like to see?");
        ShowPositions();
        string inputPosition = Console.ReadLine();
        
        if (Enum.TryParse(inputPosition, out PlayerPosition position)) // If the inputPosition is a valid PlayerPosition
        {
            List<Player> players = _manager.GetPlayersByPosition(position); // Get all the players by position from the manager
            foreach (Player player in players) Console.WriteLine(player.ToString()); // Print all the players in a foreach loop (if position == player.Position)
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
        foreach (PadelCourt padelCourt in padelCourts) Console.WriteLine(padelCourt.ToString()); // Print all the PadelCourts in a foreach loop
    }

    private void ShowPadelCourtsByFilter() // Shows all the PadelCourts with a price and/or indoor filter
    {
        double? price = GetPriceFilter();
        bool? indoor = GetIndoorFilter();
        
        List<PadelCourt> _padelCourts = _manager.GetPadelCourtsByFilter(price, indoor); // Get all the PadelCourts by filter from the manager
        foreach (PadelCourt padelCourt in _padelCourts) Console.WriteLine(padelCourt.ToString()); // Print all the PadelCourts in a foreach loop (if price == padelCourt.Price && indoor == padelCourt.IsIndoor)
    }

    private double? GetPriceFilter() // Returns a double or null (double?)
    {
        while (true)
        {
            Console.Write("Enter the price of the Padel Court or leave blank: ");
            string inputPrice = Console.ReadLine();
            
            if (string.IsNullOrWhiteSpace(inputPrice)) return null; // If the inputPrice is null or whitespace, return null
            
            if (double.TryParse(inputPrice, out double price)) return price; // If the inputPrice is a valid double, return the price
            
            Console.WriteLine("Invalid input for price. Please enter a valid number.");
        }
    }

    private bool? GetIndoorFilter() // Returns a bool or null (bool?)
    {
        while (true) // While true, run the code below (until a return statement is reached)
        {
            Console.Write("Enter (I)ndoor or (O)utdoor or leave blank: ");
            string inputIndoor = Console.ReadLine()?.ToLower();
            
            if (string.IsNullOrWhiteSpace(inputIndoor)) return null;
            
            if (inputIndoor == "i" || inputIndoor == "o") return inputIndoor == "i";
            
            Console.WriteLine("Invalid input for indoor/outdoor. Please enter 'I' for indoor or 'O' for outdoor."); // If inputIndoor is not "i" or "o"
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
        
            Console.Write("Enter the birth date of the player (DD/MM/YYYY): ");
            string inputBirthDate = Console.ReadLine();
            DateOnly? birthDate = null;
            
            if (DateTime.TryParseExact(inputBirthDate, "dd/mm/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime parsedDate))
            {
                int year = parsedDate.Year;
                int month = parsedDate.Month;
                int day = parsedDate.Day;
                birthDate = new DateOnly(year, month, day);
            }
            
           
        
            Console.Write("Enter the level of the player: ");
            string inputLevel = Console.ReadLine();
            int level;
            bool isParsedLevel = int.TryParse(inputLevel, out level); // is it Parse? Yes/No
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
            Console.ForegroundColor = ConsoleColor.Red; // Set the console color to red
            Console.WriteLine(validationException.Message);
            Console.ResetColor(); // Reset the console color
        }

    }
    
    private void AddPadelCourt() // Add a PadelCourt
    {
        try
        {
            Console.Write("Is the Padel Court indoor? (Y/n): ");
            string inputIndoor = Console.ReadLine().ToLower();
            bool isIndoor;
            if (inputIndoor == "y" || inputIndoor == "n") if (inputIndoor == "y") isIndoor = true; else isIndoor = false; // If inputIndoor is "y" or "n", set isIndoor to true or false
            else isIndoor = true; // If inputIndoor is not "y" or "n", set isIndoor to true (default)
        
            Console.Write("Enter the capacity of the Padel Court: ");
            string inputCapacity = Console.ReadLine();
            int capacity = int.TryParse(inputCapacity, out int capacityInt) ? capacityInt : 5; // If it's not convertible to an int set it to 5 to trigger the validation exception.
        
            Console.Write("Enter the price of the Padel Court: ");
            string inputPrice = Console.ReadLine();
            double price;
            bool isParsedPrice = double.TryParse(inputPrice, out price); // is it Parse? Yes/No
            if (!isParsedPrice) price = 0.1; // If it's not convertible to a double set it to 0.1 to trigger the validation exception.
            
            _manager.AddPadelCourt(isIndoor, capacity, price);
        }
        catch (ValidationException validationException)
        {
            Console.ForegroundColor = ConsoleColor.Red; // Set the console color to red
            Console.WriteLine(validationException.Message);
            Console.ResetColor(); // Reset the console color
        }
    }
}