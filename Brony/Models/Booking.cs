namespace Brony.Models;

public class Booking
{
    public int Id { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public decimal Price { get; set; }
    public int StadiumId { get; set; }
    public int UserId { get; set; }
}