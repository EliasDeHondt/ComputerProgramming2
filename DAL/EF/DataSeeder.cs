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
        // Seed data for Club
        Club club1 = new Club { ClubNumber = 1, Name = "Ter Eiken", NumberOfCours = 2, StreetName = "Kattenbroek", HouseNumber = 3, ZipCode = 2650 };
        Club club2 = new Club { ClubNumber = 2, Name = "Antwerp Padelclub", NumberOfCours = 3, StreetName = "Filip Williotstraat", HouseNumber = 1, ZipCode = 2600 };
        Club club3 = new Club { ClubNumber = 3, Name = "Open Padel", NumberOfCours = 2, StreetName = "Ranstsesteenweg", HouseNumber = 86, ZipCode = 2520  };
        Club club4 = new Club { ClubNumber = 4, Name = "The Box Padel", NumberOfCours = 4, StreetName = "Zonnestroomstraat", HouseNumber = 9, ZipCode = 2020 };
        Club club5 = new Club { ClubNumber = 5, Name = "Padel Metropool Antwerpen", NumberOfCours = 6, StreetName = "Maccabilaan", HouseNumber = 32, ZipCode = 2660 };
        
        // Seed data for PadelCourts
        PadelCourt padelCourt1 = new PadelCourt { CourtNumber = 1, IsIndoor = true, Capacity = 4, Price = 20.50};
        PadelCourt padelCourt2 = new PadelCourt { CourtNumber = 2, IsIndoor = true, Capacity = 4, Price = 20.50};
        PadelCourt padelCourt3 = new PadelCourt { CourtNumber = 3, IsIndoor = true, Capacity = 4, Price = 20.50};
        PadelCourt padelCourt4 = new PadelCourt { CourtNumber = 4, IsIndoor = false, Capacity = 2, Price = 15.75};
        PadelCourt padelCourt5 = new PadelCourt { CourtNumber = 5, IsIndoor = false, Capacity = 2, Price = 15.75};
        PadelCourt padelCourt6 = new PadelCourt { CourtNumber = 6, IsIndoor = false, Capacity = 2, Price = 20.50};
        PadelCourt padelCourt7 = new PadelCourt { CourtNumber = 7, IsIndoor = false, Capacity = 2, Price = 20.50};
        PadelCourt padelCourt8 = new PadelCourt { CourtNumber = 8, IsIndoor = false, Capacity = 2, Price = 20.50};
        
        // Seed data for Bookings
        Booking booking1 = new Booking { BookingNumber = 1, BookingDate = new DateOnly(2023, 4, 15), StartTime = new TimeSpan(9, 30, 0), EndTime = new TimeSpan(10, 30, 0) };
        Booking booking2 = new Booking { BookingNumber = 2, BookingDate = new DateOnly(2023, 4, 15), StartTime = new TimeSpan(11, 0, 0), EndTime = new TimeSpan(12, 0, 0) };
        Booking booking3 = new Booking { BookingNumber = 3, BookingDate = new DateOnly(2023, 4, 16), StartTime = new TimeSpan(14, 30, 0), EndTime = new TimeSpan(15, 30, 0) };
        Booking booking4 = new Booking { BookingNumber = 4, BookingDate = new DateOnly(2023, 4, 16), StartTime = new TimeSpan(10, 0, 0), EndTime = new TimeSpan(11, 0, 0) };
        Booking booking5 = new Booking { BookingNumber = 5, BookingDate = new DateOnly(2023, 4, 17), StartTime = new TimeSpan(13, 0, 0), EndTime = new TimeSpan(14, 0, 0) };
        
        // Seed data for Players
        Player player1 = new Player { PlayerNumber = 1, FirstName = "Elias", LastName = "De Hondt", BirthDate = new DateOnly(2001, 4, 10), Level = 5.5, Position = PlayerPosition.Member };
        Player player2 = new Player { PlayerNumber = 2, FirstName = "Alice", LastName = "Johnson", BirthDate = new DateOnly(1990, 3, 20), Level = 6.2, Position = PlayerPosition.Instructor };
        Player player3 = new Player { PlayerNumber = 3, FirstName = "Bob", LastName = "Smith", BirthDate = new DateOnly(1988, 12, 5), Level = 5.0, Position = PlayerPosition.TournamentPlayer };
        Player player4 = new Player { PlayerNumber = 4, FirstName = "Carol", LastName = "Davis", BirthDate = new DateOnly(1995, 8, 15), Level = 4.5, Position = PlayerPosition.Member };
        Player player5 = new Player { PlayerNumber = 5, FirstName = "David", LastName = "Lee", BirthDate = new DateOnly(1992, 6, 10), Level = 4.2, Position = PlayerPosition.Guest };
        
        // Relate Club to PadelCourts
        club1.PadelCourts = new List<PadelCourt> { padelCourt1, padelCourt2 };
        club2.PadelCourts = new List<PadelCourt> { padelCourt3 };
        club3.PadelCourts = new List<PadelCourt> { padelCourt4, padelCourt5 };
        club4.PadelCourts = new List<PadelCourt> { padelCourt6 };
        club5.PadelCourts = new List<PadelCourt> { padelCourt7, padelCourt8 };
        
        // Relate PadelCourts to Club
        padelCourt1.Club = club1;
        padelCourt2.Club = club1;
        padelCourt3.Club = club2;
        padelCourt4.Club = club3;
        padelCourt5.Club = club3;
        padelCourt6.Club = club4;
        padelCourt7.Club = club5;
        padelCourt8.Club = club5;
        
        // Relate PadelCourts to Bookings
        padelCourt1.Bookings = new List<Booking> { booking1, booking2 };
        padelCourt2.Bookings = new List<Booking> { booking3 };
        padelCourt3.Bookings = new List<Booking> { booking4 };
        padelCourt4.Bookings = new List<Booking> { booking5 };
        
        // Relate Bookings to PadelCourts
        booking1.PadelCourt = padelCourt1;
        booking2.PadelCourt = padelCourt1;
        booking3.PadelCourt = padelCourt2;
        booking4.PadelCourt = padelCourt3;
        booking5.PadelCourt = padelCourt4;
        
        // Relate Bookings to Players
        booking1.Player = player1;
        booking2.Player = player2;
        booking3.Player = player3;
        booking4.Player = player4;
        booking5.Player = player5;
        
        // Relate Players to Bookings
        player1.Bookings = new List<Booking> { booking1 };
        player2.Bookings = new List<Booking> { booking2 };
        player3.Bookings = new List<Booking> { booking3 };
        player4.Bookings = new List<Booking> { booking4 };
        player5.Bookings = new List<Booking> { booking5 };
        
        // Add club to the DbContext (Database)
        dbContext.Clubs.Add(club1);
        dbContext.Clubs.Add(club2);
        dbContext.Clubs.Add(club3);
        dbContext.Clubs.Add(club4);
        dbContext.Clubs.Add(club5);
        
        // Add padelCourts to the DbContext (Database)
        dbContext.PadelCourts.Add(padelCourt1);
        dbContext.PadelCourts.Add(padelCourt2);
        dbContext.PadelCourts.Add(padelCourt3);
        dbContext.PadelCourts.Add(padelCourt4);
        dbContext.PadelCourts.Add(padelCourt5);
        dbContext.PadelCourts.Add(padelCourt6);
        
        // Add bookings to the DbContext (Database)
        dbContext.Bookings.Add(booking1);
        dbContext.Bookings.Add(booking2);
        dbContext.Bookings.Add(booking3);
        dbContext.Bookings.Add(booking4);
        dbContext.Bookings.Add(booking5);
        
        // Add players to the DbContext (Database)
        dbContext.Players.Add(player1);
        dbContext.Players.Add(player2);
        dbContext.Players.Add(player3);
        dbContext.Players.Add(player4);
        dbContext.Players.Add(player5);

        dbContext.SaveChanges(); // Save changes to the database
        
        dbContext.ChangeTracker.Clear(); // Clear the change tracker (Delete all entities from the change tracker)
    }
}