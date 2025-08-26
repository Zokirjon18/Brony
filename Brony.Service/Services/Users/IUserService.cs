using Brony.Service.Models.Users;

namespace Brony.Service.Services.Users;

public interface IUserService
{
    Task RegisterAsync(UserRegisterModel model);
    Task<int> LoginAsync(UserLoginModel model);
    Task<UserViewModel> GetAsync(int id);
    Task UpdateAsync(int id, UserUpdateModel model);
    Task DeleteAsync(int id);
    Task<List<UserViewModel>> GetAllAsync(string search);
    Task ChangePasswordAsync(int id, string oldPassword, string newPassword);
}