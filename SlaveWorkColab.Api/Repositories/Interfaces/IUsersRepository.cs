using SlaveWorkColab.Core.Entities;

namespace SlaveWorkColab.Api.Interfaces;

public interface IUsersRepository
{
    Task<Users> SaveAsync(Users user);
    Task<Users> UpdateAsync(Users user);
    Task<List<Users>> GetAllAsync();
    Task<bool> DeleteAsync(int id);
    Task<Users> GetByIdAsync(int id);
    
    Task<Users> GetByEmailAndPassword(string email, string password);


}