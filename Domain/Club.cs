/***************************************
 *                                     *
 *   Created by Elias De Hondt         *
 *   Visit https://eliasdh.com         *
 *                                     *
 ***************************************/
// Class Club
namespace PadelClubManagement.BL.Domain;

public class Club
{
    public string Name { get; set; } // Id
    public int NumberOfCours { get; set; }
    public string StreetName { get; set; } // Part of address
    public int HouseNumber { get; set; } // Part of address
    public int ZipCode { get; set; } // Part of address
}