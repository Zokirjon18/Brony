using Brony.Models;

namespace Brony.Services.Bookings;

public interface IBookingService
{
    void Book(
        int userId, 
        int stadiumId, 
        DateTime startDate, 
        DateTime endDate);

    void Cancel(int bookingId);

    void ChangeDateTime(int bookingId, DateTime startDate, DateTime endDate);

    Booking Get(int id);
    
    List<Booking> GetAll();

    List<Booking> GetAllByUserId(int userId);
    
    List<Booking> GetAllByStadiumId(int stadiumId);
}