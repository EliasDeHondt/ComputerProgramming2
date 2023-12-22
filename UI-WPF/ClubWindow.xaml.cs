/***************************************
 *                                     *
 *   Created by Elias De Hondt         *
 *   Visit https://eliasdh.com         *
 *                                     *
 ***************************************/
using System.Collections.Generic;
using System.Windows;
using PadelClubManagement.BL;
using PadelClubManagement.BL.Domain;

namespace PadelClubManagement.UI.WIN;

public partial class ClubWindow
{
    private IManager _manager;
    public ClubWindow(List<Club> clubs, IManager manager)
    {
        InitializeComponent();
        DataContext = this;
        Clubs = clubs;
        _manager = manager;
    }
    
    private void AddClub_Click(object sender, RoutedEventArgs e)
    {
        string name = TxtName.Text;
        int numberOfCourts = int.Parse(TxtNumberOfCourts.Text);
        string streetName = TxtStreetName.Text;
        int houseNumber = int.Parse(TxtHouseNumber.Text);
        int zipCode = int.Parse(TxtZipCode.Text);
        _manager.AddClub(name, numberOfCourts, streetName, houseNumber, zipCode);
        MessageBox.Show("Club added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
    }
    
    public List<Club> Clubs { get; set; }
}