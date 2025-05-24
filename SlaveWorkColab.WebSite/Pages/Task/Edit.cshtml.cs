using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SlaveWorkColab.Core.Dto;
using SlaveWorkColab.Core.Http;
using SlaveWorkColab.WebSite.Services.Interfaces;

namespace SlaveWorkColab.WebSite.Pages.Task;

public class EditModel : PageModel
{
    [BindProperty] public TasksDto Task { get; set; }
    
    public List<string> Errors { get; set; } = new List<string>();

    private readonly ITasksService _service;
    private readonly IProjectsService _projectsService;
    private readonly IUsersService _usersService;
    
    public List<ProjectsDto> Projects { get; set; } = new();
    public List<UsersDto> Users { get; set; } = new();

    public EditModel(ITasksService service, IProjectsService projectsService, IUsersService usersService)
    {
        _service = service;
        _projectsService = projectsService;
        _usersService = usersService;
    }

    public async Task<IActionResult> OnGetAsync(int? id)
    {
        Task = new TasksDto();
        if (id.HasValue)
        {
            var response = await _service.GetByIdAsync(id.Value);
            Task = response.Data;
        }

        if (Task == null)
        {
            return RedirectToPage("/Error");
        }

        var projectsResponse = await _projectsService.GetAllAsync();
        Projects = projectsResponse?.Data ?? new List<ProjectsDto>();

        var usersResponse = await _usersService.GetAllAsync();
        Users = usersResponse?.Data ?? new List<UsersDto>();

        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        Response<TasksDto> response;

        if (Task.Id > 0)
        {
            //Actualizando
            response = await _service.UpdateAsync(Task);
        }
        else
        {
            //Creando
            response = await _service.SaveAsync(Task);
        }
        Task = response.Data;
        return RedirectToPage("./List");
    }
}