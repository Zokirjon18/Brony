using Brony.Constants;
using Brony.Domain;
using Dapper;
using Microsoft.Data.SqlClient;

namespace Brony;

public class Dapper : IDapper
{
    public Task CreateAsync(User user)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(User user)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<User> GetByIdAsync(int id)
    {
        using var connection = new SqlConnection(PathHolder.ConnectionString);
    
        string query = "select * from Users where Id = @Id";
        
        var result = await connection.QueryAsync<User>(query, id);
        
        return result.FirstOrDefault();
    }

    public Task<IEnumerable<User>> GetAllAsync()
    {
        throw new NotImplementedException();
    }
}