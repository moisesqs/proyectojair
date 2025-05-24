using Microsoft.AspNetCore.Mvc;
using SlaveWorkColab.Api.Interfaces;
using SlaveWorkColab.Core.Dto;
using SlaveWorkColab.Core.Entities;
using SlaveWorkColab.Core.Http;
using TecPurisima.Ecommerce.Api.Services.Interfaces;

namespace SlaveWorkColab.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUsersRepository _userRepository;

    private readonly IUsersService _userService;

    public UsersController(IUsersService userService)
    {
        _userService = userService;
    }

    [HttpGet]
    public async Task<ActionResult<Response<List<UsersDto>>>> GetAll()
    {
        var response = new Response<List<UsersDto>>
        {
            Data = await _userService.GetAllAsync()
        };
        return Ok(response);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Response<UsersDto>>> GetById(int id)
    {
        var response = new Response<UsersDto>();
    
        if (!await _userService.UserExists(id))
        {
            response.Errors.Add("User not found");
            return NotFound(response);
        }

        response.Data = await _userService.GetByIdAsync(id);
        return Ok(response);
    }

    [HttpPost]
    public async Task<ActionResult<Response<UsersDto>>> Post([FromBody] UsersDto userDto)
    {
        var response = new Response<UsersDto>
        {
            Data = await _userService.SaveAsync(userDto)
        };

        return Created($"/api/[controller]/{response.Data.Id}", response);
    }

    [HttpPut]
    public async Task<ActionResult<Response<UsersDto>>> Update([FromBody] UsersDto userDto)
    {
        var response = new Response<UsersDto>();

        if (!await _userService.UserExists(userDto.Id))
        {
            response.Errors.Add("User not found");
            return NotFound(response);
        }

        response.Data = await _userService.UpdateAsync(userDto);
        response.Message = "User successfully updated";
        return Ok(response);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<Response<bool>>> Delete(int id)
    {
        var response = new Response<bool>();

        var result = await _userService.DeleteAsync(id);

        if (!result)
        {
            response.Errors.Add("User not found");
            return NotFound(response);
        }

        response.Data = result;
        response.Message = "User successfully deleted";
        return Ok(response);
    }
}
