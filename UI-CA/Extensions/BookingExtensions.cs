/***************************************
 *                                     *
 *   Created by Elias De Hondt         *
 *   Visit https://eliasdh.com         *
 *                                     *
 ***************************************/
// Class BookingExtensions

using PadelClubManagement.BL.Domain;

namespace PadelClubManagement.UI.CA.Extensions;

public static class BookingExtensions
{
    public static string GetInfoBrief(this Booking booking)
    {
        return $"Booking ID: {booking.BookingNumber} Date: {booking.BookingDate}, Time Start: {booking.StartTime}, Time End: {booking.EndTime}";
    }
}