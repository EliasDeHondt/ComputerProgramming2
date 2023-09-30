/***************************************
 *                                     *
 *   Created by Elias De Hondt         *
 *   Visit https://eliasdh.com         *
 *                                     *
 ***************************************/
// Class PadelCourt
namespace SC.BL.Domain;

public class PadelCourt
{
    public int CourtNumber { get; set; } // Id
    public bool IsIndoor { get; set; }
    public int Capacity { get; set; }
    public double Price { get; set; }
    public Club Club { get; set; } // Club where the PadelCourt is located.
    
    // Override ToString() method
    public override string ToString()
    {
        // {(IsIndoor ? "indoor" : "outdoor")} if IsIndoor is true, return "indoor", else return "outdoor"
        return $"Padel Court {CourtNumber} is {(IsIndoor ? "indoor" : "outdoor")} and has a capacity of {Capacity} players. The price is {Price} euro per hour (Club: {Club.Name}).";
    }
}