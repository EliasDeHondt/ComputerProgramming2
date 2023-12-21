/***************************************
 *                                     *
 *   Created by Elias De Hondt         *
 *   Visit https://eliasdh.com         *
 *                                     *
 ***************************************/
using System.Collections.Generic;
using PadelClubManagement.BL.Domain;

namespace PadelClubManagement.UI.WIN;

public partial class PlayerWindow
{
    public PlayerWindow(List<Player> players)
    {
        InitializeComponent();
        DataContext = this;
        Players = players;
    }

    public List<Player> Players { get; set; }
}