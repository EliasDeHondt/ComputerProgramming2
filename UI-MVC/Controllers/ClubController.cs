/***************************************
 *                                     *
 *   Created by Elias De Hondt         *
 *   Visit https://eliasdh.com         *
 *                                     *
 ***************************************/
// Controller Club

using Microsoft.AspNetCore.Mvc;

namespace PadelClubManagement.UI.Web.Controllers;

public class ClubController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}