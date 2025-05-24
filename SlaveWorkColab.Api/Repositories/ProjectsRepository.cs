using Dapper;
using Dapper.Contrib.Extensions;
using SlaveWorkColab.Api.DataAccess.Interfaces;
using SlaveWorkColab.Api.Interfaces;
using SlaveWorkColab.Core.Entities;

namespace SlaveWorkColab.Api.Repositories;

public class ProjectsRepository : IProjectsRepository
{
    private readonly IDbContext _dbContext;

    public ProjectsRepository(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Projects> SaveAsync(Projects project)
    {
        project.Id = await _dbContext.Connection.InsertAsync(project);
        return project;
    }

    public async Task<Projects> UpdateAsync(Projects project)
    {
        await _dbContext.Connection.UpdateAsync(project);
        return project;
    }

    public async Task<List<Projects>> GetAllAsync()
    {
        const string sql = "SELECT * FROM Projects WHERE IsDeleted = 0";
        var result = await _dbContext.Connection.QueryAsync<Projects>(sql);
        return result.ToList();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var project = await GetByIdAsync(id);
        if (project == null) return false;

        project.IsDeleted = true;
        return await _dbContext.Connection.UpdateAsync(project);
    }

    public async Task<Projects> GetByIdAsync(int id)
    {
        var project = await _dbContext.Connection.GetAsync<Projects>(id);
        return project?.IsDeleted == true ? null : project;
    }
}