using SlaveWorkColab.Api.Interfaces;
using SlaveWorkColab.Core.Dto;
using SlaveWorkColab.Core.Entities;
using TecPurisima.Ecommerce.Api.Services.Interfaces;

namespace TecPurisima.Ecommerce.Api.Services;

public class TasksService : ITasksService
{
    private readonly ITasksRepository _tasksRepository;

    public TasksService(ITasksRepository tasksRepository)
    {
        _tasksRepository = tasksRepository;
    }

    public async Task<bool> TasksExists(int id)
    {
        var task = await _tasksRepository.GetByIdAsync(id);
        return task != null;
    }

    public async Task<TasksDto> SaveAsync(TasksDto tasksDto)
    {
        var task = new Tasks
        {
            Title = tasksDto.Title,
            Description = tasksDto.Description,
            Status = tasksDto.Status,
            Project_Id = tasksDto.Project_Id,
            Assigned_to = tasksDto.Assigned_to,
            Created_at = DateTime.Now,
            IsDeleted = false
        };

        task = await _tasksRepository.SaveAsync(task);
        tasksDto.Id = task.Id;  

        return tasksDto;
    }

    public async Task<TasksDto> UpdateAsync(TasksDto tasksDto)
    {
        var task = await _tasksRepository.GetByIdAsync(tasksDto.Id);
        if (task == null)
            throw new Exception("Task not found");

        task.Title = tasksDto.Title;
        task.Description = tasksDto.Description;
        task.Status = tasksDto.Status;
        task.Project_Id = tasksDto.Project_Id;
        task.Assigned_to = tasksDto.Assigned_to;
        task.IsDeleted = false;

        await _tasksRepository.UpdateAsync(task);
        return tasksDto;
    }

    public async Task<List<TasksDto>> GetAllAsync()
    {
        var tasks = await _tasksRepository.GetAllAsync();
        var tasksDto = tasks.Select(t => new TasksDto(t)).ToList();
        return tasksDto;
    }

    public async Task<TasksDto> GetByIdAsync(int id)
    {
        var task = await _tasksRepository.GetByIdAsync(id);
        if (task == null)
            throw new Exception("Task not found");

        var taskDto =  new TasksDto(task);
        return taskDto;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _tasksRepository.DeleteAsync(id);
    }
}