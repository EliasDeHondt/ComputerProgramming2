/***************************************
 *                                     *
 *   Created by Elias De Hondt         *
 *   Visit https://eliasdh.com         *
 *                                     *
 ***************************************/
using System.Collections.Generic;
using System.Windows;
using PadelClubManagement.BL.Domain;

namespace PadelClubManagement.UI.WIN;

public partial class ClubWindow
{
    public ClubWindow(List<Club> clubs)
    {
        InitializeComponent();
        ShowClubs(clubs);
    }

    private void ShowClubs(List<Club> clubs)
    {
        string message = "";
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
        ClubTextBox.Text = message;
    }

    private void ButtonClick(object sender, RoutedEventArgs e) => Close();
}