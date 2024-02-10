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
        Club terEikenClub = new Club { Name = "Ter Eiken", NumberOfCourts = 2, StreetName = "Kattenbroek", HouseNumber = 3, ZipCode = 2650 };
        Club antwerpPadelClub = new Club { Name = "Antwerp Padelclub", NumberOfCourts = 3, StreetName = "Filip Williotstraat", HouseNumber = 1, ZipCode = 2600 };
        Club openPadel = new Club { Name = "Open Padel", NumberOfCourts = 2, StreetName = "Ranstsesteenweg", HouseNumber = 86, ZipCode = 2520  };
        Club theBoxPadel = new Club { Name = "The Box Padel", NumberOfCourts = 4, StreetName = "Zonnestroomstraat", HouseNumber = 9, ZipCode = 2020 };
        Club padelMetropoolAntwerpen = new Club { Name = "Padel Metropool Antwerpen", NumberOfCourts = 6, StreetName = "Maccabilaan", HouseNumber = 32, ZipCode = 2660 };
        
        // Seed data for PadelCourts
        PadelCourt indoorCourt1 = new PadelCourt { IsIndoor = true, Capacity = 4, Price = 20.50};
        PadelCourt indoorCourt2 = new PadelCourt { IsIndoor = true, Capacity = 4, Price = 20.50};
        PadelCourt indoorCourt3 = new PadelCourt { IsIndoor = true, Capacity = 4, Price = 20.50};
        PadelCourt outdoorCourt1 = new PadelCourt { IsIndoor = false, Capacity = 2, Price = 15.75};
        PadelCourt outdoorCourt2 = new PadelCourt { IsIndoor = false, Capacity = 2, Price = 15.75};
        PadelCourt outdoorCourt3 = new PadelCourt { IsIndoor = false, Capacity = 2, Price = 20.50};
        PadelCourt outdoorCourt4 = new PadelCourt { IsIndoor = false, Capacity = 2, Price = 20.50};
        PadelCourt outdoorCourt5 = new PadelCourt { IsIndoor = false, Capacity = 2, Price = 20.50};
        
        // Seed data for Bookings
        Booking booking1 = new Booking { BookingDate = new DateOnly(2023, 4, 15), StartTime = new TimeSpan(9, 30, 0), EndTime = new TimeSpan(10, 30, 0) };
        Booking booking2 = new Booking { BookingDate = new DateOnly(2023, 4, 15), StartTime = new TimeSpan(11, 0, 0), EndTime = new TimeSpan(12, 0, 0) };
        Booking booking3 = new Booking { BookingDate = new DateOnly(2023, 4, 16), StartTime = new TimeSpan(14, 30, 0), EndTime = new TimeSpan(15, 30, 0) };
        Booking booking4 = new Booking { BookingDate = new DateOnly(2023, 4, 16), StartTime = new TimeSpan(10, 0, 0), EndTime = new TimeSpan(11, 0, 0) };
        Booking booking5 = new Booking { BookingDate = new DateOnly(2023, 4, 17), StartTime = new TimeSpan(13, 0, 0), EndTime = new TimeSpan(14, 0, 0) };
        Booking booking6 = new Booking { BookingDate = new DateOnly(2023, 4, 17), StartTime = new TimeSpan(14, 0, 0), EndTime = new TimeSpan(15, 0, 0) };
        
        // Seed data for Players
        Player eliasDeHondt = new Player { FirstName = "Elias", LastName = "De Hondt", BirthDate = new DateOnly(2001, 4, 10), Level = 5.5, Position = PlayerPosition.Member, PlayerManager = dbContext.Users.Single(user => user.UserName == "user1@eliasdh.com") };
        Player aliceJohnson = new Player { FirstName = "Alice", LastName = "Johnson", BirthDate = new DateOnly(1990, 3, 12), Level = 6.2, Position = PlayerPosition.Instructor, PlayerManager = dbContext.Users.Single(user => user.UserName == "user2@eliasdh.com") };
        Player bobSmith = new Player { FirstName = "Bob", LastName = "Smith", BirthDate = new DateOnly(1988, 12, 15), Level = 5.0, Position = PlayerPosition.TournamentPlayer, PlayerManager = dbContext.Users.Single(user => user.UserName == "user3@eliasdh.com") };
        Player carolDavis = new Player { FirstName = "Carol", LastName = "Davis", BirthDate = new DateOnly(1995, 8, 15), Level = 4.5, Position = PlayerPosition.Member, PlayerManager = dbContext.Users.Single(user => user.UserName == "user4@eliasdh.com") };
        Player davidLee = new Player { FirstName = "David", LastName = "Lee", BirthDate = new DateOnly(1992, 6, 10), Level = 4.2, Position = PlayerPosition.Guest, PlayerManager = dbContext.Users.Single(user => user.UserName == "user5@eliasdh.com") };
        
        // Relate Club to PadelCourts
        terEikenClub.PadelCourts = new List<PadelCourt> { indoorCourt1, indoorCourt2 };
        antwerpPadelClub.PadelCourts = new List<PadelCourt> { indoorCourt3 };
        openPadel.PadelCourts = new List<PadelCourt> { outdoorCourt1, outdoorCourt2 };
        theBoxPadel.PadelCourts = new List<PadelCourt> { outdoorCourt3 };
        padelMetropoolAntwerpen.PadelCourts = new List<PadelCourt> { outdoorCourt4, outdoorCourt5 };
        
        // Relate PadelCourts to Club
        indoorCourt1.Club = terEikenClub;
        indoorCourt2.Club = terEikenClub;
        indoorCourt3.Club = antwerpPadelClub;
        outdoorCourt1.Club = openPadel;
        outdoorCourt2.Club = openPadel;
        outdoorCourt3.Club = theBoxPadel;
        outdoorCourt4.Club = padelMetropoolAntwerpen;
        outdoorCourt5.Club = padelMetropoolAntwerpen;
        
        // Relate PadelCourts to Bookings
        indoorCourt1.Bookings = new List<Booking> { booking1, booking2 };
        indoorCourt2.Bookings = new List<Booking> { booking3, booking4 };
        indoorCourt3.Bookings = new List<Booking> { booking5 };
        outdoorCourt1.Bookings = new List<Booking> { booking6 };
        
        // Relate Bookings to PadelCourts
        booking1.PadelCourt = indoorCourt1;
        booking2.PadelCourt = indoorCourt1;
        booking3.PadelCourt = indoorCourt2;
        booking4.PadelCourt = indoorCourt2;
        booking5.PadelCourt = indoorCourt3;
        booking6.PadelCourt = outdoorCourt1;
        
        // Relate Bookings to Players
        booking1.Player = eliasDeHondt;
        booking2.Player = aliceJohnson;
        booking3.Player = bobSmith;
        booking4.Player = carolDavis;
        booking5.Player = davidLee;
        booking6.Player = davidLee;
        
        // Relate Players to Bookings
        eliasDeHondt.Bookings = new List<Booking> { booking1 };
        aliceJohnson.Bookings = new List<Booking> { booking2 };
        bobSmith.Bookings = new List<Booking> { booking3 };
        carolDavis.Bookings = new List<Booking> { booking4 };
        davidLee.Bookings = new List<Booking> { booking5, booking6 };
        
        // Add club to the DbContext (Database)
        dbContext.Clubs.Add(terEikenClub);
        dbContext.Clubs.Add(antwerpPadelClub);
        dbContext.Clubs.Add(openPadel);
        dbContext.Clubs.Add(theBoxPadel);
        dbContext.Clubs.Add(padelMetropoolAntwerpen);
        
        // Add padelCourts to the DbContext (Database)
        dbContext.PadelCourts.Add(indoorCourt1);
        dbContext.PadelCourts.Add(indoorCourt2);
        dbContext.PadelCourts.Add(indoorCourt3);
        dbContext.PadelCourts.Add(outdoorCourt1);
        dbContext.PadelCourts.Add(outdoorCourt2);
        dbContext.PadelCourts.Add(outdoorCourt3);
        
        // Add bookings to the DbContext (Database)
        dbContext.Bookings.Add(booking1);
        dbContext.Bookings.Add(booking2);
        dbContext.Bookings.Add(booking3);
        dbContext.Bookings.Add(booking4);
        dbContext.Bookings.Add(booking5);
        
        // Add players to the DbContext (Database)
        dbContext.Players.Add(eliasDeHondt);
        dbContext.Players.Add(aliceJohnson);
        dbContext.Players.Add(bobSmith);
        dbContext.Players.Add(carolDavis);
        dbContext.Players.Add(davidLee);

        dbContext.SaveChanges(); // Save changes to the database
        
        dbContext.ChangeTracker.Clear(); // Clear the change tracker (Delete all entities from the change tracker)
    }
}