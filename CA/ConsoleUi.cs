/***************************************
 *                                     *
 *   Created by Elias De Hondt         *
 *   Visit https://eliasdh.com         *
 *                                     *
 ***************************************/

// Entiteit ConsoleUi
namespace CA;

public class ConsoleUi
{
    private List<Player> Players = new List<Player>();
    private List<PadelCourt> PadelCourts = new List<PadelCourt>();

    public void StartConsoleUi()
    {
        SeedData();
        Console.WriteLine("Welcome to the Padel Club application!");

        bool programLoop = true;
        while (programLoop)
        {
            PrintMenu();
            string input = Console.ReadLine();

            switch (input)
            {
                case "0":
                    Console.WriteLine("Goodbye!");
                    programLoop = false;
                    break;
                case "1":
                    ShowAllPlayers();
                    break;
                case "2":
                    ShowPlayersByPosition();
                    break;
                case "3":
                    ShowAllPadelCourts();
                    break;
                case "4":
                    ShowPadelCourtsByFilter();
                    break;
                default:
                    Console.WriteLine("Invalid input. Please try again.");
                    break;
            }
        }
    }

    private void PrintMenu()
    {
        Console.Write("What would you like to do?\n" +
                      "===============================\n" +
                      "0) Quit\n" +
                      "1) Show all Players\n" +
                      "2) Show players by position\n" +
                      "3) Show all Padel Courts\n" +
                      "4) Show Padel Courts with Price and/or (Indoor?)\n" +
                      "Choice (0-4): ");
    }

    private void ShowAllPlayers()
    {
        foreach (Player player in Players)
        {
            Console.WriteLine(player.ToString());
        }
    }

    private void ShowPlayersByPosition()
    {
        
        Console.WriteLine("Which position would you like to see?");
        foreach (PlayerPosition positionE in Enum.GetValues(typeof(PlayerPosition)))
        {
            Console.WriteLine($"{(byte)positionE}) {positionE}");
        }

        string inputPosition = Console.ReadLine();
        if (Enum.TryParse(inputPosition, out PlayerPosition position))
        {
            foreach (Player player in Players)
            {
                if (player.Position == position)
                {
                    Console.WriteLine(player.ToString());
                }
            }
        }
    }

    private void ShowAllPadelCourts()
    {
        foreach (PadelCourt padelCourt in PadelCourts)
        {
            Console.WriteLine(padelCourt.ToString());
        }
    }

    private void ShowPadelCourtsByFilter()
    {
        double? price = GetPriceFilter();
        bool? indoor = GetIndoorFilter();

        foreach (PadelCourt padelCourt in PadelCourts)
        {
            // If price is null or the price of the padelCourt is equal to the price filter and indoor is null or the indoor of the padelCourt is equal to the indoor filter
            if ((!price.HasValue || padelCourt.Price == price.Value) && (!indoor.HasValue || padelCourt.IsIndoor == indoor.Value))
            {
                Console.WriteLine(padelCourt.ToString());
            }
        }
    }

