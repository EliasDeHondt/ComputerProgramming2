/***************************************
 *                                     *
 *   Created by Elias De Hondt         *
 *   Visit https://eliasdh.com         *
 *                                     *
 ***************************************/
// Entiteit Club
namespace CA;

public class Club
{
    public string Name { get; set; }
    public int NumberOfCours { get; set; }
    public string StreetName { get; set; } // Part of address
    public int HouseNumber { get; set; } // Part of address
    public int ZipCode { get; set; } // Part of address
    
    // Override ToString() method
    public override string ToString()
    {
        return $"{Name} has {NumberOfCours} courts and is located at {StreetName} {HouseNumber}, {ZipCode}.";
    }
}