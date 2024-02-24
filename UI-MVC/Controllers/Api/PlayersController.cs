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
        if (players == null || !players.Any()) return NoContent(); // 204
        return Ok(players); // 200
    }
    
    [HttpGet("/api/players")]
    public IActionResult GetAllPlayers()
    {
        IEnumerable<Player> players = _manager.GetAllPlayers();
        if (players == null || !players.Any()) return NoContent(); // 204
        return Ok(players); // 200
    }
    
    [HttpPost("/api/addPadelCourtToPlayer/{courtNumber}/{playerNumber}/bookings")]
    [AllowAnonymous]
    public IActionResult AddPadelCourtToPlayer(int playerNumber, int courtNumber, Booking booking)
    {
        if (User.Identity is { IsAuthenticated: false }) return Unauthorized(); // 401
        
        int bookingNumber = _manager.AddBooking(playerNumber, courtNumber, booking, true);

        _manager.AddPlayerToBooking(playerNumber, bookingNumber);
        _manager.AddPadelCourtToBooking(courtNumber, bookingNumber);

        return CreatedAtAction(nameof(GetAllPlayersFromPadelCourt), new { courtNumber }, null); // 201
    }
    
    [HttpPut("/api/updatePlayerLevel/{playerNumber}/{newLevel}")]
    public IActionResult UpdatePlayerLevel(int playerNumber, double newLevel)
    {
        Player existingPlayer = _manager.GetPlayerWithUser(playerNumber);
        if (existingPlayer == null) return NotFound(); // 404
        
        // The user who manages the entity or It must be an admin otherwise return 401
        if (User.Identity != null && existingPlayer.PlayerManager.UserName != User.Identity.Name && !User.IsInRole("Admin")) return Unauthorized(); // 401

        existingPlayer.Level = newLevel;
        _manager.UpdatePlayer(existingPlayer);
        return Ok(existingPlayer); // 200
    }
}