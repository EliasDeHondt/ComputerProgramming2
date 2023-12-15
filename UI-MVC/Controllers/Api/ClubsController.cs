/***************************************
 *                                     *
 *   Created by Elias De Hondt         *
 *   Visit https://eliasdh.com         *
 *                                     *
 ***************************************/
// Controller Club API
using Microsoft.AspNetCore.Mvc;
using PadelClubManagement.BL;
using PadelClubManagement.BL.Domain;

namespace PadelClubManagement.UI.Web.Controllers.Api;

[ApiController]
public class ClubsController : ControllerBase
{
    private readonly IManager _manager;

    public ClubsController(IManager manager)
    {
        _manager = manager;
    }
    
    [HttpGet("/api/clubs")]
    public IActionResult GetAllClubs()
    {
        IEnumerable<Club> clubs = _manager.GetAllClubs();
        if (clubs == null || !clubs.Any()) return NoContent();
        return Ok(clubs);
    }
    
    [HttpPost("/api/clubs")]
    public IActionResult AddClub(Club newClub)
    {
        if (newClub == null) return BadRequest(ModelState);
        _manager.AddClub(newClub.Name, newClub.NumberOfCourts, newClub.StreetName, newClub.ZipCode, newClub.ZipCode);
        return Ok();
    }
}