using Brony.Services.Bookings;
using Brony.Services.Stadiums;
using Brony.Services.Users;

namespace Brony.Menu;

public class UserMenu
{
    private readonly UserStadiumMenu _userStadiumMenu;
    private readonly UserService userService;
    private readonly BookingService bookingService;
    private readonly StadiumService stadiumService;
    public UserMenu(
        UserService userService,
        UserStadiumMenu userStadiumMenu,
        BookingService bookingService, 
        StadiumService stadiumService)
    {
        this.userService = userService;
        this._userStadiumMenu = userStadiumMenu;
        this.bookingService = bookingService;
        this.stadiumService = stadiumService;
    }
    
    public void Main(int userId)
    {
        Console.Clear();
        Console.WriteLine("---------- Welcome to Brony! ----------");
        Console.WriteLine("1. Stadiums\n2. Account\n3. Exit");

        Console.Write("Enter choice: ");
        var choice = int.Parse(Console.ReadLine());

        switch (choice)
        {
            case 1:
                _userStadiumMenu.Main();
                break;
            case 2:
                AccountMenu(userId);
                break;
            case 3:
                return;
        }
    }

    private void AccountMenu(int userId)
    {
        try
        {
            Console.Clear();
            Console.WriteLine("---------- Welcome to Brony! ----------");
            Console.Write("" +
                          "1. Bookings\n" +
                          "2. Update\n" +
                          "3. Change password\n" +
                          "4. Logout\n" +
                          "5. Delete" +
                          "6. Exit");
        
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    UserBookingsMenu(userId);
                    break;
                case 2:
                    UpdateMenu(userId);
                    break;
                case 3:
                    ChangePasswordMenu(userId);
                    break;
                case 4:
                    return;
                case 5:
                    DeleteMenu(userId);
                    break;
                case 6:
                    return;
            }
        }
        catch
        {
            Console.WriteLine("Error!");
        }
    }
    
    private void UserBookingsMenu(int userId)
    {
        var bookings = bookingService.GetAllByUserId(userId);

        foreach (var booking in bookings)
        {
            var stadium = stadiumService.Get(booking.StadiumId);
            
            Console.WriteLine($"ID. {booking.Id} | Stadium: {stadium.Name} | {booking.StartTime} - {booking.EndTime}");
        }
    }
    
    private void UpdateMenu(int userId)
    {
        try
        {
            Console.Clear();
            Console.Write("Enter firstname: ");
            string firstName = Console.ReadLine();
            Console.Write("Enter lastname: ");
            string lastName = Console.ReadLine();
            Console.Write("Enter phone number: ");
            string phoneNumber = Console.ReadLine();
        
            userService.Update(userId, firstName, lastName, phoneNumber);

            Console.WriteLine("Account updated!");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
    
    private void ChangePasswordMenu(int userId)
    {
        try
        {
            Console.Clear();
            Console.Write("Enter old password: ");
            string oldPassword = Console.ReadLine();
            Console.Write("Enter new password: ");
            string newPassword = Console.ReadLine();
            userService.ChangePassword(userId, oldPassword, newPassword);
        
            Console.WriteLine("Password changed!");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
    
    private void DeleteMenu(int userId)
    {
        try
        {
            userService.Delete(userId);
            Console.WriteLine("Account deleted!");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}