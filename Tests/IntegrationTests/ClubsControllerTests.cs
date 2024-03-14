/***************************************
 *                                     *
 *   Created by Elias De Hondt         *
 *   Visit https://eliasdh.com         *
 *                                     *
 ***************************************/

using System.Net;
using Microsoft.Extensions.DependencyInjection;
using PadelClubManagement.BL;
using Tests.Config;
using Xunit;

namespace Tests.IntegrationTests;

public class ClubsControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly CustomWebApplicationFactory<Program> _factory;
    
    public ClubsControllerTests(CustomWebApplicationFactory<Program> factory) // Constructor
    {
        _factory = factory; // Create a new web application factory
    }
    
    [Fact]                                                  // [HttpGet("/api/clubs")]
    public void GetAllClubs_Return200_GivenValidEndpoint() // Method: public IActionResult GetAllClubs();
    {
        // Arrange
        var client = _factory.CreateClient(); // Create an HTTP client
        
        // Act
        var response = client.GetAsync("/api/clubs").Result; // Send a GET request to the specified URI
        
        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
    
    [Fact]                                                    // [HttpGet("/api/clubs")]
    public void GetAllClubs_Return204_GivenInValidEndpoint() // Method: public IActionResult GetAllClubs();
    {
        // Arrange
        using IServiceScope scope = _factory.Services.CreateScope();
        IServiceProvider services = scope.ServiceProvider;
        IManager manager = services.GetRequiredService<IManager>();
        
        manager.DeleteAllClubs(); // Delete all clubs
        var client = _factory.CreateClient(); // Create an HTTP client
        
        // Act
        var response = client.GetAsync("/api/clubs").Result; // Send a GET request to the specified URI
        
        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);
    }
}