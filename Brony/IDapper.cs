using Brony.Domain;

namespace Brony;

public interface IDapper
{
    Task CreateAsync(User user);
    Task UpdateAsync(User user);
    Task DeleteAsync(int id);
    Task<User> GetByIdAsync(int id);
    Task<IEnumerable<User>> GetAllAsync();
}