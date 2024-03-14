/***************************************
 *                                     *
 *   Created by Elias De Hondt         *
 *   Visit https://eliasdh.com         *
 *                                     *
 ***************************************/

using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using PadelClubManagement.BL;
using PadelClubManagement.BL.Domain;
using PadelClubManagement.UI.Web.Controllers;
using Tests.Config;
using Xunit;

namespace Tests.IntegrationTests;

public class PlayerControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly CustomWebApplicationFactory<Program> _factory;
    
    public PlayerControllerTests(CustomWebApplicationFactory<Program> factory) // Constructor
    {
        _factory = factory; // Create a new web application factory
    }
    
    [Fact]                                                     // [Authorize] + [HttpPost]
    public void Add_ReturnRedirectToAction_GivenValidUser() // Method: public IActionResult Add(Player player);
    {
        // Arrange
        using IServiceScope scope = _factory.Services.CreateScope();
        IServiceProvider services = scope.ServiceProvider;
        IManager manager = services.GetRequiredService<IManager>();
        PlayerController controller = new PlayerController(manager);
        
        Player player = manager.GetPlayer(1); // Get a valid player from the database
        
        // Simulate an authenticated user
        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] { new (ClaimTypes.Email, "user1@eliasdh.com") }, "mock"));

        // Set the HttpContext user to the simulated user
        controller.ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext { User = user } };
        
        // Act
        IActionResult result = controller.Add(player);
        
        // Assert
        var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result); // Expected: true
        Assert.Equal("Detail", redirectToActionResult.ActionName); // Expected: "Detail"
        Assert.Equal(player.PlayerNumber, redirectToActionResult.RouteValues?["playerNumber"]); // Expected: player.PlayerNumber
    }

    [Fact]                                                     // [Authorize] + [HttpPost]
    public void Add_ReturnRedirectToAction_GivenInvalidUser() // Method: public IActionResult Add(Player player);
    {
        // Arrange
        using IServiceScope scope = _factory.Services.CreateScope();
        IServiceProvider services = scope.ServiceProvider;
        IManager manager = services.GetRequiredService<IManager>();
        PlayerController controller = new PlayerController(manager);
        
        Player player = manager.GetPlayer(1); // Get a valid player from the database
        
        // Simulate an unauthenticated user
        var user = new ClaimsPrincipal(new ClaimsIdentity());
        
        // Set the HttpContext user to the simulated user
        controller.ControllerContext = new ControllerContext { HttpContext = new DefaultHttpContext { User = user } };
        
        // Act and Assert
        Assert.Throws<ValidationException>(() =>
        {
            IActionResult result = controller.Add(player); 
            Assert.IsType<RedirectToActionResult>(result);  // Expected: true
            Assert.Equal("Detail", ((RedirectToActionResult)result).ActionName); // Expected: "Detail"
            Assert.Equal(player.PlayerNumber, ((RedirectToActionResult)result).RouteValues?["playerNumber"]); // Expected: player.PlayerNumber
        });
    }
    
    [Fact]
    public void Index_ReturnView_GivenValidEndpoint() // Method: public IActionResult Index();
    {
        // Arrange
        var client = _factory.CreateClient(); // Create an HTTP client
        
        // Act
        var response = client.GetAsync("/Player/Index").Result; // Send a GET request to the specified URI
        var responsebody = response.Content.ReadAsStringAsync().Result; // Read the response body
        
        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode); // Expected: 200
        Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType?.ToString()); // Expected: "text/html; charset=utf-8"
        Assert.Contains("<title>Players - Padel Club Management</title>", responsebody); // Expected: true
    }
    
    [Fact]
    public void Detail_ReturnView_GivenValidEndpoint() // Method: public IActionResult Detail(int playerNumber);
    {
        // Arrange
        var client = _factory.CreateClient(); // Create an HTTP client
        
        // Act
        var response = client.GetAsync("/Player/Detail?playerNumber=1").Result; // Send a GET request to the specified URI
        var responsebody = response.Content.ReadAsStringAsync().Result; // Read the response body
        
        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode); // Expected: 200
        Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType?.ToString()); // Expected: "text/html; charset=utf-8"
        Assert.Contains("Player (ID: 1) Details", responsebody); // Expected: true
    }
}