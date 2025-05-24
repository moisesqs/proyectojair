using System.Net.Http.Json;
using Newtonsoft.Json;
using SlaveWorkColab.Core.Dto;
using SlaveWorkColab.Core.Http;
using SlaveWorkColab.WebSite.Services.Interfaces;

namespace SlaveWorkColab.WebSite.Services;

public class UserService : IUsersService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;
    
    private readonly string _baseUrl = "http://localhost:5140/";
    private readonly string _endpoint = "api/Users";
    
    public UserService()
    {
        
    }

    public UserService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
    }

    
    public async Task<Response<List<UsersDto>>> GetAllAsync()
    {
        var url = $"{_baseUrl}{_endpoint}";
        
        var client = new HttpClient();
        var res = await client.GetAsync(url);
        var json = await res.Content.ReadAsStringAsync();
        
        var response = JsonConvert.DeserializeObject<Response<List<UsersDto>>>(json);
        
        return response;
    }

    public async Task<(bool success, string message)> RegisterAsync(RegisterDto registerDto)
    {
        try
        {
            var client = _httpClientFactory.CreateClient("API");
            var response = await client.PostAsJsonAsync("api/users", new UsersDto
            {
                Username = registerDto.Username,
                Email = registerDto.Email,
                Password = registerDto.Password
            });

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<Response<UsersDto>>();
                if (result?.Data != null)
                {
                    return (true, "Usuario registrado exitosamente");
                }
            }

            var errorResponse = await response.Content.ReadFromJsonAsync<Response<UsersDto>>();
            return (false, errorResponse?.Errors?.FirstOrDefault() ?? "Error al registrar el usuario");
        }
        catch (Exception ex)
        {
            return (false, "Error de conexión con el servidor");
        }
    }
}