using Microsoft.AspNetCore.Mvc;
using SlaveWorkColab.Api.Interfaces;
using SlaveWorkColab.Core.Dto;
using SlaveWorkColab.Core.Entities;
using SlaveWorkColab.Core.Http;
using TecPurisima.Ecommerce.Api.Services.Interfaces;

namespace SlaveWorkColab.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly IProjectsRepository _repository;
    private readonly IProjectsService _projectService;

    public ProjectsController(IProjectsService projectService)
    {
        _projectService = projectService;
    }

    [HttpGet]
    public async Task<ActionResult<Response<List<ProjectsDto>>>> GetAll()
    {
        var response = new Response<List<ProjectsDto>>
        {
            Data = await _projectService.GetAllAsync()
        };
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<Response<ProjectsDto>>> Post([FromBody] ProjectsDto dto)
    {
        var response = new Response<ProjectsDto>
        {
            Data = await _projectService.SaveAsync(dto)
        };
        
        Console.WriteLine(dto);

        return Created($"/api/[controller]/{response.Data.Id}", response);
    }


    [HttpGet("{id:int}")]
    public async Task<ActionResult<Response<ProjectsDto>>> GetById(int id)
    {
        var response = new Response<ProjectsDto>();

        if (!await _projectService.ProjectExists(id))
        {
            response.Errors.Add("Project not found");
            return NotFound(response);
        }

        response.Data = await _projectService.GetByIdAsync(id);
        return Ok(response);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<Response<bool>>> Delete(int id)
    {
        var response = new Response<bool>();

        var result = await _projectService.DeleteAsync(id);

        if (!result)
        {
            response.Errors.Add("Project not found");
            return NotFound(response);
        }

        response.Message = "Project successfully deleted";
        response.Data = result;
        return Ok(response);
    }

    [HttpPut]
    public async Task<ActionResult<Response<ProjectsDto>>> Update([FromBody] ProjectsDto dto)
    {
        var response = new Response<ProjectsDto>();

        if (!await _projectService.ProjectExists(dto.Id))
        {
            response.Errors.Add("Project not found");
            return NotFound(response);
        }

        response.Data = await _projectService.UpdateAsync(dto);
        response.Message = "Project successfully updated";
        return Ok(response);
    }

}
