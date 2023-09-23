/***************************************
 *                                     *
 *   Created by Elias De Hondt         *
 *   Visit https://eliasdh.com         *
 *                                     *
 ***************************************/
// Entiteit Player
namespace CA;

public class Player
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateOnly? BirthDate { get; set; } // ? = nullable
    public double Level { get; set; }
    public PlayerPosition Position { get; set; }
    public List<PadelCourt> PlayedOnCourts { get; set; } = new List<PadelCourt>(); // List of Played padel courts.
    
    // Override ToString() method
    public override string ToString()
    {
        return $"{FirstName} {LastName} born on ({BirthDate}) is a {Position} with a level of {Level}.";
    }
}