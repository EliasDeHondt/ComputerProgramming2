/***************************************
 *                                     *
 *   Created by Elias De Hondt         *
 *   Visit https://eliasdh.com         *
 *                                     *
 ***************************************/
// Class ConsoleUi
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
                          Choice (0-4): 
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
        Console.Write("Choice (1-4): ");
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
}