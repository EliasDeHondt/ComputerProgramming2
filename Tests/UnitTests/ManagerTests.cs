/***************************************
 *                                     *
 *   Created by Elias De Hondt         *
 *   Visit https://eliasdh.com         *
 *                                     *
 ***************************************/

using System.ComponentModel.DataAnnotations;
using Moq;
using PadelClubManagement.BL;
using PadelClubManagement.BL.Domain;
using PadelClubManagement.DAL;
using Tests.Config;
using Xunit;

namespace Tests.UnitTests;

public class ManagerTests : IClassFixture<ExtendedWebApplicationFactoryWithMockAuth<Program>>
{
    [Fact]
    public void AddClub_GivenInValidData_ShouldThrowValidationException() // Method: public void AddClub(string name, int numberOfCourts, string streetName, int houseNumber, int zipCode);
    {
        // Arrange
        IRepository mockRepository = new Mock<IRepository>().Object;
        IManager manager = new Manager(mockRepository);
        
        string name = "x"; // ValidationException min 2 max 50
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
    
    [Fact]
    public void AddPadelCourt_GivenInValidData_ShouldThrowValidationException() // Method: public void AddPadelCourt(int clubNumber, double price, bool indoor);
    {
        // Arrange
        IRepository mockRepository = new Mock<IRepository>().Object;
        IManager manager = new Manager(mockRepository);
        
        bool indoor = true;
        int capacity = 1; // ValidationException min 1 max 4
        double price = 15;
        Club club = new Club { Name = "Club1", NumberOfCourts = 2, StreetName = "Street1", HouseNumber = 5, ZipCode = 2650 };
        
        // Act and Assert
        Assert.Throws<ValidationException>(() => // Action
        {
            manager.AddPadelCourt(indoor, capacity, price, club); // Expected: ValidationException
        });
    }
    
    [Fact]
    public void AddPadelCourt_GivenValidData_ShouldNotThrowValidationException() // Method: public void AddPadelCourt(int clubNumber, double price, bool indoor);
    {
        // Arrange
        IRepository mockRepository = new Mock<IRepository>().Object;
        IManager manager = new Manager(mockRepository);
        
        bool indoor = true;
        int capacity = 2;
        double price = 15;
        Club club = new Club { Name = "Club1", NumberOfCourts = 2, StreetName = "Street1", HouseNumber = 5, ZipCode = 2650 };
        
        // Act and Assert
        Assert.Null(Record.Exception(() => // Action
        {
            manager.AddPadelCourt(indoor, capacity, price, club); // Expected: No exception
        }));
    }
}