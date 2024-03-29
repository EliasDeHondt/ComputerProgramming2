/***************************************
 *                                     *
 *   Created by Elias De Hondt         *
 *   Visit https://eliasdh.com         *
 *                                     *
 ***************************************/
// Class PadelCourt
using System.ComponentModel.DataAnnotations;

namespace PadelClubManagement.BL.Domain;

public class PadelCourt : IValidatableObject
{
    [Key]
    public int CourtNumber { get; set; } // Primary key
    public ICollection<Booking> Bookings { get; set; } // Foreign key   (Navigation property)
    public Club Club { get; set; } // Foreign key                       (Navigation property)
    
    public bool IsIndoor { get; set; }
    public int Capacity { get; set; }
    public double Price { get; set; }
    
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext) // Implement IValidatableObject
    {
        List<ValidationResult> errors = new List<ValidationResult>();
        
        if (!(Capacity >= 2 && Capacity <= 4)) // Capacity must be between 2 and 4
        {
            errors.Add(new ValidationResult("(Capacity) Input a number from 2 to 4", new string[] { nameof(Capacity) }));
        }
        
        if (!(Price >= 0.01 && Price < 100)) // Price must be between 0.01 and 100
        {
            errors.Add(new ValidationResult("(Price) Input a number between 0.01 and 100", new string[] { nameof(Price) }));
        }
        return errors;
    }
}