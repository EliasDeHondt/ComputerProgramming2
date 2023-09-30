/***************************************
 *                                     *
 *   Created by Elias De Hondt         *
 *   Visit https://eliasdh.com         *
 *                                     *
 ***************************************/
// Class Player
namespace SC.BL.Domain;

public class Player
{
    public int PlayerNumber { get; set; } // Id
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly? BirthDate { get; set; } // ? = nullable
    public double Level { get; set; }
    public PlayerPosition Position { get; set; }
    public List<PadelCourt> PlayedOnCourts { get; set; } = new List<PadelCourt>(); // List of Played padel courts.
    
    // Override ToString() method
    public override string ToString()
    {
        return $"PlayerNumber {PlayerNumber}, {FirstName} {LastName} born on ({BirthDate}) is a {Position} with a level of {Level}.";
    }
}