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
[Route("api/[controller]")]
public class ClubsController : ControllerBase
{
    private readonly IManager _manager;

    public ClubsController(IManager manager)
    {
        _manager = manager;
    }
    
    [HttpGet]
    public IActionResult GetAllClubs()
    {
        IEnumerable<Club> clubs = _manager.GetAllClubs();
        if (clubs == null || !clubs.Any()) return NoContent();
        return Ok(clubs);
    }
    
    [HttpPost]
    public IActionResult AddClub([FromQuery] string name, [FromQuery] int numberOfCourts, [FromQuery] string streetName, [FromQuery] int houseNumber, [FromQuery] int zipCode)
    {
        if (!ModelState.IsValid) return BadRequest();
        _manager.AddClub(name, numberOfCourts, streetName, houseNumber, zipCode);
        return Ok();
    }
}