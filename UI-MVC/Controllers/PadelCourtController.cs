/***************************************
 *                                     *
 *   Created by Elias De Hondt         *
 *   Visit https://eliasdh.com         *
 *                                     *
 ***************************************/
// Controller PadelCourt

using Microsoft.AspNetCore.Mvc;
using PadelClubManagement.BL;
using PadelClubManagement.BL.Domain;

namespace PadelClubManagement.UI.Web.Controllers;

public class PadelCourtController : Controller
{
    private readonly IManager _manager;

    public PadelCourtController(IManager manager)
    {
        _manager = manager;
    }

    public IActionResult Detail(int courtNumber)
    {
        bool isValid = ModelState.IsValid;
        if (!isValid) return BadRequest();
        
        PadelCourt padelCourt = _manager.GetPadelCourt(courtNumber);
        if (padelCourt == null) return NotFound();
        return View(padelCourt);
    }
}