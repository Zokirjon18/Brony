using Brony.Models;

namespace Brony.Services.Users;

public class UserService : IUserService
{
    private readonly List<User> users;
    private int userId;
    public UserService()
    {
        users = new List<User>()
        {
            new User()
            {
                Id = 1,
                FirsName = "Dilmurod",
                LastName = "Jabborov",
                PhoneNumber = "1234567890",
            },
            new User()
            {
                Id = 2,
                FirsName = "Dilmurod 2",
                LastName = "Jabborov 2",
                PhoneNumber = "1234567892",
            }
        };
        userId = 3;
    }
    
    public void Register(
        string firstName,
        string lastName,
        string phoneNumber,
        string password)
    {
        var existUser = users.Find(u => u.PhoneNumber == phoneNumber);
        if (existUser != null)
        {
            throw new Exception("User with this phone number already exists.");
        }
        
        var user = new User
        {
            Id = userId,
            FirsName = firstName,
            LastName = lastName,
            PhoneNumber = phoneNumber,
            Password = password
        };
        
        users.Add(user);
        
        userId++;
    }

    public int Login(string phoneNumber, string password)
    {
        var existUser = users.Find(u => u.PhoneNumber == phoneNumber);
        if (existUser == null)
        {
            throw new Exception("Phone or password is incorrect.");
        }

        if (existUser.Password != password)
        {
            throw new Exception("Phone or password is incorrect.");
        }

        return existUser.Id;
    }

    public User Get(int id)
    {
        var existUser = users.Find(u => u.Id == id);
        if (existUser == null)
        {
            throw new Exception("User is not found.");
        }
        
        return existUser;   
    }

    public void Update(
        int id,
        string firstName,
        string lastName, 
        string phoneNumber)
    {
        var existUser = users.Find(u => u.Id == id);
        if (existUser == null)
        {
            throw new Exception("Phone is not found.");
        }
        
        var alreadyExistUser = users.Find(u => u.PhoneNumber == phoneNumber);
        if (alreadyExistUser != null)
        {
            throw new Exception("User already exists with this phone number.");
        }
        
        existUser.PhoneNumber = phoneNumber;
        existUser.LastName = lastName;
        existUser.FirsName = firstName;
    }

    public void Delete(int id)
    {
        var existUser = users.Find(u => u.Id == id);
        if (existUser == null)
        {
            throw new Exception("Phone is not found.");
        }
        
        users.Remove(existUser);
    }

    public List<User> GetAll()
    {
        return users;
    }

    public List<User> Search(string search)
    {
        var result = new List<User>();
        
        if (!string.IsNullOrEmpty(search))
        {
            foreach (var user in users)
            {
                if (
                    user.FirsName.ToLower().Contains(search.ToLower()) ||
                    user.LastName.ToLower().Contains(search.ToLower()) ||
                    user.PhoneNumber.ToLower().Contains(search.ToLower()))
                {
                    result.Add(user);
                }
            }
        }
        
        return result;  
    }

    public void ChangePassword(int userId, string oldPassword, string newPassword)
    {
        var existUser = users.Find(u => u.Id == userId);
        if(existUser == null)
            throw new Exception("User is not found.");
        
        if(existUser.Password != oldPassword)
            throw new Exception("Passwords do not match.");
        
        existUser.Password = newPassword;
    }
}