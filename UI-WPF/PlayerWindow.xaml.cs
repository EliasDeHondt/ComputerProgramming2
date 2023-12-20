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

public partial class PlayerWindow
{
    public PlayerWindow(List<Player> players)
    {
        InitializeComponent();
        ShowPlayers(players);
    }

    private void ShowPlayers(List<Player> players)
    {
        string message = "";
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
        PlayerTextBox.Text = message;
    }

    private void ButtonClick(object sender, RoutedEventArgs e) => Close();
}