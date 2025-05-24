using SlaveWorkColab.Core.Dto;

namespace TecPurisima.Ecommerce.Api.Services.Interfaces;

public interface IProjectsService
{
    Task<bool>ProjectExists(int id);
    Task<ProjectsDto> SaveAsync(ProjectsDto projectsDto);
    Task<ProjectsDto> UpdateAsync(ProjectsDto projectsDto);
    Task<List<ProjectsDto>> GetAllAsync();
    Task<ProjectsDto> GetByIdAsync(int id);
    Task<bool> DeleteAsync(int id);
}