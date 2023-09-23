/***************************************
 *                                     *
 *   Created by Elias De Hondt         *
 *   Visit https://eliasdh.com         *
 *                                     *
 ***************************************/
// Entiteit PadelCourt
namespace CA;

public class PadelCourt
{
    public int CourtNumber { get; set; }
    public Boolean IsIndoor { get; set; }
    public int Capacity { get; set; }
    public double Price { get; set; }
    
    // Override ToString() method
    public override string ToString()
    {
        // {(IsIndoor ? "indoor" : "outdoor")} if IsIndoor is true, return "indoor", else return "outdoor"
        return $"PadelCourt {CourtNumber} is {(IsIndoor ? "indoor" : "outdoor")} and has a capacity of {Capacity} players. The price is {Price} euro per hour.";
    }
}