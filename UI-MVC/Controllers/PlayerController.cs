/***************************************
 *                                     *
 *   Created by Elias De Hondt         *
 *   Visit https://eliasdh.com         *
 *                                     *
 ***************************************/
// Controller Player

using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PadelClubManagement.BL;
using PadelClubManagement.BL.Domain;
using PadelClubManagement.UI.Web.Models;

namespace PadelClubManagement.UI.Web.Controllers;

public class PlayerController : Controller
{
    private readonly IManager _manager;
    private readonly SignInManager<IdentityUser> _signInManager;
    
    public PlayerController(IManager manager, SignInManager<IdentityUser> signInManager)
    {
        _manager = manager;
        _signInManager = signInManager;
    }
    
    public IActionResult Index()
    {
        IEnumerable<Player> players = _manager.GetAllPlayers();
        return View(players);
    }
    
    public IActionResult Add()
    {
        return View();
    }
    
    [HttpPost] // This method is only accessible via POST
    public IActionResult Add(Player player)
    {
        if (!ModelState.IsValid) return View(player);
        _manager.AddPlayerAsObject(player);
        return RedirectToAction("Detail", new { playerNumber = player.PlayerNumber });
    }
    
    public IActionResult Detail(int playerNumber)
    {
        Player player = _manager.GetPlayerWithBookingsAndPadelCourts(playerNumber);
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