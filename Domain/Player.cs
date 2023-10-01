/***************************************
 *                                     *
 *   Created by Elias De Hondt         *
 *   Visit https://eliasdh.com         *
 *                                     *
 ***************************************/
// Class Player
using System.ComponentModel.DataAnnotations;

namespace SC.BL.Domain;

public class Player
{
    public int PlayerNumber { get; set; } // Id
    [StringLength(25, ErrorMessage = "(FirstName) At least one character, maximum 25 characters")] [Required]
    public string FirstName { get; set; }
    [StringLength(25, ErrorMessage = "(LastName) At least one character, maximum 25 characters")] [Required]
    public string LastName { get; set; }
    [RegularExpression(@"\d{2}/\d{2}/\d{4}", ErrorMessage = "(BirthDate) Input the date as follows: dd/MM/yyyy")]
    public DateOnly? BirthDate { get; set; } // ? = nullable
    [Range(0, 10, ErrorMessage = "(Level) Input a number from 0 to 10")] [Required]
    public double Level { get; set; }
    public PlayerPosition Position { get; set; }
    public List<PadelCourt> PlayedOnCourts { get; set; } = new List<PadelCourt>(); // List of Played padel courts.
    
    // Override ToString() method
    public override string ToString()
    {
        return $"PlayerNumber {PlayerNumber}, {FirstName} {LastName} born on ({BirthDate}) is a {Position} with a level of {Level}.";
    }
}