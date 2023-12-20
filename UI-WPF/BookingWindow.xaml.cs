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

public partial class BookingWindow
{
    public BookingWindow(List<Booking> bookings)
    {
        InitializeComponent();
        ShowClick(bookings);
    }

    private void ShowClick(List<Booking> bookings)
    {
        string message = "";
        foreach (Booking booking in bookings)
        {
            message += $"Booking Number: {booking.BookingNumber}\n";
            message += $"Booking Date: {booking.BookingDate}\n";
            message += $"Start Time: {booking.StartTime}\n";
            message += $"End Time: {booking.EndTime}\n";
            message += "\n";
        }
        BookingTextBox.Text = message;
    }

    private void ButtonClick(object sender, RoutedEventArgs e) => Close();
}