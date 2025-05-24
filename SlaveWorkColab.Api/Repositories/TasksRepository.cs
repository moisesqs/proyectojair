using Dapper;
using Dapper.Contrib.Extensions;
using SlaveWorkColab.Api.DataAccess.Interfaces;
using SlaveWorkColab.Api.Interfaces;
using SlaveWorkColab.Core.Entities;

namespace SlaveWorkColab.Api.Repositories;

public class TasksRepository : ITasksRepository
{
    private readonly IDbContext _dbContext;

    public TasksRepository(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Core.Entities.Tasks> SaveAsync(Core.Entities.Tasks task)
    {
        task.Id = await _dbContext.Connection.InsertAsync(task);
        return task;
    }

    public async Task<Core.Entities.Tasks> UpdateAsync(Core.Entities.Tasks task)
    {
        await _dbContext.Connection.UpdateAsync(task);
        return task;
    }

    public async Task<List<Core.Entities.Tasks>> GetAllAsync()
    {
        const string sql = "SELECT * FROM Tasks WHERE IsDeleted = 0";
        var result = await _dbContext.Connection.QueryAsync<Core.Entities.Tasks>(sql);
        return result.ToList();
    }

    /*public async Task<bool> DeleteAsync(int id)
    {
        var task = await GetByIdAsync(id);
        if (task == null) return false;

        task.IsDeleted = true;
        return await _dbContext.Connection.UpdateAsync(task);
    }*/
    
    public async Task<bool> DeleteAsync(int id)
    {
        var tasks = await GetByIdAsync(id);
        if (tasks == null) return false;

        tasks.IsDeleted = true;
        return await _dbContext.Connection.UpdateAsync(tasks);
    }

    public async Task<Core.Entities.Tasks> GetByIdAsync(int id)
    {
        var task = await _dbContext.Connection.GetAsync<Core.Entities.Tasks>(id);
        return task?.IsDeleted == true ? null : task;
    }
}