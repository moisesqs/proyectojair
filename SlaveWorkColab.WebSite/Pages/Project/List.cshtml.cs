using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SlaveWorkColab.Core.Dto;
using SlaveWorkColab.WebSite.Services.Interfaces;

namespace SlaveWorkColab.WebSite.Pages.Project;

public class ListModel : PageModel
{
    private readonly IProjectsService _service;

    public List<ProjectsDto>? Projects { get; set; }

    public ListModel(IProjectsService service)
    {
        _service = service;
        Projects = new List<ProjectsDto>();
    }
    
    public async Task<IActionResult> OnGet()
    {
        var response = await _service.GetAllAsync();
        Projects = response.Data;
        
        return Page();
    }
    
}