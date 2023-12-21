/***************************************
 *                                     *
 *   Created by Elias De Hondt         *
 *   Visit https://eliasdh.com         *
 *                                     *
 ***************************************/
using System.Collections.Generic;
using PadelClubManagement.BL.Domain;

namespace PadelClubManagement.UI.WIN;

public partial class BookingWindow
{
    public BookingWindow(List<Booking> bookings)
    {
        InitializeComponent();
        DataContext = this;
        Bookings = bookings;
    }

    public List<Booking> Bookings { get; set; }
}