using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;
using SlaveWorkColab.Core.Dto;
using SlaveWorkColab.WebSite.Services.Interfaces;

namespace SlaveWorkColab.WebSite.Pages.Task;

public class List : PageModel
{
    private readonly ITasksService _service;

    public List<TasksDto> Tasks { get; set; }

    public List(ITasksService service)
    {
        _service = service;
        Tasks = new List<TasksDto>();
    }
    
    public async Task<IActionResult> OnGet()
    {
        var response = await _service.GetAllAsync();
        Tasks = response.Data;
        
        return Page();
    }
}