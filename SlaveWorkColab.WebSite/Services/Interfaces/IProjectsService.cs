using SlaveWorkColab.Core.Dto;
using SlaveWorkColab.Core.Http;

namespace SlaveWorkColab.WebSite.Services.Interfaces
{
    public interface IProjectsService
    {
        Task<Response<List<ProjectsDto>>> GetAllAsync();
        Task<Response<ProjectsDto>> GetByIdAsync(int id);
        Task<Response<ProjectsDto>> SaveAsync(ProjectsDto project);
        Task<Response<ProjectsDto>> UpdateAsync(ProjectsDto project);
        Task<Response<bool>> DeleteAsync(int id);
    }
}