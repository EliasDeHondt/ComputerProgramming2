/***************************************
 *                                     *
 *   Created by Elias De Hondt         *
 *   Visit https://eliasdh.com         *
 *                                     *
 ***************************************/
// Class InMemoryRepository
using PadelClubManagement.BL.Domain;

namespace PadelClubManagement.DAL;

public class InMemoryRepository : IRepository
{
      // Statische lijsten voor Player en PadelCourt
      private static List<Club> _clubs = new List<Club>();
      private static List<PadelCourt> _padelCourts = new List<PadelCourt>();
      private static List<Booking> _bookings = new List<Booking>();
      private static List<Player> _players = new List<Player>();
      
      public static void Seed() // Seed data for the application
      {
        // Seed data for Club
        Club club1 = new Club { ClubNumber = 1, Name = "Ter Eiken", NumberOfCourts = 2, StreetName = "Kattenbroek", HouseNumber = 3, ZipCode = 2650 };
        Club club2 = new Club { ClubNumber = 2, Name = "Antwerp Padelclub", NumberOfCourts = 3, StreetName = "Filip Williotstraat", HouseNumber = 1, ZipCode = 2600 };
        Club club3 = new Club { ClubNumber = 3, Name = "Open Padel", NumberOfCourts = 2, StreetName = "Ranstsesteenweg", HouseNumber = 86, ZipCode = 2520  };
        Club club4 = new Club { ClubNumber = 4, Name = "The Box Padel", NumberOfCourts = 4, StreetName = "Zonnestroomstraat", HouseNumber = 9, ZipCode = 2020 };
        Club club5 = new Club { ClubNumber = 5, Name = "Padel Metropool Antwerpen", NumberOfCourts = 6, StreetName = "Maccabilaan", HouseNumber = 32, ZipCode = 2660 };
        
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
        
        // Add Clubs to the list of Clubs
        _clubs.Add(club1);
        _clubs.Add(club2);
        _clubs.Add(club3);
        _clubs.Add(club4);
        _clubs.Add(club5);
        
        // Add padelCourts to the list of padelCourts
        _padelCourts.Add(padelCourt1);
        _padelCourts.Add(padelCourt2);
        _padelCourts.Add(padelCourt3);
        _padelCourts.Add(padelCourt4);
        _padelCourts.Add(padelCourt5);
        _padelCourts.Add(padelCourt6);
        _padelCourts.Add(padelCourt7);
        _padelCourts.Add(padelCourt8);
        
        // Add bookings to the list of bookings
        _bookings.Add(booking1);
        _bookings.Add(booking2);
        _bookings.Add(booking3);
        _bookings.Add(booking4);
        _bookings.Add(booking5);
        
        // Add players to the list of players
        _players.Add(player1);
        _players.Add(player2);
        _players.Add(player3);
        _players.Add(player4);
        _players.Add(player5);
      }

      public Player ReadPlayer(int playerNumber)
      {
          foreach (Player player in _players)
          {
              if (player.PlayerNumber == playerNumber) return player;
          }
          return null;
      }
      
      public Player ReadPlayerWithBookingsAndPadelCourts(int playerNumber)
      {
          throw new NotImplementedException("This method is not implemented in in-memory repository.");
      }

      public IEnumerable<Player> ReadAllPlayers()
      {
          return _players;
      }

      public IEnumerable<Player> ReadPlayersByPosition(PlayerPosition position)
      {
          List<Player> players = new List<Player>();
          foreach (Player player in _players)
          {
              if (player.Position == position) players.Add(player);
          }
          return players;
      }

      public void CreatePlayer(Player player)
      {
          player.PlayerNumber = _players.Count + 1;
          _players.Add(player);
      }

      public PadelCourt ReadPadelCourt(int courtNumber)
      {
          foreach (PadelCourt padelCourt in _padelCourts)
          {
              if (padelCourt.CourtNumber == courtNumber) return padelCourt;
          }
          return null;
      }

      public IEnumerable<PadelCourt> ReadPadelCourtsByFilter(double? price, bool? indoor)
      {
          List<PadelCourt> padelCourts = new List<PadelCourt>();
          foreach (PadelCourt padelCourt in _padelCourts)
          {
              // If price is null or the price of the padelCourt is equal to the price filter and indoor is null or the indoor of the padelCourt is equal to the indoor filter
              bool meetsPriceCriteria = !price.HasValue || padelCourt.Price.Equals(price.Value);
              bool meetsIndoorCriteria = !indoor.HasValue || padelCourt.IsIndoor == indoor.Value;
              
              if (meetsPriceCriteria && meetsIndoorCriteria) padelCourts.Add(padelCourt);
          }
          return padelCourts;
      }

      public void CreatePadelCourt(PadelCourt padelCourt)
      {
          padelCourt.CourtNumber = _padelCourts.Count + 1;
          _padelCourts.Add(padelCourt);
      }
      
      public IEnumerable<Player> ReadAllPlayersWithBookingsAndPadelCourts()
      {
          throw new NotImplementedException("This method is not implemented in in-memory repository.");
      }

      public IEnumerable<PadelCourt> ReadAllPadelCourtsWithClub()
      {
          throw new NotImplementedException("This method is not implemented in in-memory repository.");
      }
      
      public IEnumerable<Club> ReadAllClubs()
      {
          throw new NotImplementedException("This method is not implemented in in-memory repository.");
      }
      
      public void CreateClub(Club club)
      {
          throw new NotImplementedException("This method is not implemented in in-memory repository.");
      }
      
      public IEnumerable<Booking> ReadAllBookings()
      {
          throw new NotImplementedException("This method is not implemented in in-memory repository.");
      }

      public Booking ReadBooking(int bookingNumber)
      {
          throw new NotImplementedException("This method is not implemented in in-memory repository.");
      }

      public void CreatePlayerToBooking(Player player, Booking booking)
      {
          throw new NotImplementedException("This method is not implemented in in-memory repository.");
      }
      
      public void CreatePadelCourtToBooking(PadelCourt padelCourt, Booking booking)
      {
          throw new NotImplementedException("This method is not implemented in in-memory repository.");
      }

      public void DeletePlayerFromBooking(Player player, Booking booking)
      {
          throw new NotImplementedException("This method is not implemented in in-memory repository.");
      }

      public IEnumerable<Booking> ReadBookingsOfPlayer(Player player)
      {
          throw new NotImplementedException("This method is not implemented in in-memory repository.");
      }
      
      public IEnumerable<Player> ReadPlayersOfPadelCourt(int courtNumber)
      {
          throw new NotImplementedException("This method is not implemented in in-memory repository.");
      }
      
      public int CreateBooking(Booking booking, bool returnBookingNumber)
      {
          throw new NotImplementedException("This method is not implemented in in-memory repository.");
      }
      
      public IEnumerable<PadelCourt> ReadAllPadelCourts()
      {
          throw new NotImplementedException("This method is not implemented in in-memory repository.");
      }
}