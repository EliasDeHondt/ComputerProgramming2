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
    [Range(2,4, ErrorMessage = "Input a number from 2 to 4")] [Required]
    public int Capacity { get; set; }
    public double Price { get; set; }
    public Club Club { get; set; } // Club where the PadelCourt is located.
    
    // Implement IValidatableObject
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        List<ValidationResult> errors = new List<ValidationResult>();

        // Check that the price has exactly two decimal places
        if (Math.Abs((Price * 100) % 1) > double.Epsilon)
        {
            errors.Add(new ValidationResult("Price must have exactly two decimal places", new string[] { "Price" }));
        }

        return errors;
    }
    
    // Override ToString() method
    public override string ToString()
    {
        // {(IsIndoor ? "indoor" : "outdoor")} if IsIndoor is true, return "indoor", else return "outdoor"
        return $"Padel Court {CourtNumber} is {(IsIndoor ? "indoor" : "outdoor")} and has a capacity of {Capacity} players. The price is {Price} euro per hour (Club: {Club.Name}).";
    }
}