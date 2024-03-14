/***************************************
 *                                     *
 *   Created by Elias De Hondt         *
 *   Visit https://eliasdh.com         *
 *                                     *
 ***************************************/

using Microsoft.Extensions.DependencyInjection;
using PadelClubManagement.BL;
using PadelClubManagement.BL.Domain;
using Tests.Config;
using Xunit;

namespace Tests.IntegrationTests;

public class PadelCourtTests : IClassFixture<ExtendedWebApplicationFactoryWithMockAuth<Program>>
{
    private readonly ExtendedWebApplicationFactoryWithMockAuth<Program> _factory;
    
    public PadelCourtTests(ExtendedWebApplicationFactoryWithMockAuth<Program> factory) // Constructor
    {
        _factory = factory; // Create a new web application factory
    }
    
    [Fact]
    public void GetPadelCourt_ReturnPadelCourt_GivenValidCourtNumber() // Method: public PadelCourt GetPadelCourt(int courtNumber):
    {
        // Arrange
        using IServiceScope scope = _factory.Services.CreateScope();
        IServiceProvider services = scope.ServiceProvider;
        IManager manager = services.GetRequiredService<IManager>();
        
        // Act
        PadelCourt result = manager.GetPadelCourt(1);
        
        // Assert
        Assert.NotNull(result); // Expected: true
        Assert.Equal(1, result.CourtNumber); // Expected: true
    }
    
    [Fact]
    public void GetPadelCourt_ReturnNull_GivenInvalidCourtNumber() // Method: public PadelCourt GetPadelCourt(int courtNumber):
    {
        // Arrange
        using IServiceScope scope = _factory.Services.CreateScope();
        IServiceProvider services = scope.ServiceProvider;
        IManager manager = services.GetRequiredService<IManager>();
        
        // Act
        PadelCourt result = manager.GetPadelCourt(-1);
        
        // Assert
        Assert.Null(result); // Expected: true
    }
}