using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SlaveWorkColab.Core.Dto;
using System.Net.Http.Json;

namespace SlaveWorkColab.WebSite.Pages.Login;

public class Login : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;

    public Login(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
        LoginModel = new LoginDto();
    }

    [BindProperty]
    public LoginDto LoginModel { get; set; }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        try
        {
            var client = _httpClientFactory.CreateClient("API");
            var response = await client.PostAsJsonAsync("api/auth/login", LoginModel);
            
            Console.WriteLine(response);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToPage("/Project/List");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Email o contraseña incorrectos");
                return Page();
            }
        }
        catch (Exception)
        {
            ModelState.AddModelError(string.Empty, "Error al intentar iniciar sesión");
            return Page();
        }
    }
}