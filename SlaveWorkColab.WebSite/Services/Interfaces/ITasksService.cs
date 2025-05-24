using SlaveWorkColab.Core.Dto;
using SlaveWorkColab.Core.Http;

namespace SlaveWorkColab.WebSite.Services.Interfaces;

public interface ITasksService
{
    Task<Response<List<TasksDto>>> GetAllAsync();
    Task<Response<TasksDto>> GetByIdAsync(int id);
    Task<Response<TasksDto>> SaveAsync(TasksDto task);
    Task<Response<TasksDto>> UpdateAsync(TasksDto task);
    Task<Response<bool>> DeleteAsync(int id);
}