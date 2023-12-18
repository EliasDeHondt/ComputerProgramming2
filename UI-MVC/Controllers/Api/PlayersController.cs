/***************************************
 *                                     *
 *   Created by Elias De Hondt         *
 *   Visit https://eliasdh.com         *
 *                                     *
 ***************************************/
// Controller Player API
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
    public IActionResult AddPadelCourtToPlayer(int playerNumber, int courtNumber, Booking booking)
    {
        int bookingNumber = _manager.AddBooking(playerNumber, courtNumber, booking, true);

        _manager.AddPlayerToBooking(playerNumber, bookingNumber);
        _manager.AddPadelCourtToBooking(courtNumber, bookingNumber);

        return CreatedAtAction(nameof(GetAllPlayersFromPadelCourt), new { courtNumber = courtNumber }, null);
    }
}