using SlaveWorkColab.Core.Entities;

namespace SlaveWorkColab.Api.Interfaces;

public interface ICommentsRepository
{
    Task<Comments> SaveAsync(Comments comment);
    Task<Comments> UpdateAsync(Comments comment);
    Task<List<Comments>> GetAllAsync();
    Task<bool> DeleteAsync(int id);
    Task<Comments> GetByIdAsync(int id);
}