using Brony.DataAccess.Contexts;
using Brony.Domain.Entities;
using Brony.Service.Helpers;
using Brony.Service.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace Brony.Service.Services.Users;

public class UserService : IUserService
{
    private readonly AppDbContext context;
    public UserService()
    {
        context = new AppDbContext();
    }

    public async Task RegisterAsync(UserRegisterModel model)
    {
        var existUser = await context.Users
            .FirstOrDefaultAsync(u => u.PhoneNumber == model.PhoneNumber);

        if (existUser != null)
            throw new Exception("User with this phone number already exists.");

        var user = new User
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            PhoneNumber = model.PhoneNumber,
            Password = PasswordHasher.HashPassword(model.Password),
        };

        await context.Users.AddAsync(user);
        await context.SaveChangesAsync();
    }
    
    public async Task<int> LoginAsync(UserLoginModel model)
    {
        var existUser = await context.Users.FirstOrDefaultAsync(u => u.PhoneNumber == model.PhoneNumber)
            ?? throw new Exception("Phone or password is incorrect.");
        
        if (PasswordHasher.Verify(model.Password, existUser.Password))
            throw new Exception("Phone or password is incorrect.");
    
        return existUser.Id;
    }

    public async Task UpdateAsync(int id, UserUpdateModel model)
    {
        var existUser = await context.Users.FirstOrDefaultAsync(t => t.Id == id)
            ?? throw new Exception("User is not found.");
    
        var alreadyExistUser = await context.Users
            .FirstOrDefaultAsync(u => u.PhoneNumber == model.PhoneNumber);
       
        if (alreadyExistUser != null)
            throw new Exception("User already exists with this phone number.");

        existUser.FirstName = model.FirstName;
        existUser.LastName = model.LastName;
        existUser.PhoneNumber = model.PhoneNumber;

        context.Users.Update(existUser);

        await context.SaveChangesAsync();
    }
    
    public async Task ChangePasswordAsync(int userId, string oldPassword, string newPassword)
    {
        var existUser = await context.Users.FirstOrDefaultAsync(u => u.Id == userId) 
            ?? throw new Exception("User is not found.");

        if (PasswordHasher.Verify(oldPassword, existUser.Password))
            throw new Exception("Passwords do not match.");
    
        existUser.Password = PasswordHasher.HashPassword(newPassword);

        context.Users.Update(existUser);
        await context.SaveChangesAsync();
    }
    
    public async Task DeleteAsync(int id)
    {
        var existUser = await context.Users.FirstOrDefaultAsync(u => u.Id == id)
            ?? throw new Exception("User is not found.");
    
        context.Users.Remove(existUser);
        await context.SaveChangesAsync();
    }
    
    public async Task<UserViewModel> GetAsync(int id)
    {
        var existUser = await context.Users.FirstOrDefaultAsync(u => u.Id == id)
             ?? throw new Exception("User is not found.");

        return new UserViewModel
        {
            Id = existUser.Id,
            PhoneNumber = existUser.PhoneNumber,
            FirstName = existUser.FirstName,
            LastName = existUser.LastName
        };
    }
    
    public async Task<List<UserViewModel>> GetAllAsync(string search)
    {
        var users = await context.Users.ToListAsync();
    
        if (!string.IsNullOrEmpty(search))
        {
            search = search.ToLower();
            
            users = users.Where(u =>
                u.FirstName.ToLower().Contains(search) ||
                u.LastName.ToLower().Contains(search))
                .ToList();
        }

        return users.Select(u => new UserViewModel
        {
            Id = u.Id,
            PhoneNumber = u.PhoneNumber,
            FirstName = u.FirstName,
            LastName = u.LastName
        }).ToList();
    }
}