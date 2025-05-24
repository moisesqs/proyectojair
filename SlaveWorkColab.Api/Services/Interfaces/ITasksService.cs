using SlaveWorkColab.Core.Dto;

namespace TecPurisima.Ecommerce.Api.Services.Interfaces;

public interface ITasksService
{
    Task<bool>TasksExists(int id);
    Task<TasksDto> SaveAsync(TasksDto tasksDto);
    Task<TasksDto> UpdateAsync(TasksDto tasksDto);
    Task<List<TasksDto>> GetAllAsync();
    Task<TasksDto> GetByIdAsync(int id);
    Task<bool> DeleteAsync(int id);
    
}