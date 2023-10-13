/***************************************
 *                                     *
 *   Created by Elias De Hondt         *
 *   Visit https://eliasdh.com         *
 *                                     *
 ***************************************/
// Class Player
using System.ComponentModel.DataAnnotations;

namespace PadelClubManagement.BL.Domain;

public class Player
{
    public int PlayerNumber { get; set; } // Id
    [StringLength(50, MinimumLength = 2, ErrorMessage = "(FirstName) At least 2 character, maximum 50 characters")] [Required]
    public string FirstName { get; set; }
    [StringLength(50, MinimumLength = 2, ErrorMessage = "(LastName) At least 2 character, maximum 50 characters")] [Required]
    public string LastName { get; set; }
    [RegularExpression(@"\d{2}/\d{2}/\d{4}", ErrorMessage = "(BirthDate) Input the date as follows: dd/MM/yyyy")]
    public DateOnly? BirthDate { get; set; } // ? = nullable
    [Range(0, 10, ErrorMessage = "(Level) Input a number from 0 to 10")] [Required]
    public double Level { get; set; }
    public PlayerPosition Position { get; set; }
    public List<PadelCourt> PlayedOnCourts { get; set; } = new List<PadelCourt>(); // List of Played padel courts.
}