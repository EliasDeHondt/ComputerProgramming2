/***************************************
 *                                     *
 *   Created by Elias De Hondt         *
 *   Visit https://eliasdh.com         *
 *                                     *
 ***************************************/
// Controller Player

using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using PadelClubManagement.BL;
using PadelClubManagement.BL.Domain;
using PadelClubManagement.UI.Web.Models;

namespace PadelClubManagement.UI.Web.Controllers;

public class PlayerController : Controller
{
    private readonly IManager _manager;
    
    public PlayerController(IManager manager)
    {
        _manager = manager;
    }
    
    public IActionResult Index()
    {
        IEnumerable<Player> players = _manager.GetAllPlayersWithManager();
        return View(players);
    }
    
    public IActionResult Add()
    {
        return View();
    }
    
    [HttpPost] // This method is only accessible via POST
    public IActionResult Add(Player player)
    {
        string email = User.FindFirstValue(ClaimTypes.Email);
        _manager.AddPlayerAsObject(player, email);
        return RedirectToAction("Detail", new { playerNumber = player.PlayerNumber });
    }
    
    public IActionResult Detail(int playerNumber)
    {
        Player player = _manager.GetPlayerWithBookingsAndPadelCourtsAndManager(playerNumber);
        return View(player);
    }
    
    public IActionResult Privacy()
    {
        return View();
    }
    
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}