using Task = SlaveWorkColab.Core.Entities.Tasks;

namespace SlaveWorkColab.Api.Interfaces;

public interface ITasksRepository
{
    Task<Task> SaveAsync(Task task);
    Task<Task> UpdateAsync(Task task);
    Task<List<Task>> GetAllAsync();
    Task<bool> DeleteAsync(int id);
    Task<Task> GetByIdAsync(int id);
}