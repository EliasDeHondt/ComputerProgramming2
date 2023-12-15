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
        InitializeComponent();
        Title = "Padel Club Management";
        Icon = new BitmapImage(new Uri("pack://application:,,,/Resources/icon.ico"));
        Background = new SolidColorBrush(Color.FromRgb(90, 214, 255));

        SetupDatabase();
        LoadPlayers();
    }

    private void LoadPlayers()
    {
        playersListBox.Items.Clear(); // Clear the list before adding new items
            
        IEnumerable<Player> players = _manager.GetAllPlayers();

        foreach (Player player in players)
        {
            playersListBox.Items.Add($"Player Number: {player.PlayerNumber}");
            playersListBox.Items.Add($"First Name: {player.FirstName}");
            playersListBox.Items.Add($"Last Name: {player.LastName}");
            playersListBox.Items.Add($"Birth Date: {player.BirthDate}");
            playersListBox.Items.Add($"Level: {player.Level}");
            playersListBox.Items.Add($"Position: {player.Position}");
            playersListBox.Items.Add("");
        }
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

    private void RefreshButton_Click(object sender, RoutedEventArgs e)
    {
        LoadPlayers();
    }
}