using SlaveWorkColab.Core.Dto;

namespace TecPurisima.Ecommerce.Api.Services.Interfaces;

public interface IUsersService
{
    Task<bool>UserExists(int id);
    Task<UsersDto> SaveAsync(UsersDto usersDto);
    Task<UsersDto> UpdateAsync(UsersDto usersDto);
    Task<List<UsersDto>> GetAllAsync();
    Task<UsersDto> GetByIdAsync(int id);
    Task<bool> DeleteAsync(int id);
    Task<UsersDto?> AuthenticateAsync(string email, string password);
}