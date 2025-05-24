using SlaveWorkColab.Core.Dto;
using SlaveWorkColab.Core.Http;

namespace SlaveWorkColab.WebSite.Services.Interfaces;

public interface IUsersService
{
    Task<Response<List<UsersDto>>> GetAllAsync();
    Task<(bool success, string message)> RegisterAsync(RegisterDto registerDto);
}