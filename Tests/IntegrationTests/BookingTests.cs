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

namespace Tests.IntegrationTests;

public class BookingTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly CustomWebApplicationFactory<Program> _factory;
    
    public BookingTests(CustomWebApplicationFactory<Program> factory) // Constructor
    {
        _factory = factory; // Create a new web application factory
    }
    
    [Fact]
    public void AddBooking_GivenValidPlayerAndPadelCourt() // Method: public int AddBooking(int playerNumber, int courtNumber, Booking booking, bool returnBookingNumber)
    {
        // Arrange
        using IServiceScope scope = _factory.Services.CreateScope();
        IServiceProvider services = scope.ServiceProvider;
        IManager manager = services.GetRequiredService<IManager>();

        Booking booking = new Booking
        {
            BookingDate = new DateOnly(2024, 4, 15), 
            StartTime = new TimeSpan(9, 30, 0), 
            EndTime = new TimeSpan(10, 30, 0)
        };
        int playerNumber = 1;
        int courtNumber = 3;
        
        // Act
        int bookingNumber = manager.AddBooking(playerNumber, courtNumber, booking, true);
        manager.AddPlayerToBooking(playerNumber, bookingNumber);
        manager.AddPadelCourtToBooking(courtNumber, bookingNumber);


        // Assert
        Assert.True(true);
    }
    
    [Fact]
    public void AddBooking_GivenInvalidPlayerAndPadelCourt() // Method: public int AddBooking(int playerNumber, int courtNumber, Booking booking, bool returnBookingNumber)
    {
        // Arrange
        using IServiceScope scope = _factory.Services.CreateScope();
        IServiceProvider services = scope.ServiceProvider;
        IManager manager = services.GetRequiredService<IManager>();
        
        // Act
        
        
        
        // Assert

    }
}