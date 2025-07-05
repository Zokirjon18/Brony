using Brony.Domain;
using Brony.Models;

namespace Brony.Services.Users;

public interface IUserService
{
    void Register(UserRegisterModel model);

    int Login(string phoneNumber, string password);
    
    User Get(int id);
    
    void Update(
        int id, 
        string firstName,
        string lastName, 
        string phoneNumber);
    
    void Delete(int id);
    
    List<User> GetAll();
    
    List<User> Search(string search);
    
    void ChangePassword(int userId, string oldPassword, string newPassword);
}