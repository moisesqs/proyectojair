using Dapper;
using Dapper.Contrib.Extensions;
using SlaveWorkColab.Api.DataAccess.Interfaces;
using SlaveWorkColab.Api.Interfaces;
using SlaveWorkColab.Core.Entities;

namespace SlaveWorkColab.Api.Repositories;
public class CommentsRepository : ICommentsRepository
{
    private readonly IDbContext _dbContext;

    public CommentsRepository(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Comments> SaveAsync(Comments comment)
    {
        comment.Id = await _dbContext.Connection.InsertAsync(comment);
        return comment;
    }

    public async Task<Comments> UpdateAsync(Comments comment)
    {
        await _dbContext.Connection.UpdateAsync(comment);
        return comment;
    }

    public async Task<List<Comments>> GetAllAsync()
    {
        const string sql = "SELECT * FROM Comments WHERE IsDeleted = 0";
        var result = await _dbContext.Connection.QueryAsync<Comments>(sql);
        return result.ToList();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var comment = await GetByIdAsync(id);
        if (comment == null) return false;

        comment.IsDeleted = true;
        return await _dbContext.Connection.UpdateAsync(comment);
    }

    public async Task<Comments> GetByIdAsync(int id)
    {
        var comment = await _dbContext.Connection.GetAsync<Comments>(id);
        return comment?.IsDeleted == true ? null : comment;
    }
}