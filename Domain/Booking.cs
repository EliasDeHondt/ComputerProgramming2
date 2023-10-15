namespace PadelClubManagement.BL.Domain;

public class Booking
{
    public int BookingNumber { get; set; } // Primary key
    public Player Player { get; set; } // Foreign key           (Navigation property)
    public PadelCourt PadelCourt { get; set; } // Foreign key   (Navigation property)
    
    public DateOnly? BookingDate { get; set; }
    public TimeSpan? StartTime { get; set; }
    public TimeSpan? EndTime { get; set; }
}