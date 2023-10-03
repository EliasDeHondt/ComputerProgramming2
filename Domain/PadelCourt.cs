/***************************************
 *                                     *
 *   Created by Elias De Hondt         *
 *   Visit https://eliasdh.com         *
 *                                     *
 ***************************************/
// Class PadelCourt
using System.ComponentModel.DataAnnotations;

namespace SC.BL.Domain;

public class PadelCourt : IValidatableObject
{
    public int CourtNumber { get; set; } // Id
    public bool IsIndoor { get; set; }
    public int Capacity { get; set; }
    public double Price { get; set; }
    public Club Club { get; set; } // Club where the PadelCourt is located.
    
    // Implement IValidatableObject
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        List<ValidationResult> errors = new List<ValidationResult>();
        
        if (!(Capacity >= 2 && Capacity <= 4)) // Capacity must be between 2 and 4
        {
            errors.Add(new ValidationResult("(Capacity) Input a number from 2 to 4", new string[] { "Capacity" }));
        }
        
        if (!(Price >= 0.01 && Price < 100)) // Price must be between 0.01 and 100
        {
            errors.Add(new ValidationResult("(Price) Input a number between 0.01 and 100", new string[] { "Price" }));
        }
        return errors;
    }
    
    // Override ToString() method
    public override string ToString()
    {
        // {(IsIndoor ? "indoor" : "outdoor")} if IsIndoor is true, return "indoor", else return "outdoor"
        return $"Padel Court {CourtNumber} is {(IsIndoor ? "indoor" : "outdoor")} and has a capacity of {Capacity} players. The price is {Price} euro per hour (Club: {Club.Name})."; // Notation: $"" = string interpolation
        // string IsIndoorString = IsIndoor ? "indoor" : "outdoor";
        // return String.Format("Padel Court {0} is {1} and has a capacity of {2} players. The price is {3} euro per hour (Club: {4}).", CourtNumber, IsIndoorString, Capacity, Price, Club.Name); // Notation: String.Format()
    }
}