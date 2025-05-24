using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SlaveWorkColab.Core.Dto;
using SlaveWorkColab.WebSite.Services.Interfaces;

namespace SlaveWorkColab.WebSite.Pages.Project;

public class Add : PageModel
{
    [BindProperty] public ProjectsDto Project { get; set; }
    
    public List<string> Errors { get; set; } = new List<string>();

    private readonly IProjectsService _service;

    public Add(IProjectsService service)
    {
        _service = service;
    }

    public void OnGet()
    {
        Project = new ProjectsDto();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }
        
        var response = await _service.SaveAsync(Project);
        
        if (response?.Data != null)
            return RedirectToPage("./List");

        Errors = response?.Errors ?? new List<string> { "Error desconocido" };
        return Page();

    }

}