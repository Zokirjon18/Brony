using Brony.Services.Stadiums;

namespace Brony.Menu;

public class UserStadiumMenu
{
    private readonly StadiumService stadiumService;
    public UserStadiumMenu(StadiumService stadiumService)
    {
        this.stadiumService = stadiumService;
    }
    
    public void Main()
    {
        try
        {
            GetAllMenu();
        
            Console.WriteLine("1. Search by name\n" +
                              "2. Set filter\n" +
                              "3. Exit");

            Console.Write("Enter choice: ");
            var choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    SearchMenu();
                    break;
                case 2:
                    GetFilteredListMenu();
                    break;
                case 3:
                    return; 
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Something went wrong");
        }
    }
    
    private void GetAllMenu()
    {
        var stadiums = stadiumService.GetAll();

        foreach (var stadium in stadiums)
        {
            Console.WriteLine($"ID: {stadium.Id} | " +
                              $"Name: {stadium.Name} | " +
                              $"Location: {stadium.Location} | " +
                              $"Price: {stadium.Price}");
            
        }
    }

    private void SearchMenu()
    {
        
    }

    private void GetFilteredListMenu()
    {
        
    }
}