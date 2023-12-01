/***************************************
 *                                     *
 *   Created by Elias De Hondt         *
 *   Visit https://eliasdh.com         *
 *                                     *
 ***************************************/
// Main window of the application

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
    public MainWindow()
    {
        InitializeComponent(); // Initialize the components of the window
        Title = "Padel Club Management";
        Icon = new BitmapImage(new Uri("pack://application:,,,/Resources/icon.ico"));
        Background = new SolidColorBrush(Color.FromRgb(90, 214, 255));

        SetupDatabase();
    }
    
    private void SetupDatabase()
    {
        DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder();
        optionsBuilder.UseSqlite(@"Data Source=..\..\..\..\PadelClubManagement.db");
        PadelClubManagementDbContext padelClubManagementDbContext = new PadelClubManagementDbContext(optionsBuilder.Options);
        DbContextRepository dbContextRepository = new DbContextRepository(padelClubManagementDbContext);
        
        using (padelClubManagementDbContext)
        {
            bool databaseCreated = padelClubManagementDbContext.CreateDatabase(true); // Create the database (Code first flow)
            if (databaseCreated) DataSeeder.Seed(padelClubManagementDbContext); // Seed the database with some data
        }
        
        Manager manager = new Manager(dbContextRepository);
        
        IEnumerable<Player> players = manager.GetAllPlayers();
    }
}