/***************************************
 *                                     *
 *   Created by Elias De Hondt         *
 *   Visit https://eliasdh.com         *
 *                                     *
 ***************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using PadelClubManagement.BL;
using PadelClubManagement.BL.Domain;
using PadelClubManagement.DAL.EF;

namespace PadelClubManagement.UI.WIN;

public partial class MainWindow
{ 
    private IManager _manager;
    private PadelClubManagementDbContext _context;

    public MainWindow()
    {
        InitializeComponent();
        SetupDatabase();
    }
    
    private static void BoxMessage(string message, string title = "") => MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Error);

    private void ClubClick(object sender, RoutedEventArgs e)
    {
        try
        {
            List<Club> clubs = _manager.GetAllClubs().ToList();
            if (clubs.Any())
            {
                ClubWindow clubWindow = new ClubWindow(clubs, _manager);
                clubWindow.ShowDialog();
            }
            else BoxMessage("No clubs were found.");
        }
        catch (Exception exception) {BoxMessage(exception.Message, "Error retrieving clubs");}
    }
    
    private void PadelCourtClick(object sender, RoutedEventArgs e)
    {
        try
        {
            List<PadelCourt> padelCourts = _manager.GetAllPadelCourts().ToList();
            if (padelCourts.Any())
            {
                PadelCourtWindow padelCourtWindow = new PadelCourtWindow(padelCourts);
                padelCourtWindow.ShowDialog();
            }
            else BoxMessage("No PadelCourts were found.");
        }
        catch (Exception exception) {BoxMessage(exception.Message, "Error retrieving PadelCourts");}
    }
    
    private void BookingClick(object sender, RoutedEventArgs e)
    {
        try
        {
            List<Booking> bookings = _manager.GetAllBookings().ToList();
            if (bookings.Any())
            {
                BookingWindow bookingWindow = new BookingWindow(bookings);
                bookingWindow.ShowDialog();
            }
            else BoxMessage("No Bookings were found.");
        }
        catch (Exception exception) {BoxMessage(exception.Message, "Error retrieving Bookings");}
    }
    
    private void PlayerClick(object sender, RoutedEventArgs e)
    {
        try
        {
            List<Player> players = _manager.GetAllPlayers().ToList();
            if (players.Any())
            {
                PlayerWindow playerWindow = new PlayerWindow(players);
                playerWindow.ShowDialog();
            }
            else BoxMessage("No Players were found.");
        }
        catch (Exception exception) {BoxMessage(exception.Message, "Error retrieving Players");}
    }
    
    private void SetupDatabase()
    {
        try
        {
            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseSqlite(@"Data Source=..\..\..\..\PadelClubManagement.db");
            _context = new PadelClubManagementDbContext(optionsBuilder.Options);
            DbContextRepository dbContextRepository = new DbContextRepository(_context);
            bool databaseCreated = _context.Database.EnsureCreated();
            if (databaseCreated) DataSeeder.Seed(_context);
            _manager = new Manager(dbContextRepository);
        }
        catch (Exception exception) {BoxMessage(exception.Message, "Error setting up the database");}
    }
}