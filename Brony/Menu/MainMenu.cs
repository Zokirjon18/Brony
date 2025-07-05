// using System.Runtime.InteropServices;
// using Brony.Services.Bookings;
// using Brony.Services.Stadiums;
// using Brony.Services.Users;
//
// namespace Brony.Menu;
//
// public class MainMenu
// {
//     private AccountMenu accountMenu;
//     private UserMenu userMenu;
//     private UserStadiumMenu _userStadiumMenu;
//     private UserService userService;
//     private StadiumService stadiumService;
//     public MainMenu()
//     {
//         
//         _userStadiumMenu = new UserStadiumMenu();
//         userService = new UserService();
//         userMenu = new UserMenu(userService, _userStadiumMenu);
//         accountMenu = new AccountMenu(userService, userMenu);
//         userMenu = new UserMenu(userService, _userStadiumMenu);
//     }
//     
//     public void Main()
//     {
//         while (true)
//         {
//             try
//             {
//                 Console.WriteLine("---------- Welcome to Brony! ----------");
//
//                 Console.WriteLine("1. Login\n2. Register\n3. Exit");
//
//                 Console.Write("Enter choice: ");
//                 int choice = Convert.ToInt32(Console.ReadLine());
//
//                 switch (choice)
//                 {
//                     case 1:
//                         accountMenu.Login();
//                         break;
//                     case 2:
//                         accountMenu.Register();
//                         break;
//                     case 3:
//                         return;
//                 }
//             }
//             catch
//             {
//                 Console.WriteLine("Error!");
//             }
//         }
//     }
// }