using Microsoft.AspNetCore.Mvc;
using SlaveWorkColab.Api.Interfaces;
using SlaveWorkColab.Core.Dto;
using SlaveWorkColab.Core.Http;
using TecPurisima.Ecommerce.Api.Services.Interfaces;

namespace SlaveWorkColab.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly ITasksRepository _repository;

    private readonly ITasksService _taskService;

    public TasksController(ITasksService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet]
    public async Task<ActionResult<Response<List<TasksDto>>>> GetAll()
    {
        var response = new Response<List<TasksDto>>
        {
            Data = await _taskService.GetAllAsync()
        };
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<Response<TasksDto>>> Post([FromBody] TasksDto dto)
    {
        var response = new Response<TasksDto>
        {
            Data = await _taskService.SaveAsync(dto)
        };
        return Created($"/api/[controller]/{response.Data.Id}", response);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Response<TasksDto>>> GetById(int id)
    {
        var response = new Response<TasksDto>();

        if (!await _taskService.TasksExists(id))
        {
            response.Errors.Add("Task not found");
            return NotFound(response);
        }

        response.Data = await _taskService.GetByIdAsync(id);
        return Ok(response);
    }

    /*[HttpDelete("{id:int}")]
    public async Task<ActionResult<Response<bool>>> Delete(int id)
    {
        var response = new Response<bool>();
        var result = await _taskService.DeleteAsync(id);

        if (!result)
        {
            response.Errors.Add("Task not found");
            return NotFound(response);
        }

        response.Data = result;
        response.Message = "Task successfully deleted";
        return Ok(response);
    }*/
    
    [HttpDelete("{id:int}")]
    public async Task<ActionResult<Response<bool>>> Delete(int id)
    {
        var response = new Response<bool>();

        var result = await _taskService.DeleteAsync(id);

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
    public async Task<ActionResult<Response<TasksDto>>> Update([FromBody] TasksDto dto)
    {
        var response = new Response<TasksDto>();

        if (!await _taskService.TasksExists(dto.Id))
        {
            response.Errors.Add("Task not found");
            return NotFound(response);
        }

        response.Data = await _taskService.UpdateAsync(dto);
        response.Message = "Task successfully updated";
        return Ok(response);
    }
}
