/***************************************
 *                                     *
 *   Created by Elias De Hondt         *
 *   Visit https://eliasdh.com         *
 *                                     *
 ***************************************/
// Controller Player API

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PadelClubManagement.BL;
using PadelClubManagement.BL.Domain;

namespace PadelClubManagement.UI.Web.Controllers.Api;

[ApiController]
public class PlayersController : ControllerBase
{
    private readonly IManager _manager;
    
    public PlayersController(IManager manager)
    {
        _manager = manager;
    }
    
    [HttpGet("/api/player/{courtNumber}")]
    public IActionResult GetAllPlayersFromPadelCourt(int courtNumber)
    {
        IEnumerable<Player> players = _manager.GetPlayersOfPadelCourt(courtNumber);
        if (players == null || !players.Any()) return NoContent();
        return Ok(players);
    }
    
    [HttpGet("/api/players")]
    public IActionResult GetAllPlayers()
    {
        IEnumerable<Player> players = _manager.GetAllPlayers();
        if (players == null || !players.Any()) return NoContent();
        return Ok(players);
    }
    
    [HttpPost("/api/addPadelCourtToPlayer/{courtNumber}/{playerNumber}/bookings")]
    [Authorize] // You need to be logged in a user to access this
    public IActionResult AddPadelCourtToPlayer(int playerNumber, int courtNumber, Booking booking)
    {
        int bookingNumber = _manager.AddBooking(playerNumber, courtNumber, booking, true);

        _manager.AddPlayerToBooking(playerNumber, bookingNumber);
        _manager.AddPadelCourtToBooking(courtNumber, bookingNumber);

        return CreatedAtAction(nameof(GetAllPlayersFromPadelCourt), new { courtNumber }, null);
    }
    
    [HttpPut("/api/updatePlayerLevel/{playerNumber}/{newLevel}")]
    [Authorize] // You need to be logged in a user to access this
    public IActionResult UpdatePlayerLevel(int playerNumber, double newLevel)
    {
        Player existingPlayer = _manager.GetPlayerWithUser(playerNumber);

        if (existingPlayer == null) return NotFound();
        
        if (User.Identity != null && User.Identity.Name != existingPlayer.PlayerManager.UserName) return Redirect("https://eliasdh.com/assets/pages/403.html"); // Redirect to custom page

        existingPlayer.Level = newLevel;
        
        _manager.UpdatePlayer(existingPlayer);

        return Ok(existingPlayer);
    }
}