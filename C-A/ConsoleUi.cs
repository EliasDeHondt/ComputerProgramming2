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
    public List<Player> Players = new List<Player>();
    public List<PadelCourt> PadelCourts = new List<PadelCourt>();
    
    public void StartConsoleUi()
    {
        Seed(); // Seed the application with some data
        Console.WriteLine("Welcome to the Padel Club application!");
        
        Boolean ProgramLoop = true;
        while (ProgramLoop)
        {
            Console.WriteLine("What would you like to do?");
            Console.WriteLine("===============================");
            Console.WriteLine("0) Quit\n1) Show all Players\n2) Show players by position\n3) Show all Padel Courts\n4) Show Padel Courts with Price and/or (Indoor?)");
            Console.Write("Choice (0-4): ");
            string Input = Console.ReadLine();
            

            switch (Input)
            {
                case "0":
                    Console.WriteLine("Goodbye!");
                    ProgramLoop = false;
                    break;
                case "1":
                    foreach (Player player in Players)
                    {
                        Console.WriteLine(player.ToString());
                    }
                    break;
                case "2":
                    Console.WriteLine("Which position would you like to see?");
                    
                    foreach (PlayerPosition position in Enum.GetValues(typeof(PlayerPosition))) // Show all possible positions
                    {
                        Console.WriteLine($"{(byte)position}) {position}");
                    }

                    string InputPosition = Console.ReadLine();
                    PlayerPosition Position = (PlayerPosition) Enum.Parse(typeof(PlayerPosition), InputPosition); // Parse the input to a PlayerPosition enum
                    
                    foreach (Player player in Players)
                    {
                        if (player.Position == Position)
                        {
                            Console.WriteLine(player.ToString());
                        }
                    }
                    break;
                case "3":
                    foreach (PadelCourt padelCourt in PadelCourts)
                    {
                        Console.WriteLine(padelCourt.ToString());
                    }
                    break;
                case "4":
                    Double Price = 0.0; // Set the price to 0.0 by default
                    Boolean NewInputIndoor = false; // Set the indoor/outdoor to false by default
                    Boolean InputFirstQuestion = true;
                    Boolean IsParsedPrice;
                    Boolean InputSecondQuestion = true;
                    string InputPrice = "";
                    string InputIndoor = "";
                    
                    while (InputFirstQuestion)
                    {
                        Console.Write("Enter te price of the Padel Court or leave blank: ");
                        InputPrice = Console.ReadLine();
                        
                        IsParsedPrice = Double.TryParse(InputPrice, out Price); // Try to parse the input to a double
                        // If the input is a double, set InputFirstQuestion to false (stop the while loop)
                        // of
                        // If the input is null, empty or whitespace, set InputFirstQuestion to false (stop the while loop)
                        if (IsParsedPrice || string.IsNullOrWhiteSpace(InputPrice)) InputFirstQuestion = false;
                    }

                    while (InputSecondQuestion)
                    {
                        Console.Write("Enter (I)ndoor or (O)utdoor or leave blank: ");
                        InputIndoor = Console.ReadLine();
                        InputIndoor = InputIndoor.ToLower();
                        if (InputIndoor == "i" || InputIndoor == "o") InputSecondQuestion = false; // If the input is "i" or "o", set InputSecondQuestion to false (stop the while loop)
                        if (string.IsNullOrWhiteSpace(InputIndoor)) InputSecondQuestion = false; // If the input is null, empty or whitespace, set InputSecondQuestion to false (stop the while loop)
                        if (InputIndoor == "i") NewInputIndoor = true; else NewInputIndoor = false; // If the input is "i", set NewInputIndoor to true, else set it to false
                    }
                    
                    
                    foreach (PadelCourt padelCourt in PadelCourts)
                    {
                        if (string.IsNullOrWhiteSpace(InputPrice) == false && string.IsNullOrWhiteSpace(InputIndoor) == false) // If both inputs are not null, empty or whitespace
                        {
                            if (padelCourt.Price == Price && padelCourt.IsIndoor == NewInputIndoor) // If the price and indoor/outdoor match
                            {
                                if (Price != 0.0) Console.WriteLine(padelCourt.ToString()); // If the price is not 0.0, show the Padel Court
                            }
                        }
                        else if (string.IsNullOrWhiteSpace(InputPrice) == false) // If only the price input is not null, empty or whitespace
                        {
                            if (padelCourt.Price == Price) // If the price matches
                            {
                                if (Price != 0.0) Console.WriteLine(padelCourt.ToString()); // If the price is not 0.0, show the Padel Court
                            }
                        }
                        else if (string.IsNullOrWhiteSpace(InputIndoor) == false) // If only the indoor/outdoor input is not null, empty or whitespace
                        {
                            if (padelCourt.IsIndoor == NewInputIndoor) // If the indoor/outdoor matches
                            {
                                Console.WriteLine(padelCourt.ToString());
                            }
                        }
                        else // If both inputs are null, empty or whitespace
                        {
                            Console.WriteLine(padelCourt.ToString());
                        }
                    }
                    break;
            }
        }

    }

    public void Seed() // Seed the application with some data
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