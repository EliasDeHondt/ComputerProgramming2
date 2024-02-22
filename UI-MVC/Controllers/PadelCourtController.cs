/***************************************
 *                                     *
 *   Created by Elias De Hondt         *
 *   Visit https://eliasdh.com         *
 *                                     *
 ***************************************/
// Controller PadelCourt

using Microsoft.AspNetCore.Authorization;
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

    //[Authorize(Roles = "Admin, User")] // Only authenticated users can access this action
    //[Authorize(Roles = "Admin")]
    [Authorize] // Only authenticated users can access this action
    public IActionResult Detail(int courtNumber)
    {
        if (!User.IsInRole("Admin")) return Redirect("https://eliasdh.com/assets/pages/403.html"); // If the user is not in the Admin role, redirect to a 403 page
        
        PadelCourt padelCourt = _manager.GetPadelCourt(courtNumber);
        return View(padelCourt);
    }
}