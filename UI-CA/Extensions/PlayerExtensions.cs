/***************************************
 *                                     *
 *   Created by Elias De Hondt         *
 *   Visit https://eliasdh.com         *
 *                                     *
 ***************************************/
// Class PlayerExtensions
using PadelClubManagement.BL.Domain;

namespace PadelClubManagement.UI.CA.Extensions;

public static class PlayerExtensions
{
    public static string GetInfoBrief(this Player player) // Override ToString() method
    {
        return $"PlayerNumber {player.PlayerNumber}, {player.FirstName} {player.LastName} born on ({player.BirthDate}) is a {player.Position} with a level of {player.Level}."; // Notation: $"" = string interpolation
        // return String.Format("PlayerNumber {0}, {1} {2} born on ({3}) is a {4} with a level of {5}.", player.PlayerNumber, player.FirstName, player.LastName, player.BirthDate, player.Position, player.Level); // Notation: String.Format()
    }

    public static string GetInfo(this Player player)
    {
        string playerInfo = GetInfoBrief(player);

        if (player.Bookings != null && player.Bookings.Any())
        {
            foreach (Booking booking in player.Bookings)
            {
                string bookingInfo = booking.GetInfoBrief(); // Get booking info without padel court
                if (booking.PadelCourt != null)
                {
                    bookingInfo += $"\n\t\tPadel Court Info: {booking.PadelCourt.GetInfoBrief()}";
                }
                playerInfo += $"\n\t{bookingInfo}";
            }
        }
        return playerInfo;
    }
}