using Brony.Models;
using Brony.Services.Bookings;
using Brony.Services.Stadiums;
using Brony.Services.Users;

namespace Brony.Menu;

public class BookingMenu
{
    private readonly BookingService _bookingService;
    public BookingMenu(BookingService bookingService)
    {
        _bookingService = bookingService;
    }

    public void Main()
    {
        while (true)
        {
            Console.WriteLine("1. Book\n" +
                              "2. Cancel\n" +
                              "3. Change DateTime\n" +
                              "4. Get\n" +
                              "5. Get list\n" +
                              "6. Exit");

            Console.Write("Enter choice: ");
            if (!int.TryParse(Console.ReadLine(), out var choice))
            {
                Console.WriteLine("Invalid input. Please enter a number.");
                continue;
            }

            if (choice == 6)
                break;

            try
            {
                switch (choice)
                {
                    case 1:
                        BookMenu();
                        break;
                    case 2:
                        CancelMenu();
                        break;
                    case 3:
                        ChangeDateTimeMenu();
                        break;
                    case 4:
                        GetMenu();
                        break;
                    case 5:
                        GetAllMenu();
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please select a number between 1 and 6.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }
    }

    private void BookMenu()
    {
        
        
        Console.Write("Enter User ID: ");
        if (!int.TryParse(Console.ReadLine(), out var userId))
        {
            throw new Exception("Invalid User ID.");
        }

        Console.Write("Enter Stadium ID: ");
        if (!int.TryParse(Console.ReadLine(), out var stadiumId))
        {
            throw new Exception("Invalid Stadium ID.");
        }

        Console.Write("Enter Start Time (yyyy-MM-dd HH:mm): ");
        if (!DateTime.TryParse(Console.ReadLine(), out var startTime))
        {
            throw new Exception("Invalid Start Time format. Use yyyy-MM-dd HH:mm.");
        }

        Console.Write("Enter End Time (yyyy-MM-dd HH:mm): ");
        if (!DateTime.TryParse(Console.ReadLine(), out var endTime))
        {
            throw new Exception("Invalid End Time format. Use yyyy-MM-dd HH:mm.");
        }

        _bookingService.Book(userId, stadiumId, startTime, endTime);
        Console.WriteLine("Booking created successfully!");
    }

    private void CancelMenu()
    {
        Console.Write("Enter Booking ID: ");
        if (!int.TryParse(Console.ReadLine(), out var bookingId))
        {
            throw new Exception("Invalid Booking ID.");
        }

        _bookingService.Cancel(bookingId);
        Console.WriteLine("Booking cancelled successfully!");
    }

    private void ChangeDateTimeMenu()
    {
        Console.Write("Enter Booking ID: ");
        if (!int.TryParse(Console.ReadLine(), out var bookingId))
        {
            throw new Exception("Invalid Booking ID.");
        }

        Console.Write("Enter New Start Time (yyyy-MM-dd HH:mm): ");
        if (!DateTime.TryParse(Console.ReadLine(), out var startTime))
        {
            throw new Exception("Invalid Start Time format. Use yyyy-MM-dd HH:mm.");
        }

        Console.Write("Enter New End Time (yyyy-MM-dd HH:mm): ");
        if (!DateTime.TryParse(Console.ReadLine(), out var endTime))
        {
            throw new Exception("Invalid End Time format. Use yyyy-MM-dd HH:mm.");
        }

        _bookingService.ChangeDateTime(bookingId, startTime, endTime);
        Console.WriteLine("Booking time updated successfully!");
    }

    private void GetMenu()
    {
        Console.Write("Enter Booking ID: ");
        if (!int.TryParse(Console.ReadLine(), out var bookingId))
        {
            throw new Exception("Invalid Booking ID.");
        }

        var booking = _bookingService.Get(bookingId);
        Console.WriteLine($"Booking ID: {booking.Id}");
        Console.WriteLine($"User ID: {booking.UserId}");
        Console.WriteLine($"Stadium ID: {booking.StadiumId}");
        Console.WriteLine($"Start Time: {booking.StartTime:yyyy-MM-dd HH:mm}");
        Console.WriteLine($"End Time: {booking.EndTime:yyyy-MM-dd HH:mm}");
        Console.WriteLine($"Price: {booking.Price:C}");
    }

    private void GetAllMenu()
    {
        Console.WriteLine("1. Get all bookings");
        Console.WriteLine("2. Get bookings by User ID");
        Console.WriteLine("3. Get bookings by Stadium ID");
        Console.Write("Enter choice: ");
        if (!int.TryParse(Console.ReadLine(), out var choice))
        {
            throw new Exception("Invalid input.");
        }

        switch (choice)
        {
            case 1:
                var allBookings = _bookingService.GetAll();
                if (allBookings.Count == 0)
                {
                    Console.WriteLine("No bookings found.");
                    return;
                }
                foreach (var booking in allBookings)
                {
                    DisplayBooking(booking);
                }
                break;

            case 2:
                Console.Write("Enter User ID: ");
                if (!int.TryParse(Console.ReadLine(), out var userId))
                {
                    throw new Exception("Invalid User ID.");
                }
                var userBookings = _bookingService.GetAllByUserId(userId);
                if (userBookings.Count == 0)
                {
                    Console.WriteLine("No bookings found for this user.");
                    return;
                }
                foreach (var booking in userBookings)
                {
                    DisplayBooking(booking);
                }
                break;

            case 3:
                Console.Write("Enter Stadium ID: ");
                if (!int.TryParse(Console.ReadLine(), out var stadiumId))
                {
                    throw new Exception("Invalid Stadium ID.");
                }
                var stadiumBookings = _bookingService.GetAllByStadiumId(stadiumId);
                if (stadiumBookings.Count == 0)
                {
                    Console.WriteLine("No bookings found for this stadium.");
                    return;
                }
                foreach (var booking in stadiumBookings)
                {
                    DisplayBooking(booking);
                }
                break;

            default:
                throw new Exception("Invalid choice.");
        }
    }

    private void DisplayBooking(Booking booking)
    {
        Console.WriteLine("-------------------");
        Console.WriteLine($"Booking ID: {booking.Id}");
        Console.WriteLine($"User ID: {booking.UserId}");
        Console.WriteLine($"Stadium ID: {booking.StadiumId}");
        Console.WriteLine($"Start Time: {booking.StartTime:yyyy-MM-dd HH:mm}");
        Console.WriteLine($"End Time: {booking.EndTime:yyyy-MM-dd HH:mm}");
        Console.WriteLine($"Price: {booking.Price:C}");
    }
}