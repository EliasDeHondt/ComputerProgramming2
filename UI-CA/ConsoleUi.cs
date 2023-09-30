/***************************************
 *                                     *
 *   Created by Elias De Hondt         *
 *   Visit https://eliasdh.com         *
 *                                     *
 ***************************************/
// Entiteit ConsoleUi
using SC.BL.Domain;
namespace SC.UI.CA;

public class ConsoleUi
{   
    private List<Player> _players = new List<Player>(); // "_" means private (backfield)
    private List<PadelCourt> _padelCourts = new List<PadelCourt>(); // "_" means private (backfield)
    
    public ConsoleUi() // Constructor
    {
        SeedData();
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
        foreach (Player player in _players) // Loop through all the players
        {
            Console.WriteLine(player.ToString());
        }
    }

    private void ShowPlayersByPosition() // Shows all the players by position
    {
        Console.WriteLine("Which position would you like to see?");
        ShowPositions();
        Console.Write("Choice (1-4): ");
        string inputPosition = Console.ReadLine();
        
        if (Enum.TryParse(inputPosition, out PlayerPosition position)) // If the inputPosition is a valid PlayerPosition
        {
            foreach (Player player in _players)
            {
                if (player.Position == position) // If the position of the player is equal to the position of the user
                {
                    Console.WriteLine(player.ToString());
                }
            }
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
        foreach (PadelCourt padelCourt in _padelCourts)
        {
            Console.WriteLine(padelCourt.ToString());
        }
    }

    private void ShowPadelCourtsByFilter() // Shows all the PadelCourts with a price and/or indoor filter
    {
        double? price = GetPriceFilter();
        bool? indoor = GetIndoorFilter();

        foreach (PadelCourt padelCourt in _padelCourts)
        {
            // If price is null or the price of the padelCourt is equal to the price filter and indoor is null or the indoor of the padelCourt is equal to the indoor filter
            if ((!price.HasValue || padelCourt.Price == price.Value) && (!indoor.HasValue || padelCourt.IsIndoor == indoor.Value))
            {
                Console.WriteLine(padelCourt.ToString());
            }
        }
    }

    private double? GetPriceFilter() // Returns a double or null (double?)
    {
        while (true)
        {
            Console.Write("Enter the price of the Padel Court or leave blank: ");
            string inputPrice = Console.ReadLine();
            
            if (string.IsNullOrWhiteSpace(inputPrice)) return null;
            
            if (double.TryParse(inputPrice, out double price)) return price;
            
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
    
    private void SeedData() // Seed data for the application
    {
        // Seed data for Club (1 Object)
        Club club = new Club(); // Create a new instance of Club
        club.Name = "Padel Club"; // Set the name of the club
        club.NumberOfCours = 5; // Set the number of courts
        club.StreetName = "Kattenbroek"; // Set the street name
        club.HouseNumber = 3; // Set the house number
        club.ZipCode = 2650; // Set the zip code
        
        // Seed data for PadelCourts (6 Objects)
        _padelCourts.Add(new PadelCourt
        {
            CourtNumber = 1, 
            IsIndoor = true, 
            Capacity = 4, 
            Price = 20.50, 
            Club = club  // (1 op veel relatie)
        });
        _padelCourts.Add(new PadelCourt
        {
            CourtNumber = 2, 
            IsIndoor = true, 
            Capacity = 4, 
            Price = 20.50, 
            Club = club  // (1 op veel relatie)
        });
        _padelCourts.Add(new PadelCourt
        {
            CourtNumber = 3, 
            IsIndoor = true, 
            Capacity = 4, 
            Price = 20.50, 
            Club = club  // (1 op veel relatie)
        });
        _padelCourts.Add(new PadelCourt
        {
            CourtNumber = 4, 
            IsIndoor = false, 
            Capacity = 2, 
            Price = 15.75, 
            Club = club  // (1 op veel relatie)
        });
        _padelCourts.Add(new PadelCourt
        {
            CourtNumber = 5, 
            IsIndoor = false,
            Capacity = 2, 
            Price = 15.75, 
            Club = club // (1 op veel relatie)
        });
        _padelCourts.Add(new PadelCourt
        {
            CourtNumber = 6, 
            IsIndoor = false, 
            Capacity = 2, 
            Price = 20.50, 
            Club = club // (1 op veel relatie)
        });
        
        // Seed data for Players (5 Objects)
        _players.Add(new Player
        {
            PlayerNumber = 1,
            FirstName = "Elias", 
            LastName = "De Hondt", 
            BirthDate = new DateOnly(2001, 4, 10), 
            Level = 5.5, 
            Position = PlayerPosition.Member, 
            PlayedOnCourts = new List<PadelCourt> {_padelCourts[0], _padelCourts[1], _padelCourts[5]}  // 3 courts (veel-op-veel relatie)
        });
        _players.Add(new Player
        {
            PlayerNumber = 2,
            FirstName = "Alice", 
            LastName = "Johnson", 
            BirthDate = new DateOnly(1990, 3, 20), 
            Level = 6.2, 
            Position = PlayerPosition.Instructor,
            PlayedOnCourts = new List<PadelCourt> {_padelCourts[2]}  // 1 courts (veel-op-veel relatie)
        });
        _players.Add(new Player
        {
            PlayerNumber = 3,
            FirstName = "Bob", 
            LastName = "Smith", 
            BirthDate = new DateOnly(1988, 12, 5), 
            Level = 5.0, 
            Position = PlayerPosition.TournamentPlayer,
            PlayedOnCourts = new List<PadelCourt> {_padelCourts[3], _padelCourts[4]}  // 2 courts (veel-op-veel relatie)
        });
        _players.Add(new Player
        {
            PlayerNumber = 4,
            FirstName = "Carol", 
            LastName = "Davis", 
            BirthDate = new DateOnly(1995, 8, 15), 
            Level = 4.5, 
            Position = PlayerPosition.Member,
            PlayedOnCourts = new List<PadelCourt> {_padelCourts[4]}  // 1 courts (veel-op-veel relatie)
        });
        _players.Add(new Player
        {
            PlayerNumber = 5,
            FirstName = "David", 
            LastName = "Lee", 
            BirthDate = new DateOnly(1992, 6, 10), 
            Level = 4.2, 
            Position = PlayerPosition.Guest,
            PlayedOnCourts = new List<PadelCourt> {_padelCourts[0], _padelCourts[1], _padelCourts[2]} // 3 courts (veel-op-veel relatie)
        });
    }
}