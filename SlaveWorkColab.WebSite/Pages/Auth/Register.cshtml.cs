using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SlaveWorkColab.Core.Dto;
using SlaveWorkColab.WebSite.Services.Interfaces;

namespace SlaveWorkColab.WebSite.Pages.Login;

public class Register : PageModel
{
    private readonly IUsersService _usersService;

    public Register(IUsersService usersService)
    {
        _usersService = usersService;
        RegisterModel = new RegisterDto();
    }

    [BindProperty]
    public RegisterDto RegisterModel { get; set; }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        var (success, message) = await _usersService.RegisterAsync(RegisterModel);

        if (success)
        {
            TempData["SuccessMessage"] = message;
            return RedirectToPage("/Auth/Login");
        }
        else
        {
            ModelState.AddModelError(string.Empty, message);
            return Page();
        }
    }
}