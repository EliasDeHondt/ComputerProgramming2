/***************************************
 *                                     *
 *   Created by Elias De Hondt         *
 *   Visit https://eliasdh.com         *
 *                                     *
 ***************************************/
// Controller Club

using Microsoft.AspNetCore.Mvc;
using PadelClubManagement.BL;
using PadelClubManagement.BL.Domain;

namespace PadelClubManagement.UI.Web.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
public class ClubController : ControllerBase
{
    private readonly IManager _manager;

    public ClubController(IManager manager)
    {
        _manager = manager;
    }
    
    [HttpGet] // GET: api/Club (REST endpoint)
    public IActionResult GetAllClubs()
    {
        IEnumerable<Club> clubs = _manager.GetAllClubs();
        if (clubs == null || !clubs.Any()) // !clubs.Any() is the same as clubs.Count() == 0
        {
            return NoContent();
        }
        return Ok(clubs);
    }
    
    [HttpPost] // POST: api/Club (REST endpoint)
    public IActionResult AddClub([FromQuery] string name, [FromQuery] int numberOfCourts, [FromQuery] string streetName, [FromQuery] int houseNumber, [FromQuery] int zipCode)
    {
        if (!ModelState.IsValid) return BadRequest(); // If the model is not valid, return a 400 Bad Request
        _manager.AddClub(name, numberOfCourts, streetName, houseNumber, zipCode);
        return Ok();
    }
}