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
    [Key]
    public int ClubNumber { get; set; } // Primary key
    public ICollection<PadelCourt> PadelCourts { get; set; } // Foreign key   (Navigation property)
    
    [StringLength(50, MinimumLength = 2, ErrorMessage = "(Name) At least 2 character, maximum 50 characters")] [Required]
    public string Name { get; set; }
    public int NumberOfCourts { get; set; }
    [StringLength(50, MinimumLength = 2, ErrorMessage = "(StreetName) At least 2 character, maximum 50 characters")] [Required]
    public string StreetName { get; set; }
    public int HouseNumber { get; set; }
    public int ZipCode { get; set; }
}