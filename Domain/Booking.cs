/***************************************
 *                                     *
 *   Created by Elias De Hondt         *
 *   Visit https://eliasdh.com         *
 *                                     *
 ***************************************/
// Class Booking
using System.ComponentModel.DataAnnotations;

namespace PadelClubManagement.BL.Domain;

public class Booking
{
    [Key]
    public int BookingNumber { get; set; } // Primary key
    [Required]
    public Player Player { get; set; } // Foreign key           (Navigation property)
    [Required]
    public PadelCourt PadelCourt { get; set; } // Foreign key   (Navigation property)
    
    public DateOnly? BookingDate { get; set; }
    public TimeSpan? StartTime { get; set; }
    public TimeSpan? EndTime { get; set; }
}