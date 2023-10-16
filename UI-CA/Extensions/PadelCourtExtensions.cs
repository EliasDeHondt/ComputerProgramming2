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
    public static string GetInfoBrief(this PadelCourt padelCourt) // Override ToString() method
    {
        // {(IsIndoor ? "indoor" : "outdoor")} if IsIndoor is true, return "indoor", else return "outdoor"
        return $"Padel Court {padelCourt.CourtNumber} is {(padelCourt.IsIndoor ? "indoor" : "outdoor")} and has a capacity of {padelCourt.Capacity} players. The price is {padelCourt.Price} euro per hour.";
    }
    
    public static string GetInfo(this PadelCourt padelCourt) // Override ToString() method
    {
        string padelCourtInfo = GetInfoBrief(padelCourt);
        
        padelCourtInfo += $"\n\t\tClub Info: {padelCourt.Club.GetInfoBrief()}";

        return padelCourtInfo;

    }
}