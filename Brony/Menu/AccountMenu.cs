using Brony.Services.Users;

namespace Brony.Menu;

public class AccountMenu
{
    private readonly UserService userService;
    private readonly UserMenu userMenu;
    public AccountMenu(UserService userService, UserMenu userMenu)
    {
        userMenu = userMenu;
        this.userService = userService;
    }
    public void Login()
    {
        try
        {
            Console.Write("Enter phone number: ");
            string phone = Console.ReadLine();
        
            Console.Write("Enter password: ");
            string password = Console.ReadLine();
        
            var userId = userService.Login(phone, password);

            userMenu.Main(userId);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    public void Register()
    {
        try
        {
            Console.Write("Enter firstname: ");
            string firstName = Console.ReadLine();
            Console.Write("Enter lastname: ");
            string lastName = Console.ReadLine();
            Console.Write("Enter phone number: ");
            string phoneNumber = Console.ReadLine();
            Console.Write("Enter password: ");
            string password = Console.ReadLine();
        
            userService.Register(firstName, lastName, phoneNumber, password);

            Console.WriteLine("Account created!");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}