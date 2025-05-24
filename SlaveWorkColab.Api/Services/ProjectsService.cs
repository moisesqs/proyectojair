using SlaveWorkColab.Api.Interfaces;
using SlaveWorkColab.Core.Dto;
using TecPurisima.Ecommerce.Api.Services.Interfaces;

namespace TecPurisima.Ecommerce.Api.Services;

public class ProjectsService :IProjectsService
{
    private readonly IProjectsRepository _projectsRepository;

    public ProjectsService(IProjectsRepository projectsRepository)
    {
        _projectsRepository = projectsRepository;
    }

    public async Task<bool> ProjectExists(int id)
    {
        var project = await _projectsRepository.GetByIdAsync(id);
        return project != null;
    }
    
    
    public async Task<ProjectsDto> SaveAsync(ProjectsDto projectsDto)
    {
        var project = new Projects
        {
            Name = projectsDto.Name,
            Description = projectsDto.Description,
            CreatedBy = "Admin",
            IsDeleted = false,
        };

        project = await _projectsRepository.SaveAsync(project);
        projectsDto.Id = project.Id;
        
        return projectsDto;
    }


    public async Task<ProjectsDto> UpdateAsync(ProjectsDto projectsDto)
    {
        var project = await _projectsRepository.GetByIdAsync(projectsDto.Id);
        if (project == null)
            throw new Exception("Project not found");

        project.Name = projectsDto.Name;
        project.Description = projectsDto.Description;
        project.IsDeleted = false;

        await _projectsRepository.UpdateAsync(project);
        return projectsDto;
    }

    public async Task<List<ProjectsDto>> GetAllAsync()
    {
        var projects = await _projectsRepository.GetAllAsync();
        var projectsDto = projects.Select(p => new ProjectsDto(p)).ToList();
        return projectsDto;
    }

    public async Task<ProjectsDto> GetByIdAsync(int id)
    {
        var project = await _projectsRepository.GetByIdAsync(id);
        if (project == null)
            throw new Exception("Project not found");

        var projectDto = new ProjectsDto(project);
        return projectDto;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _projectsRepository.DeleteAsync(id);
    }
}