    private double? GetPriceFilter()
    {
        while (true)
        {
            Console.Write("Enter the price of the Padel Court or leave blank: ");
            string inputPrice = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(inputPrice))
            {
                return null;
            }
            if (double.TryParse(inputPrice, out double price))
            {
                return price;
            }
            else
            {
                Console.WriteLine("Invalid input for price. Please enter a valid number.");
            }
        }
    }

    private bool? GetIndoorFilter()
    {
        while (true)
        {
            Console.Write("Enter (I)ndoor or (O)utdoor or leave blank: ");
            string inputIndoor = Console.ReadLine()?.ToLower();
            if (string.IsNullOrWhiteSpace(inputIndoor))
            {
                return null;
            }
            if (inputIndoor == "i" || inputIndoor == "o")
            {
                return inputIndoor == "i";
            }
            else
            {
                Console.WriteLine("Invalid input for indoor/outdoor. Please enter 'I' for indoor or 'O' for outdoor.");
            }
        }
    }
    
    private void SeedData() // Seed data for the application
    {
                // Seed data for Club (1 Object)
        Club Club = new Club(); // Create a new instance of Club
        Club.Name = "Padel Club"; // Set the name of the club
        Club.NumberOfCours = 5; // Set the number of courts
        Club.StreetName = "Kattenbroek"; // Set the street name
        Club.HouseNumber = 3; // Set the house number
        Club.ZipCode = 2650; // Set the zip code
        
        // Seed data for PadelCourts (6 Objects)
        PadelCourts.Add(new PadelCourt
        {
            CourtNumber = 1, 
            IsIndoor = true, 
            Capacity = 4, 
            Price = 20.50, 
            Club = Club  // (1 op veel relatie)
        });
        PadelCourts.Add(new PadelCourt
        {
            CourtNumber = 2, 
            IsIndoor = true, 
            Capacity = 4, 
            Price = 20.50, 
            Club = Club  // (1 op veel relatie)
        });
        PadelCourts.Add(new PadelCourt
        {
            CourtNumber = 3, 
            IsIndoor = true, 
            Capacity = 4, 
            Price = 20.50, 
            Club = Club  // (1 op veel relatie)
        });
        PadelCourts.Add(new PadelCourt
        {
            CourtNumber = 4, 
            IsIndoor = false, 
            Capacity = 2, 
            Price = 15.75, 
            Club = Club  // (1 op veel relatie)
        });
        PadelCourts.Add(new PadelCourt
        {
            CourtNumber = 5, 
            IsIndoor = false,
            Capacity = 2, 
            Price = 15.75, 
            Club = Club // (1 op veel relatie)
        });
        PadelCourts.Add(new PadelCourt
        {
            CourtNumber = 6, 
            IsIndoor = false, 
            Capacity = 2, 
            Price = 20.50, 
            Club = Club // (1 op veel relatie)
        });
        
        // Seed data for Players (5 Objects)
        Players.Add(new Player
        {
            FirstName = "Elias", 
            LastName = "De Hondt", 
            BirthDate = new DateOnly(2001, 4, 10), 
            Level = 5.5, 
            Position = PlayerPosition.Member, 
            PlayedOnCourts = new List<PadelCourt> {PadelCourts[0], PadelCourts[1], PadelCourts[5]}  // 3 courts (veel-op-veel relatie)
        });
        Players.Add(new Player
        {
            FirstName = "Alice", 
            LastName = "Johnson", 
            BirthDate = new DateOnly(1990, 3, 20), 
            Level = 6.2, 
            Position = PlayerPosition.Instructor,
            PlayedOnCourts = new List<PadelCourt> {PadelCourts[2]}  // 1 courts (veel-op-veel relatie)
        });
        Players.Add(new Player
        {
            FirstName = "Bob", 
            LastName = "Smith", 
            BirthDate = new DateOnly(1988, 12, 5), 
            Level = 5.0, 
            Position = PlayerPosition.TournamentPlayer,
            PlayedOnCourts = new List<PadelCourt> {PadelCourts[3], PadelCourts[4]}  // 2 courts (veel-op-veel relatie)
        });
        Players.Add(new Player
        {
            FirstName = "Carol", 
            LastName = "Davis", 
            BirthDate = new DateOnly(1995, 8, 15), 
            Level = 4.5, 
            Position = PlayerPosition.Member,
            PlayedOnCourts = new List<PadelCourt> {PadelCourts[4]}  // 1 courts (veel-op-veel relatie)
        });
        Players.Add(new Player
        {
            FirstName = "David", 
            LastName = "Lee", 
            BirthDate = new DateOnly(1992, 6, 10), 
            Level = 4.2, 
            Position = PlayerPosition.Guest,
            PlayedOnCourts = new List<PadelCourt> {PadelCourts[0], PadelCourts[1], PadelCourts[2]} // 3 courts (veel-op-veel relatie)
        });
    }
}
