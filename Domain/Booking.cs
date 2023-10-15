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
    public int BookingNumber { get; set; } [Key] // Primary key
    public Player Player { get; set; } // Foreign key           (Navigation property)
    public PadelCourt PadelCourt { get; set; } // Foreign key   (Navigation property)
    
    public DateOnly? BookingDate { get; set; }
    public TimeSpan? StartTime { get; set; }
    public TimeSpan? EndTime { get; set; }
}