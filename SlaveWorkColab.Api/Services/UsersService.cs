using SlaveWorkColab.Api.Interfaces;
using SlaveWorkColab.Core.Dto;
using TecPurisima.Ecommerce.Api.Services.Interfaces;

namespace TecPurisima.Ecommerce.Api.Services;

public class UsersService : IUsersService
{
    private readonly IUsersRepository _usersrepository;

    public UsersService(IUsersRepository usersrepository)
    {
        _usersrepository = usersrepository;
    }
    
    public async Task<bool> UserExists(int id)
    {
        var user = await _usersrepository.GetByIdAsync(id);
        return user != null;
    }

    public async Task<UsersDto> SaveAsync(UsersDto usersDto)
    {
        var user = new Users
        {
            Username = usersDto.Username,
            Email = usersDto.Email,
            Password = usersDto.Password,
            IsDeleted = false
        };

        user = await _usersrepository.SaveAsync(user);
        usersDto.Id = user.Id;
        return usersDto;
    }

    public async Task<UsersDto> UpdateAsync(UsersDto usersDto)
    {
        var user = await _usersrepository.GetByIdAsync(usersDto.Id);
        if (user == null)
            throw new Exception("User not found");

        user.Username = usersDto.Username;
        user.Email = usersDto.Email;
        user.Password = usersDto.Password;
        user.IsDeleted = false;

        await _usersrepository.UpdateAsync(user);
        return usersDto;
    }

    public async Task<List<UsersDto>> GetAllAsync()
    {
        var users = await _usersrepository.GetAllAsync();
        var usersDto = users.Select(u => new UsersDto(u)).ToList();
        return usersDto;
    }

    public async Task<UsersDto> GetByIdAsync(int id)
    {
        var user = await _usersrepository.GetByIdAsync(id);
        if (user == null)
            throw new Exception("User not found");

        var userDto = new UsersDto(user);
        return userDto;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _usersrepository.DeleteAsync(id);
    }
    
    public async Task<UsersDto?> AuthenticateAsync(string email, string password)
    {
        var user = await _usersrepository.GetByEmailAndPassword(email, password);
        return user == null ? null : new UsersDto(user);
    }
}