/***************************************
 *                                     *
 *   Created by Elias De Hondt         *
 *   Visit https://eliasdh.com         *
 *                                     *
 ***************************************/

using System.ComponentModel.DataAnnotations;
using Moq;
using PadelClubManagement.BL;
using PadelClubManagement.DAL;
using Tests.Config;
using Xunit;

namespace Tests.UnitTests;

public class ManagerTests : IClassFixture<ExtendedWebApplicationFactoryWithMockAuth<Program>>
{
    private readonly ExtendedWebApplicationFactoryWithMockAuth<Program> _factory;
    
    public ManagerTests(ExtendedWebApplicationFactoryWithMockAuth<Program> factory) // Constructor
    {
        _factory = factory; // Create a new web application factory
    }
    
    [Fact]
    public void AddClub_GivenInValidData_ShouldThrowValidationException() // Method: public void AddClub(string name, int numberOfCourts, string streetName, int houseNumber, int zipCode);
    {
        // Arrange
        IRepository mockRepository = new Mock<IRepository>().Object;
        IManager manager = new Manager(mockRepository);
        
        string name = "x";
        int numberOfCourts = 2;
        string streetName = "Street1";
        int houseNumber = 5;
        int zipCode = 2650;
        
        // Act and Assert
        Assert.Throws<ValidationException>(() => // Action
        {
            manager.AddClub(name, numberOfCourts, streetName, houseNumber, zipCode); // Expected: ValidationException
        });
    }
    
    [Fact]
    public void AddClub_GivenValidData_ShouldNotThrowValidationException() // Method: public void AddClub(string name, int numberOfCourts, string streetName, int houseNumber, int zipCode);
    {
        // Arrange
        IRepository mockRepository = new Mock<IRepository>().Object;
        IManager manager = new Manager(mockRepository);
        
        string name = "Club1";
        int numberOfCourts = 2;
        string streetName = "Street1";
        int houseNumber = 5;
        int zipCode = 2650;
        
        // Act and Assert
        Assert.Null(Record.Exception(() => // Action
        {
            manager.AddClub(name, numberOfCourts, streetName, houseNumber, zipCode); // Expected: No exception
        }));
    }
}