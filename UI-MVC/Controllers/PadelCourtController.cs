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
        PadelCourt padelCourt = _manager.GetPadelCourt(courtNumber);
        return View(padelCourt);
    }
}

//[Authorize(Roles = "Admin, User")] // Only authenticated users can access this action
//[Authorize(Roles = "Admin")]
//[Authorize] // You need to be logged in a user to access this
//if (!User.IsInRole("Admin")) return Redirect("https://eliasdh.com/assets/pages/403.html"); // If you're not an admin, redirect to custom page