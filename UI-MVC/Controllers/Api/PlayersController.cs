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
    
    [HttpGet]
    public IActionResult GetAllPlayersFromPadelCourt(int courtNumber)
    {
        IEnumerable<Player> players = _manager.GetPlayersOfPadelCourt(courtNumber);
        if (players == null || !players.Any()) return NoContent();
        return Ok(players);
    }
}