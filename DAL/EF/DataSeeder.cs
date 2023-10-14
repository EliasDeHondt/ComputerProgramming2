/***************************************
 *                                     *
 *   Created by Elias De Hondt         *
 *   Visit https://eliasdh.com         *
 *                                     *
 ***************************************/
// Class DataSeeder
using PadelClubManagement.BL.Domain;

namespace PadelClubManagement.DAL.EF;

public static class DataSeeder
{
    public static void Seed(PadelClubManagementDbContext dbContext)
    {
        // Seed data for Club (1 Object) (1 op veel relatie)
        Club club1 = new Club { ClubNumber = 1, Name = "Padel Club1", NumberOfCours = 5, StreetName = "Kattenbroek", HouseNumber = 3, ZipCode = 2650 };
        
        // Seed data for PadelCourts (6 Objects)
        PadelCourt padelCourt1 = new PadelCourt { CourtNumber = 1, IsIndoor = true, Capacity = 4, Price = 20.50, Club = club1 };
        PadelCourt padelCourt2 = new PadelCourt { CourtNumber = 2,  IsIndoor = true, Capacity = 4, Price = 20.50, Club = club1 };
        PadelCourt padelCourt3 = new PadelCourt { CourtNumber = 3,  IsIndoor = true, Capacity = 4, Price = 20.50, Club = club1 };
        PadelCourt padelCourt4 = new PadelCourt { CourtNumber = 4,  IsIndoor = false, Capacity = 2, Price = 15.75, Club = club1 };
        PadelCourt padelCourt5 = new PadelCourt { CourtNumber = 5,  IsIndoor = false, Capacity = 2, Price = 15.75, Club = club1 };
        PadelCourt padelCourt6 = new PadelCourt { CourtNumber = 6,  IsIndoor = false, Capacity = 2, Price = 20.50, Club = club1 };
        
        // Seed data for Players (5 Objects)
        Player player1 = new Player { PlayerNumber = 1, FirstName = "Elias", LastName = "De Hondt", BirthDate = new DateOnly(2001, 4, 10), Level = 5.5, Position = PlayerPosition.Member };
        Player player2 = new Player { PlayerNumber = 2,  FirstName = "Alice", LastName = "Johnson", BirthDate = new DateOnly(1990, 3, 20), Level = 6.2, Position = PlayerPosition.Instructor };
        Player player3 = new Player { PlayerNumber = 3,  FirstName = "Bob", LastName = "Smith", BirthDate = new DateOnly(1988, 12, 5), Level = 5.0, Position = PlayerPosition.TournamentPlayer };
        Player player4 = new Player { PlayerNumber = 4,  FirstName = "Carol", LastName = "Davis", BirthDate = new DateOnly(1995, 8, 15), Level = 4.5, Position = PlayerPosition.Member };
        Player player5 = new Player { PlayerNumber = 5,  FirstName = "David", LastName = "Lee", BirthDate = new DateOnly(1992, 6, 10), Level = 4.2, Position = PlayerPosition.Guest };
          
        // Add players to the DbContext (Database)
        dbContext.Players.Add(player1);
        dbContext.Players.Add(player2);
        dbContext.Players.Add(player3);
        dbContext.Players.Add(player4);
        dbContext.Players.Add(player5);
          
        // Add padelCourts to the DbContext (Database)
        dbContext.PadelCourts.Add(padelCourt1);
        dbContext.PadelCourts.Add(padelCourt2);
        dbContext.PadelCourts.Add(padelCourt3);
        dbContext.PadelCourts.Add(padelCourt4);
        dbContext.PadelCourts.Add(padelCourt5);
        dbContext.PadelCourts.Add(padelCourt6);
          
        // Add club to the DbContext (Database)
        dbContext.Clubs.Add(club1);

        dbContext.SaveChanges(); // Save changes to the database
        
        dbContext.ChangeTracker.Clear(); // Clear the change tracker (Delete all entities from the change tracker)
    }
}