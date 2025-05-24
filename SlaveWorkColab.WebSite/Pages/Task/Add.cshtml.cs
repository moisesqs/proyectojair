using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SlaveWorkColab.Core.Dto;
using SlaveWorkColab.WebSite.Services.Interfaces;

namespace SlaveWorkColab.WebSite.Pages.Task;

public class Add : PageModel
{
    [BindProperty] public TasksDto Task { get; set; }
    
    public List<string> Errors { get; set; } = new List<string>();

    private readonly ITasksService _service;
    private readonly IProjectsService _projectsService;
    private readonly IUsersService _usersService;
    
    public List<ProjectsDto> Projects { get; set; } = new();
    public List<UsersDto> Users { get; set; } = new();

    public Add(ITasksService service, IProjectsService projectsService, IUsersService usersService)
    {
        _service = service;
        _projectsService = projectsService;
        _usersService = usersService;
    }

    public async System.Threading.Tasks.Task OnGetAsync()
    {
        Task = new TasksDto();

        var projectsResponse = await _projectsService.GetAllAsync();
        Projects = projectsResponse?.Data ?? new List<ProjectsDto>();

        var usersResponse = await _usersService.GetAllAsync();
        Users = usersResponse?.Data ?? new List<UsersDto>();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        Console.WriteLine(Task.Status);
        
        var response = await _service.SaveAsync(Task);
        
        if (response?.Data != null)
            return RedirectToPage("./List");

        Errors = response?.Errors ?? new List<string> { "Error desconocido" };
        return Page();
    }
}