/***************************************
 *                                     *
 *   Created by Elias De Hondt         *
 *   Visit https://eliasdh.com         *
 *                                     *
 ***************************************/
// Class Club

using System.ComponentModel.DataAnnotations;

namespace PadelClubManagement.BL.Domain;

public class Club
{
    public int ClubNumber { get; set; } // Id
    [StringLength(50, MinimumLength = 2, ErrorMessage = "(Name) At least 2 character, maximum 50 characters")] [Required]
    public string Name { get; set; }
    public int NumberOfCours { get; set; }
    [StringLength(50, MinimumLength = 2, ErrorMessage = "(StreetName) At least 2 character, maximum 50 characters")] [Required]
    public string StreetName { get; set; } // Part of address
    public int HouseNumber { get; set; } // Part of address
    public int ZipCode { get; set; } // Part of address
}