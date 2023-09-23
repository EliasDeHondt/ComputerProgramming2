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
            Console.WriteLine("0) Quit\n1) Show all Players\n2) Show players by position\n3) Show all Padel Courts\n4) Show Padel Courts with CourtNumber and/or (Indoor?)");
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
                    Console.WriteLine("Which court number would you like to see?");
                    string InputCourtNumber = Console.ReadLine();
                    
                    
                    
                    Console.WriteLine("Would you like to see indoor courts? (y/n)");
                    string InputIndoor = Console.ReadLine();
                    
                    
                    break;
            }
        }

    }

    public void Seed()
    { 
        // Seed data for Club (1 Object)
        Club Club = new Club(); // Create a new instance of Club
        Club.Name = "Padel Club"; // Set the name of the club
        Club.NumberOfCours = 5; // Set the number of courts
        Club.StreetName = "Kattenbroek"; // Set the street name
        Club.HouseNumber = 3; // Set the house number
        Club.ZipCode = 2650; // Set the zip code
        
        // Seed data for PadelCourts (5 Objects)
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
        
        // Seed data for Players (5 Objects)
        Players.Add(new Player
        {
            FirstName = "Elias", 
            LastName = "De Hondt", 
            BirthDate = new DateOnly(2001, 4, 10), 
            Level = 5.5, 
            Position = PlayerPosition.Member, 
            PlayedOnCourts = new List<PadelCourt> {PadelCourts[0], PadelCourts[1]}  // 2 courts (veel-op-veel relatie)
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