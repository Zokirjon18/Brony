using Brony.Services.Bookings;
using Brony.Services.Stadiums;
using Brony.Services.Users;

namespace Brony;

public class ObjectHolder
{
    public UserService UserService { get; set; }
    public StadiumService StadiumService { get; set; }
    public BookingService BookingService { get; set; }
}

