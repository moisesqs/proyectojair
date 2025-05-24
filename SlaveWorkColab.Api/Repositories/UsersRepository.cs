using Dapper;
using Dapper.Contrib.Extensions;
using SlaveWorkColab.Api.DataAccess.Interfaces;
using SlaveWorkColab.Api.Interfaces;
using SlaveWorkColab.Core.Entities;

namespace SlaveWorkColab.Api.Repositories;

public class UsersRepository : IUsersRepository
{
    private readonly IDbContext _dbContext;

    public UsersRepository(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Users> SaveAsync(Users user)
    {
        user.Id = await _dbContext.Connection.InsertAsync(user);
        return user;
    }

    public async Task<Users> UpdateAsync(Users user)
    {
        await _dbContext.Connection.UpdateAsync(user);
        return user;
    }

    public async Task<List<Users>> GetAllAsync()
    {
        const string sql = "SELECT * FROM Users WHERE IsDeleted = 0";
        var result = await _dbContext.Connection.QueryAsync<Users>(sql);
        return result.ToList();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var user = await GetByIdAsync(id);
        if(user == null)
            return false;
        
        user.IsDeleted = true;
        
        return await _dbContext.Connection.UpdateAsync(user);
    }

    public async Task<Users> GetByIdAsync(int id)
    {
        var user = await _dbContext.Connection.GetAsync<Users>(id);
        
        if(user == null)
            return null;
        
        return user.IsDeleted == true ? null : user;
    }
    public async Task<Users> GetByEmailAndPassword(string email, string password)
    {
        var sql = "SELECT * FROM users WHERE email = @Email AND password = @Password";
    
        var user = await _dbContext.Connection.QueryFirstOrDefaultAsync<Users>(sql, new
        {
            Email = email,
            Password = password
        });

        return user != null && user.IsDeleted == false ? user : null;
    }



}