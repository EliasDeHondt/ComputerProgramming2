/***************************************
 *                                     *
 *   Created by Elias De Hondt         *
 *   Visit https://eliasdh.com         *
 *                                     *
 ***************************************/

using System.ComponentModel.DataAnnotations;
using PadelClubManagement.BL;
using System.Net;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using PadelClubManagement.BL.Domain;
using PadelClubManagement.DAL;
using Tests.Config;
using Xunit;

namespace Tests.UnitTests;

public class ClubsControllerTests : IClassFixture<ExtendedWebApplicationFactoryWithMockAuth<Program>>
{
    private readonly ExtendedWebApplicationFactoryWithMockAuth<Program> _factory;
    
    public ClubsControllerTests(ExtendedWebApplicationFactoryWithMockAuth<Program> factory) // Constructor
    {
        _factory = factory; // Create a new web application factory
    }
    
    [Fact]                                                     // [HttpGet("/api/clubs")]
    public void GetAllClubs_Return200_GivenValidEndpointData() // Method: public IActionResult GetAllClubs();
    {
        // Arrange
        var client = _factory.CreateClient(); // Create an HTTP client
        
        // Act
        var response = client.GetAsync("/api/clubs").Result; // Send a GET request to the specified URI
        
        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode); // Expected: 200
        Assert.NotNull(response.Content); // Expected: Not null
        Assert.True(response.Content.Headers.ContentLength > 0); // Expected: True
    }
    
    [Fact]                                                       // [HttpGet("/api/clubs")]
    public void GetAllClubs_Return204_GivenInValidEndpointData() // Method: public IActionResult GetAllClubs();
    {
        // Arrange
        DropDatabase(); // Drop the database
        var client = _factory.CreateClient(); // Create an HTTP client
        
        // Act
        var response = client.GetAsync("/api/clubs").Result; // Send a GET request to the specified URI
        
        // Assert
        Assert.Equal(HttpStatusCode.NoContent, response.StatusCode);  // Expected: 204
    }
    
    private void DropDatabase()
    {
        using IServiceScope scope = _factory.Services.CreateScope();
        IServiceProvider services = scope.ServiceProvider;
        IManager manager = services.GetRequiredService<IManager>();
        manager.DeleteAllClubs(); // Delete all clubs
    }
    
    [Fact]                                            // [AllowAnonymous] [HttpPost("/api/clubs")]
    public void AddClub_GivenValidClub_Return200() // Method: public IActionResult AddClub(Club newClub);
    {
        // Arrange
        var mockRepo = new Mock<IRepository>(); // Create a new mock repository
        mockRepo.Setup(r => r.CreateClub(It.IsAny<Club>())).Callback((Club club) => {club.ClubNumber=69; }).Verifiable(Times.Once); // Setup the mock repository
        var manager = new Manager(mockRepo.Object); // Create a new manager

        // Act
        manager.AddClub("TestClub", 4, "TestStreet", 14, 2650); // Add a new club
        
        
        // Assert
        mockRepo.Verify(r => r.CreateClub(It.IsAny<Club>()), Times.Once); // Expected: Once (200)
        
        mockRepo.VerifyAll(); // Drop mock repository
    }
    
    [Fact]                                                                // [AllowAnonymous] [HttpPost("/api/clubs")]
    public void AddClub_GivenInValidClub_ShouldThrowValidationException() // Method: public IActionResult AddClub(Club newClub);
    {
        // Arrange
        var mockRepo = new Mock<IRepository>(); // Create a new mock repository
        var manager = new Manager(mockRepo.Object); // Create a new manager
        
        // Act
        var ex = Assert.Throws<ValidationException>(() => 
            manager.AddClub("T", 4, "TestStreet", 14, 2650)); // Add a new club
        
        // Assert
        Assert.NotNull(ex); // Expected: Not null
        Assert.Equal("\nAn error occurred, please try again:\n * (Name) At least 2 character, maximum 50 characters\n * end", ex.Message); // Expected: ValidationException
        
        mockRepo.VerifyAll(); // Drop mock repository
    }
    
}