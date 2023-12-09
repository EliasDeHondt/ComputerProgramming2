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
[Route("api/[controller]")]
public class PlayersController : ControllerBase
{
    private readonly IManager _manager;
    
    public PlayersController(IManager manager)
    {
        _manager = manager;
    }
    
    [HttpGet("fromPadelCourt/{courtNumber}")]
    public IActionResult GetAllPlayersFromPadelCourt(int courtNumber)
    {
        IEnumerable<Player> players = _manager.GetPlayersOfPadelCourt(courtNumber);
        if (players == null || !players.Any()) return NoContent();
        return Ok(players);
    }
    
    [HttpGet("all")]
    public IActionResult GetAllPlayers()
    {
        IEnumerable<Player> players = _manager.GetAllPlayers();
        if (players == null || !players.Any()) return NoContent();
        return Ok(players);
    }

    [HttpPost("addPadelCourtsToPlayer/{courtNumber}/{playerNumber}/{bookingDate}/{startTime}/{endTime}")]
    public IActionResult AddPadelCourtsToPlayer(int playerNumber, int courtNumber, DateOnly bookingDate, TimeSpan startTime, TimeSpan endTime)
    {
        int bookingNumber = _manager.AddBooking(playerNumber, courtNumber, bookingDate, startTime, endTime, true);

        _manager.AddPlayerToBooking(playerNumber, bookingNumber);
        _manager.AddPadelCourtToBooking(courtNumber, bookingNumber);
        
        return Ok();
    }
}