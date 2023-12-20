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

public partial class PadelCourtWindow
{
    public PadelCourtWindow(List<PadelCourt> padelCourts)
    {
        InitializeComponent();
        ShowPadelCourts(padelCourts);
    }

    private void ShowPadelCourts(List<PadelCourt> padelCourts)
    {
        string message = "";
        foreach (PadelCourt padelCourt in padelCourts)
        {
            message += $"Court Number: {padelCourt.CourtNumber}\n";
            message += $"Is Indoor: {padelCourt.IsIndoor}\n";
            message += $"Capacity: {padelCourt.Capacity}\n";
            message += $"Price: {padelCourt.Price}\n";
            message += "\n";
        }
        PadelCourtTextBox.Text = message;
    }

    private void ButtonClick(object sender, RoutedEventArgs e) => Close();
}