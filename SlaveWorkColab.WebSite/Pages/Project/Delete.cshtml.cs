using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SlaveWorkColab.Core.Dto;
using SlaveWorkColab.WebSite.Services.Interfaces;

namespace SlaveWorkColab.WebSite.Pages.Project;

public class DeleteModel : PageModel
{
    [BindProperty] public ProjectsDto Project { get; set; }
    
    private readonly IProjectsService _service;
    
    public DeleteModel(IProjectsService service)
    {
        _service = service;
    }
    
    public async Task<IActionResult> OnGetAsync(int id)
    {
        Project = new ProjectsDto();
        
        var response = await _service.GetByIdAsync(id);
        Project = response.Data;

        if (Project == null)
        {
            return RedirectToPage("/Error");
        }
        
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var response = await _service.DeleteAsync(Project.Id);
        
        return RedirectToPage("./list");
        
    }    
}