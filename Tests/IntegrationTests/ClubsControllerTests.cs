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

// This test class is different as the rest. Here we have ensured that the tests are independent of each other by using a new scope for each test.
// Because in the second test we delete all information in the database. This way we can be sure that the second test is not dependent on the first test.
public class ClubsControllerTests
{
    [Fact]                                                      // [HttpGet("/api/clubs")]
    public void GetAllClubs_Return200_GivenValidEndpointData() // Method: public IActionResult GetAllClubs();
    {
        // Arrange
        using var factory = new ExtendedWebApplicationFactoryWithMockAuth<Program>();
        var client = factory.CreateClient(); // Create an HTTP client
        
        // Act
        var response = client.GetAsync("/api/clubs").Result; // Send a GET request to the specified URI
        
        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode); // Expected: 200
        Assert.NotNull(response.Content); // Expected: Not null
        Assert.True(response.Content.Headers.ContentLength > 0);  // Expected: Content length > 0
    }

    [Fact]                                                        // [HttpGet("/api/clubs")]
    public void GetAllClubs_Return204_GivenInValidEndpointData() // Method: public IActionResult GetAllClubs();
    {
        // Arrange
        using var factory = new ExtendedWebApplicationFactoryWithMockAuth<Program>();
        using var scope = factory.Services.CreateScope(); // Create a new scope
        var services = scope.ServiceProvider; // Get the service provider
        var manager = services.GetRequiredService<IManager>(); // Get the manager
        manager.DeleteAllClubs(); // Delete all clubs

        var client = factory.CreateClient(); // Create an HTTP client
        
        // Act
        var response = client.GetAsync("/api/clubs").Result; // Send a GET request to the specified URI
        
        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode); // Expected: 204
    }
}