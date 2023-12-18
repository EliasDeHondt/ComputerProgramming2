/***************************************
 *                                     *
 *   Created by Elias De Hondt         *
 *   Visit https://eliasdh.com         *
 *                                     *
 ***************************************/
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Microsoft.EntityFrameworkCore;
using PadelClubManagement.BL;
using PadelClubManagement.BL.Domain;
using PadelClubManagement.DAL.EF;

namespace PadelClubManagement.UI.WIN;

public partial class MainWindow : Window
{ 
    private IManager _manager;
    private PadelClubManagementDbContext _context;

    public MainWindow()
    {
        Title = "Padel Club Management";
        Icon = new BitmapImage(new Uri("pack://application:,,,/Resources/icon.ico"));
        SetupDatabase();
    }
    
    private void Club_Click(object sender, RoutedEventArgs e)
    {
        IEnumerable<Club> clubs = _manager.GetAllClubs();
        string message = "Lijst van clubs:\n\n";
        foreach (Club club in clubs)
        {
            message += $"Club Number: {club.ClubNumber}\n";
            message += $"Name: {club.Name}\n";
            message += $"Number Of Courts: {club.NumberOfCourts}\n";
            message += $"Street Name: {club.StreetName}\n";
            message += $"House Number: {club.HouseNumber}\n";
            message += $"Zip Code: {club.ZipCode}\n";
            message += "\n";
        }
        MessageBox.Show(message);
    }
    
    private void PadelCourt_Click(object sender, RoutedEventArgs e)
    {
        IEnumerable<PadelCourt> padelCourts = _manager.GetAllPadelCourts();
        string message = "Lijst van padelCourts:\n\n";
        foreach (PadelCourt padelCourt in padelCourts)
        {
            message += $"Court Number: {padelCourt.CourtNumber}\n";
            message += $"Is Indoor: {padelCourt.IsIndoor}\n";
            message += $"Capacity: {padelCourt.Capacity}\n";
            message += $"Price: {padelCourt.Price}\n";
            message += "\n";
        }
        MessageBox.Show(message);
    }
    
    private void Booking_Click(object sender, RoutedEventArgs e)
    {
        IEnumerable<Booking> bookings = _manager.GetAllBookings();
        string message = "Lijst van booking:\n\n";
        foreach (Booking booking in bookings)
        {
            message += $"Booking Number: {booking.BookingNumber}\n";
            message += $"Booking Date: {booking.BookingDate}\n";
            message += $"Start Time: {booking.StartTime}\n";
            message += $"End Time: {booking.EndTime}\n";
            message += "\n";
        }
        MessageBox.Show(message);
    }
    
    private void Player_Click(object sender, RoutedEventArgs e)
    {
        IEnumerable<Player> players = _manager.GetAllPlayers();
        string message = "Lijst van booking:\n\n";
        foreach (Player player in players)
        {
            message += $"Player Number: {player.PlayerNumber}\n";
            message += $"First Name: {player.FirstName}\n";
            message += $"Last Name: {player.LastName}\n";
            message += $"Birth Date: {player.BirthDate}\n";
            message += $"Level: {player.Level}\n";
            message += $"Position: {player.Position}\n";
            message += "\n";
        }
        MessageBox.Show(message);
    }
    
    private void SetupDatabase()
    {
        DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder();
        optionsBuilder.UseSqlite(@"Data Source=..\..\..\..\PadelClubManagement.db");
        _context = new PadelClubManagementDbContext(optionsBuilder.Options);
        DbContextRepository dbContextRepository = new DbContextRepository(_context);
        bool databaseCreated = _context.Database.EnsureCreated();
        if (databaseCreated) DataSeeder.Seed(_context);
        _manager = new Manager(dbContextRepository);
    }
}