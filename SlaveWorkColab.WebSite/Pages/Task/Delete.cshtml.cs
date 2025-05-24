using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SlaveWorkColab.Core.Dto;
using SlaveWorkColab.WebSite.Services.Interfaces;

namespace SlaveWorkColab.WebSite.Pages.Task;

public class DeleteModel : PageModel
{
    [BindProperty] public TasksDto Task { get; set; }
    
    private readonly ITasksService _service;
    
    public DeleteModel(ITasksService service)
    {
        _service = service;
    }
    
    public async Task<IActionResult> OnGetAsync(int id)
    {
        Task = new TasksDto();
        
        var response = await _service.GetByIdAsync(id);
        Task = response.Data;

        if (Task == null)
        {
            return RedirectToPage("/Error");
        }
        
        return Page();
    }

    public async Task<IActionResult> OnPostAsync()
    {
        var response = await _service.DeleteAsync(Task.Id);
        
        return RedirectToPage("./list");
    }
}