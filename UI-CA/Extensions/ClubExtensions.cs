using PadelClubManagement.BL.Domain;

namespace PadelClubManagement.UI.CA.Extensions;

public static class ClubExtensions
{
    public static string GetInfo(this Club club) // Override ToString() method
    {
        return $"{club.Name} ({club.ClubNumber}) has {club.NumberOfCours} courts and is located at {club.StreetName} {club.HouseNumber}, {club.ZipCode}."; // Notation: $"" = string interpolation
        // return String.Format("{0} ({1}) has {2} courts and is located at {3} {4}, {5}.", club.Name, club.ClubNumber, club.NumberOfCours, club.StreetName, club.HouseNumber, club.ZipCode); // Notation: String.Format()
    }
}