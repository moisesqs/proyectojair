using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SlaveWorkColab.Core.Dto;
using SlaveWorkColab.Core.Http;
using SlaveWorkColab.WebSite.Services.Interfaces;

namespace SlaveWorkColab.WebSite.Pages.Project;

public class EditModel : PageModel
{
    [BindProperty] public ProjectsDto Project { get; set; }
    
    public List<string> Errors { get; set; } = new List<string>();

    private readonly IProjectsService _service;

    public EditModel(IProjectsService service)
    {
        _service = service;
    }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        Project = new ProjectsDto();
        if (id.HasValue)
        {
            var response = await _service.GetByIdAsync(id.Value);
            Project = response.Data;
        }

        if (Project == null)
        {
            return RedirectToPage("/Error");
        }
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        Response<ProjectsDto> response;

        if (Project.Id > 0)
        {
            //Actualizando
            response = await _service.UpdateAsync(Project);
        }
        else
        {
            //Creando
            response = await _service.SaveAsync(Project);
        }
        Project =  response.Data;
        return RedirectToPage("./List");
    }
}