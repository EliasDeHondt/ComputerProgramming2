/***************************************
 *                                     *
 *   Created by Elias De Hondt         *
 *   Visit https://eliasdh.com         *
 *                                     *
 ***************************************/

using System.ComponentModel.DataAnnotations;
using PadelClubManagement.BL.Domain;

namespace Tests.IntegrationTests;

public class ClubTests
{
    [Fact]
    public void CheckClubState_GivenValidData_Name() // Method: private void Validate(Club club)
    {
        // Arrange
        Club club = new Club
        {
            Name = "Ter Eiken", 
            NumberOfCourts = 1, // NumberOfCourts minimum 2 and maximum 4
            StreetName = "Kattenbroek", 
            HouseNumber = 3, 
            ZipCode = 2650
        };
        
        // Act
        List<ValidationResult> errors = new List<ValidationResult>();
        bool isValid = Validator.TryValidateObject(club, new ValidationContext(club), errors, true);
        
        // Assert
        Assert.True(isValid); // Expected: true
        Assert.Empty(errors); // Expected: true
    }
    
    [Fact]
    public void CheckClubState_GivenInvalidData_Name() // Method: private void Validate(Club club)
    {
        // Arrange
        Club club = new Club
        {
            Name = "X", // Name minimum 2 and maximum 50 characters
            NumberOfCourts = 2,
            StreetName = "Kattenbroek", 
            HouseNumber = 3, 
            ZipCode = 2650
        };
        
        // Act
        List<ValidationResult> errors = new List<ValidationResult>();
        bool isValid = Validator.TryValidateObject(club, new ValidationContext(club), errors, true);
        
        // Assert
        Assert.False(isValid); // Expected: false
        Assert.NotEmpty(errors); // Expected: true
        Assert.Single(errors); // Expected: 1
        Assert.Equal("(Name) At least 2 character, maximum 50 characters", errors[0].ErrorMessage);
    }
    
    [Fact]
    public void CheckClubState_GivenValidData_StreetName() // Method: private void Validate(Club club)
    {
        // Arrange
        Club club = new Club
        {
            Name = "Ter Eiken", 
            NumberOfCourts = 1, // NumberOfCourts minimum 2 and maximum 4
            StreetName = "Kattenbroek", 
            HouseNumber = 3, 
            ZipCode = 2650
        };
        
        // Act
        List<ValidationResult> errors = new List<ValidationResult>();
        bool isValid = Validator.TryValidateObject(club, new ValidationContext(club), errors, true);
        
        // Assert
        Assert.True(isValid); // Expected: true
        Assert.Empty(errors); // Expected: true
    }
    
    [Fact]
    public void CheckClubState_GivenInvalidData_StreetName() // Method: private void Validate(Club club)
    {
        // Arrange
        Club club = new Club
        {
            Name = "Ter Eiken", 
            NumberOfCourts = 1, // NumberOfCourts minimum 2 and maximum 4
            StreetName = "X", // StreetName minimum 2 and maximum 50 characters
            HouseNumber = 3, 
            ZipCode = 2650
        };
        
        // Act
        List<ValidationResult> errors = new List<ValidationResult>();
        bool isValid = Validator.TryValidateObject(club, new ValidationContext(club), errors, true);
        
        // Assert
        Assert.False(isValid); // Expected: false
        Assert.NotEmpty(errors); // Expected: true
        Assert.Single(errors); // Expected: 1
        Assert.Equal("(StreetName) At least 2 character, maximum 50 characters", errors[0].ErrorMessage);
    }
}