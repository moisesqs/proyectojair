using SlaveWorkColab.Core.Entities;

namespace SlaveWorkColab.Api.Interfaces;

public interface IProjectsRepository
{
    Task<Projects> SaveAsync(Projects project);
    Task<Projects> UpdateAsync(Projects project);
    Task<List<Projects>> GetAllAsync();
    Task<bool> DeleteAsync(int id);
    Task<Projects> GetByIdAsync(int id);
}