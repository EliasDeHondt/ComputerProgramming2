/***************************************
 *                                     *
 *   Created by Elias De Hondt         *
 *   Visit https://eliasdh.com         *
 *                                     *
 ***************************************/

using Microsoft.EntityFrameworkCore;
using PadelClubManagement.BL.Domain;
using PadelClubManagement.DAL.EF;

namespace PadelClubManagement.BL.Tests;

public class PadelCourtTests
{
    [Fact]
    public void GetPadelCourt_ReturnPadelCourt_GivenValidCourtNumber() // Method: public PadelCourt GetPadelCourt(int courtNumber)
    {
        // Arrange
        PadelCourt padelCourt = new PadelCourt
        {
            CourtNumber = 1,
            IsIndoor = true, 
            Capacity = 4, 
            Price = 20.50
        };
        var optionsBuilder = new DbContextOptionsBuilder<PadelClubManagementDbContext>();
        optionsBuilder.UseSqlite("Data Source=PadelCourt.db");
        PadelClubManagementDbContext dbContext = new PadelClubManagementDbContext(optionsBuilder.Options);
        DbContextRepository repository = new DbContextRepository(dbContext);
        Manager manager = new Manager(repository);
        
        int courtNumber = 1;
        
        // Act
        PadelCourt result = manager.GetPadelCourt(courtNumber);
        
        // Assert
        Assert.NotNull(result); // Expected: true
        Assert.IsType<PadelCourt>(result); // Expected: true (Not necessary because the method signature already guarantees this)
        Assert.Equal(courtNumber, result.CourtNumber); // Expected: true
    }
}