/***************************************
 *                                     *
 *   Created by Elias De Hondt         *
 *   Visit https://eliasdh.com         *
 *                                     *
 ***************************************/
// Class InMemoryRepository
using SC.BL.Domain;

namespace SC.DAL;

public class InMemoryRepository : IRepository
{
      // Statische lijsten voor Player en PadelCourt
      private static List<Player> _players = new List<Player>(); // "_" means private (backfield)
      private static List<PadelCourt> _padelCourts = new List<PadelCourt>(); // "_" means private (backfield)

      public static void Seed() // Seed data for the application
      {
          // Seed data for Club (1 Object) (1 op veel relatie)
          Club club = new Club(); // Create a new instance of Club
          club.Name = "Padel Club"; // Set the name of the club
          club.NumberOfCours = 5; // Set the number of courts
          club.StreetName = "Kattenbroek"; // Set the street name
          club.HouseNumber = 3; // Set the house number
          club.ZipCode = 2650; // Set the zip code
        
          // Seed data for PadelCourts (6 Objects)
          PadelCourt padelCourt1 = new PadelCourt { IsIndoor = true, Capacity = 4, Price = 20.50, Club = club };
          PadelCourt padelCourt2 = new PadelCourt { IsIndoor = true, Capacity = 4, Price = 20.50, Club = club };
          PadelCourt padelCourt3 = new PadelCourt { IsIndoor = true, Capacity = 4, Price = 20.50, Club = club };
          PadelCourt padelCourt4 = new PadelCourt { IsIndoor = false, Capacity = 2, Price = 15.75, Club = club };
          PadelCourt padelCourt5 = new PadelCourt { IsIndoor = false, Capacity = 2, Price = 15.75, Club = club };
          PadelCourt padelCourt6 = new PadelCourt { IsIndoor = false, Capacity = 2, Price = 20.50, Club = club };
        
          // Seed data for Players (5 Objects)
          Player player1 = new Player { FirstName = "Elias", LastName = "De Hondt", BirthDate = new DateOnly(2001, 4, 10), Level = 5.5, Position = PlayerPosition.Member };
          Player player2 = new Player { FirstName = "Alice", LastName = "Johnson", BirthDate = new DateOnly(1990, 3, 20), Level = 6.2, Position = PlayerPosition.Instructor };
          Player player3 = new Player { FirstName = "Bob", LastName = "Smith", BirthDate = new DateOnly(1988, 12, 5), Level = 5.0, Position = PlayerPosition.TournamentPlayer };
          Player player4 = new Player { FirstName = "Carol", LastName = "Davis", BirthDate = new DateOnly(1995, 8, 15), Level = 4.5, Position = PlayerPosition.Member };
          Player player5 = new Player { FirstName = "David", LastName = "Lee", BirthDate = new DateOnly(1992, 6, 10), Level = 4.2, Position = PlayerPosition.Guest };
          
          
          InMemoryRepository rep = new InMemoryRepository(); // Create a new instance of InMemoryRepository (Only way to call a non-static method)
          // Add players to the list of players
          rep.CreatePlayer(player1);
          rep.CreatePlayer(player2);
          rep.CreatePlayer(player3);
          rep.CreatePlayer(player4);
          rep.CreatePlayer(player5);
          // Add padelCourts to the list of padelCourts
          rep.CreatePadelCourt(padelCourt1);
          rep.CreatePadelCourt(padelCourt2);
          rep.CreatePadelCourt(padelCourt3);
          rep.CreatePadelCourt(padelCourt4);
          rep.CreatePadelCourt(padelCourt5);
          rep.CreatePadelCourt(padelCourt6);

      }

      public Player ReadPlayer(int playerNumber)
      {
          foreach (Player player in _players)
          {
              if (player.PlayerNumber == playerNumber) return player;
          }
          return null;
      }

      public List<Player> ReadAllPlayers()
      {
          return _players;
      }

      public List<Player> ReadPlayersByPosition(PlayerPosition position)
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

      public List<PadelCourt> ReadAllPadelCourts()
      {
          return _padelCourts;
      }

      public List<PadelCourt> ReadPadelCourtsByFilter(double? price, bool? indoor)
      {
          List<PadelCourt> padelCourts = new List<PadelCourt>();
          foreach (PadelCourt padelCourt in _padelCourts)
          {
              // If price is null or the price of the padelCourt is equal to the price filter and indoor is null or the indoor of the padelCourt is equal to the indoor filter
              if ((!price.HasValue || padelCourt.Price == price.Value) && (!indoor.HasValue || padelCourt.IsIndoor == indoor.Value))
              {
                    padelCourts.Add(padelCourt);
              }
          }
          return padelCourts;
      }

      public void CreatePadelCourt(PadelCourt padelCourt)
      {
          padelCourt.CourtNumber = _padelCourts.Count + 1;
          _padelCourts.Add(padelCourt);
      }
}