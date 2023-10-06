using PadelClubManagement.BL.Domain;

namespace PadelClubManagement.UI.CA.Extensions;

public static class ClubExtensions
{
    public static string GetInfo(this Club club) // Override ToString() method
    {
        return $"{club.Name} has {club.NumberOfCours} courts and is located at {club.StreetName} {club.HouseNumber}, {club.ZipCode}."; // Notation: $"" = string interpolation
        // return String.Format("{0} has {1} courts and is located at {2} {3}, {4}.", club.Name, club.NumberOfCours, club.StreetName, club.HouseNumber, club.ZipCode); // Notation: String.Format()
    }
}