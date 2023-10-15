/***************************************
 *                                     *
 *   Created by Elias De Hondt         *
 *   Visit https://eliasdh.com         *
 *                                     *
 ***************************************/
// Class PlayerControllerExtensions
using PadelClubManagement.BL.Domain;

namespace PadelClubManagement.UI.CA.Extensions;

public static class PadelCourtExtensions
{
    public static string GetInfo(this PadelCourt padelCourt) // Override ToString() method
    {
        // {(IsIndoor ? "indoor" : "outdoor")} if IsIndoor is true, return "indoor", else return "outdoor"
        return $"Padel Court {padelCourt.CourtNumber} is {(padelCourt.IsIndoor ? "indoor" : "outdoor")} and has a capacity of {padelCourt.Capacity} players. The price is {padelCourt.Price} euro per hour."; // Notation: $"" = string interpolation
        // string IsIndoorString = IsIndoor ? "indoor" : "outdoor";
        // return String.Format("Padel Court {0} is {1} and has a capacity of {2} players. The price is {3} euro per hour.", padelCourt.CourtNumber, padelCourt.IsIndoorString, padelCourt.Capacity, padelCourt.Price); // Notation: String.Format() player.Level); // Notation: String.Format()
    }
}