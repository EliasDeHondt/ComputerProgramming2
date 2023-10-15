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
      private static List<Player> _players = new List<Player>(); // "_" means private (backfield)
      private static List<PadelCourt> _padelCourts = new List<PadelCourt>(); // "_" means private (backfield)

      public static void Seed() // Seed data for the application
      {
        // Seed data for Club (1 Object) (1 op veel relatie)
        Club club1 = new Club { ClubNumber = 1, Name = "Padel Club", NumberOfCours = 5, StreetName = "Kattenbroek", HouseNumber = 3, ZipCode = 2650 };
        
        // Seed data for PadelCourts (6 Objects)
        PadelCourt padelCourt1 = new PadelCourt { CourtNumber = 1, IsIndoor = true, Capacity = 4, Price = 20.50};
        PadelCourt padelCourt2 = new PadelCourt { CourtNumber = 2,  IsIndoor = true, Capacity = 4, Price = 20.50};
        PadelCourt padelCourt3 = new PadelCourt { CourtNumber = 3,  IsIndoor = true, Capacity = 4, Price = 20.50};
        PadelCourt padelCourt4 = new PadelCourt { CourtNumber = 4,  IsIndoor = false, Capacity = 2, Price = 15.75};
        PadelCourt padelCourt5 = new PadelCourt { CourtNumber = 5,  IsIndoor = false, Capacity = 2, Price = 15.75};
        PadelCourt padelCourt6 = new PadelCourt { CourtNumber = 6,  IsIndoor = false, Capacity = 2, Price = 20.50};
        
        // Seed data for Players (5 Objects)
        Player player1 = new Player { PlayerNumber = 1, FirstName = "Elias", LastName = "De Hondt", BirthDate = new DateOnly(2001, 4, 10), Level = 5.5, Position = PlayerPosition.Member };
        Player player2 = new Player { PlayerNumber = 2,  FirstName = "Alice", LastName = "Johnson", BirthDate = new DateOnly(1990, 3, 20), Level = 6.2, Position = PlayerPosition.Instructor };
        Player player3 = new Player { PlayerNumber = 3,  FirstName = "Bob", LastName = "Smith", BirthDate = new DateOnly(1988, 12, 5), Level = 5.0, Position = PlayerPosition.TournamentPlayer };
        Player player4 = new Player { PlayerNumber = 4,  FirstName = "Carol", LastName = "Davis", BirthDate = new DateOnly(1995, 8, 15), Level = 4.5, Position = PlayerPosition.Member };
        Player player5 = new Player { PlayerNumber = 5,  FirstName = "David", LastName = "Lee", BirthDate = new DateOnly(1992, 6, 10), Level = 4.2, Position = PlayerPosition.Guest };
          
        // Add padelCourts to the list of padelCourts
        _padelCourts.Add(padelCourt1);
        _padelCourts.Add(padelCourt2);
        _padelCourts.Add(padelCourt3);
        _padelCourts.Add(padelCourt4);
        _padelCourts.Add(padelCourt5);
        _padelCourts.Add(padelCourt6);
          
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
}